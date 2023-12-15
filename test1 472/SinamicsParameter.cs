using System;

namespace StartDriveParameterEditor
{
    [Serializable]
    public class SinamicsParameter
    {
        private bool active;
        private string number;
        private string description;
        private string pvalue;
        private string min;
        private string max;
        private string unit;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pNumber"></param>
        /// <param name="pactive"></param>
        /// <param name="parValue"></param>
        /// <param name="pUnit"></param>
        /// <param name="pDescription"></param>
        /// <param name="pmin"></param>
        /// <param name="pmax"></param>
        public SinamicsParameter(string pNumber, bool pactive = true, string parValue = "", string pUnit = "", string pDescription = "", string pmin = "", string pmax = "")
        {
            number = pNumber;
            active = pactive;
            description = pDescription;
            pvalue = parValue;
            unit = pUnit;
            min = pmin;
            max = pmax;

        }




        /// <summary>
        /// Allows the user to select if the parameter schould be read or not
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        /// <summary>
        /// Parameter number rxx or pxx
        /// </summary>
        public string Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }

        }

        /// <summary>
        /// Description of the parameter
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
        /// Parameter value
        /// </summary>
        public string Value
        {
            get
            {
                return pvalue;
            }
            set
            {
                this.pvalue = value;
            }
        }

        /// <summary>
        /// parameter physical unit
        /// </summary>
        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
            }
        }

        /// <summary>
        /// Minimum value of the parameter
        /// </summary>
        public string Minimum
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }

        /// <summary>
        /// Maximum value of the parameter
        /// </summary>
        public string Maximum
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
            }
        }

    }
}