using System;
using System.Windows.Forms;

namespace StartDriveParameterEditor
{
    internal partial class RamToRomDialog : Form
    {
        private ApplicationSettingsServer SettingsServer;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="settings"></param>
        internal RamToRomDialog(ref ApplicationSettingsServer settings)
        {
            InitializeComponent();
            SettingsServer = settings;
            Text = SettingsServer.AppSettings.ApplicationTitle;
        }



        /// <summary>
        /// Save confirmation setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RamToRomDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            SettingsServer.AppSettings.ConfirmRamToRom = !AskAgainCheckbox.Checked;
        }
    }
}