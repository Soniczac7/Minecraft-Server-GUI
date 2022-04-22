namespace Minecraft_Server_GUI
{
    public partial class GetServer : Form
    {
        public GetServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Existing Server
            folderBrowserDialog1.Description = "Select the folder that contains your server.";
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings1.Default.serverPath = folderBrowserDialog1.SelectedPath + @"\";
                Settings1.Default.Save();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // New Server
            DialogResult result = MessageBox.Show("The only supported server version at the moment is Paper 1.8.8\nWould you like to continue?", "Minecraft Server GUI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Continue
                string location;
                folderBrowserDialog1.Description = "Choose where you would like to contain your server.";
                result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    location = folderBrowserDialog1.SelectedPath + @"\server.jar";
                    Settings1.Default.serverPath = folderBrowserDialog1.SelectedPath + @"\";
                    Settings1.Default.Save();
                }
                else
                {
                    return;
                }
                Download download = new Download("https://papermc.io/api/v2/projects/paper/versions/1.8.8/builds/445/downloads/paper-1.8.8-445.jar", location);
                download.ShowDialog();
                MainForm.newServer = true;
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Cancel
            this.Close();
        }
    }
}