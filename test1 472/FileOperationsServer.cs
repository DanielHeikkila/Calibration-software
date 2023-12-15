using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;
using System.Reflection;
using test1_472;

namespace StartDriveParameterEditor
{
    public static class FileOperationsServer
    {
        const string XML_ROOT_NAME = "SinamicsParameterList";
        const string PARAMETERS_XPATH = "Parameter";


        /// <summary>
        /// Check if the given folder exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static bool DoesFolderExist(string path)
        {
            return Directory.Exists(path);
        }


        /// <summary>
        /// Create a folder to store parameter lists in the given path  
        /// </summary>
        /// <param name="newFolderPath"></param>
        /// <returns></returns>
        internal static bool CreateParameterListFolder(string newFolderPath)
        {
            try
            {
                Directory.CreateDirectory(newFolderPath);
                return true;
            }

            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Get all xml files in the given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static ArrayList GetParameterListFiles(string path)
        {

            ArrayList FileList = new ArrayList();
            try
            {
                foreach (string ParameterListFile in Directory.GetFiles(path, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    FileList.Add(ParameterListFile);
                }
            }

            catch
            {
            }
            return FileList;
        }


        /// <summary>
        /// Read and create a parameter list from the given file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static SinamicsParameterList GetParameterListFromXmlFile(string fileName)
        {

            SinamicsParameterList ParameterList;
            string ParameterListName;
            string ParameterListType;
            bool ParameterListIsOnline;
            string ParameterListDescription;

            string pnumber;
            string pvalue;
            string pUnit;
            string pDescription;
            string pmax;
            string pmin;
            bool pActive;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            XmlElement rootElement;
            XmlNodeList parameterlist;


            try
            {
                xmlDoc.Load(fileName);
                if (xmlDoc.DocumentElement.Name != XML_ROOT_NAME)
                    return null;
                rootElement = xmlDoc.DocumentElement;
                ParameterListName = rootElement.GetAttribute("DisplayName");
                ParameterListType = rootElement.GetAttribute("Type");
                ParameterListDescription = rootElement.GetAttribute("Description");
                if (!Boolean.TryParse(rootElement.GetAttribute("ExecuteOnline"), out ParameterListIsOnline))
                    ParameterListIsOnline = false;
                if (string.IsNullOrWhiteSpace(ParameterListName))
                {
                    ParameterListName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                    ParameterListName = ParameterListName.Substring(0, ParameterListName.LastIndexOf('.'));
                }

                ParameterList = new SinamicsParameterList(ParameterListName, fileName, ParameterListType, ParameterListIsOnline, ParameterListDescription);
                parameterlist = rootElement.SelectNodes(PARAMETERS_XPATH);
                foreach (XmlElement parameternode in parameterlist)
                {
                    pnumber = parameternode.GetAttribute("Number");
                    if (!Boolean.TryParse(parameternode.GetAttribute("Active"), out pActive))
                        pActive = false;
                    pDescription = parameternode.GetAttribute("Description");
                    pvalue = parameternode.GetAttribute("Value");
                    pUnit = parameternode.GetAttribute("Unit");
                    pmin = parameternode.GetAttribute("Min");
                    pmax = parameternode.GetAttribute("Max");

                    ParameterList.addParameter(new SinamicsParameter(pnumber, pActive, pvalue, pUnit, pDescription, pmin, pmax));
                }
                return ParameterList;


            }
            catch
            {

            }

            return null;

        }


        /// <summary>
        /// Write a parameter list to file
        /// </summary>
        /// <param name="ParameterList"></param>
        /// <param name="newFile"> if true create a new file otherwise update the existing file</param>
        /// <returns></returns>
        internal static void WriteParameterListToXmlFile(SinamicsParameterList ParameterList, bool newFile = false)
        {

            if (newFile && !File.Exists(ParameterList.Location))
            {
                FileStream f = File.Create(ParameterList.Location);
                f.Close();
            }
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.CloseOutput = true;
            settings.Indent = true;

            using (XmlWriter ParameterListWriter = XmlWriter.Create(ParameterList.Location, settings))
            {
                ParameterListWriter.WriteStartDocument();
                ParameterListWriter.WriteStartElement("", "SinamicsParameterList", "");
                ParameterListWriter.WriteAttributeString("", "DisplayName", "", ParameterList.Name);
                ParameterListWriter.WriteAttributeString("", "Description", "", ParameterList.Description);
                ParameterListWriter.WriteAttributeString("", "Type", "", ParameterList.Type);
                ParameterListWriter.WriteAttributeString("", "ExecuteOnline", "", ParameterList.ExecuteOnline.ToString());

                foreach (SinamicsParameter parameter in ParameterList.Parameters)
                {
                    ParameterListWriter.WriteStartElement("", "Parameter", "");
                    ParameterListWriter.WriteAttributeString("", "Number", "", parameter.Number);
                    ParameterListWriter.WriteAttributeString("", "Active", "", parameter.Active.ToString());
                    ParameterListWriter.WriteAttributeString("", "Description", "", parameter.Description);
                    ParameterListWriter.WriteAttributeString("", "Value", "", parameter.Value);
                    ParameterListWriter.WriteAttributeString("", "Unit", "", parameter.Unit);
                    ParameterListWriter.WriteAttributeString("", "Min", "", parameter.Minimum);
                    ParameterListWriter.WriteAttributeString("", "Max", "", parameter.Maximum);
                    ParameterListWriter.WriteEndElement();
                }

                ParameterListWriter.WriteEndDocument();
                ParameterListWriter.Close();
            }
        }


        /// <summary>
        /// create a copy of the given parameter list
        /// </summary>
        /// <param name="ParameterList"></param>
        /// <returns></returns>
        internal static SinamicsParameterList CopyParameterListToXmlFile(SinamicsParameterList ParameterList)
        {
            SinamicsParameterList CopyParameterList = ParameterList.GetCopy();
            string path = CopyParameterList.Location;
            int i = 1;
            string newPath = path;
            while (File.Exists(newPath))
            {
                newPath = path.Substring(0, path.LastIndexOf(".xml")) + i + ".xml";   ///// checks for a spot where it can create a object /////
                i++;
            }
            CopyParameterList.Location = newPath;
            WriteParameterListToXmlFile(CopyParameterList, true);
            return CopyParameterList;
        }


        /// <summary>
        /// Remove the given file from disk
        /// </summary>
        /// <param name="FilePath"></param>
        internal static void DeleteFile(string FilePath)
        {
            File.Delete(FilePath);
        }


        /// <summary>
        /// Write log to file
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="logText"></param>
        /// <param name="InstalledSoftware"></param>
        /// <returns></returns>
        internal static void ExportLog(string FileName, string logText, string InstalledSoftware)
        {

            StringReader reader = new StringReader(InstalledSoftware);
            string nextLine;
            Assembly assembly = typeof(Form1).Assembly;
            string format = "{0,-15} {1,-55} {2,-15}";
            string borderLeftRight = "==========";
            string borderUpDown = "==================================================================================\n";

            string ExportText = borderUpDown;
            ExportText += String.Format(format, borderLeftRight, assembly.GetName().Name + " LogFile", borderLeftRight) + "\n";
            ExportText += String.Format(format, borderLeftRight, "Version: " + assembly.GetName().Version, borderLeftRight) + "\n";
            ExportText += String.Format(format, borderLeftRight, "Time: " + DateTime.Now, borderLeftRight) + "\n";
            ExportText += String.Format(format, borderLeftRight, "", borderLeftRight) + "\n";

            ExportText += String.Format(format, borderLeftRight, "Installed Software: ", borderLeftRight) + "\n";
            while ((nextLine = reader.ReadLine()) != null)
                ExportText += String.Format(format, borderLeftRight, nextLine, borderLeftRight) + "\n";
            reader.Close();
            ExportText += borderUpDown;
            ExportText += logText;
            reader = new StringReader(ExportText);
            StreamWriter logger = new StreamWriter(FileName, false);
            while ((nextLine = reader.ReadLine()) != null)
                logger.WriteLine(nextLine);
            reader.Close();
            logger.Close();


        }
    }
}
