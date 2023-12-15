using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using System.Xml.Linq;
using MathProject;
using test1_472;
using test1_472.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Newtonsoft.Json;


namespace test1_472
{
    public partial class MathForm : Form
    {
        private bool dragging = false;
        private Point dragStart;

        General gen = new General();
        string LoadWheelCirc;
        public MathForm(ParametersReadSingle obje)
        {
            InitializeComponent();
            SettingDataSingle(obje);
        }
        public MathForm(ParametersReadDouble obje) 
        {
            InitializeComponent();
            SettingDataDouble(obje);
        }
        public MathForm()
        {
            InitializeComponent();
        }

        //DATA TRANSFER

        public DataToReadSingle current = new DataToReadSingle();
        public DataToReadDouble currentDouble = new DataToReadDouble();
        public memory save = new memory();
        bool Single = false;
        bool Double = false;
        string driveName = "default";

        private void SettingDataSingle(ParametersReadSingle obje)
        {
            try
            {
                driveName = obje.projectName.ToString() + obje.PositionNumber.ToString();
            }
            catch
            {

            }
            if (File.Exists(".\\" + driveName + ".json"))
            {
                save = RestoreSettings(".\\" + driveName + ".json");
                try
                {
                    TBp408_0.Text = save.p408v0;
                    TBp421_0.Text = save.p421v0;
                    TBp423_0.Text = save.p423v0;
                    TBDrivePitchLoadWheelDiameterMM.Text = save.DPLWheelDiameter;
                    TBDriveLoadWheelCircumference.Text = save.mmPLWRevolution;
                    TBLUPerMeasuringUnit.Text = save.LUPerMeasuringUnit;
                    TBMaxPositioningSpdMMmin.Text = save.MaxPosSpdMmPerMin;
                    TBAccelTimeS.Text = save.AccelTimeS;
                    TBDecelTimeS.Text = save.DecelTimeS;
                    TBjogTest.Text = save.TestPercentage;
                    TBLimSwitchPlusMM.Text = save.TBLimSwitchPlusMM;
                    TBSWLimSwitchMinusMM.Text = save.TBSWLimSwitchMinusMM;
                    TBjog1.Text = save.TBjog1;
                    TBjog2.Text = save.TBjog2;
                }
                catch
                {

                }
            }
            foreach (var PVN in obje.pValNams1)
            {
                try
                {
                    switch (PVN.PNam)
                    {
                        case "p2504[0]":
                            TBp2504.Text = PVN.PVal;
                            break;
                        case "p2505[0]":
                            TBp2505.Text = PVN.PVal;
                            break;
                        case "p1082[0]":
                            TBp1082.Text = PVN.PVal;
                            break;
                        case "p2000":
                            TBp2000.Text = PVN.PVal;
                            break;
                        case "p1121[0]":
                            TBp1121.Text = PVN.PVal;
                            break;
                        case "p1135[0]":
                            TBp1135.Text = PVN.PVal;
                            break;
                        case "p2506[0]":
                            TBp2506.Text = PVN.PVal;
                            break;
                        case "p2503[0]":
                            TBp2503.Text = PVN.PVal;
                            break;
                        case "r2524":
                            TBr2524.Text = PVN.PVal;
                            break;
                        case "p2571":
                            TBp2571.Text = PVN.PVal;
                            break;
                        case "p2572":
                            TBp2572.Text = PVN.PVal;
                            break;
                        case "p2573":
                            TBp2573.Text = PVN.PVal;
                            break;
                        case "p2580":
                            TBp2580.Text = PVN.PVal;
                            break;
                        case "p2581":
                            TBp2581.Text = PVN.PVal;
                            break;
                        case "p2585":
                            TBp2585.Text = PVN.PVal;
                            break;
                        case "p2586":
                            TBp2586.Text = PVN.PVal;
                            break;
                    }
                }
                catch
                {
                }
            }
            try
                {
                    switch (obje.encoder.ToString())
                    {
                        case "No encoder":
                            comboBox1.SelectedIndex = 0;
                            break;
                        case "Siemens absolute":
                            comboBox1.SelectedIndex = 1;
                            TBp408_0.Text = "512";
                            TBp421_0.Text = "4096";
                            break;
                        case "Rotary HTL/TTL":
                            comboBox1.SelectedIndex = 2;
                            break;
                        case "Linear":
                            comboBox1.SelectedIndex = 3;
                            break;
                    }
                }
                catch
                {
                }
            foreach (var PVN in obje.pValNams1)
            {
                try
                {
                    switch (PVN.PNam)
                    {
                        case "p418[0]":
                            TBp418_0.Text = PVN.PVal;
                            break;
                        case "p419[0]":
                            TBp419_0.Text = PVN.PVal;
                            break;
                    }
                }
                catch 
                { 
                }
            }
                richTextBox2.Text = obje.axis;
                Single = true;
            foreach (PValNam PVN in obje.pValNams1)
            {
                dataGridViewParameters1.Rows.Add(PVN.PNam, PVN.PVal);
            }
        }

        private void SettingDataDouble(ParametersReadDouble obje)
        {
            try
            {
                driveName = obje.PositionNumber.ToString() + obje.PositionNumber.ToString();
            }
            catch
            {

            }
            if (File.Exists(".\\" + driveName + ".json"))
            {
                save = RestoreSettings(".\\" + driveName + ".json");

                TBp408_0.Text = save.p408v0;
                TBp408_1.Text = save.p408v1;
                TBp421_0.Text = save.p421v0;
                TBp421_1.Text = save.p421v1;
                TBp423_1.Text = save.p423v1;
                TBp423_0.Text = save.p423v0;
                TBDrivePitchLoadWheelDiameterMM.Text = save.DPLWheelDiameter;
                TBDriveLoadWheelCircumference.Text = save.mmPLWRevolution;
                TBLUPerMeasuringUnit.Text = save.LUPerMeasuringUnit;
                TBMaxPositioningSpdMMmin.Text = save.MaxPosSpdMmPerMin;
                TBAccelTimeS.Text = save.AccelTimeS;
                TBDecelTimeS.Text = save.DecelTimeS;
                TBp407_0.Text = save.p407v0;
                TBp407_1.Text = save.p407v1;
                TBjogTest.Text = save.TestPercentage;
                TBLimSwitchPlusMM.Text = save.TBLimSwitchPlusMM;
                TBSWLimSwitchMinusMM.Text = save.TBSWLimSwitchMinusMM;
                TBjog1.Text = save.TBjog1;
                TBjog2.Text = save.TBjog2;
            }
            foreach (var PVN in obje.pValNams1)
            {
                try
                {
                    switch (PVN.PNam)
                    {
                        case "p2504[0]":
                            TBp2504.Text = PVN.PVal;
                            break;
                        case "p2505[0]":
                            TBp2505.Text = PVN.PVal;
                            break;
                        case "p1082[0]":
                            TBp1082.Text = PVN.PVal;
                            break;
                        case "p2000":
                            TBp2000.Text = PVN.PVal;
                            break;
                        case "p1121[0]":
                            TBp1121.Text = PVN.PVal;
                            break;
                        case "p1135[0]":
                            TBp1135.Text = PVN.PVal;
                            break;
                        case "p2506[0]":
                            TBp2506.Text = PVN.PVal;
                            break;
                        case "p2503[0]":
                            TBp2503.Text = PVN.PVal;
                            break;
                        case "r2524":
                            TBr2524.Text = PVN.PVal;
                            break;
                        case "p2571":
                            TBp2571.Text = PVN.PVal;
                            break;
                        case "p2572":
                            TBp2572.Text = PVN.PVal;
                            break;
                        case "p2573":
                            TBp2573.Text = PVN.PVal;
                            break;
                        case "p2580":
                            TBp2580.Text = PVN.PVal;
                            break;
                        case "p2581":
                            TBp2581.Text = PVN.PVal;
                            break;
                        case "p2585":
                            TBp2585.Text = PVN.PVal;
                            break;
                        case "p2586":
                            TBp2586.Text = PVN.PVal;
                            break;
                    }
                }
                catch 
                { 
                }
            }
            try
                {
                    switch (obje.encoderv0.ToString())
                    {
                        case "No encoder":
                            comboBox1.SelectedIndex = 0;
                            break;
                        case "Siemens absolute":
                            comboBox1.SelectedIndex = 1;
                            TBp408_0.Text = "512";
                            TBp421_0.Text = "4096";
                            break;
                        case "Rotary HTL/TTL":
                            comboBox1.SelectedIndex = 2;
                            break;
                        case "Linear":
                            comboBox1.SelectedIndex = 3;
                            break;
                    }
                    switch (obje.encoderv1.ToString())
                    {
                        case "No encoder":
                            comboBox2.SelectedIndex = 0;
                            break;
                        case "Siemens absolute":
                            comboBox2.SelectedIndex = 1;
                            TBp408_0.Text = "512";
                            TBp421_0.Text = "4096";
                            break;
                        case "Rotary HTL/TTL":
                            comboBox2.SelectedIndex = 2;
                            break;
                        case "Linear":
                            comboBox2.SelectedIndex = 3;
                            break;
                    }
                }
                catch
                {

            }
            foreach (var PVN in obje.pValNams1)
            {
                try
                {
                    switch (PVN.PNam)
                    {
                        case "p418[0]":
                            TBp418_0.Text = PVN.PVal;   
                            break;
                        case "p419[0]":
                            TBp419_0.Text = PVN.PVal;
                            break;
                        case "418[1]":
                            TBp418_1.Text = PVN.PVal;
                            break;
                        case "419[1]":
                            TBp419_1.Text = PVN.PVal;
                            break;
                    }
                }
                catch
                {
                }
            }
            richTextBox2.Text = obje.axis;
            Double = true;
            foreach (PValNam PVN in obje.pValNams1)
            {
                dataGridViewParameters1.Rows.Add(PVN.PNam, PVN.PVal);
            }
        }

        //TOOLSTRIP CONTROLS

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem toolstripmenuitem = sender as ToolStripMenuItem;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
            panel7.Visible = true;
            dataGridViewParameters1.Visible = false;
            switch (toolstripmenuitem.Name) //This switch makes the selected page visible and interactable and disables other pages
            {
                case "toolStripMenuItem1":
                    panel1.Visible = true;
                    break;
                case "toolStripMenuItem2":
                    panel2.Visible = true;
                    updateValue(TBLUPerMeasuringUnit);
                    var tuple = gen.Math(TBp2505.Name, TBp2504.Text, TBGearboxRatio.Name, TBGearboxRatio.Text, TBp2505.Name, TBp2505.Text, "", "", "", "");
                    if (tuple.Item1 != "")                                                                                                                 
                        TBGearboxRatio.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple.Item1), 3));                                       
                    break;
                case "toolStripMenuItem3":
                    panel3.Visible = true;
                    updateValue(TBAccelTimeS);
                    updateValue(TBDecelTimeS);
                    updateValue(TBMaxPositioningSpdMMmin);
                    updateValue(TBp1121);
                    updateValue(TBp1135);
                    break;
                case "toolStripMenuItem4":
                    panel4.Visible = true;
                    updateValue(TBLimSwitchPlusMM);
                    updateValue(TBjogTest);
                    break;
                case "toolStripMenuItem5":
                    panel5.Visible = true;
                    if (gen.NecessaryChecks(TBDriveLoadWheelCircumference.Text) && gen.NecessaryChecks(LoadWheelCirc))
                        if (Convert.ToString(System.Math.Round(Convert.ToDouble(LoadWheelCirc), 3)) != TBDriveLoadWheelCircumference.Text)
                            LoadWheelCirc = TBDriveLoadWheelCircumference.Text;
                    updateValue(TBPhysicalSpd);
                    updateValue(TBPhysicalPositionMM);
                    updateValue(TBPhysicalEndPosition);
                    updateValue(TBDriveEndPositionLU);
                    break;
                case "toolStripMenuItem6":
                    panel6.Visible = true;
                    dataGridViewParameters1.Visible = true;
                    break;
            }
        }

        //COMBOBOX CONTROLS

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //This selects which parameters are equal to what based on the selected encoder
        {                                                                       //It also shows/hides the resolution textbox based on whether or not the linear encoder is selected.
            System.Windows.Forms.ComboBox combobox = sender as System.Windows.Forms.ComboBox;
            switch (combobox.Name)
            {
                case "comboBox2":
                    switch (combobox.SelectedIndex)
                    {
                        case 0:
                        case 2:
                        case 1:
                        case 3:
                            var tuple1 = gen.Math(combobox.Name, Convert.ToString(combobox.SelectedIndex), TBp408_1.Name, TBp408_1.Text, TBp421_1.Name, TBp421_1.Text, TBEncoder1TotalNumOfBits.Name, TBEncoder1TotalNumOfBits.Text, TBp423_1.Name, TBp423_1.Text);
                            TBp418_1.Text = tuple1.Item1;
                            TBp419_1.Text = tuple1.Item2;
                            TBp421_1.Text = tuple1.Item3;
                            TBp408_1.Text = tuple1.Item4;
                            if (combobox.SelectedIndex == 3)
                            {
                                TBp407_1.Visible = true;
                                label128.Visible = true;
                                label129.Visible = true;
                                label130.Visible = true;
                                TBResolutionMM1.Visible = true;
                                //TBp421_1.Enabled = false;
                                //TBp421_1.Text = "";
                            }
                            else
                            {
                                TBp407_1.Visible = false;
                                label128.Visible = false;
                                label129.Visible = false;
                                label130.Visible = false;
                                TBResolutionMM1.Visible = false;
                            }
                            break;
                    }
                    break;
                case "comboBox1":
                    switch (combobox.SelectedIndex)
                    {
                        case 0:
                        case 2:
                        case 1:
                        case 3:
                            var tuple1 = gen.Math(combobox.Name, Convert.ToString(combobox.SelectedIndex), TBp408_0.Name, TBp408_0.Text, TBp421_0.Name, TBp421_0.Text, TBEncoder0TotalNumOfBits.Name, TBEncoder0TotalNumOfBits.Text, TBp423_0.Name, TBp423_0.Text);
                            TBp418_0.Text = tuple1.Item1;
                            TBp419_0.Text = tuple1.Item2;
                            TBp421_0.Text = tuple1.Item3;
                            TBp408_0.Text = tuple1.Item4;
                            if (combobox.SelectedIndex == 3)
                            {
                                TBp407_0.Visible = true;
                                label125.Visible = true;
                                label126.Visible = true;
                                label127.Visible = true;
                                TBResolutionMM.Visible = true;
                                TBp421_0.Enabled = false;
                                TBp421_0.Text = "";
                            }
                            else
                            {
                                TBp407_0.Visible = false;
                                label125.Visible = false;
                                label126.Visible = false;
                                label127.Visible = false;
                                TBResolutionMM.Visible = false;
                            }
                            break;
                    }
                    break;
            }
        }

        //BUTTON CONTROLS

        private void button1_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Button button = sender as System.Windows.Forms.Button;
            switch (button.Name)
            {
                case "button1":
                    TBEPPPosSWLimitMax.Text = TBLimSwitchPlusMM.Text;
                    TBEPPPosSWLimitMin.Text = TBSWLimSwitchMinusMM.Text;
                    TBEPPSpeedMax.Text = TBMaxPositioningSpdMMmin.Text;
                    TBEPPPosEngUnitsLU.Text = TBLUPerMeasuringUnit.Text;
                    if (General.IsDoubl(TBp2000MaxMM.Text) && TBp2000MaxMM.Text != "" || General.IsDoubl(TBp2000MaxMM.Text.ToString(CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name))) && TBp2000MaxMM.Text != "" && TBp2000MaxMM.Text != "0")
                        TBEPPSpdActSpdConstant.Text = Convert.ToString(System.Math.Round(1073741824 / Convert.ToDouble(TBp2000MaxMM.Text), 3));
                    break;
                case "button2":
                    if (TBDriveLoadWheelCircumference.Text != TBDLWheelCircumference.Text)
                        TBDriveLoadWheelCircumference.Text = TBDLWheelCircumference.Text;
                    break;
            }
        }

        //TEXTBOX CONTROLS
        public void checkBox() //This checks for Siemens or Rotary encoder
        {
            if (comboBox1.Text == "Siemens absolute")
            {
                TBp418_0.Text = "11";
                TBp419_0.Text = "9";
            }
            else if (comboBox2.Text == "Siemens absolute")
            {
                TBp418_1.Text = "11";
                TBp419_1.Text = "9";
            }
            if (comboBox1.Text == "Rotary HTL/TTL")
                TBp418_0.Text = TBp419_0.Text = "2";
            else if (comboBox2.Text == "Rotary HTL/TTL")
                TBp418_1.Text = TBp419_1.Text = "2";
        }
        public void checkgearbox() //This checks if gearbox ratio falls below 0.03 (must not happen)
        {
            try
            {
                if (General.IsDoubl(TBGearboxRatio.Text))
                    if (Convert.ToDouble(TBGearboxRatio.Text) < 0.03 && TBGearboxRatio.Text != "0" && TBGearboxRatio.Text != "0," && TBGearboxRatio.Text != "0." && TBGearboxRatio.Text != "0,0" && TBGearboxRatio.Text != "0.0" || TBGearboxRatio.Text == "0,02" || TBGearboxRatio.Text == "0,01" || TBGearboxRatio.Text == "0.02" || TBGearboxRatio.Text == "0.01")
                    {
                        TBGearboxRatio.Text = "0.03";
                        TBp2505.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(TBp2504.Text) / Convert.ToDouble(TBGearboxRatio.Text), 3));
                    }
                    else if (General.IsDoubl(TBGearboxRatio.Text.ToString(CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name))))
                        if (Convert.ToDouble(TBGearboxRatio.Text) < 0.03 && TBGearboxRatio.Text != "0" && TBGearboxRatio.Text != "0," && TBGearboxRatio.Text != "0." && TBGearboxRatio.Text != "0,0" && TBGearboxRatio.Text != "0.0" || TBGearboxRatio.Text == "0,02" || TBGearboxRatio.Text == "0,01" || TBGearboxRatio.Text == "0.02" || TBGearboxRatio.Text == "0.01")
                        {
                            TBGearboxRatio.Text = "0.03";
                            TBp2505.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(TBp2504.Text) / Convert.ToDouble(TBGearboxRatio.Text), 3));
                        }
            }
            catch
            {
            }
        }
        public void updateValue(System.Windows.Forms.TextBox sender)
        {
            string tp = sender.Text;  //This
            sender.Text = "0";        //Updates these values to 
            sender.Text = tp;         //trigger case "sender"
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox textbox = sender as System.Windows.Forms.TextBox;

            int currentCaretPosition = textbox.SelectionStart;
            if (textbox.Text.Contains(","))
            {
                string t = textbox.Text;
                textbox.Text = t.Replace(",", ".");
            }
            textbox.SelectionStart = currentCaretPosition;
            textbox.SelectionLength = 0;

            switch (textbox.Name)
            {
                /////////////////////////////////PAGE 1//////////////////////////////////////
                case "TBp408_0": //
                case "TBp408_1": //This case simply calculates
                case "TBp421_0": //the according bit amounts
                case "TBp421_1": //Math case is : "TBp408_0NumberOfBits"
                    var tuple = gen.Math(TBp408_0NumberOfBits.Name, TBp408_0.Text, TBp408_1NumberOfBits.Name, TBp408_1.Text, TBp421_0NumberOfBits.Name, TBp421_0.Text, TBp421_1NumberOfBits.Name, TBp421_1.Text, "", "");
                    if (tuple.Item1 != "" && textbox.Name == TBp408_0.Name)
                        TBp408_0NumberOfBits.Text = tuple.Item1;
                    else if (tuple.Item2 != "" && textbox.Name == TBp408_1.Name)
                        TBp408_1NumberOfBits.Text = tuple.Item2;
                    else if (tuple.Item3 != "" && textbox.Name == TBp421_0.Name)
                        TBp421_0NumberOfBits.Text = tuple.Item3;
                    else if (tuple.Item4 != "" && textbox.Name == TBp421_1.Name)
                        TBp421_1NumberOfBits.Text = tuple.Item4;
                    updateValue(TBp418_0); //These just 
                    updateValue(TBp418_1); //update the values
                    break;
                case "TBp408_0NumberOfBits": //
                case "TBp408_1NumberOfBits": //This case First just adds up bits
                case "TBp421_0NumberOfBits": //then adjusts p418 and p419 if needed
                case "TBp421_1NumberOfBits": //Math case is : "TBEncoder0TotalNumOfBits"
                    var tuple1 = gen.Math(TBEncoder0TotalNumOfBits.Name, TBp408_0NumberOfBits.Text, TBEncoder1TotalNumOfBits.Name, TBp421_0NumberOfBits.Text, "", TBp408_1NumberOfBits.Text, "", TBp421_1NumberOfBits.Text, "", "");
                    //First just adds up bits here
                    if (tuple1.Item1 != "" && textbox.Name == TBp408_0NumberOfBits.Name || tuple1.Item1 != "" && textbox.Name == TBp421_0NumberOfBits.Name)
                        TBEncoder0TotalNumOfBits.Text = tuple1.Item1;
                    if (tuple1.Item2 != "" && textbox.Name == TBp408_1NumberOfBits.Name || tuple1.Item2 != "" && textbox.Name == TBp421_1NumberOfBits.Name)
                        TBEncoder1TotalNumOfBits.Text = tuple1.Item2;
                    //then adjusts p418 and p419 if needed here
                    if (comboBox2.Text != "Siemens absolute" && comboBox2.Text != "Rotary HTL/TTL" && gen.NecessaryChecks(TBEncoder0TotalNumOfBits.Text))
                        TBp418_0.Text = TBp419_0.Text = Convert.ToString(32 - Convert.ToDouble(TBEncoder0TotalNumOfBits.Text));
                    if (comboBox2.Text != "Siemens absolute" && comboBox2.Text != "Rotary HTL/TTL" && gen.NecessaryChecks(TBEncoder1TotalNumOfBits.Text))
                        TBp418_1.Text = TBp419_1.Text = Convert.ToString(32 - Convert.ToDouble(TBEncoder1TotalNumOfBits.Text));
                    checkBox(); //This checks for Siemens or Rotary encoder
                    break;
                case "TBp418_0": //This calculates 423 from p418 and p418NumberOfBits Math case is : "TBp423_0"
                case "TBp418_1": //
                case "TBResolutionMM":  //This calculates p407 by multiplying resolution by 1M
                case "TBResolutionMM1": //
                case "TBp419_0":
                case "TBp419_1":
                    var tuple2 = gen.Math(TBp423_0.Name, TBp408_0NumberOfBits.Text, TBp423_1.Name, TBp418_0.Text, "", TBp408_1NumberOfBits.Text, "", TBp418_1.Text, "", "");
                    if (tuple2.Item1 != "" && textbox.Name == TBp418_0.Name)
                        TBp423_0.Text = tuple2.Item1;
                    else if (tuple2.Item1 != "" && textbox.Name == TBp418_1.Name)
                        TBp423_1.Text = tuple2.Item2;
                    if (gen.NecessaryChecks(TBResolutionMM.Text) && textbox.Name == TBResolutionMM.Name)
                        TBp407_0.Text = Convert.ToString(Convert.ToDouble(TBResolutionMM.Text) * 1000000);
                    if (gen.NecessaryChecks(TBResolutionMM1.Text) && textbox.Name == TBResolutionMM1.Name)
                        TBp407_1.Text = Convert.ToString(Convert.ToDouble(TBResolutionMM1.Text) * 1000000);
                    checkBox();
                    updateValue(TBp2503);
                    break;
                /////////////////////////////////PAGE 2//////////////////////////////////////
                case "TBp2504": //This calculates Gearbox Ratio Math case is "TBGearBoxRatio"
                case "TBp2505": //
                case "TBGearboxRatio": //This calculates p2505 when Gearbox Ratio is changed manually
                    var tuple3 = gen.Math(TBp2505.Name, TBp2504.Text, TBGearboxRatio.Name, TBGearboxRatio.Text, TBp2505.Name, TBp2505.Text, "", "", "", "");
                    if ((textbox as Control).Focused && gen.NecessaryChecks(textbox.Text) && textbox.Name != TBGearboxRatio.Name)
                    {
                        if (tuple3.Item1 != "")
                            TBGearboxRatio.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple3.Item1), 3));
                    }
                    else if ((textbox as Control).Focused && gen.NecessaryChecks(textbox.Text) && textbox.Name == TBGearboxRatio.Name)
                        if (tuple3.Item2 != "")
                            TBp2505.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple3.Item2), 3));

                    checkgearbox(); //This checks if gearbox ratio falls below 0.03 (must not happen)
                    updateValue(TBLUPerMeasuringUnit);
                    break;
                case "TBDriveLoadWheelCircumference": //These calculate TBp2506, r2524,
                case "TBp2506":                       //TBMMPerRevolution, TBDrivePitchLoadWheelDiameterMM,
                case "TBLUPerMeasuringUnit":          //TBp2503, TBp2506Max and max travel distance. Math case is "TBUnitLengthPositionLU".
                case "TBDrivePitchLoadWheelDiameterMM": //Only affects TBDriveLoadWheelCircumference
                    var tuple4 = gen.Math(TBp2506Max.Name, TBDriveLoadWheelCircumference.Text, TBr2524.Name, TBLUPerMeasuringUnit.Text, TBp2506.Name, TBGearboxRatio.Text, TBp2506Max.Name, TBp423_0.Text, "", TBDrivePitchLoadWheelDiameterMM.Text);
                    if (gen.NecessaryChecks(textbox.Text) || gen.NecessaryChecks(textbox.Text))
                    {
                        if (tuple4.Item1 != "")
                            TBp2506.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple4.Item1), 3));
                        if (tuple4.Item2 != "")
                            TBr2524.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple4.Item2), 3));
                        if (gen.NecessaryChecks(tuple4.Item3))
                            TBMMPerRevolution.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple4.Item3), 3));
                        if ((textbox as Control).Focused && gen.NecessaryChecks(textbox.Text) && gen.NecessaryChecks(TBDriveLoadWheelCircumference.Text) && textbox.Name == TBDriveLoadWheelCircumference.Name)
                            TBDrivePitchLoadWheelDiameterMM.Text = Convert.ToString(Math.Round((Convert.ToDouble(TBDriveLoadWheelCircumference.Text) / System.Math.PI), 3));
                        if (gen.NecessaryChecks(TBLUPerMeasuringUnit.Text))
                            TBp2503.Text = Convert.ToString(Convert.ToDouble(TBLUPerMeasuringUnit.Text) * 10);
                        if (gen.NecessaryChecks(TBGearboxRatio.Text) && gen.NecessaryChecks(TBp423_0.Text) && gen.NecessaryChecks(tuple4.Item4))
                            TBp2506Max.Text = tuple4.Item4;
                        if (gen.NecessaryChecks(TBLUPerMeasuringUnit.Text) && textbox.Name == TBLUPerMeasuringUnit.Name)
                            TBMaxTravelDistance.Text = Convert.ToString(System.Math.Round(2147483647 / Convert.ToDouble(TBLUPerMeasuringUnit.Text), 1));
                        if ((textbox as Control).Focused && gen.NecessaryChecks(textbox.Text) && gen.NecessaryChecks(TBDrivePitchLoadWheelDiameterMM.Text) && textbox.Name == TBDrivePitchLoadWheelDiameterMM.Name)
                            TBDriveLoadWheelCircumference.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(TBDrivePitchLoadWheelDiameterMM.Text) * System.Math.PI, 3));
                    }
                    updateValue(TBp2000);
                    break;
                case "TBp1082": //This calculates TBp1082 and TBp2000
                case "TBp2000": //max MM and max LU respectively Math case "TBp1082MaxMM"
                    var tuple5 = gen.Math(TBp2000MaxMM.Name, TBp2000.Text, TBp2000MaxLU.Name, TBr2524.Text, TBp1082MaxMM.Name, TBLUPerMeasuringUnit.Text, TBp1082MaxLU.Name, TBp1082.Text, "", "");
                    if (gen.NecessaryChecks(tuple5.Item1))
                        TBp1082MaxMM.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple5.Item1), 2));
                    if (gen.NecessaryChecks(tuple5.Item2))
                        TBp1082MaxLU.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple5.Item2), 2));
                    if (gen.NecessaryChecks(tuple5.Item3))
                        TBp2000MaxMM.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple5.Item3), 2));
                    if (gen.NecessaryChecks(tuple5.Item4))
                        TBp2000MaxLU.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple5.Item4), 2));
                    break;
                case "TBp2503": //This linear encoder check only activates if you have linear encoder as your seconde encoder (also needs resolution to be > 0) Math case is "TBLinearEncoderCheck"
                    var tuple6 = gen.Math(TBLinearEncoderCheck.Name, TBp407_1.Text, "", TBp2503.Text, "", TBp419_1.Text, "", "", "", "");
                    if (comboBox2.SelectedIndex == 3 && gen.NecessaryChecks(TBp407_1.Text) && textbox.Name == TBp2503.Name && gen.NecessaryChecks(tuple6.Item1))
                        TBLinearEncoderCheck.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple6.Item1), 3));
                    if (gen.NecessaryChecks(TBLinearEncoderCheck.Text) && Convert.ToDouble(TBLinearEncoderCheck.Text) > 1 && TBLinearEncoderCheck.Text != "")
                    {
                        TBLinearEncoderCheck.BackColor = Color.FromArgb(255, 192, 192);
                        labelWarning.Visible = true;
                    }
                    else
                    {
                        TBLinearEncoderCheck.BackColor = SystemColors.ScrollBar;
                        labelWarning.Visible = false;
                    }
                    break;
                /////////////////////////////////PAGE 3//////////////////////////////////////
                case "TBMaxPositioningSpdMMmin": //This together with the case "TBAccelTimeS" calculates everything related to "TBp2571" Mat hcase is "TBp2571" respectively
                    var tuple7 = gen.Math(TBp2571.Name, TBMaxPositioningSpdMMmin.Text, TBp2571_CorrespondsToMs.Name, TBLUPerMeasuringUnit.Text, TBp2571_Correspondsto1MIN.Name, TBr2524.Text, TBDelayRelativeTo_p1082.Name, TBp1082.Text, "", TBp2573.Text);
                    if (gen.NecessaryChecks(tuple7.Item1))
                        TBp2571.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple7.Item1), 3));
                    if (gen.NecessaryChecks(tuple7.Item2))
                        TBp2571_CorrespondsToMs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple7.Item2), 3));
                    if (gen.NecessaryChecks(tuple7.Item3))
                        TBp2571_Correspondsto1MIN.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple7.Item3), 3));
                    if (gen.NecessaryChecks(tuple7.Item4))
                        TBDelayRelativeTo_p1082.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple7.Item4), 3));
                    var tuple8 = gen.Math(TBAsPercentOfRefSpd.Name, TBp2571_Correspondsto1MIN.Text, "", TBp2000.Text, "", "", "", "", "", ""); //This part updates TBAsPercentOfRefSpd using a different Math case "TBAsPercentOfRefSpd"
                    if (gen.NecessaryChecks(tuple8.Item1))
                        TBAsPercentOfRefSpd.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple8.Item1), 3));
                    updateValue(TBp1121);
                    updateValue(TBp1135);
                    break;
                case "TBAccelTimeS": //These two calculate everything related to acceleration and deceleration time
                case "TBDecelTimeS": //Math case is "TBAsPercentOfRefSpd"
                    var tuple9 = gen.Math(TBAsPercentOfRefSpd.Name, TBp2571_Correspondsto1MIN.Text, "", TBp2000.Text, TBp2572.Name, TBAccelTimeS.Text, TBp2572_CorrespondsToMMs.Name, TBDecelTimeS.Text, TBLUPerMeasuringUnit.Text, TBp2571.Text);
                    if (gen.NecessaryChecks(tuple9.Item2))
                        TBp2572.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple9.Item2)));
                    if (gen.NecessaryChecks(tuple9.Item3) && gen.NecessaryChecks(tuple9.Item2))
                        TBp2572_CorrespondsToMMs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple9.Item3), 1));
                    if (gen.NecessaryChecks(tuple9.Item4))
                        TBp2573.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple9.Item4)));
                    if (gen.NecessaryChecks(tuple9.Item5) && gen.NecessaryChecks(tuple9.Item4))
                        TBp2573_CorrespondsToMMs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple9.Item5), 1));
                    var tuple10 = gen.Math(TBp2571.Name, "", "", "", "", TBr2524.Text, TBDelayRelativeTo_p1082.Name, TBp1082.Text, "", TBp2573.Text); //This here updates TBDelayRelativeTo_p1082 Math case "TBp2571"
                    if (gen.NecessaryChecks(tuple10.Item4))
                        TBDelayRelativeTo_p1082.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple10.Item4), 3));
                    break;
                case "TBp1121": //These calculate wverything related to ramp-up and ramp-down time
                case "TBp1135": //Math case is "TBp1121"
                    var tuple11 = gen.Math(textbox.Name, textbox.Text, TBp1135_CorrespondsTo1000LUs.Name, TBp1082.Text, TBp1135_CorrespondsToMMs.Name, TBr2524.Text, TBp1121_StopTimeFromMaxVelocity.Name, TBp2571.Text, "", TBLUPerMeasuringUnit.Text);
                    if (textbox.Name == "TBp1121" && gen.NecessaryChecks(textbox.Text)) //TBp1121
                    {
                        if (gen.NecessaryChecks(tuple11.Item1))
                            TBp1121_CorrespondsTo1000LUs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple11.Item1), 3));
                        if (gen.NecessaryChecks(tuple11.Item2))
                            TBp1121_CorrespondsToMMs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple11.Item2), 3));
                        if (gen.NecessaryChecks(tuple11.Item3))
                            TBp1121_StopTimeFromMaxVelocity.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple11.Item3), 3));
                    }
                    if (textbox.Name == "TBp1135" && gen.NecessaryChecks(textbox.Text)) //TBp1135
                    {
                        if (gen.NecessaryChecks(tuple11.Item1))
                            TBp1135_CorrespondsTo1000LUs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple11.Item1), 3));
                        if (gen.NecessaryChecks(tuple11.Item2))
                            TBp1135_CorrespondsToMMs.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple11.Item2), 3));
                        if (gen.NecessaryChecks(tuple11.Item3))
                            TBp1135_StopTimeFromMaxVelocity.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple11.Item3), 3));
                    }
                    break;
                /////////////////////////////////PAGE 4//////////////////////////////////////
                case "TBLimSwitchPlusMM":    //These just calculate LU values 
                case "TBSWLimSwitchMinusMM": //for TBLimSwitchPlusMM and TBSWLimSwitchMinusMM.
                case "TBjog1":               //And 1000LU/Min for TBjog1 and TBjog2
                case "TBjog2":               // Math case is "TBp2581"
                    var tuple22 = gen.Math(TBp2581.Name, TBLimSwitchPlusMM.Text, TBp2580.Name, TBLUPerMeasuringUnit.Text, TBp2585.Name, TBSWLimSwitchMinusMM.Text, TBp2586.Name, TBjog1.Text, "", TBjog2.Text);
                    if (gen.NecessaryChecks(tuple22.Item1))
                        TBp2581.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple22.Item1), 3));
                    if (gen.NecessaryChecks(tuple22.Item2))
                        TBp2580.Text = tuple22.Item2;
                    if (gen.NecessaryChecks(tuple22.Item3))
                        TBp2585.Text = tuple22.Item3;
                    if (gen.NecessaryChecks(tuple22.Item4))
                        TBp2586.Text = tuple22.Item4;
                    break;
                case "TBjogTest": //Here you input a percentage 0-199 and according to previous parameters on this test page it calculates ramp-up and ramp-down time for that percentage.
                    var tuple23 = gen.Math(TBjogMM.Name, textbox.Text, TBR_UPTimeJogS.Name, TBLUPerMeasuringUnit.Text, TBR_DownTimeJogS.Name, TBp2585.Text, "", TBp2572.Text, "", TBp2573.Text);
                    if (gen.NecessaryChecks(tuple23.Item1))
                        TBjogMM.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple23.Item1), 3));
                    if (gen.NecessaryChecks(tuple23.Item2))
                        TBR_UPTimeJogS.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple23.Item2), 3));
                    if (gen.NecessaryChecks(tuple23.Item3))
                        TBR_DownTimeJogS.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple23.Item3), 3));
                    break;
                /////////////////////////////////PAGE 5//////////////////////////////////////
                case "TBPhysicalPositionMM":   //These two just transform eachother 
                case "TBUnitLengthPositionLU": //from LU to mm and back Math case is "TBPhysicalPositionMM"
                    var tuple24 = gen.Math(textbox.Name, textbox.Text, TBUnitLengthPositionLU.Name, TBLUPerMeasuringUnit.Text, TBPhysicalPositionMM.Name, "", "", "", "", "");
                    if (gen.NecessaryChecks(tuple24.Item1) && (TBPhysicalPositionMM as Control).Focused && sender == TBPhysicalPositionMM)
                        TBUnitLengthPositionLU.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple24.Item1), 3));
                    if (gen.NecessaryChecks(tuple24.Item2) && (TBUnitLengthPositionLU as Control).Focused && sender == TBUnitLengthPositionLU)
                        TBPhysicalPositionMM.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple24.Item2), 3));
                    break;
                case "TBPhysicalSpd":              //These three are interdependent
                case "TBUnitLengthSpeed1000LUmin": //so they all calculate each other 
                case "TBServoSpdInRPM":            //Math case is "TBPhysicalSpd"
                    var tuple25 = gen.Math(textbox.Name, TBServoSpdInRPM.Text, "", TBLUPerMeasuringUnit.Text, "", TBr2524.Text, TBServoSpdInRPM.Name, TBPhysicalSpd.Text, TBUnitLengthSpeed1000LUmin.Name, TBUnitLengthSpeed1000LUmin.Text);
                    if (gen.NecessaryChecks(tuple25.Item1) && (TBUnitLengthSpeed1000LUmin as Control).Focused && sender == TBUnitLengthSpeed1000LUmin)
                        TBPhysicalSpd.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple25.Item1), 3));
                    if (gen.NecessaryChecks(tuple25.Item1) && (TBServoSpdInRPM as Control).Focused && sender == TBServoSpdInRPM)
                        TBPhysicalSpd.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple25.Item1), 3));
                    if (gen.NecessaryChecks(tuple25.Item2) && (TBPhysicalSpd as Control).Focused && sender == TBPhysicalSpd)
                        TBServoSpdInRPM.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple25.Item2), 3));
                    if (gen.NecessaryChecks(tuple25.Item2) && (TBUnitLengthSpeed1000LUmin as Control).Focused && sender == TBUnitLengthSpeed1000LUmin)
                        TBServoSpdInRPM.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple25.Item2), 3));
                    if (gen.NecessaryChecks(tuple25.Item3) && (TBPhysicalSpd as Control).Focused && sender == TBPhysicalSpd)
                        TBUnitLengthSpeed1000LUmin.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple25.Item3), 3));
                    if (gen.NecessaryChecks(tuple25.Item3) && (TBServoSpdInRPM as Control).Focused && sender == TBServoSpdInRPM)
                        TBUnitLengthSpeed1000LUmin.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple25.Item3), 3));
                    break;
                case "TBPhysicalStartPosition": //This just calulates the distance travelled
                case "TBPhysicalEndPosition":   //Math case is "TBDistanceTravelled"
                    var tuple26 = gen.Math(TBDistanceTravelled.Name, TBPhysicalStartPosition.Text, "", TBPhysicalEndPosition.Text, "", "", "", "", "", "");
                    if (gen.NecessaryChecks(tuple26.Item1))
                        TBDistanceTravelled.Text = tuple26.Item1;
                    else if (TBPhysicalEndPosition.Text == "" || TBPhysicalStartPosition.Text == "")
                        TBDistanceTravelled.Text = "";
                    else
                        TBDistanceTravelled.Text = "0";
                    break;
                case "TBDistanceTravelled":    //This calculates the error of input distance to travel and real distance travelled arising from an incorrect Drive Wheel Circumference 
                case "TBDriveStartPositionLU": //and then it calculates the correct Drive Wheel circumference (NOTE: better to repeat this process a couple times for best result)
                case "TBDriveEndPositionLU":   //Math case is "TBPositioningError"
                    var tuple27 = gen.Math(TBPositioningError.Name, TBDriveEndPositionLU.Text, TBDLWheelCircumference.Name, TBDriveStartPositionLU.Text, "", TBLUPerMeasuringUnit.Text, "", TBDistanceTravelled.Text, "", TBDriveLoadWheelCircumference.Text);
                    if (gen.NecessaryChecks(tuple27.Item1))
                        TBPositioningError.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple27.Item1), 3));
                    else if (tuple27.Item1 == "0")
                        TBPositioningError.Text = "0";
                    if (gen.NecessaryChecks(tuple27.Item2))
                        TBDLWheelCircumference.Text = Convert.ToString(System.Math.Round(Convert.ToDouble(tuple27.Item2), 3));
                    else if (tuple27.Item2 == "0")
                        TBDLWheelCircumference.Text = "0";
                    if (TBDriveStartPositionLU.Text == "" || TBDriveEndPositionLU.Text == "")
                        TBDLWheelCircumference.Text = "";
                    if (TBDriveStartPositionLU.Text == "" || TBDriveEndPositionLU.Text == "")
                        TBPositioningError.Text = "";
                    if (TBPositioningError.Text == "0")
                        TBDLWheelCircumference.Text = TBDriveLoadWheelCircumference.Text;
                    break;
            }
        }

        //KEYUP EVENTS
        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MathForm_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragStart = new Point(e.X, e.Y);
        }
        private void MathForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point newPos = PointToScreen(new Point(e.X - dragStart.X, e.Y - dragStart.Y));
                DesktopLocation = newPos;
            }
        }

        private void MathForm_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void buttonWrite_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            if (Single == true)
            {
                current.p418 = TBp418_0.Text;
                current.p419 = TBp419_0.Text;
                current.p2504 = TBp2504.Text;
                current.p2505 = TBp2505.Text;
                current.p1082 = TBp1082.Text;
                current.p2000 = TBp2000.Text;
                current.p1121 = TBp1121.Text;
                current.p1135 = TBp1135.Text;
                current.p2506 = TBp2506.Text;
                current.p2503 = TBp2503.Text;
                current.r2524 = TBr2524.Text;
                current.p2571 = TBp2571.Text;
                current.p2572 = TBp2572.Text;
                current.p2573 = TBp2573.Text;
                current.p2580 = TBp2580.Text;
                current.p2581 = TBp2581.Text;
                current.p2585 = TBp2585.Text;
                current.p2586 = TBp2586.Text;
                current.write = true;
                this.Close();
            }
            else if (Double == true)
            {
                currentDouble.p418v0 = TBp418_0.Text;
                currentDouble.p419v0 = TBp419_0.Text;
                currentDouble.p2504 = TBp2504.Text;
                currentDouble.p2505 = TBp2505.Text;
                currentDouble.p1082 = TBp1082.Text;
                currentDouble.p2000 = TBp2000.Text;
                currentDouble.p1121 = TBp1121.Text;
                currentDouble.p1135 = TBp1135.Text;
                currentDouble.p2506 = TBp2506.Text;
                currentDouble.p2503 = TBp2503.Text;
                currentDouble.r2524 = TBr2524.Text;
                currentDouble.p2571 = TBp2571.Text;
                currentDouble.p2572 = TBp2572.Text;
                currentDouble.p2573 = TBp2573.Text;
                currentDouble.p2580 = TBp2580.Text;
                currentDouble.p2581 = TBp2581.Text;
                currentDouble.p2585 = TBp2585.Text;
                currentDouble.p2586 = TBp2586.Text;
                currentDouble.p418v1 = TBp418_1.Text;
                currentDouble.p419v1 = TBp419_1.Text;
                currentDouble.write = true;
                this.Close();
            }
        }
        public DataToReadSingle BackwardsDataTransferSingle()
        {
            if (Single == true)
            {
                return current;
            }
            else
            {
                return null;
            }
        }
        public DataToReadDouble BackwardsDataTransferDouble()
        {
            if (Double == true)
            {
                return currentDouble;
            }
            else
            {
                return null;
            }
        }
        public class DataToReadSingle
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
            public string TBDLWheelCircFinal { get; set; } ////////// if tbdlwheelcircumfrence(page 5) = wheel circumference(page 2) then proceed, if not then reevaluate both of them with current values, if they are still inequal then throw an error/////////////
            public bool write { get; set; }
        }
        public class DataToReadDouble
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
            public string TBDLWheelCircFinal { get; set; }
            public bool write { get; set; }
        }

        public class memory
        {
            public string p408v0 { get; set; }
            public string p421v0 { get; set; }
            public string p408v1 { get; set; }
            public string p421v1 { get; set; }
            public string p423v0 { get; set; }
            public string p423v1 { get; set; }
            public string DPLWheelDiameter { get; set; }
            public string mmPLWRevolution { get; set; }
            public string LUPerMeasuringUnit { get; set; }
            public string MaxPosSpdMmPerMin { get; set; }
            public string AccelTimeS { get; set; }
            public string DecelTimeS { get; set; }
            public string TestPercentage { get; set; }
            public string p407v0 { get; set; }
            public string p407v1 { get; set; }
            public string TBLimSwitchPlusMM { get; set; }
            public string TBSWLimSwitchMinusMM { get; set; }
            public string TBjog1 { get; set; }
            public string TBjog2 { get; set; }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Single == true) 
                { 
                    save.p408v0 = TBp408_0.Text;
                    save.p421v0 = TBp421_0.Text;
                    save.p423v0 = TBp423_0.Text;
                    save.DPLWheelDiameter = TBDrivePitchLoadWheelDiameterMM.Text;
                    save.mmPLWRevolution = TBDriveLoadWheelCircumference.Text;
                    save.LUPerMeasuringUnit = TBLUPerMeasuringUnit.Text;
                    save.MaxPosSpdMmPerMin = TBMaxPositioningSpdMMmin.Text;
                    save.AccelTimeS = TBAccelTimeS.Text;
                    save.DecelTimeS = TBDecelTimeS.Text;
                    save.TestPercentage = TBjogTest.Text;
                    save.p407v0 = TBp407_0.Text;
                    save.TBLimSwitchPlusMM = TBLimSwitchPlusMM.Text;
                    save.TBSWLimSwitchMinusMM = TBSWLimSwitchMinusMM.Text;
                    save.TBjog1 = TBjog1.Text;
                    save.TBjog2 = TBjog2.Text;

                    SaveSettings(save, ".\\" + driveName + ".json");
                }
                else if (Double == true) 
                { 
                    save.p408v0 = TBp408_0.Text;
                    save.p408v1 = TBp408_1.Text;
                    save.p421v0 = TBp421_0.Text;
                    save.p421v1 = TBp421_1.Text;
                    save.p423v1 = TBp423_1.Text;
                    save.p423v0 = TBp423_0.Text;
                    save.DPLWheelDiameter = TBDrivePitchLoadWheelDiameterMM.Text;
                    save.mmPLWRevolution = TBDriveLoadWheelCircumference.Text;
                    save.LUPerMeasuringUnit = TBLUPerMeasuringUnit.Text;
                    save.MaxPosSpdMmPerMin = TBMaxPositioningSpdMMmin.Text;
                    save.AccelTimeS = TBAccelTimeS.Text;
                    save.DecelTimeS = TBDecelTimeS.Text;
                    save.TestPercentage = TBjogTest.Text;
                    save.p407v0 = TBp407_0.Text;
                    save.p407v1 = TBp407_1.Text;
                    save.TBLimSwitchPlusMM = TBLimSwitchPlusMM.Text;
                    save.TBSWLimSwitchMinusMM = TBSWLimSwitchMinusMM.Text;
                    save.TBjog1 = TBjog1.Text;
                    save.TBjog2 = TBjog2.Text;

                    SaveSettings(save, ".\\" + driveName + ".json");
                }
            }
            catch
            {

            }
        }

        public void SaveSettings(memory settings, string filename)
        {
            string json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(filename, json);
        }
        public memory RestoreSettings(string filename)
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                return JsonConvert.DeserializeObject<memory>(json);
            }
            else
            {
                // Handle the case where the settings file doesn't exist
                return new memory(); // Return defaults or an empty settings object
            }
        }
    }
}

