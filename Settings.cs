using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minecraft_Server_GUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Reset application settings
            Settings1.Default.Reset();
            Settings1.Default.Save();
            // Start another instance of the application
            ProcessStartInfo restartInfo = new ProcessStartInfo();
            restartInfo.UseShellExecute = true;
            restartInfo.ErrorDialog = true;
            restartInfo.FileName = "Minecraft Server GUI.exe";
            restartInfo.Arguments = "";
            Process restart = new Process();
            restart.StartInfo = restartInfo;
            restart.Start();
            // Stop the current instance of the application
            Application.Exit();
        }
    }
}
