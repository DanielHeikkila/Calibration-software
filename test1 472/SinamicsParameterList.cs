using System;
using System.Collections;


namespace StartDriveParameterEditor
{

    public class SinamicsParameterList : IDisposable

    {
        private System.Windows.Forms.BindingSource parameters;
        private string name;
        private string location;
        private string type;
        private string description;
        private bool executeOnline;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="plocation"></param>
        /// <param name="ptype"></param>
        /// <param name="ponline"></param>
        /// <param name="pdescription"></param>
        public SinamicsParameterList(string pname = "newList", string plocation = "", string ptype = "S120", bool ponline = false, string pdescription = "")
        {
            parameters = new System.Windows.Forms.BindingSource();
            name = pname;
            location = plocation;
            type = ptype;
            executeOnline = ponline;
            description = pdescription;
        }




        /// <summary>
        /// Name ofthe parameterlist
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        /// <summary>
        /// path of the  parameterlist file 
        /// </summary>
        public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }


        /// <summary>
        /// Type of the list
        /// not used yet
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }


        /// <summary>
        /// Always reading or write values to the list online or offline
        /// Not used yet
        /// </summary>
        public bool ExecuteOnline
        {
            get
            {
                return executeOnline;
            }
            set
            {
                executeOnline = value;
            }
        }


        /// <summary>
        /// Description of the list
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }


        /// <summary>
        /// the parameter collection
        /// </summary>
        internal System.Windows.Forms.BindingSource Parameters
        {
            get
            {
                return parameters;
            }

        }



        /// <summary>
        /// add a new parameter to the parameterlist
        /// </summary>
        /// <param name="p"></param>
        internal void addParameter(SinamicsParameter p)
        {
            parameters.Add(p);
        }


        /// <summary>
        /// Create a copy of the parameterlist
        /// </summary>
        /// <returns></returns>
        internal SinamicsParameterList GetCopy()
        {
            SinamicsParameterList copyParameterList = new SinamicsParameterList();
            copyParameterList.name = name;
            copyParameterList.location = location;
            copyParameterList.type = type;
            copyParameterList.description = description;
            copyParameterList.executeOnline = executeOnline;
            foreach (SinamicsParameter par in parameters)
                copyParameterList.parameters.Add(new SinamicsParameter(par.Number, par.Active, par.Value, par.Unit, par.Description, par.Minimum, par.Maximum));
            return copyParameterList;
        }


        /// <summary>
        /// Dispose resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {

            if (disposing)
            {
                parameters.Dispose();
            }

        }

        /// <summary>
        /// internal Dispose called by Form
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
