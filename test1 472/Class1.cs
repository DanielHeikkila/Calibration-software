using Siemens.Engineering.Library.MasterCopies;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW;
using Siemens.Engineering;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MathProject
{
    public class GeneralCalc : General
    {
        public string CalculationsOffline(string name1, string value1, string value2, string value3, string value4, string value5)
        {
            string vr = "";
            double number = 0;
            try
            {
                switch (name1)
                {
                    case "TBGearboxRatio":
                    case "TBMMPerRevolution":
                    case "TBr2524":
                    case "TBp2505":
                    case "TBPhysicalPositionMM":
                        vr = Convert.ToString(Convert.ToDouble(value1) / Convert.ToDouble(value2)); ///(GR)value1 is p2504 and value2 is p2505 OR (MMPR)value1 is DriveLoadWheelCircumference and value2 is GearboxRatio OR (r2524)value1 is p2506 and value2 is GearboxRatio OR (p2505)value1 is p2504 and value2 is GearboxRatio OR (PPMM)value1 is UnitLengthPositionLU and value2 is LUPerMeasuringUnit
                        break;
                    case "TBp2506":
                    case "TBp2581":
                    case "TBp2580":
                    case "TBUnitLengthPositionLU":
                    case "TBp2506Max":
                        vr = Convert.ToString(Convert.ToDouble(value1) * Convert.ToDouble(value2)); ///(p2506)value1 is DriveLoadWheelCircumference and value2 is LUPerMeasuringUnit OR value1 is LimSwitchPlusMM or SWLimSwitchMinusMM and value2 is LUPerMeasuringUnit OR (ULPLU)value1 is PhysicalPositionMM and value 2 is LUPerMeasuringUnit OR (TBp2506Max)value1 is p423 and value2 is GearboxRatio
                        break;
                    case "TBDrivePitchLoadWheelDiameterMM":
                        vr = Convert.ToString(Convert.ToDouble(value1) / System.Math.PI); ///value1 is DriveLoadWheelCircumference
                        break;
                    case "TBp1082MaxMM":
                    case "TBp2000MaxMM":
                        vr = Convert.ToString(Convert.ToDouble(value1) * Convert.ToDouble(value2) / Convert.ToDouble(value3)); ///value1 is p1082, value2 is r2524 and value3 is LUPerMeasuringUnit or value1 is p2000, value2 is r2524 and value3 is LUPerMeasuringUnit
                        break;
                    case "TBp1082MaxLU":
                    case "TBp2000MaxLU":
                    case "TBUnitLengthSpeed1000LUmin":
                        vr = Convert.ToString(Convert.ToDouble(value1) * Convert.ToDouble(value2) / 1000); ///value1 is p1082 and value2 is r2524 or value1 is p2000 and value2 is r2524 OR value1 is PhysicalSpd and value2 is LUPerMeasuringUnit
                        break;
                    case "TBp2503":
                        vr = Convert.ToString(Convert.ToDouble(value1) * 10); ///value1 is LUPerMeasuringUnit
                        break;
                    case "TBp2571":
                        vr = Convert.ToString(Convert.ToDouble(value1) / (1000 / Convert.ToDouble(value2))); ///value1 is MaxPositioningSpdMMmin and value2 is LUPerMeasuringUnit
                        break;
                    case "TBp2571_CorrespondsToMs":
                        vr = Convert.ToString(Convert.ToDouble(value1) / 1000 / 60); ///value1 is MaxPositioningSpdMMmin
                        break;
                    case "TBp2571_Correspondsto1MIN":
                        vr = Convert.ToString((Convert.ToDouble(value1) * 1000) / Convert.ToDouble(value2)); ///value1 is p2571 and value2 is r2524
                        break;
                    case "TBAsPercentOfRefSpd":
                        vr = Convert.ToString((Convert.ToDouble(value1) / Convert.ToDouble(value2)) * 100); ///value1 is p2571_Correspondsto1MIN and value2 is p2000
                        break;
                    case "TBDelayRelativeTo_p1082":
                    case "TBp1121_CorrespondsTo1000LUs":
                    case "TBp1135_CorrespondsTo1000LUs":
                        vr = Convert.ToString((Convert.ToDouble(value1) * Convert.ToDouble(value2)) / 60 / 1000 / Convert.ToDouble(value3)); ///value1 is p1082, value2 is r2524 and value3 is p1121 or p1135 or TBp2573
                        break;
                    case "TBp1121_CorrespondsToMMs":
                    case "TBp1135_CorrespondsToMMs":
                        number = (Convert.ToDouble(value1) * Convert.ToDouble(value2)) / 60 / 1000 / Convert.ToDouble(value3); ///value1 is p1082 and value2 r2524 is and value3 is p1121 or p1135
                        vr = Convert.ToString((number * 1000) / Convert.ToDouble(value4)); ///value4 is LUPerMeasuringUnit
                        break;
                    case "TBp1121_StopTimeFromMaxVelocity":
                    case "TBp1135_StopTimeFromMaxVelocity":
                        number = (Convert.ToDouble(value1) * Convert.ToDouble(value2)) / 60 / 1000 / Convert.ToDouble(value3); ///value1 is p1082 and value2 r2524 is and value3 is p1121 or p1135
                        vr = Convert.ToString((Convert.ToDouble(value4) / 60) / number); ///value4 is p2571
                        break;
                    case "TBp2572":
                    case "TBp2573":
                        vr = Convert.ToString((Convert.ToDouble(value1) / 60) / Convert.ToDouble(value2)); ///value1 is p2571 and value2 is TBAccelTimeS or TBDecelTimeS
                        break;
                    case "TBp2572_CorrespondsToMMs":
                    case "TBp2573_CorrespondsToMMs":
                        number = (Convert.ToDouble(value1) / 60) / Convert.ToDouble(value2); ///value1 is p2571 and value2 is TBAccelTimeS or TBDecelTimeS
                        vr = Convert.ToString((number * 1000) / Convert.ToDouble(value3)); ///value3 is LUPerMeasuringUnit
                        break;
                    case "TBp2585":
                    case "TBp2586":
                        vr = Convert.ToString((Convert.ToDouble(value1) * Convert.ToDouble(value2)) / 1000); ///value1 is jog1 or jog2 and value2 is LUPerMeasuringUnit
                        break;
                    case "TBjogMM":
                        vr = Convert.ToString((Convert.ToDouble(value1) / Convert.ToDouble(value2) * 1000) * (Convert.ToDouble(value3) / 100)); ///value1 is p2585, value2 is LUPerMeasuringUnit and value3 is jogTest
                        break;
                    case "TBR_UPTimeJogS":
                    case "TBR_DownTimeJogS":
                        number = ((Convert.ToDouble(value1) / Convert.ToDouble(value2)) * 1000) * (Convert.ToDouble(value3) / 100);///value1 is p2585, value2 is LUPerMeasuringUnit and value3 is jogTest
                        vr = Convert.ToString(number / (Convert.ToDouble(value4) * 1000 * (60 / Convert.ToDouble(value2))));///value4 is p2572 or p2573
                        break;
                    case "TBServoSpdInRPM":
                    case "TBPhysicalSpd":
                        vr = Convert.ToString(Convert.ToDouble(value1) * 1000 / Convert.ToDouble(value2)); ///value1 is UnitLengthSpeed1000LUmin and value2 is r2524 OR value1 is UnitLengthSpeed1000LUmin and value2 is LUPerMeasuringUnit
                        break;
                    case "TBDistanceTravelled":
                        vr = Convert.ToString(Convert.ToDouble(value1) - Convert.ToDouble(value2)); ///value1 is PhysicalEndPosition and value2 is PhysicalStartPosition
                        break;
                    case "TBPositioningError":
                        number = Convert.ToDouble(value1) - Convert.ToDouble(value2); ///value1 is DriveEndPositionLU and value2 is DriveStartPositionLU
                        vr = Convert.ToString((number / Convert.ToDouble(value3)) - Convert.ToDouble(value4)); ///value3 is LUPerMeasuringUnit and value4 is DistanceTravelled
                        break;
                    case "TBDLWheelCircumference":
                        number = Convert.ToDouble(value2) / Convert.ToDouble(value1); /// value1 is DriveLoadWheelCircumference and value2 is Distancetravelled
                        vr = Convert.ToString(Convert.ToDouble(value1) - (Convert.ToDouble(value3) / Convert.ToDouble(number))); ///value3 is TBPositioningError
                        break;
                }
            }
            catch
            {
            }
            return vr;
        }
        public string CalculationsOnline(string name1, string value1, string value2, string value3, string value4, string value5)
        {
            string vr = "";
            double number = 0;
            double number1 = 0;
            try
            {
                switch (name1)
                {
                    case "TBp407_0":
                    case "TBp407_1":
                        vr = Convert.ToString(Convert.ToDouble(value1) * 1000000); ///value1 is ResolutionMM or ResolutionMM1
                        break;
                    case "TBp418_0":
                    case "TBp418_1":
                    case "TBp419_0":
                    case "TBp419_1":
                        vr = Convert.ToString(32 - Convert.ToDouble(value1)); ///value1 is Encoder1TotalNumOfBits and/or Encoder0TotalNumOfBits
                        break;
                    case "TBEncoder1TotalNumOfBits":
                    case "TBEncoder0TotalNumOfBits":
                        vr = Convert.ToString(Convert.ToDouble(value1) + Convert.ToDouble(value2)); /// value1 is TBp408_0NumberOfBits or TBp408_1NumberOfBits and value2 is TBp421_0NumberOfBits or TBp421_1NumberOfBits
                        break;
                    case "TBp423_0":
                    case "TBp423_1":
                        vr = Convert.ToString(System.Math.Ceiling(System.Math.Pow(2, (Convert.ToDouble(value1) + Convert.ToDouble(value2))))); ///value1 is p408NumberOfBits and/or p408v1NumberOfBits and value2 is p418 and/or p418v1
                        break;
                    case "TBp408_0NumberOfBits":
                    case "TBp408_1NumberOfBits":
                    case "TBp421_0NumberOfBits":
                    case "TBp421_1NumberOfBits":
                        vr = Convert.ToString(System.Math.Ceiling(System.Math.Log(Convert.ToDouble(value1), 2))); ///value1 is p408 and p408v1 or value1 is p421 and p421v1
                        break;
                    case "TBLinearEncoderCheck":
                        number = Convert.ToDouble(value1) * Convert.ToDouble(value2); ///value1 is p407v1 and value2 is p2503
                        number1 = System.Math.Pow(2, Convert.ToDouble(value3)) * System.Math.Pow(10, 7); ///value3 is p419v1
                        vr = Convert.ToString(number / number1);
                        break;
                }
            }
            catch
            {
            }
            return vr;
        }
    }
    public class General
    {

        public static void Create(Project project, PlcSoftware plcSoftware) // create a master copy from a code block in the project library
        {
            MasterCopySystemFolder masterCopyFolder = project.ProjectLibrary.MasterCopyFolder;
            CodeBlock block = plcSoftware.BlockGroup.Groups[0].Blocks.Find("Block_1") as CodeBlock;
            MasterCopy masterCopy = masterCopyFolder.MasterCopies.Create(block);
        }

        public Dictionary<string, string> Par = new Dictionary<string, string>()
        {
            //////PAGE1//////
            ////////////PAGE2//////////////
            ["TBp2505"] = "",
            ["TBp2506"] = "",
            ["TBr2524"] = "",
            ["TBMMPerRevolution"] = "",
            ["TBDrivePitchLoadWheelDiameterMM"] = "",
            ["TBDriveLoadWheelCircumference"] = "",
            ["TBp1082MaxMM"] = "",
            ["TBp1082MaxLU"] = "",
            ["TBp2000MaxMM"] = "",
            ["TBp2000MaxLU"] = "",
            ["TBLinearEncoderCheck"] = "",
            ////////////PAGE3//////////////
            ["TBp2571"] = "",
            ["TBp2571_CorrespondsToMs"] = "",
            ["TBp2571_Correspondsto1MIN"] = "",
            ["TBDelayRelativeTo_p1082"] = "",
            ["TBAsPercentOfRefSpd"] = "",
            ["TBp2572.Text"] = "",
            ["TBp2572_CorrespondsToMMs"] = "",
            ["TBp2573"] = "",
            ["TBp2573_CorrespondsToMMs"] = "",
            ["TBp1121_CorrespondsTo1000LUs"] = "",
            ["TBp1121_CorrespondsToMMs"] = "",
            ["TBp1121_StopTimeFromMaxVelocity"] = "",
            ["TBp1135_CorrespondsTo1000LUs"] = "",
            ["TBp1135_CorrespondsToMMs"] = "",
            ["TBp1135_StopTimeFromMaxVelocity"] = "",
            ////////////PAGE4//////////////
            ["TBR_UPTimeJogS"] = "",
            ["TBR_DownTimeJogS"] = "",
            ////////////PAGE5//////////////
            ["TBPhysicalSpd"] = "",
            ["TBServoSpdInRPM"] = "",
            ["TBUnitLengthSpeed1000LUmin"] = "",
        };

        string v1 = "";
        string v2 = "";
        string v3 = "";
        string v4 = "";
        string v5 = "";
        string v6 = "";
        string v7 = "";

        public bool NecessaryChecks(string value)
        {
            if (value != null)
            {
                if (General.IsDoubl(value) && value != "" && value != "0," && value != "0." && value != "0,0" && value != "0.0" && value != "0")
                    return true;
                else if (General.IsDoubl(value.ToString(CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name))) && value != "" && value != "0," && value != "0." && value != "0,0" && value != "0.0" && value != "0")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public Tuple<string, string, string, string, string, string, string> Math(string name1, string value1, string name2, string value2, string name3, string value3, string name4, string value4, string name5, string value5)
        {
            GeneralCalc calc = new GeneralCalc();

            v1 = "";
            v2 = "";
            v3 = "";
            v4 = "";
            v5 = "";
            v6 = "";
            v7 = "";
            switch (name1)
            {
                /////////////////////////////////PAGE 1//////////////////////////////////////
                case "comboBox2":
                case "comboBox1":
                    switch (value1)
                    {
                        case "0":
                            if (value4 != "")
                            {
                                v1 = "1";
                                v2 = "1";
                                v3 = "1";
                                v4 = "1";
                                v5 = "1";
                            }
                            else
                            {
                                v1 = "32";
                                v2 = "32";
                                v3 = value3;
                                v4 = value2;
                                v5 = value5;
                            }
                            break;
                        case "1":
                            v1 = "11";
                            v2 = "9";
                            v3 = "4096";
                            v4 = "512";
                            v5 = "1048576";
                            break;
                        case "2":
                            v1 = "2";
                            v2 = "2";
                            v3 = value3;
                            v4 = value2;
                            v5 = value5;
                            break;
                        case "3":
                            if (value4 != "")
                            {
                                v1 = Convert.ToString(32 - Convert.ToDouble(value4));
                                v2 = Convert.ToString(32 - Convert.ToDouble(value4));
                            }
                            v3 = value3;
                            v4 = value2;
                            v5 = value5;
                            break;
                    }
                    break;
                case "TBp408_0NumberOfBits":
                case "TBp408_1NumberOfBits":
                case "TBp421_0NumberOfBits":
                case "TBp421_1NumberOfBits":
                    if (NecessaryChecks(value1))
                        v1 = calc.CalculationsOnline(name1, value1, "", "", "", ""); //TBp408_0NumberOfBits
                    if (NecessaryChecks(value3))
                        v3 = calc.CalculationsOnline(name3, value3, "", "", "", ""); //TBp421_0NumberOfBits
                    if (NecessaryChecks(value2))
                        v2 = calc.CalculationsOnline(name2, value2, "", "", "", ""); //TBp408_1NumberOfBits
                    if (NecessaryChecks(value4))
                        v4 = calc.CalculationsOnline(name4, value4, "", "", "", ""); //TBp421_1NumberOfBits
                    break;
                case "TBp423_0":
                case "TBp423_1":
                    if (NecessaryChecks(value1) && NecessaryChecks(value2))
                        v1 = calc.CalculationsOnline(name1, value1, value2, "", "", ""); //TBp423_0
                    if (NecessaryChecks(value3) && NecessaryChecks(value4))
                        v2 = calc.CalculationsOnline(name2, value3, value4, "", "", ""); //TBp423_1
                    break;
                case "TBEncoder1TotalNumOfBits":
                case "TBEncoder0TotalNumOfBits":
                    if (NecessaryChecks(value1) && NecessaryChecks(value2))
                        v1 = calc.CalculationsOnline(name1, value1, value2, "", "", ""); //TBEncoder0TotalNumberOfBits
                    if (NecessaryChecks(value3) && NecessaryChecks(value4))
                        v2 = calc.CalculationsOnline(name2, value3, value4, "", "", ""); //TBEncoder1TotalNumberOfBits
                    break;
                /////////////////////////////////PAGE 2//////////////////////////////////////
                case "TBp2505":
                case "TBp2504":
                case "TBGearboxRatio":
                    if (NecessaryChecks(Par["TBp2505"]))
                    {
                        if (NecessaryChecks(value1) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name1, value1, value3, "", "", ""); //TBGearboxRatio
                        if (NecessaryChecks(value1) && NecessaryChecks(value2))
                            v2 = calc.CalculationsOffline(name1, value1, value2, "", "", ""); //TBp2505
                        if (NecessaryChecks(v2))
                            Par["TBp2505"] = v2;
                    }
                    else if (!NecessaryChecks(Par["TBp2505"]))
                    {
                        if (NecessaryChecks(value1) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name1, value1, value3, "", "", ""); //TBGearboxRatio
                        if (NecessaryChecks(value1) && NecessaryChecks(value2))
                            v2 = calc.CalculationsOffline(name1, value1, value2, "", "", ""); //TBp2505
                        if (NecessaryChecks(v2))
                            Par["TBp2505"] = v2;
                    }
                    break;
                case "TBp2506":
                case "TBp2506Max":
                    if (NecessaryChecks(Par["TBDriveLoadWheelCircumference"]))
                    {
                        if (NecessaryChecks(value1) && NecessaryChecks(value2))
                            v1 = calc.CalculationsOffline(name3, Par["TBDriveLoadWheelCircumference"], value2, "", "", ""); //TBp2506
                        if (NecessaryChecks(v1) && NecessaryChecks(value3))
                            v2 = calc.CalculationsOffline(name2, v1, value3, "", "", ""); //TBr2524
                        if (NecessaryChecks(value1) && NecessaryChecks(value3))
                            v3 = calc.CalculationsOffline(name2, Par["TBDriveLoadWheelCircumference"], value3, "", "", ""); //TBMMPerRevolution
                    }
                    else
                    {
                        if (NecessaryChecks(value1) && NecessaryChecks(value2))
                            v1 = calc.CalculationsOffline(name3, value1, value2, "", "", ""); //TBp2506
                        if (NecessaryChecks(v1) && NecessaryChecks(value3))
                            v2 = calc.CalculationsOffline(name2, v1, value3, "", "", ""); //TBr2524
                        if (NecessaryChecks(value1) && NecessaryChecks(value3))
                            v3 = calc.CalculationsOffline(name2, value1, value3, "", "", ""); //TBMMPerRevolution
                    }

                    if (NecessaryChecks(value3) && NecessaryChecks(value4))
                        v4 = calc.CalculationsOffline(name4, value3, value4, "", "", ""); //TBp2506Max
                    if (NecessaryChecks(v1))
                        Par["TBp2506"] = v1;
                    if (NecessaryChecks(v2))
                        Par["TBr2524"] = v2;
                    if (NecessaryChecks(v3))
                        Par["TBMMPerRevolution"] = v3;
                    if (NecessaryChecks(value1))
                        Par["TBDriveLoadWheelCircumference"] = value1;
                    if (NecessaryChecks(value5))
                        Par["TBDrivePitchLoadWheelDiameterMM"] = value5;

                    break;
                case "TBp1082MaxMM":
                case "TBp2000MaxMM":
                case "TBp1082MaxLU":
                case "TBp2000MaxLU":
                    if (NecessaryChecks(Par["TBr2524"]))
                    {
                        if (NecessaryChecks(value4))
                        {
                            if (NecessaryChecks(value4) && NecessaryChecks(value2) && NecessaryChecks(value3))
                                v1 = calc.CalculationsOffline(name1, value4, Par["TBr2524"], value3, "", ""); //TBp1082MaxMM
                            if (NecessaryChecks(value4) && NecessaryChecks(value2))
                                v2 = calc.CalculationsOffline(name2, value4, Par["TBr2524"], "", "", ""); //TBp1082MaxLU
                        }
                        if (NecessaryChecks(value4) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name1, value4, Par["TBr2524"], value3, "", ""); //TBp1082MaxMM
                        if (NecessaryChecks(value4) && NecessaryChecks(value2))
                            v2 = calc.CalculationsOffline(name2, value4, Par["TBr2524"], "", "", ""); //TBp1082MaxLU
                        if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v3 = calc.CalculationsOffline(name1, value1, Par["TBr2524"], value3, "", ""); //TBp2000MaxMM
                        if (NecessaryChecks(value1) && NecessaryChecks(value2))
                            v4 = calc.CalculationsOffline(name2, value1, Par["TBr2524"], "", "", ""); //TBp2000MaxLU
                    }
                    else
                    {
                        if (NecessaryChecks(value4) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name1, value4, value2, value3, "", ""); //TBp1082MaxMM
                        if (NecessaryChecks(value4) && NecessaryChecks(value2))
                            v2 = calc.CalculationsOffline(name2, value4, value2, "", "", ""); //TBp1082MaxLU
                        if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v3 = calc.CalculationsOffline(name1, value1, value2, value3, "", ""); //TBp2000MaxMM
                        if (NecessaryChecks(value1) && NecessaryChecks(value2))
                            v4 = calc.CalculationsOffline(name2, value1, value2, "", "", ""); //TBp2000MaxLU
                    }
                    if (NecessaryChecks(v1))
                        Par["TBp1082MaxMM"] = v1;
                    if (NecessaryChecks(v1))
                        Par["TBp1082MaxLU"] = v2;
                    if (NecessaryChecks(v1))
                        Par["TBp2000MaxMM"] = v3;
                    if (NecessaryChecks(v1))
                        Par["TBp2000MaxLU"] = v4;
                    break;
                case "TBLinearEncoderCheck":
                    if (NecessaryChecks(value3) && NecessaryChecks(value1) && NecessaryChecks(value2))
                        v1 = calc.CalculationsOnline(name1, value1, value2, value3, "", ""); //TBLinearEncoderCheck
                    if (NecessaryChecks(v1))
                        Par["TBLinearEncoderCheck"] = v1;
                    break;
                /////////////////////////////////PAGE 3//////////////////////////////////////
                case "TBp2571":
                    if (NecessaryChecks(value1))
                        v2 = calc.CalculationsOffline(name2, value1, "", "", "", ""); //TBp2571_CorrespondsToMs
                    if (NecessaryChecks(value2) && NecessaryChecks(value1))
                        v1 = calc.CalculationsOffline(name1, value1, value2, "", "", ""); //TBp2571
                    if (NecessaryChecks(Par["TBr2524"]))
                    {
                        if (NecessaryChecks(value3) && NecessaryChecks(v1))
                            v3 = calc.CalculationsOffline(name3, v1, Par["TBr2524"], "", "", ""); //TBp2571_Correspondsto1MIN
                        if (NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(value4) && NecessaryChecks(Par["TBp2573"]))
                            v4 = calc.CalculationsOffline(name4, value4, Par["TBr2524"], Par["TBp2573"], "", ""); //TBDelayRelativeTo_p1082
                        else if (NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(value4))
                            v4 = calc.CalculationsOffline(name4, value4, Par["TBr2524"], value5, "", ""); //TBDelayRelativeTo_p1082
                        else if (NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(Par["TBp2573"]))
                            v4 = calc.CalculationsOffline(name4, value4, Par["TBr2524"], Par["TBp2573"], "", ""); //TBDelayRelativeTo_p1082
                    }
                    else
                    {
                        if (NecessaryChecks(value3) && NecessaryChecks(v1))
                            v3 = calc.CalculationsOffline(name3, v1, value3, "", "", ""); //TBp2571_Correspondsto1MIN
                        if (NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5))
                            v4 = calc.CalculationsOffline(name4, value4, value3, value5, "", ""); //TBDelayRelativeTo_p1082
                        else if (NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(value4))
                            v4 = calc.CalculationsOffline(name4, value4, value3, value5, "", ""); //TBDelayRelativeTo_p1082
                        else if (NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(Par["TBp2573"]))
                            v4 = calc.CalculationsOffline(name4, value4, value3, Par["TBp2573"], "", ""); //TBDelayRelativeTo_p1082
                    }
                    if (NecessaryChecks(v1))
                        Par["TBp2571"] = v1;
                    if (NecessaryChecks(v2))
                        Par["TBp2571_CorrespondsToMs"] = v2;
                    if (NecessaryChecks(v3))
                        Par["TBp2571_Correspondsto1MIN"] = v3;
                    if (NecessaryChecks(v4))
                        Par["TBDelayRelativeTo_p1082"] = v4;
                    break;
                case "TBAsPercentOfRefSpd":
                    if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(Par["TBp2571_Correspondsto1MIN"]))
                        v1 = calc.CalculationsOffline(name1, Par["TBp2571_Correspondsto1MIN"], value2, "", "", ""); //TBAsPercentOfRefSpd
                    else if (NecessaryChecks(value1) && NecessaryChecks(value2) && !NecessaryChecks(Par["TBp2571_Correspondsto1MIN"]))
                        v1 = calc.CalculationsOffline(name1, Par["TBp2571_Correspondsto1MIN"], value2, "", "", ""); //TBAsPercentOfRefSpd
                    if (NecessaryChecks(Par["TBp2571"]))
                    {
                        if (NecessaryChecks(value3) && NecessaryChecks(value5))
                            v2 = calc.CalculationsOffline(name3, Par["TBp2571"], value3, "", "", ""); //TBp2572
                        if (NecessaryChecks(value3) && NecessaryChecks(value5) && NecessaryChecks(name5))
                            v3 = calc.CalculationsOffline(name4, Par["TBp2571"], value3, name5, "", ""); //TBp2572_CorrespondsToMMs
                        if (NecessaryChecks(value4) && NecessaryChecks(value5))
                            v4 = calc.CalculationsOffline(name3, Par["TBp2571"], value4, "", "", ""); //TBp2573
                        if (NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(name5))
                            v5 = calc.CalculationsOffline(name4, Par["TBp2571"], value4, name5, "", ""); //TBp2573_CorrespondsToMMs
                    }
                    else
                    {
                        if (NecessaryChecks(value3) && NecessaryChecks(value5))
                            v2 = calc.CalculationsOffline(name3, value5, value3, "", "", ""); //TBp2572
                        if (NecessaryChecks(value3) && NecessaryChecks(value5) && NecessaryChecks(name5))
                            v3 = calc.CalculationsOffline(name4, value5, value3, name5, "", ""); //TBp2572_CorrespondsToMMs
                        if (NecessaryChecks(value4) && NecessaryChecks(value5))
                            v4 = calc.CalculationsOffline(name3, value5, value4, "", "", ""); //TBp2573
                        if (NecessaryChecks(value4) && NecessaryChecks(value5) && NecessaryChecks(name5))
                            v5 = calc.CalculationsOffline(name4, value5, value4, name5, "", ""); //TBp2573_CorrespondsToMMs
                    }
                    if (NecessaryChecks(v1))
                        Par["TBAsPercentOfRefSpd"] = v1;
                    if (NecessaryChecks(v2))
                        Par["TBp2572"] = v2;
                    if (NecessaryChecks(v3))
                        Par["TBp2572_CorrespondsToMMs"] = v3;
                    if (NecessaryChecks(v4))
                        Par["TBp2573"] = v4;
                    if (NecessaryChecks(v5))
                        Par["TBp2573_CorrespondsToMMs"] = v5;
                    break;
                case "TBp1121":
                case "TBp1135":
                    if (NecessaryChecks(Par["TBr2524"]))
                    {
                        if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name2, value2, Par["TBr2524"], value1, "", ""); //TBp1135_CorrespondsTo1000LUs
                        if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value5))
                            v2 = calc.CalculationsOffline(name3, value2, Par["TBr2524"], value1, value5, ""); //TBp1135_CorrespondsToMMs
                        if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name2, value2, Par["TBr2524"], value1, "", ""); //TBp1121_CorrespondsTo1000LUs
                        if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value5))
                            v2 = calc.CalculationsOffline(name3, value2, Par["TBr2524"], value1, value5, ""); //TBp1121_CorrespondsToMMs
                        if (NecessaryChecks(Par["TBp2571"]))
                        {
                            if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, Par["TBr2524"], value1, Par["TBp2571"], ""); //TBp1121_StopTimeFromMaxVelocity
                            if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, Par["TBr2524"], value1, Par["TBp2571"], ""); //TBp1135_StopTimeFromMaxVelocity
                        }
                        else
                        {
                            if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, Par["TBr2524"], value1, value4, ""); //TBp1135_StopTimeFromMaxVelocity
                            if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, Par["TBr2524"], value1, value4, ""); //TBp1121_StopTimeFromMaxVelocity
                        }
                    }
                    else
                    {
                        if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name2, value2, value3, value1, "", ""); //TBp1135_CorrespondsTo1000LUs
                        if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value5))
                            v2 = calc.CalculationsOffline(name3, value2, value3, value1, value5, ""); //TBp1135_CorrespondsToMMs
                        if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name2, value2, value3, value1, "", ""); //TBp1121_CorrespondsTo1000LUs
                        if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value5))
                            v2 = calc.CalculationsOffline(name3, value2, value3, value1, value5, ""); //TBp1121_CorrespondsToMMs
                        if (NecessaryChecks(Par["TBp2571"]))
                        {
                            if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, value3, value1, Par["TBp2571"], ""); //TBp1121_StopTimeFromMaxVelocity
                            if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, value3, value1, Par["TBp2571"], ""); //TBp1135_StopTimeFromMaxVelocity
                        }
                        else
                        {
                            if (name1 == "TBp1135" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, value3, value1, value4, ""); //TBp1135_StopTimeFromMaxVelocity
                            if (name1 == "TBp1121" && NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                                v3 = calc.CalculationsOffline(name4, value2, value3, value1, value4, ""); //TBp1121_StopTimeFromMaxVelocity
                        }
                    }
                    if (name1 == "TBp1135" && NecessaryChecks(v1))
                        Par["TBp1135_CorrespondsTo1000LUs"] = v1;
                    if (name1 == "TBp1135" && NecessaryChecks(v2))
                        Par["TBp1135_CorrespondsToMMs"] = v2;
                    if (name1 == "TBp1135" && NecessaryChecks(v3))
                        Par["TBp1135_StopTimeFromMaxVelocity"] = v3;
                    if (name1 == "TBp1121" && NecessaryChecks(v1))
                        Par["TBp1121_CorrespondsTo1000LUs"] = v1;
                    if (name1 == "TBp1121" && NecessaryChecks(v2))
                        Par["TBp1121_CorrespondsToMMs"] = v2;
                    if (name1 == "TBp1121" && NecessaryChecks(v3))
                        Par["TBp1121_StopTimeFromMaxVelocity"] = v3;
                    break;
                /////////////////////////////////PAGE 4//////////////////////////////////////
                case "TBp2581":
                case "TBp2580":
                case "TBp2585":
                case "TBp2586":
                    if (NecessaryChecks(value1) && NecessaryChecks(value2))
                        v1 = calc.CalculationsOffline(name1, value1, value2, "", "", ""); //TBp2581
                    if (NecessaryChecks(value3) && NecessaryChecks(value2))
                        v2 = calc.CalculationsOffline(name2, value3, value2, "", "", ""); //TBp2580
                    if (NecessaryChecks(value4) && NecessaryChecks(value2))
                        v3 = calc.CalculationsOffline(name3, value4, value2, "", "", ""); //TBp2585
                    if (NecessaryChecks(value5) && NecessaryChecks(value2))
                        v4 = calc.CalculationsOffline(name4, value5, value2, "", "", ""); //TBp2586
                    break;
                case "TBjogMM":
                case "TBR_UPTimeJogS":
                case "TBR_DownTimeJogS":
                    if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3))
                        v1 = calc.CalculationsOffline(name1, value3, value2, value1, "", ""); //TBjogMM
                    if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(Par["TBp2572"]))
                        v2 = calc.CalculationsOffline(name2, value3, value2, value1, Par["TBp2572"], ""); //TBR_UPTimeJogS
                    else if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(value4))
                        v2 = calc.CalculationsOffline(name2, value3, value2, value1, value4, ""); //TBR_UPTimeJogS
                    if (NecessaryChecks(value1) && NecessaryChecks(value2) && NecessaryChecks(value3) && NecessaryChecks(Par["TBp2573"]))
                        v3 = calc.CalculationsOffline(name3, value3, value2, value1, Par["TBp2573"], ""); //TBR_DownTimeJogS
                    if (NecessaryChecks(v2))
                        Par["TBR_UPTimeJogS"] = v2;
                    if (NecessaryChecks(v3))
                        Par["TBR_DownTimeJogS"] = v3;
                    break;
                /////////////////////////////////PAGE 5//////////////////////////////////////
                case "TBUnitLengthPositionLU":
                case "TBPhysicalPositionMM":
                    if (NecessaryChecks(value1) && NecessaryChecks(value2) && name1 == "TBPhysicalPositionMM")
                        v1 = calc.CalculationsOffline(name2, value1, value2, "", "", ""); //TBUnitLengthPositionLU
                    else if (NecessaryChecks(value1) && NecessaryChecks(value2))
                        v2 = calc.CalculationsOffline(name3, value1, value2, "", "", ""); //TBPhysicalPositionMM
                    break;
                case "TBUnitLengthSpeed1000LUmin": //value5
                case "TBPhysicalSpd": //value4
                case "TBServoSpdInRPM": //value1
                    if (name1 == "TBUnitLengthSpeed1000LUmin") //value 3 is TBr2524
                    {
                        if (NecessaryChecks(value2) && NecessaryChecks(value5))
                            v1 = calc.CalculationsOffline(name4, value5, value2, "", "", ""); //TBPhysicalSpd
                        if (NecessaryChecks(value5) && NecessaryChecks(Par["TBr2524"]))
                            v2 = calc.CalculationsOffline(name4, value5, Par["TBr2524"], "", "", ""); //TBServoSpdInRPM
                        else if (NecessaryChecks(value5) && NecessaryChecks(value3))
                            v2 = calc.CalculationsOffline(name4, value5, value3, "", "", ""); //TBServoSpdInRPM
                        if (NecessaryChecks(v1))
                            Par["TBPhysicalSpd"] = v1;
                        if (NecessaryChecks(v2))
                            Par["TBServoSpdInRPM"] = v2;
                    }
                    else if (name1 == "TBPhysicalSpd")
                    {
                        if (NecessaryChecks(value4) && NecessaryChecks(value2))
                            v3 = calc.CalculationsOffline(name5, value4, value2, "", "", ""); //TBUnitLengthSpeed1000LUmin
                        if (NecessaryChecks(v3) && NecessaryChecks(Par["TBr2524"]))
                            v2 = calc.CalculationsOffline(name4, v3, Par["TBr2524"], "", "", ""); //TBServoSpdInRPM
                        else if (NecessaryChecks(v3) && NecessaryChecks(value3))
                            v2 = calc.CalculationsOffline(name4, v3, value3, "", "", ""); //TBServoSpdInRPM
                        if (NecessaryChecks(v2))
                            Par["TBServoSpdInRPM"] = v2;
                        if (NecessaryChecks(v3))
                            Par["TBUnitLengthSpeed1000LUmin"] = v3;
                    }
                    else
                    {
                        if (NecessaryChecks(value1) && NecessaryChecks(Par["TBr2524"]))
                            v3 = calc.CalculationsOffline(name5, value1, Par["TBr2524"], "", "", ""); //TBUnitLengthSpeed1000LUmin
                        else if (NecessaryChecks(value1) && NecessaryChecks(value3))
                            v3 = calc.CalculationsOffline(name5, value1, value3, "", "", ""); //TBUnitLengthSpeed1000LUmin
                        if (NecessaryChecks(v3) && NecessaryChecks(value3))
                            v1 = calc.CalculationsOffline(name4, v3, value2, "", "", ""); //TBPhysicalSpd
                        if (NecessaryChecks(v1))
                            Par["TBPhysicalSpd"] = v1;
                        if (NecessaryChecks(v3))
                            Par["TBUnitLengthSpeed1000LUmin"] = v3;
                    }
                    break;
                case "TBPhysicalStartPosition":
                case "TBPhysicalEndPosition":
                case "TBDistanceTravelled":
                    v1 = calc.CalculationsOffline(name1, value1, value2, "", "", ""); //TBDistanceTravelled
                    if (NecessaryChecks(v1) && Convert.ToDouble(v1) < 0)
                        v1 = Convert.ToString(Convert.ToDouble(v1) * -1); //TBDistanceTravelled if negative
                    break;
                case "TBDriveStartPositionLU":
                case "TBPositioningError":
                case "TBDriveEndPositionLU": //v5 is TBDriveLoadWheelCircumference
                    if (NecessaryChecks(value2) && NecessaryChecks(value1) && NecessaryChecks(value3) && NecessaryChecks(value4) && NecessaryChecks(value5))
                        v1 = calc.CalculationsOffline(name1, value1, value2, value3, value4, ""); //TBPositioningError
                    if (NecessaryChecks(v1) && NecessaryChecks(value4) && NecessaryChecks(Par["TBDriveLoadWheelCircumference"]))
                        v2 = calc.CalculationsOffline(name2, Par["TBDriveLoadWheelCircumference"], value4, v1, "", ""); //TBDLWheelCircumference
                    if (NecessaryChecks(v1) && NecessaryChecks(value4) && NecessaryChecks(value5))
                        v2 = calc.CalculationsOffline(name2, value5, value4, v1, "", ""); //TBDLWheelCircumference
                    break;
            }
            return Tuple.Create(v1, v2, v3, v4, v5, v6, v7);
        }
        public static bool IsDoubl(string valueToTest)
        {
            if (double.TryParse(valueToTest, out double d) && !Double.IsNaN(d) && !Double.IsInfinity(d))
            {
                return true;
            }

            return false;
        }
    }
}
