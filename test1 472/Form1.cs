using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using Siemens.Engineering;
using Siemens.Engineering.Compiler;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.HW;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.MC.Drives;
using Siemens.Engineering.MC.Drives.DFI;
using Siemens.Engineering.MC.Drives.Enums;
using Siemens.Engineering.MC;
//using Siemens.Engineering.MC.Driveconfiguration;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Blocks;
using System.IO;
using System.Reflection;
using System.Xml;
using Siemens.Engineering.SW.TechnologicalObjects.Motion;
using System.Net.Http.Headers;
using test1_472.Properties;
using System.Runtime;
using test1_472;
using StartDriveParameterEditor;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Security;
using Splash_Screen_nTest;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Siemens.Engineering.Online;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Siemens.Engineering.Library;
using Siemens.Engineering.Library.MasterCopies;
using MathProject;
using System.Media;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;

namespace test1_472
{
    public delegate void handleTiaConfirmationDelegate(ConfirmationEventArgs e);
    public partial class Form1 : Form
    {
        public SplashScreen1 s = new SplashScreen1();
        private string ActualProjectPath; // current project
        private string pendingProjectPath;//project to be opened
        private TiaOpennessServer TiaServer; // TiaOpennessServer object for Tia portal operations
        private BindingSource ParameterListsSource; // parameter lists
        private ApplicationSettingsServer SettingsServer; // Application settings
        TreeNode[] drivesInProject; //Array to save all the initial nodes after opening a project. The array is used to restore the removed nodes after pressing show selected buttons
        private bool TiaLoaded = true; // Tia portal successfully loaded
        private bool TiaProjectSaved; // Tia portal project was saved   
        public bool online = false;
        General gen = new General();
        public List<string> ParNames = new List<string>()
        {
            "p418[0]", "p419[0]", "p2504[0]", "p2505[0]", "p1082[0]", "p2000", "p1121[0]", "p1135[0]", "p2506[0]", "p2503[0]", "r2524", "p2571", "p2572", "p2573", "p2580", "p2581", "p2585", "p2586", "p418[1]", "p419[1]"
        };
        public List<string> ParNamesSingle = new List<string>()
        {
            "p418[0]", "p419[0]", "p2504[0]", "p2505[0]", "p1082[0]", "p2000", "p1121[0]", "p1135[0]", "p2506[0]", "p2503[0]", "r2524", "p2571", "p2572", "p2573", "p2580", "p2581", "p2585", "p2586"
        };
        public List<string> ParNamesOnline1 = new List<string>()
        {
            "p2504[0]", "p2505[0]", "p2506[0]", "p2503[0]", "r2524", "p2571", "p2572", "p2573", "p2580", "p2581", "p2585", "p2586"
        };
        public List<string> ParNamesOnline2 = new List<string>()
        {
            "p418[0]", "p419[0]", "p1082[0]", "p2000", "p1121[0]", "p1135[0]", "p418[1]", "p419[1]"
        };
        public List<string> ParNamesSingleOnline2 = new List<string>()
        {
            "p418[0]", "p419[0]", "p1082[0]", "p2000", "p1121[0]", "p1135[0]"
        };
        public List<PValNam> pValNams = new List<PValNam>();
        public ParametersReadSingle current = new ParametersReadSingle();
        public ParametersReadDouble currentDouble = new ParametersReadDouble();
        internal event EventHandler<EventArgs> TiaDisposedEvent;
        private static TiaPortalProcess _tiaProcess;
        private TreeNode lastSelectedNode;
        private bool NodeSelectActive = true;
        private bool dragging = false;
        string SelectedDeviceItemName;
        private Point dragStart;
        public List<string> listOfParameters = new List<string>();
        bool isSingle = false;
        bool isDouble = false;
        string DevicePosition;
        string ProjectName;

        DriveParameter DeviceParameterCheck = null;
        public TiaPortal MyTiaPortal
        {
            get; set;
        }
        public Project MyProject
        {
            get; set;
        }
        public void StartForm()
        {
            Application.Run(new SplashScreen1());
        }

        public Form1()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
            AppDomain CurrentDomain = AppDomain.CurrentDomain;
            CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolver);
            SettingsServer = new ApplicationSettingsServer();
            AssemblyResolve.AddResolver();
            ParameterListsSource = new BindingSource();
            SettingsServer.Load();
            Text = SettingsServer.AppSettings.ApplicationTitle;
            TiaProjectSaved = true;
            StartWithoutInterface();
        }

        private static Assembly MyResolver(object sender, ResolveEventArgs args)
        {
            int index = args.Name.IndexOf(',');
            if (index == -1)
            {
                return null;
            }
            string name = args.Name.Substring(0, index);
            RegistryKey filePathReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Siemens\\Automation\\Openness\\16\\PublicAPI\\16.0.0.0");
            if (filePathReg == null)
                return null;
            object oRegKeyValue = filePathReg.GetValue(name);
            if (oRegKeyValue != null)
            {
                string filePath = oRegKeyValue.ToString();
                string path = filePath;
                string fullPath = Path.GetFullPath(path);
                if (File.Exists(fullPath))
                {
                    return Assembly.LoadFrom(fullPath);
                }
            }
            return null;
        }
        private void StartWithoutInterface()
        {
            try
            {
                TiaServer = new TiaOpennessServer(new handleTiaConfirmationDelegate(HandleTiaConfirmation));
                TiaServer.TiaDisposedEvent += TiaServer_TiaDisposedEvent;
                MyTiaPortal = new TiaPortal(TiaPortalMode.WithoutUserInterface);
                ConsoleAnswer.Text = "TIA Portal started";
                OpenProjectButton.Enabled = true;
                IList<TiaPortalProcess> processes = TiaPortal.GetProcesses();
                _tiaProcess = processes[0];
                MyTiaPortal = _tiaProcess.Attach();
                MyProject = MyTiaPortal.Projects[0];
            }
            catch
            {
                TiaLoaded = false;
            }
        }
        private void TiaServer_TiaDisposedEvent(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                ConsoleAnswer.Text = "Tia Portal was closed. Please restart the application if you want to work with a Tia Portal project";
                ClearTreeviewDevices();
                TiaServer = null;
                TiaLoaded = false;

            }
        }
        private void OpenTheOpeningDialog(object sender, EventArgs e)
        {
            if (TiaServer == null)
                return;
            ProjectSelectionForm selectProjectForm = new ProjectSelectionForm(ref SettingsServer);
            DialogResult result = selectProjectForm.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                pendingProjectPath = selectProjectForm.NewProject;
                if (pendingProjectPath == ActualProjectPath)
                {
                    selectProjectForm.Dispose();
                    return;
                }
                OpenProject(pendingProjectPath);
            }
            selectProjectForm.Dispose();
        }
        private void OpenProject(string pendingProjectPath)
        {
            if (TiaServer == null)
                return;
            //Save & close actual project first
            if (TiaServer.CurrentProject != null)
            {
                try
                {
                    if (SettingsServer.AppSettings.AutomaticallySaveProject && !TiaProjectSaved)
                    {
                        SaveTiaProject();
                        ConsoleAnswer.Text = string.Format("Project saved: {0}", ActualProjectPath);
                    }
                    ConsoleAnswer.Text = "Closing project";
                    TiaServer.CloseProject();
                    drivesInProject = null;
                    ConsoleAnswer.Text = string.Format("Project closed: {0}", ActualProjectPath);
                }
                catch (Exception ex)
                {
                    ConsoleAnswer.Text = string.Format("Error Closing Project - Exception: {0} - ", ex.Message);
                }
                finally
                {
                    ClearTreeviewDevices();
                }
                OpenProjectButton.Enabled = true;
            }
            //Open new project
            string projectName = pendingProjectPath.Substring(pendingProjectPath.LastIndexOf("\\") + 1);
            ConsoleAnswer.Text = string.Format("Opening project: {0} ", projectName);
            try
            {
                if (TiaServer.OpenProject(pendingProjectPath) != null)
                {
                    ActualProjectPath = TiaServer.CurrentProject.Path.ToString();
                    SettingsServer.AppSettings.AddProjectToRecent(ActualProjectPath);
                    ConsoleAnswer.Text = "Project opened";
                    RefreshDrivesTreeviewFromProject();
                    return;
                }
            }
            catch (Exception ex)
            {
                ConsoleAnswer.Text = string.Format("Error opening project - Exception {0} -", ex.Message);
            }
            ActualProjectPath = "";
            CloseButton.Enabled = true;
            OpenProjectButton.Enabled = false;
            SaveButton.Enabled = true;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                SaveTiaProject();
            }
            catch
            {
            }
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (TiaServer.CurrentProject != null)
            {
                try
                {
                    if (SettingsServer.AppSettings.AutomaticallySaveProject && !TiaProjectSaved)
                    {
                        SaveTiaProject();
                        ConsoleAnswer.Text = string.Format("Project saved: {0}", ActualProjectPath);
                    }
                    ConsoleAnswer.Text = string.Format("Closing project: {0} ", ActualProjectPath);
                    TiaServer.CloseProject();
                    drivesInProject = null;
                    ConsoleAnswer.Text = string.Format("Project closed: {0}", ActualProjectPath);
                    MyProject = null;
                }
                catch (Exception ex)
                {
                    ConsoleAnswer.Text = string.Format("Error Closing Project - Exception: {0} - ", ex.Message);
                }
                finally
                {
                    ClearTreeviewDevices();
                }
            }
            ActualProjectPath = "";
            OpenProjectButton.Enabled = true;
        }
        private void SaveTiaProject()
        {
            try
            {
                TiaServer.CurrentProject.Save();
                ConsoleAnswer.Text = "Project saved successfully.";
            }
            catch (Exception ex)
            {
                ConsoleAnswer.Text = string.Format("Error saving project - Exception {0}", ex.Message);
            }
        }
        private void RefreshDrivesTreeviewFromProject()
        {
            ConsoleAnswer.Text = "Getting Sinamics drives in project";
            bool searchGroups = !SettingsServer.AppSettings.ShowUserGroups;
            List<Device> SinamicsInProject = TiaServer.GetAllSinamicsDevicesInProject(searchGroups);
            List<TreeNode> DeviceNodes = new List<TreeNode>();
            foreach (Device dev in SinamicsInProject)
            {
                DeviceItem headModule;
                if (TiaServer.IsSinamicsG120Device(dev) || TiaServer.IsSinamicsG110Device(dev))
                {
                    TiaTreeNodeType nodeType = TiaTreeNodeType.TiaG120DONode;
                    if (TiaServer.IsSinamicsG110Device(dev))
                        nodeType = TiaTreeNodeType.TiaG110DONode;
                    headModule = TiaServer.GetSinamicsGHeadModule(dev);
                    if (headModule != null)
                    {
                        DeviceNodes.Add(new TiaTreeNode(headModule, nodeType));
                    }
                }
                else if (TiaServer.IsSinamicsS120Device(dev))
                {
                    headModule = TiaServer.GetS120HeadModule(dev);
                    if (headModule != null)
                    {
                        DeviceNodes.Add(CreateNewS120Node(headModule));
                    }
                }
            }

            if (!searchGroups)
            {
                foreach (TiaTreeNode groupNode in GetAllGroupNodes())
                {
                    DeviceNodes.Add(groupNode);
                }

            }

            if (DeviceNodes.Count != 0)
            {
                drivesInProject = new TiaTreeNode[DeviceNodes.Count];
                for (int i = 0; i < drivesInProject.Length; i++)
                {
                    drivesInProject[i] = (TiaTreeNode)DeviceNodes[i].Clone();
                }
            }

            Invoke(new MethodInvoker(delegate
            {
                ClearTreeviewDevices();
                TreeviewDevices.BeginUpdate();
                foreach (TiaTreeNode node in DeviceNodes)
                {
                    TreeviewDevices.Nodes.Add((TiaTreeNode)node.Clone());
                }

                if (TreeviewDevices.Nodes.Count != 0)
                {
                }

                TreeviewDevices.EndUpdate();
            }));



        }
        private TiaTreeNode CreateNewS120Node(DeviceItem headModule)
        {
            TiaTreeNode newNode = new TiaTreeNode(headModule, TiaTreeNodeType.TiaS120DeviceNode);
            foreach (TiaTreeNode node in CreateNodesForS120DriveObjects(headModule))
                newNode.Nodes.Add(node);

            return newNode;
        }
        private List<TiaTreeNode> CreateNodesForS120DriveObjects(DeviceItem headModule)
        {
            List<TiaTreeNode> TiaNodes = new List<TiaTreeNode>();
            foreach (DeviceItem itm in TiaServer.GetS120DriveObjects(headModule))
            {
                TiaNodes.Add(new TiaTreeNode(itm, TiaTreeNodeType.TiaS120DONode));
            }
            return TiaNodes;
        }
        private List<TiaTreeNode> GetAllGroupNodes()
        {
            List<TiaTreeNode> TiaNodes = new List<TiaTreeNode>();
            foreach (DeviceUserGroup grp in TiaServer.GetAllDriveGroupsInProject())
            {
                TiaTreeNode grpNode = new TiaTreeNode(grp, TiaTreeNodeType.TiaDeviceGroupNode);
                RefreshGroupNode(grpNode);
                TiaNodes.Add(grpNode);
            }
            return TiaNodes;
        }
        private void RefreshGroupNode(TiaTreeNode deviceGroupNode)
        {
            DeviceUserGroup deviceGroup = (DeviceUserGroup)deviceGroupNode.Tag;
            List<Device> DriveDevices = new List<Device>();
            deviceGroupNode.Nodes.Clear();
            TiaServer.GetSinamicsDevicesFromGroup(deviceGroup, ref DriveDevices);

            foreach (TiaTreeNode node in CreateNodesForDrives(DriveDevices))
                deviceGroupNode.Nodes.Add(node);

            foreach (TiaTreeNode node in GetNodesForDeviceGroups(TiaServer.GetDriveGroupsInGroup(deviceGroup.Groups)))
            {
                deviceGroupNode.Nodes.Add(node);
                RefreshGroupNode(node);
            }
        }
        private List<TiaTreeNode> CreateNodesForDrives(List<Device> Drives = null)
        {

            List<TiaTreeNode> TiaNodes = new List<TiaTreeNode>();
            foreach (Device dev in Drives)
            {
                DeviceItem headModule;
                if (TiaServer.IsSinamicsG120Device(dev) || TiaServer.IsSinamicsG110Device(dev))
                {
                    TiaTreeNodeType nodeType = TiaTreeNodeType.TiaG120DONode;
                    if (TiaServer.IsSinamicsG110Device(dev))
                        nodeType = TiaTreeNodeType.TiaG110DONode;
                    headModule = TiaServer.GetSinamicsGHeadModule(dev);
                    if (headModule != null)
                        TiaNodes.Add(new TiaTreeNode(headModule, nodeType));
                }
                else if (TiaServer.IsSinamicsS120Device(dev))
                {
                    headModule = TiaServer.GetS120HeadModule(dev);
                    if (headModule != null)
                        TiaNodes.Add(CreateNewS120Node(headModule));
                }
            }

            return TiaNodes;
        }
        private static List<TiaTreeNode> GetNodesForDeviceGroups(List<DeviceUserGroup> DriveDeviceGroups)
        {
            List<TiaTreeNode> TiaNodes = new List<TiaTreeNode>();
            foreach (DeviceUserGroup devGroup in DriveDeviceGroups)
            {
                TiaNodes.Add(new TiaTreeNode(devGroup, TiaTreeNodeType.TiaDeviceGroupNode));

            }
            return TiaNodes;
        }
        private void ClearTreeviewDevices()
        {
            Invoke(new MethodInvoker(delegate
            {
                TreeviewDevices.BeginUpdate();
                TreeviewDevices.Nodes.Clear();
                TreeviewDevices.EndUpdate();
                return;
            }));
        }
        internal void HandleTiaConfirmation(ConfirmationEventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (TiaServer.GetConfirmationType(e) == TIAConfirmation.RAM2ROM)
                {

                    if (SettingsServer.AppSettings.ConfirmRamToRom)
                    {
                        RamToRomDialog dlg = new RamToRomDialog(ref SettingsServer);
                        DialogResult result = dlg.ShowDialog();
                        if (result == DialogResult.Yes)
                            SettingsServer.AppSettings.AutomaticRamToRom = true;
                        else
                            SettingsServer.AppSettings.AutomaticRamToRom = false;

                        dlg.Dispose();
                    }


                    if (SettingsServer.AppSettings.AutomaticRamToRom)
                        e.Result = ConfirmationResult.Yes;
                    else
                        e.Result = ConfirmationResult.No;

                    e.IsHandled = true;
                }

            }));
        }
        private DeviceItem GetSelectedDeviceItem()
        {
            try
            {
                TiaTreeNode node = (TiaTreeNode)TreeviewDevices.SelectedNode;
                return (DeviceItem)node.Tag;
            }
            catch
            {
                return null;
            }
        }

        private void RefreshParameterState(int RowIndex, ParameterState state)
        {
            /*Color rowColor = Color.Black;
            Bitmap ParameterBitmap = null;
            switch (state)
            {
                case ParameterState.IGNORED:
                    ParameterBitmap = Resources.tag_white;
                    break;
                case ParameterState.SUCCESS:
                    ParameterBitmap = Resources.tag_green;
                    break;
                case ParameterState.ERROR:
                    rowColor = Color.Red;
                    ParameterBitmap = Resources.tag_red;
                    break;
            }
            */
            //DataGridViewParameters.Rows[RowIndex].DefaultCellStyle.ForeColor = rowColor;
            //DataGridViewParameters.Rows[RowIndex].Cells[0].Value = ParameterBitmap;
        }
        TiaTreeNode lastnode;
        TiaTreeNode node;
        private void TreeviewDevices_NodeSelect(object sender, TreeNodeMouseClickEventArgs e)
        {
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            node = (TiaTreeNode)TreeviewDevices.SelectedNode;
            if (node != null)
            {
                try
                {
                    if (node != lastnode)
                    {
                        pValNams.Clear();
                        lastnode = (TiaTreeNode)TreeviewDevices.SelectedNode;
                    }
                    lastnode = (TiaTreeNode)TreeviewDevices.SelectedNode;
                }
                catch
                {
                    lastnode = (TiaTreeNode)TreeviewDevices.SelectedNode;
                }
            }
            current.axis = "";
            currentDouble.axis = "";
            lastSelectedNode = e.Node;
            if (NodeSelectActive == false)
            {
                NodeSelectActive = true;
                return;
            }
            object tempValR = null;
            DeviceItem selectedDeviceItem = null;
            Invoke(new MethodInvoker(delegate
            {
                selectedDeviceItem = GetSelectedDeviceItem();
            }));
            try
            {
                ConsoleAnswer.Text = $"OFFLINE: Reading parameter values from {selectedDeviceItem.Name}";
                DriveObject SelectedDriveCheck = selectedDeviceItem.GetService<DriveObjectContainer>().DriveObjects[0];
                DevicePosition = selectedDeviceItem.PositionNumber.ToString();
                ProjectName = this.TiaServer.CurrentProject.Name.ToString();
                DeviceParameterCheck = SelectedDriveCheck.Parameters.Find("p2502[0]");
            }
            catch
            {
            }
            if (!OnlineCheckbox1.Checked)
            {
                string SelectedDeviceItemNameCheck = "";
                SelectedDeviceItemNameCheck = selectedDeviceItem.Name;
                int actualLine = 0;
                try
                {
                    if (Convert.ToInt32(DeviceParameterCheck.Value) == 2)
                    {
                        isSingle = false;
                        isDouble = true;
                        ConsoleAnswer.Text = $"OFFLINE: Reading parameter values from {selectedDeviceItem.Name}";
                        DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                        DriveParameter DeviceParameter;
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        currentDouble.axis = selectedDeviceItem.Name;
                        foreach (string Par in ParNames)
                        {
                            DeviceParameter = SelectedDrive.Parameters.Find(Par);
                            if (DeviceParameter == null)
                            {
                                ConsoleAnswer.Text = $"Parameter {Par} not found in device {SelectedDeviceItemName}";
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                actualLine++;
                                continue;
                            }
                            try
                            {
                                if (DeviceParameter.Value is DriveParameter)
                                    DeviceParameter.Value = ((DriveParameter)DeviceParameter.Value).Name;
                                else if (Par == "r2524")
                                    tempValR = ((DriveParameter)DeviceParameter).Value;
                                else
                                    DeviceParameter.Value = DeviceParameter.Value.ToString();
                                RefreshParameterState(actualLine, ParameterState.SUCCESS);
                                if (Par != "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(DeviceParameter.Value) });
                                }
                                else if (Par == "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(tempValR) });
                                }
                            }
                            catch (Exception)
                            {
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                            }
                            finally
                            {
                                actualLine++;
                            }
                        }
                    }
                    else if (Convert.ToInt32(DeviceParameterCheck.Value) == 1)
                    {
                        isSingle = true;
                        isDouble = false;
                        ConsoleAnswer.Text = $"OFFLINE: Reading parameter values from {selectedDeviceItem.Name}";
                        DriveObject SelectedDrive = selectedDeviceItem.GetService<DriveObjectContainer>().DriveObjects[0];
                        DriveParameter DeviceParameter;
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        current.axis = selectedDeviceItem.Name;
                        foreach (string Par in ParNamesSingle)
                        {
                            DeviceParameter = SelectedDrive.Parameters.Find(Par);
                            if (DeviceParameter == null)
                            {
                                ConsoleAnswer.Text = $"Parameter {Par} not found in device {SelectedDeviceItemName}";
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                actualLine++;
                                continue;
                            }
                            try
                            {
                                if (DeviceParameter.Value is DriveParameter)
                                    DeviceParameter.Value = ((DriveParameter)DeviceParameter.Value).Name;
                                else if (Par == "r2524")
                                {
                                    tempValR = ((DriveParameter)DeviceParameter).Value;
                                }
                                else
                                    DeviceParameter.Value = DeviceParameter.Value.ToString();
                                RefreshParameterState(actualLine, ParameterState.SUCCESS);
                                if (Par != "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(DeviceParameter.Value) });
                                }
                                else if (Par == "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(tempValR) });
                                }
                            }
                            catch (Exception)
                            {
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                            }
                            finally
                            {
                                actualLine++;
                            }
                        }
                    }
                }
                catch
                {
                    ConsoleAnswer.Text = string.Format("Error reading parameter from device");
                    RefreshParameterState(actualLine, ParameterState.ERROR);
                }
            }
            else
            {
                string SelectedDeviceItemNameCheck = "";
                SelectedDeviceItemNameCheck = selectedDeviceItem.Name;
                int actualLine = 0;
                try
                {
                    if (Convert.ToInt32(DeviceParameterCheck.Value) == 2)
                    {
                        isSingle = false;
                        isDouble = true;
                        DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                        DriveParameter DeviceParameter;
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        currentDouble.axis = selectedDeviceItem.Name;
                        foreach (string Par in ParNamesOnline1)
                        {
                            DeviceParameter = SelectedDrive.Parameters.Find(Par);
                            if (DeviceParameter == null)
                            {
                                ConsoleAnswer.Text = $"Parameter {Par} not found in device {SelectedDeviceItemName}";
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                actualLine++;
                                continue;
                            }
                            try
                            {
                                if (DeviceParameter.Value is DriveParameter)
                                    DeviceParameter.Value = ((DriveParameter)DeviceParameter.Value).Name;
                                else if (Par == "r2524")
                                    tempValR = ((DriveParameter)DeviceParameter).Value;
                                else
                                    DeviceParameter.Value = DeviceParameter.Value.ToString();
                                RefreshParameterState(actualLine, ParameterState.SUCCESS);
                                if (Par != "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(DeviceParameter.Value) });
                                }
                                else if (Par == "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(tempValR) });
                                }
                            }
                            catch (Exception)
                            {
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                            }
                            finally
                            {
                                actualLine++;
                            }
                        }
                        SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 0);
                        ConsoleAnswer.Text = $"ONLINE: Reading parameter values from {selectedDeviceItem.Name}";
                        OnlineDriveObject SelectedOnlineDrive = TiaServer.GetDriveObjectOnlineService(selectedDeviceItem);
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        current.axis = selectedDeviceItem.Name;
                        string DeviceParameterValue;
                        foreach (string Par in ParNamesOnline2)
                        {
                            DriveParameter DeviceOnlineParameter = SelectedOnlineDrive.Parameters.Find(Par);
                            if (DeviceOnlineParameter == null)
                            {
                                ConsoleAnswer.Text = $"Parameter {Par} not found in device {SelectedDeviceItemName}";
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                                actualLine++;
                                continue;
                            }
                            try
                            {
                                if (DeviceOnlineParameter.Value is DriveParameter)
                                    DeviceParameterValue = ((DriveParameter)DeviceOnlineParameter.Value).ToString();
                                else if (Par == "r2524")
                                {
                                    tempValR = ((DriveParameter)DeviceOnlineParameter).Value;
                                }
                                else
                                    DeviceParameterValue = DeviceOnlineParameter.Value.ToString();
                                RefreshParameterState(actualLine, ParameterState.SUCCESS);
                                if (Par != "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(DeviceOnlineParameter.Value) });
                                }
                                else if (Par == "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(tempValR) });
                                }
                            }
                            catch
                            {
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                            }
                            finally
                            {
                                actualLine++;
                            }
                        }
                    }
                    else if (Convert.ToInt32(DeviceParameterCheck.Value) == 1)
                    {
                        isSingle = true;
                        isDouble = false;
                        DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                        DriveParameter DeviceParameter;
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        currentDouble.axis = selectedDeviceItem.Name;
                        foreach (string Par in ParNamesOnline1)
                        {
                            DeviceParameter = SelectedDrive.Parameters.Find(Par);
                            if (DeviceParameter == null)
                            {
                                ConsoleAnswer.Text = $"Parameter {Par} not found in device {SelectedDeviceItemName}";
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                actualLine++;
                                continue;
                            }
                            try
                            {
                                if (DeviceParameter.Value is DriveParameter)
                                    DeviceParameter.Value = ((DriveParameter)DeviceParameter.Value).Name;
                                else if (Par == "r2524")
                                    tempValR = ((DriveParameter)DeviceParameter).Value;
                                else
                                    DeviceParameter.Value = DeviceParameter.Value.ToString();
                                RefreshParameterState(actualLine, ParameterState.SUCCESS);
                                if (Par != "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(DeviceParameter.Value) });
                                }
                                else if (Par == "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(tempValR) });
                                }
                            }
                            catch (Exception)
                            {
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                            }
                            finally
                            {
                                actualLine++;
                            }
                        }
                        SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 0);
                        ConsoleAnswer.Text = $"ONLINE: Reading parameter values from {selectedDeviceItem.Name}";
                        OnlineDriveObject SelectedOnlineDrive = TiaServer.GetDriveObjectOnlineService(selectedDeviceItem);
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        current.axis = selectedDeviceItem.Name;
                        string DeviceParameterValue;
                        foreach (string Par in ParNamesSingleOnline2)
                        {
                            DriveParameter DeviceOnlineParameter = SelectedOnlineDrive.Parameters.Find(Par);
                            if (DeviceOnlineParameter == null)
                            {
                                ConsoleAnswer.Text = $"Parameter {Par} not found in device {SelectedDeviceItemName}";
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                                actualLine++;
                                continue;
                            }
                            try
                            {
                                if (DeviceOnlineParameter.Value is DriveParameter)
                                    DeviceParameterValue = ((DriveParameter)DeviceOnlineParameter.Value).ToString();
                                else if (Par == "r2524")
                                {
                                    tempValR = ((DriveParameter)DeviceOnlineParameter).Value;
                                }
                                else
                                    DeviceParameterValue = DeviceOnlineParameter.Value.ToString();
                                RefreshParameterState(actualLine, ParameterState.SUCCESS);
                                if (Par != "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(DeviceOnlineParameter.Value) });
                                }
                                else if (Par == "r2524")
                                {
                                    pValNams.Add(new PValNam { PNam = Par, PVal = Convert.ToString(tempValR) });
                                }
                            }
                            catch
                            {
                                RefreshParameterState(actualLine, ParameterState.ERROR);
                                pValNams.Add(new PValNam { PNam = Par, PVal = "ERROR" });
                            }
                            finally
                            {
                                actualLine++;
                            }
                        }
                    }
                    SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 1);
                }
                catch
                {
                    ConsoleAnswer.Text = string.Format("Error reading parameter from device");
                    RefreshParameterState(actualLine, ParameterState.ERROR);
                    SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 0);
                }
            }
            CalibrateButton_Click();
        }

        private void WriteAllNewValues_Click(object sender, EventArgs e)
        {
            string SelectedDeviceItemName;
            StringBuilder sb = new StringBuilder();
            var list = new List<string>();
            if (online == false)
            {
                foreach (var PVN in pValNams)
                {
                    list.Add(Convert.ToString(PVN.PNam) + Convert.ToString(PVN.PVal));
                }
                foreach (string Row in list)
                {
                    try
                    {
                        string[] ParameterNameValue = Row.Split(',');
                        string ParameterName = ParameterNameValue[0];
                        string ParameterValue = ParameterNameValue[1];
                        DeviceItem selectedDeviceItem = null;
                        Invoke(new MethodInvoker(delegate
                        {
                            selectedDeviceItem = GetSelectedDeviceItem();
                        }));
                        DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                        DriveParameter DeviceParameter;
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        DeviceParameter = SelectedDrive.Parameters.Find(ParameterName);
                        try
                        {
                            if (ParameterValue != null && ParameterName != null && ParameterName != "r2524")
                            {
                                DeviceParameter.Value = ParameterValue;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch
                        {
                        }
                        ConsoleAnswer.Text = "Parameters written";
                    }
                    catch
                    {
                    }
                }
            }
            if (online == true)
            {
                foreach (var PVN in pValNams)
                {
                    list.Add(Convert.ToString(PVN.PNam) + Convert.ToString(PVN.PVal));
                }
                foreach (string Row in list)
                {
                    try
                    {
                        string[] ParameterNameValue = Row.Split(',');
                        string ParameterName = ParameterNameValue[0];
                        string ParameterValue = ParameterNameValue[1];
                        if (ParNamesOnline1.Contains(ParameterName))
                        {
                            DeviceItem selectedDeviceItem = null;
                            Invoke(new MethodInvoker(delegate
                            {
                                selectedDeviceItem = GetSelectedDeviceItem();
                            }));
                            DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                            DriveParameter DeviceParameter;
                            SelectedDeviceItemName = selectedDeviceItem.Name;
                            DeviceParameter = SelectedDrive.Parameters.Find(ParameterName);
                            try
                            {
                                if (ParameterValue != null && ParameterName != null && ParameterName != "r2524")
                                {
                                    DeviceParameter.Value = ParameterValue;
                                }
                                else
                                {
                                    ConsoleAnswer.Text = string.Format("{0}: Error writing parameter {1} ", SelectedDeviceItemName, ParameterName);
                                    ConsoleAnswer.Text = string.Format("{0}: Parameter {1} not found ", SelectedDeviceItemName, ParameterValue);
                                    continue;
                                }
                            }
                            catch
                            {
                            }
                        }
                        ConsoleAnswer.Text = "Parameters written";
                    }
                    catch
                    {
                    }
                }
                SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 0);
                foreach (string Row in list)
                {
                    try
                    {
                        string[] ParameterNameValue = Row.Split(',');
                        string ParameterName = ParameterNameValue[0];
                        string ParameterValue = ParameterNameValue[1];
                        if (!ParNamesOnline1.Contains(ParameterName) && ParameterName != "p408[0]" && ParameterName != "p408[1]" && ParameterName != "p407[0]" && ParameterName != "p407[1]" && ParameterName != "p421[0]" && ParameterName != "p421[1]" && ParameterName != "p423[0]" && ParameterName != "p423[1]")
                        {
                            DeviceItem selectedDeviceItem = null;
                            Invoke(new MethodInvoker(delegate
                            {
                                selectedDeviceItem = GetSelectedDeviceItem();
                            }));
                            OnlineDriveObject SelectedDrive = selectedDeviceItem.GetService<OnlineDriveObjectContainer>().OnlineDriveObjects[0];
                            DriveParameter DeviceParameter;
                            SelectedDeviceItemName = selectedDeviceItem.Name;
                            DeviceParameter = SelectedDrive.Parameters.Find(ParameterName);
                            try
                            {
                                if (ParameterValue != null && ParameterName != null && ParameterName != "r2524")
                                {
                                    DeviceParameter.Value = ParameterValue;
                                }
                                else
                                {
                                    ConsoleAnswer.Text = string.Format("{0}: Error writing parameter {1} ", SelectedDeviceItemName, ParameterName);
                                    ConsoleAnswer.Text = string.Format("{0}: Parameter {1} not found ", SelectedDeviceItemName, ParameterValue);
                                    continue;
                                }
                            }
                            catch
                            {
                            } 
                        }
                        ConsoleAnswer.Text = "Parameters written";
                    }
                    catch
                    {
                    }
                }
                SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 1);
            }
        }
        private void WriteAllNewValues()
        {
            string SelectedDeviceItemName;
            StringBuilder sb = new StringBuilder();
            var list = new List<string>();
            if (online == false)
            {
                foreach (var PVN in pValNams)
                {
                    try
                    {
                        DeviceItem selectedDeviceItem = null;
                        Invoke(new MethodInvoker(delegate
                        {
                            selectedDeviceItem = GetSelectedDeviceItem();
                        }));
                        DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                        DriveParameter DeviceParameter;
                        SelectedDeviceItemName = selectedDeviceItem.Name;
                        DeviceParameter = SelectedDrive.Parameters.Find(PVN.PNam);
                        try
                        {
                            if (PVN.PVal != null && PVN.PNam != null && PVN.PNam != "r2524")
                            {
                                DeviceParameter.Value = PVN.PVal;
                            }
                            else
                            {
                                ConsoleAnswer.Text = string.Format("{0}: Error writing parameter {1} ", SelectedDeviceItemName, PVN.PNam);
                                ConsoleAnswer.Text = string.Format("{0}: Parameter {1} not found ", SelectedDeviceItemName, PVN.PVal);
                                continue;
                            }
                        }
                        catch
                        {
                        }
                        ConsoleAnswer.Text = "Parameters written";
                    }
                    catch
                    {
                    }
                }
            }
            if (online == true)
            {
                foreach (var PVN in pValNams)
                {
                    try
                    {
                            DeviceItem selectedDeviceItem = null;
                            Invoke(new MethodInvoker(delegate
                            {
                                selectedDeviceItem = GetSelectedDeviceItem();
                            }));
                            DriveObject SelectedDrive = TiaServer.GetDriveObjectService(selectedDeviceItem);
                            DriveParameter DeviceParameter;
                            SelectedDeviceItemName = selectedDeviceItem.Name;
                            DeviceParameter = SelectedDrive.Parameters.Find(PVN.PNam);
                        try
                        {
                            if (PVN.PVal != null && PVN.PNam != null && PVN.PNam != "r2524")
                            {
                                DeviceParameter.Value = PVN.PVal;
                            }
                            else
                            {
                                ConsoleAnswer.Text = string.Format("{0}: Error writing parameter {1} ", SelectedDeviceItemName, PVN.PNam);
                                ConsoleAnswer.Text = string.Format("{0}: Parameter {1} not found ", SelectedDeviceItemName, PVN.PVal);
                                continue;
                            }
                        }
                        catch
                        {
                        }
                        ConsoleAnswer.Text = "Parameters written";
                    }
                    catch
                    {
                    }
                }
                SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 0);
                foreach (string Row in list)
                {
                    try
                    {
                        string[] ParameterNameValue = Row.Split(',');
                        string ParameterName = ParameterNameValue[0];
                        string ParameterValue = ParameterNameValue[1];
                        if (!ParNamesOnline1.Contains(ParameterName) && ParameterName != "p408[0]" && ParameterName != "p408[1]" && ParameterName != "p407[0]" && ParameterName != "p407[1]" && ParameterName != "p421[0]" && ParameterName != "p421[1]" && ParameterName != "p423[0]" && ParameterName != "p423[1]")
                        {
                            DeviceItem selectedDeviceItem = null;
                            Invoke(new MethodInvoker(delegate
                            {
                                selectedDeviceItem = GetSelectedDeviceItem();
                            }));
                            OnlineDriveObject SelectedDrive = selectedDeviceItem.GetService<OnlineDriveObjectContainer>().OnlineDriveObjects[0];
                            DriveParameter DeviceParameter;
                            SelectedDeviceItemName = selectedDeviceItem.Name;
                            DeviceParameter = SelectedDrive.Parameters.Find(ParameterName);
                            try
                            {
                                if (ParameterValue != null && ParameterName != null && ParameterName != "r2524")
                                {
                                    DeviceParameter.Value = ParameterValue;
                                }
                                else
                                {
                                    ConsoleAnswer.Text = string.Format("{0}: Error writing parameter {1} ", SelectedDeviceItemName, ParameterName);
                                    ConsoleAnswer.Text = string.Format("{0}: Parameter {1} not found ", SelectedDeviceItemName, ParameterValue);
                                    continue;
                                }
                            }
                            catch
                            {
                            }
                        }
                        ConsoleAnswer.Text = "Parameters written";
                    }
                    catch
                    {
                    }
                }
                SetOnlineConnectionForAllPLCs(TiaServer.CurrentProject, 1);
            }
        }
        private void CalibrateButton_Click()
        {
            string EncoderChosen = "";
            string Encoder1Chosen = "";
            DialogResult resultChoice = DialogResult.Cancel;
            if (isSingle == true)
            {
                ChoiceForm MChoiceForm = new ChoiceForm(1);
                resultChoice = MChoiceForm.ShowDialog(this);
                if (resultChoice == DialogResult.OK)
                {
                    EncoderChosen = MChoiceForm.obtainEncoderChoice();
                }
            }
            else if (isDouble == true)
            {
                ChoiceForm MChoiceForm = new ChoiceForm("two");
                resultChoice = MChoiceForm.ShowDialog(this);
                if (resultChoice == DialogResult.OK)
                {
                    EncoderChosen = MChoiceForm.obtainEncoderChoice();
                    Encoder1Chosen = MChoiceForm.obtainEncoder1Choice();
                }
            }
            if (resultChoice == DialogResult.OK)
            {
                foreach (var PVN in pValNams)
                {
                    try
                    {
                        if (Convert.ToInt32(DeviceParameterCheck.Value) == 1)
                        {
                            if (EncoderChosen == "No encoder")
                                current.encoder = "No encoder";
                            else if (EncoderChosen == "Siemens absolute")
                                current.encoder = "Siemens absolute";
                            else if (EncoderChosen == "Rotary HTL/TTL")
                                current.encoder = "Rotary HTL/TTL";
                            else if (EncoderChosen == "Linear")
                                current.encoder = "Linear";
                            current.projectName = ProjectName;
                            current.PositionNumber = DevicePosition;
                            current.pValNams1 = pValNams;
                            current.write = false;
                        }
                        else if (Convert.ToInt32(DeviceParameterCheck.Value) == 2)
                        {
                            if (EncoderChosen == "No encoder")
                                currentDouble.encoderv0 = "No encoder";
                            else if (EncoderChosen == "Siemens absolute")
                                currentDouble.encoderv0 = "Siemens absolute";
                            else if (EncoderChosen == "Rotary HTL/TTL")
                                currentDouble.encoderv0 = "Rotary HTL/TTL";
                            else if (EncoderChosen == "Linear")
                                currentDouble.encoderv0 = "Linear";
                            if (Encoder1Chosen == "No encoder")
                                currentDouble.encoderv1 = "No encoder";
                            else if (Encoder1Chosen == "Siemens absolute")
                                currentDouble.encoderv1 = "Siemens absolute";
                            else if (Encoder1Chosen == "Rotary HTL/TTL")
                                currentDouble.encoderv1 = "Rotary HTL/TTL";
                            else if (Encoder1Chosen == "Linear")
                                currentDouble.encoderv1 = "Linear";
                            currentDouble.projectName = ProjectName;
                            currentDouble.PositionNumber = DevicePosition;
                            currentDouble.pValNams1 = pValNams;
                            currentDouble.write = false;
                        }
                    }
                    catch
                    {
                        //ConsoleAnswer.Text = "Failed to write parameters";
                    }
                }
                if (Convert.ToInt32(DeviceParameterCheck.Value) == 1)
                {
                    try
                    {
                        MathForm MForm = new MathForm(current);
                        DialogResult result = MForm.ShowDialog(this);
                        if (MForm.BackwardsDataTransferSingle() != null && result == DialogResult.OK)
                        {
                            MathForm.DataToReadSingle currentTransition = MForm.BackwardsDataTransferSingle();

                            pValNams.Clear();

                            pValNams.Add(new PValNam { PNam = "p418[0]", PVal = Convert.ToString(currentTransition.p418) });
                            pValNams.Add(new PValNam { PNam = "p419[0]", PVal = Convert.ToString(currentTransition.p419) });
                            pValNams.Add(new PValNam { PNam = "p2504[0]", PVal = Convert.ToString(currentTransition.p2504) });
                            pValNams.Add(new PValNam { PNam = "p2505[0]", PVal = Convert.ToString(currentTransition.p2505) });
                            pValNams.Add(new PValNam { PNam = "p1082[0]", PVal = Convert.ToString(currentTransition.p1082) });
                            pValNams.Add(new PValNam { PNam = "p2000", PVal = Convert.ToString(currentTransition.p2000) });
                            pValNams.Add(new PValNam { PNam = "p1121[0]", PVal = Convert.ToString(currentTransition.p1121) });
                            pValNams.Add(new PValNam { PNam = "p1135[0]", PVal = Convert.ToString(currentTransition.p1135) });
                            pValNams.Add(new PValNam { PNam = "p2506[0]", PVal = Convert.ToString(currentTransition.p2506) });
                            pValNams.Add(new PValNam { PNam = "p2503[0]", PVal = Convert.ToString(currentTransition.p2503) });
                            pValNams.Add(new PValNam { PNam = "r2524", PVal = Convert.ToString(currentTransition.r2524) });
                            pValNams.Add(new PValNam { PNam = "p2571", PVal = Convert.ToString(currentTransition.p2571) });
                            pValNams.Add(new PValNam { PNam = "p2572", PVal = Convert.ToString(currentTransition.p2572) });
                            pValNams.Add(new PValNam { PNam = "p2573", PVal = Convert.ToString(currentTransition.p2573) });
                            pValNams.Add(new PValNam { PNam = "p2580", PVal = Convert.ToString(currentTransition.p2580) });
                            pValNams.Add(new PValNam { PNam = "p2581", PVal = Convert.ToString(currentTransition.p2581) });
                            pValNams.Add(new PValNam { PNam = "p2585", PVal = Convert.ToString(currentTransition.p2585) });
                            pValNams.Add(new PValNam { PNam = "p2586", PVal = Convert.ToString(currentTransition.p2586) });
                            pValNams.Add(new PValNam { PNam = "p408[0]", PVal = Convert.ToString(currentTransition.p408) });
                            pValNams.Add(new PValNam { PNam = "p421[0]", PVal = Convert.ToString(currentTransition.p421) });
                            pValNams.Add(new PValNam { PNam = "p423[0]", PVal = Convert.ToString(currentTransition.p423) });
                            pValNams.Add(new PValNam { PNam = "p407[0]", PVal = Convert.ToString(currentTransition.p407) });
                            WriteAllNewValues();
                        }
                    }
                    catch
                    {
                    }
                }
                else if (Convert.ToInt32(DeviceParameterCheck.Value) == 2)
                {                    try
                    {
                        MathForm MForm = new MathForm(currentDouble);
                        DialogResult result = MForm.ShowDialog(this);
                        if (MForm.BackwardsDataTransferDouble() != null && result == DialogResult.OK)
                        {
                            MathForm.DataToReadDouble currentTransition = MForm.BackwardsDataTransferDouble();
                            
                            pValNams.Clear();

                            pValNams.Add(new PValNam { PNam = "p418[0]", PVal = Convert.ToString(currentTransition.p418v0) });
                            pValNams.Add(new PValNam { PNam = "p419[0]", PVal = Convert.ToString(currentTransition.p419v0) });
                            pValNams.Add(new PValNam { PNam = "p2504[0]", PVal = Convert.ToString(currentTransition.p2504) });
                            pValNams.Add(new PValNam { PNam = "p2505[0]", PVal = Convert.ToString(currentTransition.p2505) });
                            pValNams.Add(new PValNam { PNam = "p1082[0]", PVal = Convert.ToString(currentTransition.p1082) });
                            pValNams.Add(new PValNam { PNam = "p2000", PVal = Convert.ToString(currentTransition.p2000) });
                            pValNams.Add(new PValNam { PNam = "p1121[0]", PVal = Convert.ToString(currentTransition.p1121) });
                            pValNams.Add(new PValNam { PNam = "p1135[0]", PVal = Convert.ToString(currentTransition.p1135) });
                            pValNams.Add(new PValNam { PNam = "p2506[0]", PVal = Convert.ToString(currentTransition.p2506) });
                            pValNams.Add(new PValNam { PNam = "p2503[0]", PVal = Convert.ToString(currentTransition.p2503) });
                            pValNams.Add(new PValNam { PNam = "r2524", PVal = Convert.ToString(currentTransition.r2524) });
                            pValNams.Add(new PValNam { PNam = "p2571", PVal = Convert.ToString(currentTransition.p2571) });
                            pValNams.Add(new PValNam { PNam = "p2572", PVal = Convert.ToString(currentTransition.p2572) });
                            pValNams.Add(new PValNam { PNam = "p2573", PVal = Convert.ToString(currentTransition.p2573) });
                            pValNams.Add(new PValNam { PNam = "p2580", PVal = Convert.ToString(currentTransition.p2580) });
                            pValNams.Add(new PValNam { PNam = "p2581", PVal = Convert.ToString(currentTransition.p2581) });
                            pValNams.Add(new PValNam { PNam = "p2585", PVal = Convert.ToString(currentTransition.p2585) });
                            pValNams.Add(new PValNam { PNam = "p2586", PVal = Convert.ToString(currentTransition.p2586) });
                            pValNams.Add(new PValNam { PNam = "p418[1]", PVal = Convert.ToString(currentTransition.p418v1) });
                            pValNams.Add(new PValNam { PNam = "p419[1]", PVal = Convert.ToString(currentTransition.p419v1) });
                            pValNams.Add(new PValNam { PNam = "p408[0]", PVal = Convert.ToString(currentTransition.p408v0) });
                            pValNams.Add(new PValNam { PNam = "p421[0]", PVal = Convert.ToString(currentTransition.p421v0) });
                            pValNams.Add(new PValNam { PNam = "p423[0]", PVal = Convert.ToString(currentTransition.p423v0) });
                            pValNams.Add(new PValNam { PNam = "p407[0]", PVal = Convert.ToString(currentTransition.p407v0) });
                            pValNams.Add(new PValNam { PNam = "p408[1]", PVal = Convert.ToString(currentTransition.p408v1) });
                            pValNams.Add(new PValNam { PNam = "p421[1]", PVal = Convert.ToString(currentTransition.p421v1) });
                            pValNams.Add(new PValNam { PNam = "p423[1]", PVal = Convert.ToString(currentTransition.p423v1) });
                            pValNams.Add(new PValNam { PNam = "p407[1]", PVal = Convert.ToString(currentTransition.p407v1) });
                            WriteAllNewValues();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                ConsoleAnswer.Text = "Failed to choose encoder type";
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStart = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point newPos = PointToScreen(new Point(e.X - dragStart.X, e.Y - dragStart.Y));
                DesktopLocation = newPos;
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.SelectedPath = SettingsServer.AppSettings.LastProjectsFolder;
            folderDialog.Description = "Select the project folder to delete";
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    Directory.Delete(folderDialog.SelectedPath, true);
                    MessageBox.Show("The folder has been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting the folder: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnlineCheckbox(object sender, EventArgs e)
        {
            CheckBox OnlineCheckbox = sender as CheckBox;
            if (OnlineCheckbox.Checked == true)
            {
                online = true;
            }
            else
            {
                online = false;
            }
        }

        public static bool SetOnlineConnectionForAllPLCs(Project project, int x)
        {
            bool anyPLCOnline = false;

            foreach (Device device in project.Devices)
            {
                foreach (DeviceItem deviceItem in device.DeviceItems)
                {
                    OnlineProvider onlineProvider = deviceItem.GetService<OnlineProvider>();
                    if (onlineProvider != null)
                    {
                        if (x == 0)
                        {
                            // Establish online connection to PLC: 
                            OnlineState onlineState = onlineProvider.GoOnline();
                            if (onlineState == OnlineState.Online)
                            {
                                anyPLCOnline = true;
                            }
                        }
                        else if (x == 1)
                        {
                            // Disconnect online connection to PLC: 
                            onlineProvider.GoOffline();
                        }
                    }
                }
            }
            return anyPLCOnline;
        }
        private void TreeviewDevices_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
        }
        public class DriveParametersDTO
        {
            public double? P408_0 { get; set; }
            public double? P421_0 { get; set; }
            public double? P423_0 { get; set; }
        }

        // Function to convert the parameters to the DTO
        public DriveParametersDTO ConvertParametersToDTO(DriveObject cuDriveObject)
        {
            var parametersDTO = new DriveParametersDTO();
            parametersDTO.P408_0 = GetParameterDoubleValue(cuDriveObject.Parameters, 408);
            parametersDTO.P421_0 = GetParameterDoubleValue(cuDriveObject.Parameters, 421);
            parametersDTO.P423_0 = GetParameterDoubleValue(cuDriveObject.Parameters, 423);
            return parametersDTO;
        }

        // Helper function to retrieve parameter values as doubles
        private double? GetParameterDoubleValue(DriveParameterComposition parameters, int parameterNumber)
        {
            DriveParameter parameter = parameters[parameterNumber];
            if (parameter != null)
            {
                if (parameter.Value != null)
                {
                    try
                    {
                        return Convert.ToDouble(parameter.Value);
                    }
                    catch (FormatException)
                    {
                        // Handle the case where the value cannot be converted to a double
                        return null;
                    }
                }
            }
            return null;  // Return null if the parameter is not found or has no value
        }

        // Export parameters to an XML file
        public void ExportParametersToXML(DriveObject cuDriveObject, string exportFilePath)
        {
            DriveParametersDTO parametersDTO = ConvertParametersToDTO(cuDriveObject);

            // Create an XML serializer for the DTO
            XmlSerializer serializer = new XmlSerializer(typeof(DriveParametersDTO));

            // Create a StreamWriter to write the XML data to the file
            using (StreamWriter writer = new StreamWriter(exportFilePath))
            {
                serializer.Serialize(writer, parametersDTO);
            }
        }
    }
    public enum ParameterState
    {
        SUCCESS,
        ERROR,
        IGNORED
    }
    public class PValNam
    {
        public string PNam { get; set; }
        public string PVal { get; set; }
    }
    public class ParametersReadSingle
    {
        public string p418 { get; set; }
        public string p419 { get; set; }
        public string p2504 { get; set; }
        public string p2505 { get; set; }
        public string p1082 { get; set; }
        public string p2000 { get; set; }
        public string p1121 { get; set; }
        public string p1135 { get; set; }
        public string p2506 { get; set; }
        public string p2503 { get; set; }
        public string r2524 { get; set; }
        public string p2571 { get; set; }
        public string p2572 { get; set; }
        public string p2573 { get; set; }
        public string p2580 { get; set; }
        public string p2581 { get; set; }
        public string p2585 { get; set; }
        public string p2586 { get; set; }
        public string p408 { get; set; }
        public string p421 { get; set; }
        public string p423 { get; set; }
        public string p407 { get; set; }
        public string axis { get; set; }
        public string encoder { get; set; }
        public string projectName { get; set; }
        public string PositionNumber { get; set; }
        public List<PValNam> pValNams1 { get; set; }
        public bool write { get; set; }
    }
    public class ParametersReadDouble
    {
        public string p418v0 { get; set; }
        public string p419v0 { get; set; }
        public string p2504 { get; set; }
        public string p2505 { get; set; }
        public string p1082 { get; set; }
        public string p2000 { get; set; }
        public string p1121 { get; set; }
        public string p1135 { get; set; }
        public string p2506 { get; set; }
        public string p2503 { get; set; }
        public string r2524 { get; set; }
        public string p2571 { get; set; }
        public string p2572 { get; set; }
        public string p2573 { get; set; }
        public string p2580 { get; set; }
        public string p2581 { get; set; }
        public string p2585 { get; set; }
        public string p2586 { get; set; }
        public string p418v1 { get; set; }
        public string p419v1 { get; set; }
        public string p408v0 { get; set; }
        public string p421v0 { get; set; }
        public string p423v0 { get; set; }
        public string p407v0 { get; set; }
        public string p408v1 { get; set; }
        public string p421v1 { get; set; }
        public string p423v1 { get; set; }
        public string p407v1 { get; set; }
        public string axis { get; set; }
        public string encoderv0 { get; set; }
        public string encoderv1 { get; set; }
        public string projectName { get; set; }
        public string PositionNumber { get; set; }
        public List<PValNam> pValNams1 { get; set; }
        public bool write { get; set; }
    }
}