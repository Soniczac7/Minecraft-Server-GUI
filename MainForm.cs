using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Server_GUI
{
    public partial class MainForm : Form
    {
        License license = new License();
        GetServer getServer = new GetServer();
        Settings settings = new Settings();

        public MainForm()
        {
            InitializeComponent();
            if(Settings1.Default.licenseShown == false)
            {
                // Show the license dialogue if the user has not seen the license dialogue yet
                license.ShowDialog();
            }
            if (Settings1.Default.serverPath == null || Settings1.Default.serverPath == "")
            {
                // If the user has not navigated to a server before then show open server dialogue
                getServer.ShowDialog();
            }
            toolStripStatusLabel1.Text = "Loading server at: " + Settings1.Default.serverPath;
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Show the settings form
            settings.Show();
        }
    }
}
