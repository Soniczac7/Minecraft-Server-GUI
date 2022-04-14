using System.Diagnostics;
using System.Windows.Forms;

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
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                MainForm.serverPath = folderBrowserDialog1.SelectedPath;
                this.Close();

            }
            else
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}