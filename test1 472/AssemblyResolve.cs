using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;

namespace StartDriveParameterEditor
{

    /// <summary>
    /// Load the Openness Assembly. No configuration file is needed in this case
    /// The application configuration file(app.config)  can also be used to load the assembly.
    /// In this case the application configuration  file must be distributed with the application executable.
    /// </summary>
    public static class AssemblyResolve
    {
        /// <summary>
        /// Add assembly resolver
        /// </summary>
        public static void AddResolver()
        {
            AppDomain CurrentDomain = AppDomain.CurrentDomain;
            CurrentDomain.AssemblyResolve += new ResolveEventHandler(MyResolver);
        }


        /// <summary>
        /// Find the correct path of the openness API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly MyResolver(object sender, ResolveEventArgs args)
        {
            int index = args.Name.IndexOf(',');
            if (index == -1)
            {
                return null;
            }
            string name = args.Name.Substring(0, index) + ".dll";

            // Check for 64bit installation
            RegistryKey filePathReg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Siemens\Automation\Openness\16.0\PublicAPI\16.0.0.0");

            // Check for 32bit installation
            if (filePathReg == null)
            {
                filePathReg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Siemens\Automation\Openness\16.0\PublicAPI\16.0.0.0");
            }

            string filePath = filePathReg.GetValue("Siemens.Engineering").ToString();
            string fullPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(filePath), name));

            if (File.Exists(fullPath))
            {
                return Assembly.LoadFrom(fullPath);
            }
            return null;
        }


    }

}
