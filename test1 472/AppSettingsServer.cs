using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using test1_472;

namespace StartDriveParameterEditor
{

    [Serializable]
    [XmlRootAttribute("AppConfig", IsNullable = false)]
    public class ApplicationSettings
    {
        const int ABSOLUTE_MAX_RECENT_COUNT = 10;
        const int USER_PARAMETER_COUNT = 10;
        const string DEFAULT_PARAMETERLIST_FOLDER = "Parameter Lists";

        private string[] recentProjects;
        private string lastProjectsFolder;
        private string parameterListFolderPath;
        private bool showUserGroups;
        private bool confirmRemoveParameterList;
        private bool removeParameterListFile;
        private bool confirmRemoveParameter;
        private bool showInfo;
        private bool showWarnings;
        private bool showErrors;
        private bool loadLastProject;
        private bool showRecentProjects;
        private bool automaticallySaveProject;
        private bool automaticallySaveParameterLists;
        private bool automaticRamToRom;
        private bool confirmRamToRom;
        private bool confirmExitApplication;

        private int recentProjectsToShow;
        private string assemblyName;
        private string assemblyVersion;
        private ParameterGridView parameterView;
        private ParameterListGridView parameterListView;




        /// <summary>
        /// Constructor
        /// </summary>
        internal ApplicationSettings()
        {
            recentProjects = new string[ABSOLUTE_MAX_RECENT_COUNT];
            RestoreDefaultSettings();
        }


        /// <summary>
        /// Name of the assembly
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return assemblyName;
            }
        }


        /// <summary>
        /// version of the assembly
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return assemblyVersion;
            }
        }


        /// <summary>
        /// Application title (assembly name & version)
        /// </summary>
        public string ApplicationTitle
        {
            get
            {
                Assembly assembly = typeof(Form1).Assembly;
                return assemblyName + " " + string.Format("V{0}.{1}", assembly.GetName().Version.Major, assembly.GetName().Version.Minor);
            }
        }


        /// <summary>
        /// Path of the folder where the parameter lists are stored
        /// </summary>
        [XmlElement]
        public string ParameterListFolderPath
        {
            get
            {
                return parameterListFolderPath;
            }
            set
            {
                parameterListFolderPath = value;
            }
        }


        /// <summary>
        /// The path of the last folder used to search TIA projects
        /// </summary>
        public string LastProjectsFolder
        {
            get
            {
                return lastProjectsFolder;
            }
            set
            {
                lastProjectsFolder = value;
            }
        }


        /// <summary>
        /// If true show the user groups in the treeview
        /// </summary>
        public bool ShowUserGroups
        {
            get
            {
                return showUserGroups;
            }
            set
            {
                showUserGroups = value;
            }
        }


        /// <summary>
        /// if true user must confirm removing parameter lists
        /// </summary>
        public bool ConfirmRemoveParameterList
        {
            get
            {
                return confirmRemoveParameterList;
            }
            set
            {
                confirmRemoveParameterList = value;
            }
        }


        /// <summary>
        /// If true the parameter list file is removed from disk when the list is deleted in the application
        /// </summary>
        public bool RemoveParameterListFile
        {
            get
            {
                return removeParameterListFile;
            }
            set
            {
                removeParameterListFile = value;
            }
        }


        /// <summary>
        /// If true user must confirm removing parameters
        /// </summary>
        public bool ConfirmRemoveParameter
        {
            get
            {
                return confirmRemoveParameter;
            }
            set
            {
                confirmRemoveParameter = value;
            }
        }


        /// <summary>
        /// If true the TIA project is automatically saved after writing parameters
        /// </summary>
        public bool AutomaticallySaveProject
        {
            get
            {
                return automaticallySaveProject;
            }
            set
            {
                automaticallySaveProject = value;
            }
        }


        /// <summary>
        /// If true the parameter lists are automatically saved before exiting the application
        /// </summary>
        public bool AutomaticallySaveParameterLists
        {
            get
            {
                return automaticallySaveParameterLists;
            }
            set
            {
                automaticallySaveParameterLists = value;
            }
        }

        /// <summary>
        /// If true user must confirm exiting application
        /// </summary>
        public bool ConfirmExitApplication
        {
            get
            {
                return confirmExitApplication;
            }
            set
            {
                confirmExitApplication = value;
            }
        }


        /// <summary>
        /// If true RAM is automatically saved to ROM after writing parameters online (Only G120)
        /// </summary>
        public bool AutomaticRamToRom
        {
            get
            {
                return automaticRamToRom;
            }
            set
            {
                automaticRamToRom = value;
            }
        }


        /// <summary>
        /// If true user must confirm saving RAM to ROM
        /// </summary>
        public bool ConfirmRamToRom
        {
            get
            {
                return confirmRamToRom;
            }

            set
            {
                confirmRamToRom = value;
            }
        }


        /// <summary>
        /// Info messages filter 
        /// </summary>
        public bool ShowInfo
        {
            get
            {
                return showInfo;
            }
            set
            {
                showInfo = value;
            }
        }


        /// <summary>
        /// Warning messages filter 
        /// </summary>
        public bool ShowWarnings
        {
            get
            {
                return showWarnings;
            }
            set
            {
                showWarnings = value;
            }
        }


        /// <summary>
        /// Error messages filter 
        /// </summary>
        public bool ShowErrors
        {
            get
            {
                return showErrors;
            }
            set
            {
                showErrors = value;
            }
        }


        /// <summary>
        /// If true the last project is automatically opened when the application starts 
        /// </summary>
        public bool LoadLastProject
        {
            get
            {
                return loadLastProject;
            }
            set
            {
                loadLastProject = value;
            }
        }


        /// <summary>
        /// If true the list of recent projects is shown
        /// </summary>
        public bool ShowRecentProjects
        {
            get
            {
                return showRecentProjects;
            }
            set
            {
                showRecentProjects = value;
            }
        }


        /// <summary>
        /// The number of recent projects to be shown 
        /// </summary>
        public int RecentProjectsToShow
        {
            get
            {
                return recentProjectsToShow;
            }
            set
            {
                recentProjectsToShow = value;
            }
        }


        /// <summary>
        /// the recent projects
        /// </summary>
        public string[] RecentProjects
        {
            get
            {
                return recentProjects;
            }
            set
            {
                recentProjects = value;
            }
        }



        /// <summary>
        /// the last opened project
        /// </summary>
        public string LastProject
        {
            get
            {
                return recentProjects[0];
            }
        }


        /// <summary>
        /// Visible columns in the parameter grid
        /// </summary>
        public ParameterGridView ParameterView
        {
            get
            {
                return parameterView;
            }
            set
            {
                parameterView = value;
            }
        }


        /// <summary>
        /// Visible columns in the parameter list grid
        /// </summary>
        public ParameterListGridView ParameterListView
        {
            get
            {
                return parameterListView;
            }
            set
            {
                parameterListView = value;
            }
        }


        /// <summary>
        /// Restore default values
        /// </summary>
        internal void RestoreDefaultSettings()
        {
            lastProjectsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            showUserGroups = true;
            confirmRemoveParameterList = true;
            confirmRemoveParameter = true;
            removeParameterListFile = false;
            ShowInfo = true;
            ShowWarnings = true;
            ShowErrors = true;
            loadLastProject = false;
            automaticallySaveProject = false;
            automaticallySaveParameterLists = false;
            automaticRamToRom = false;
            confirmRamToRom = true;
            showRecentProjects = true;
            confirmExitApplication = true;
            recentProjectsToShow = ABSOLUTE_MAX_RECENT_COUNT;
            Assembly assembly = typeof(Form1).Assembly;
            assemblyName = assembly.GetName().Name;
            assemblyVersion = string.Format("V{0}", assembly.GetName().Version.ToString());
            string location = assembly.Location;
            location = location.Substring(0, location.LastIndexOf('\\') + 1);
            parameterListFolderPath = location + DEFAULT_PARAMETERLIST_FOLDER;
            parameterListView.showParameterListName = true;
            parameterListView.showParameterListDescription = true;
            parameterListView.showParameterListLocation = false;
            parameterListView.showParameterListOnline = false;
            parameterListView.showParameterListType = false;
            parameterView.showParameterNumber = true;
            parameterView.showParameterDescription = true;
            parameterView.showParameterValue = true;
            parameterView.showParameterUnit = true;
            parameterView.showParameterMaximum = true;
            parameterView.showParameterMinimum = true;
            parameterView.showParameterActive = true;

        }


        /// <summary>
        /// Add a project to the list of the recent projects
        /// </summary>
        /// <param name="projectPath"></param>
        internal void AddProjectToRecent(string projectPath)
        {
            if (recentProjects[0] == projectPath)
                return;
            int count = recentProjects.Length;
            int indexOfProject = -1; // index of project if it's already in the list 
            string lastProject = recentProjects[count - 1];
            for (int i = count - 1; i > 0; i--)
            {
                if (recentProjects[i] == projectPath)
                    indexOfProject = i + 1;
                recentProjects[i] = recentProjects[i - 1];
            }
            if (indexOfProject != -1)
            {
                for (int i = indexOfProject; i < count - 1; i++)
                    recentProjects[i] = recentProjects[i + 1];
                recentProjects[count - 1] = lastProject;
            }
            recentProjects[0] = projectPath;

        }


        /// <summary>
        /// Number of projects in the recent projects list
        /// </summary>
        /// <returns></returns>
        internal int GetProjectCount()
        {
            int count = 0;
            for (int i = 0; i < recentProjects.Length; i++)
            {
                if (!string.IsNullOrEmpty(recentProjects[i]))
                    count++;
            }
            if (count > recentProjectsToShow)
                count = recentProjectsToShow;

            return count;
        }


        /// <summary>
        /// Clear the list of recent projects
        /// </summary>
        internal void ClearRecentProjects()
        {
            for (int i = 0; i < recentProjects.Length; i++)
                recentProjects[i] = "";
        }

    }


    public class ApplicationSettingsServer
    {
        const string DEFAULT_SETTINGS_FILE_NAME = "Edit parameters in several drives.setting";
        private ApplicationSettings appSettings;


        /// <summary>
        /// Application settings
        /// </summary>
        public ApplicationSettings AppSettings
        {
            get
            {
                return appSettings;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        internal ApplicationSettingsServer()
        {
            appSettings = new ApplicationSettings();
        }


        /// <summary>
        /// Save Settings to file
        /// </summary>
        /// <param name="myConfigFileName"></param>
        internal void Save(string myConfigFileName)
        {
            try
            {
                if (!File.Exists(myConfigFileName))
                {
                    FileStream f = File.Create(myConfigFileName);
                    f.Close();
                }

                XmlSerializer mySerializer = new XmlSerializer(typeof(ApplicationSettings));
                using (TextWriter writer = new StreamWriter(myConfigFileName))
                {
                    mySerializer.Serialize(writer, appSettings);
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// Save Settings to file
        /// </summary>
        /// 
        internal void Save()
        {
            Save(DEFAULT_SETTINGS_FILE_NAME);

        }

        /// <summary>
        /// Load settings from file
        /// </summary>
        /// <param name="myConfigFileName"></param>
        internal void Load(string myConfigFileName)
        {
            if (!File.Exists(myConfigFileName))
            {
                return;
            }

            XmlSerializer mySerializer = new XmlSerializer(typeof(ApplicationSettings));
            using (FileStream fstream = new FileStream(myConfigFileName, FileMode.Open))
            {
                try
                {
                    appSettings = (ApplicationSettings)mySerializer.Deserialize(fstream);

                }
                catch
                {

                }

            }
        }

        /// <summary>
        /// Load settings from file
        /// </summary>
        /// <param name="myConfigFileName"></param>
        internal void Load()
        {
            Load(DEFAULT_SETTINGS_FILE_NAME);
        }
    }


    /// <summary>
    /// Visible parameter columns
    /// </summary>
    [Serializable]
    public struct ParameterGridView
    {
        public bool showParameterNumber;
        public bool showParameterDescription;
        public bool showParameterValue;
        public bool showParameterUnit;
        public bool showParameterMaximum;
        public bool showParameterMinimum;
        public bool showParameterActive;
    }



    /// <summary>
    /// Visible parameter list columns
    /// </summary>
    [Serializable]
    public struct ParameterListGridView
    {
        public bool showParameterListName;
        public bool showParameterListDescription;
        public bool showParameterListLocation;
        public bool showParameterListOnline;
        public bool showParameterListType;
    }
}
