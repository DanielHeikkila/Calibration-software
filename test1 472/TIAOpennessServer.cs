using Siemens.Engineering;
using Siemens.Engineering.HW;
using Siemens.Engineering.MC.Drives;
using System.Collections.Generic;
using Siemens.Engineering.Online;
using System;
using System.Security;
using System.Text;
using test1_472;

namespace StartDriveParameterEditor
{
    public class TiaOpennessServer
    {
        TiaPortal refTiaPortal;
        Project refActualProject;
        handleTiaConfirmationDelegate dlgHandleConfirmation;//delegate to handle confimation Events in the main form  
        internal event EventHandler<EventArgs> TiaDisposedEvent; // Create event if TIA Portal is disposed


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="confirmationDelegate"></param>
        /// <param name="makeVisible"></param>
        internal TiaOpennessServer(handleTiaConfirmationDelegate confirmationDelegate, bool makeVisible = false)
        {

            if (makeVisible)
                refTiaPortal = new TiaPortal(TiaPortalMode.WithUserInterface);
            else
                refTiaPortal = new TiaPortal(TiaPortalMode.WithoutUserInterface);

            refTiaPortal.Confirmation += RefTiaPortal_Confirmation;
            dlgHandleConfirmation = confirmationDelegate;
            refTiaPortal.Disposed += RefTiaPortal_Disposed;


        }


        #region TIA Portal & Project Operations       


        /// <summary>
        /// Current project
        /// </summary>
        internal Project CurrentProject
        {
            get
            {
                return refActualProject;
            }
        }


        /// <summary>
        /// Open TIA peoject. The project will be automatically upgraded if necessary
        /// </summary>
        /// <param name="newProjectPath">path of the project</param>
        /// <param name="umacDlg">Umac delegate to invokeif the project is secured by a password</param>
        /// <returns></returns>
        public string OpenProject(string newProjectPath)
        {
            refActualProject = refTiaPortal.Projects.OpenWithUpgrade(new System.IO.FileInfo(newProjectPath));
            if (refActualProject != null)
            {
                return newProjectPath;
            }
            return null;
        }

        /// <summary>
        /// Get the Detailed text of an exception
        /// </summary>
        /// <param name="Ex"></param>
        /// <returns></returns>
        internal string GetExceptionMessage(EngineeringException Ex)
        {
            StringBuilder message = new StringBuilder();
            int i = 0;
            foreach (ExceptionMessageData x in Ex.DetailMessageData)
            {
                message.Append(x.Text + " ");
                i++;
            }
            return message.ToString();
        }


        /// <summary>
        /// Get the installed software
        /// </summary>
        /// <returns></returns>
        internal string GetInstalledSoftware()
        {
            if (refTiaPortal == null)
                return null;
            string InstalledSoftware = "";
            foreach (TiaPortalProduct product in refTiaPortal.GetCurrentProcess().InstalledSoftware)
            {
                InstalledSoftware += GetProductAndOptions(product);
            }
            return InstalledSoftware;
        }


        /// <summary>
        /// Get product info and options
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        internal string GetProductAndOptions(TiaPortalProduct product)
        {
            if (product == null)
                return null;
            string ProductInfo = product.Name + " - " + product.Version + "\n";
            if (product.Options.Count != 0)
            {
                foreach (TiaPortalProduct option in product.Options)
                    ProductInfo += GetProductAndOptions(option);
            }
            return ProductInfo;

        }


        /// <summary>
        /// Close the current project
        /// </summary>
        internal void CloseProject()
        {
            if (refActualProject == null)
                return;
            refActualProject.Close();
            refActualProject = null;
        }


        /// <summary>
        /// Close TIA Portal
        /// </summary>
        internal void CloseTIA()
        {
            if (refTiaPortal == null)
                return;
            refTiaPortal.GetCurrentProcess().Dispose();
            refTiaPortal = null;




        }


        /// <summary>
        /// TIA Portal awaits a user confirmation. Forward event to the main form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefTiaPortal_Confirmation(object sender, ConfirmationEventArgs e)
        {
            dlgHandleConfirmation(e);
        }


        /// <summary>
        /// Get the Type of the TIA Portal Confirmation
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        internal TIAConfirmation GetConfirmationType(ConfirmationEventArgs e)
        {
            //Currently no information about the confirmation available
            return TIAConfirmation.UNKNOWN;
        }

        /// <summary>
        /// Fire an event when TIA Portal is disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefTiaPortal_Disposed(object sender, EventArgs e)
        {
            TiaDisposedEvent?.Invoke(this, e);

        }


        #endregion


        #region Drive Operations

        /// <summary>
        /// Get a list of all Devices that represent Sinamics drives in a project
        /// </summary>
        /// <param name="SearchAlsoInGroups"> if true search also user groups</param>
        /// <returns></returns>
        internal List<Device> GetAllSinamicsDevicesInProject(bool SearchAlsoInGroups = false)
        {

            List<Device> drvDevices = new List<Device>();
            foreach (Device dev in refActualProject.Devices)
            {
                if (IsSinamicsG120Device(dev) || IsSinamicsG110Device(dev) || IsSinamicsS120Device(dev))
                    drvDevices.Add(dev);
            }
            if (SearchAlsoInGroups)
            {
                foreach (DeviceUserGroup grp in refActualProject.DeviceGroups)
                    GetSinamicsDevicesFromGroup(grp, ref drvDevices, true);
            }

            drvDevices.TrimExcess();
            return drvDevices;
        }


        /// <summary>
        /// search a user group for sinamics devices and add them to the given list 
        /// </summary>
        /// <param name="devicegroup"> the user group to search</param>
        /// <param name="drvDevices">the list where the found devices are added</param>
        /// <param name="SearchAlsoInSubGroups">if true subgroups are also searched</param>
        internal void GetSinamicsDevicesFromGroup(DeviceUserGroup devicegroup, ref List<Device> drvDevices, bool SearchAlsoInSubGroups = false)
        {
            foreach (Device dev in devicegroup.Devices)
            {
                if (IsSinamicsG120Device(dev) || IsSinamicsG110Device(dev) || IsSinamicsS120Device(dev))
                {
                    drvDevices.Add(dev);
                }
            }

            if (SearchAlsoInSubGroups)
            {
                foreach (DeviceUserGroup group in devicegroup.Groups)
                {
                    GetSinamicsDevicesFromGroup(group, ref drvDevices, true);
                }
            }

        }


        /// <summary>
        /// Get all user groups containing Sinamics drives in the current project
        /// </summary>
        /// <returns></returns>
        internal List<DeviceUserGroup> GetAllDriveGroupsInProject()
        {
            return GetDriveGroupsInGroup(refActualProject.DeviceGroups);
        }


        /// <summary>
        /// find  all user groups in a group that contain Sinamics drives
        /// </summary>
        /// <param name="devGroup">the group to search</param>
        /// <returns></returns>
        internal List<DeviceUserGroup> GetDriveGroupsInGroup(DeviceUserGroupComposition devGroup)
        {
            List<DeviceUserGroup> deviceGroups = new List<DeviceUserGroup>();
            foreach (DeviceUserGroup grp in devGroup)
            {
                if (DeviceGroupContainsDriveObject(grp))
                    deviceGroups.Add(grp);
            }
            deviceGroups.TrimExcess();
            return deviceGroups;
        }


        /// <summary>
        /// Get all drive objects in a Sinamics S120 device
        /// </summary>
        /// <param name="headerModule">the header module of the S120 device</param>
        /// <returns></returns>
        internal List<DeviceItem> GetS120DriveObjects(DeviceItem headerModule)
        {
            List<DeviceItem> DriveObjects = new List<DeviceItem>();
            DriveObject drvObject;
            Device S120Device = (Device)headerModule.Parent;
            foreach (DeviceItem itm in S120Device.DeviceItems)
            {
                drvObject = GetDriveObjectService(itm);
                if (drvObject != null)
                    DriveObjects.Add(itm);
            }

            return DriveObjects;
        }


        /// <summary>
        /// Get the head module of a Sinamics G device
        /// </summary>
        /// <param name="GDevice"></param>
        /// <returns></returns>
        internal DeviceItem GetSinamicsGHeadModule(Device GDevice)
        {
            DriveObject drvObject;
            foreach (DeviceItem itm in GDevice.DeviceItems)
            {
                try
                {
                    drvObject = GetDriveObjectService(itm);
                    if (drvObject != null && itm.Classification == DeviceItemClassifications.HM)
                    {
                        return itm;
                    }
                }
                catch
                {

                }
            }
            return null;
        }


        /// <summary>
        /// Get the head module of a Sinamics S120 device
        /// </summary>
        /// <param name="S120Device"></param>
        /// <returns></returns>
        internal DeviceItem GetS120HeadModule(Device S120Device)
        {
            foreach (DeviceItem itm in S120Device.DeviceItems)
            {
                try
                {
                    if (itm.TypeIdentifier.EndsWith("S120") && itm.Classification == DeviceItemClassifications.HM)
                    {
                        return itm;
                    }
                }
                catch
                {

                }
            }
            return null;
        }


        /// <summary>
        /// Check if a user group or one of its subgroups contain a Sinamics device 
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        internal bool DeviceGroupContainsDriveObject(DeviceUserGroup group)
        {
            foreach (Device dev in group.Devices)
            {
                if (IsSinamicsG120Device(dev) || IsSinamicsG110Device(dev) || IsSinamicsS120Device(dev))
                {
                    return true;
                }

            }
            foreach (DeviceUserGroup grp in group.Groups)
                return DeviceGroupContainsDriveObject(grp);

            return false;


        }


        /// <summary>
        /// Check if the given device is a Sinamics G120
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        internal bool IsSinamicsG120Device(Device dev)
        {
            try
            {
                return dev.TypeIdentifier.Contains("Device.G120");
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the given device is a Sinamics G110
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        internal bool IsSinamicsG110Device(Device dev)
        {
            try
            {
                return dev.TypeIdentifier.Contains("Device.G110");
            }
            catch(System.NullReferenceException)
            {
                return false;
            }

        }


        /// <summary>
        ///  Check if the given device is a Sinamics S120
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        internal bool IsSinamicsS120Device(Device dev)
        {
            try
            {
                foreach (DeviceItem itm in dev.DeviceItems)
                {
                    if (itm.Classification == DeviceItemClassifications.HM && itm.TypeIdentifier.EndsWith("S120"))        ///see SinamicsParameterList///
                        return true;
                }
                return false;
            }

            catch
            {
                return false;
            }

        }


        /// <summary>
        /// Get the DriveObject service of a deviceItem for offline operations
        /// </summary>
        /// <param name="deviceItem"></param>
        /// <returns></returns>
        internal DriveObject GetDriveObjectService(DeviceItem deviceItem)
        {
            try
            {
                 return deviceItem.GetService<DriveObjectContainer>().DriveObjects[0];
            }
            catch(System.NullReferenceException)
            {
                return null;
            }
        }


        /// <summary>
        /// Get the OnlineDriveObject service of a deviceItem for online operations
        /// </summary>
        /// <param name="deviceItem"></param>
        /// <returns></returns>
        internal OnlineDriveObject GetDriveObjectOnlineService(DeviceItem deviceItem)
        {
            try
            {
                return deviceItem.GetService<OnlineDriveObjectContainer>().OnlineDriveObjects[0];
            }

            catch
            {
                return null;
            }


        }


        /// <summary>
        /// Go Online
        /// </summary>
        /// <param name="onlineProvider"></param>
        /// <returns></returns>
        internal bool GoOnline(OnlineProvider onlineProvider)
        {
            if (onlineProvider.State != OnlineState.Online)
                onlineProvider.GoOnline();

            return (onlineProvider.State == OnlineState.Online);
        }


        /// <summary>
        /// Go offline
        /// </summary>
        /// <param name="onlineProvider"></param>
        /// <returns></returns>
        internal bool GoOffline(OnlineProvider onlineProvider)
        {
            if (onlineProvider.State != OnlineState.Offline)
                onlineProvider.GoOffline();

            return (onlineProvider.State == OnlineState.Offline);
        }


        #endregion





    }

    internal enum TIAConfirmation
    {
        UNKNOWN,
        RAM2ROM
    }
}