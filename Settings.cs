using System.Diagnostics;

namespace Minecraft_Server_GUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            if (Settings1.Default.startServerOnStart)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Changed start server on start
            if (checkBox1.Checked)
            {
                Settings1.Default.startServerOnStart = true;
            }
            else
            {
                Settings1.Default.startServerOnStart = false;
            }
            Settings1.Default.Save();
        }
    }
}
