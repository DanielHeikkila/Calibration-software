using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace StartDriveParameterEditor
{
    internal partial class ProjectSelectionForm : Form
    {
        private ApplicationSettingsServer SettingsServer;
        private string newProject;


        /// <summary>
        /// The path of the project to be opened
        /// </summary>
        internal string NewProject
        {
            get
            {
                return TxtBoxProjectName.Text;
            }
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="settings"></param>
        internal ProjectSelectionForm(ref ApplicationSettingsServer settings)
        {
            InitializeComponent();
            SettingsServer = settings;
            Text = SettingsServer.AppSettings.ApplicationTitle;


            TxtBoxProjectDirectory.Text = SettingsServer.AppSettings.LastProjectsFolder;
            OpenBtn.Enabled = false;
            if (!String.IsNullOrEmpty(TxtBoxProjectDirectory.Text))
                OnProjectFolderChanged(null, new EventArgs());
        }


        /// <summary>
        /// Get the Path of the project to be opened
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBrowseProjectFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TxtBoxProjectDirectory.Text = folderBrowserDialog1.SelectedPath;
            }

        }


        /// <summary>
        /// Find and list all TIA projects in the selected folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnProjectFolderChanged(object sender, EventArgs e)
        {
            /*try
            {
                lblErrorstatusl.Visible = false;
                lblErrorstatusl.Text = "";
                LblStatusProjectSize.Text = "";
                LblStatusLastModified.Text = "";
                LblStatusProjectSize.Visible = false;
                LblStatusLastModified.Visible = false;
                TxtBoxProjectName.Text = "";
            }
            catch 
            {
            }*/
            ListViewItem tmpItem;
            Cursor currentCursor = Cursor.Current;
            try
            {
                ListviewProjects.BeginUpdate();
                ListviewProjects.Items.Clear();
                Cursor.Current = Cursors.WaitCursor;

                foreach (string ProjectFile in Directory.GetFiles(TxtBoxProjectDirectory.Text, "*.ap*", System.IO.SearchOption.AllDirectories))
                {

                    string FileExtension = ProjectFile.Substring(ProjectFile.LastIndexOf('.') + 1).ToLower();
                    if (FileExtension != "ap16" && FileExtension != "ap14" && FileExtension != "ap13" && FileExtension != "ap12" && FileExtension != "ap11")
                        continue;

                    tmpItem = new ListViewItem(ProjectFile);
                    ListviewProjects.Items.Add(tmpItem);
                }
                if (ListviewProjects.Items.Count == 0)
                {
                    lblErrorstatusl.Text = "No TIA projects in the selected folder";
                    lblErrorstatusl.Visible = true;
                }
            }

            catch (Exception ex)
            {
                lblErrorstatusl.Text = ex.Message;
                lblErrorstatusl.Visible = true;
            }
            finally
            {
                ListviewProjects.EndUpdate();
                Cursor.Current = currentCursor;
            }
        }


        /// <summary>
        /// Show size and last modified date of the selected folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnselectedProjectChanged(object sender, EventArgs e)
        {
            try
            {
                if (ListviewProjects.SelectedItems.Count == 0)
                {
                    OpenBtn.Enabled = false;
                    return;
                }


                String ProjectFilePath = ListviewProjects.SelectedItems[0].Text;
                TxtBoxProjectName.Text = ProjectFilePath;
                String ProjectPath = Path.GetDirectoryName(ProjectFilePath);
                DateTime LastWriteTime = Directory.GetLastWriteTime(ProjectFilePath);
                OpenBtn.Enabled = true;
            }
            catch
            {
            }
        }


        /// <summary>
        /// Get the size of a folder in kB
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        private static long GetFolderSizeInKB(string folderPath)
        {
            string[] fileNames = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
            long SizeInBytes = 0;
            foreach (string name in fileNames)
            {
                FileInfo info = new FileInfo(name);
                SizeInBytes += info.Length;
            }
            return SizeInBytes / 1024;
        }


        /// <summary>
        /// Save project folder path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            SettingsServer.AppSettings.LastProjectsFolder = TxtBoxProjectDirectory.Text;
            newProject = TxtBoxProjectName.Text;
        }


        /// <summary>
        /// Simulate Ok button click if the list is doubleclicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListviewProjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (ListviewProjects.SelectedItems.Count > 0)
                {
                    OpenBtn_Click(sender, null);
                    DialogResult = DialogResult.OK;
                }
            }
            catch
            {
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
