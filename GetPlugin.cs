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
    public partial class GetPlugin : Form
    {
        public string pluginPath;
        public GetPlugin()
        {
            InitializeComponent();
        }

        private void GetPlugin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Close Window
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Browse For A Plugin

            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                pluginPath = folderBrowserDialog1.SelectedPath + @"\";

                if (File.Exists(pluginPath + @"src\plugin.yml"))
                {
                    DialogResult msgboxResult = MessageBox.Show("This Is A Real Plugin! Press Ok To Continue :)");
                    if (msgboxResult == DialogResult.OK)
                    {
                        MessageBox.Show("Test");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("This Is Not A Real Plugin :(");
                }
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            
        }
    }
}
