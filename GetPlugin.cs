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
        public string pluginName;
        public string pluginVersion;
        public string pluginAuthor;
        public static GetPlugin getPlugin;
        public GetPlugin()
        {
            InitializeComponent();
            getPlugin = this;
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
                        string[] pluginNameTemp;
                        pluginNameTemp = folderBrowserDialog1.SelectedPath.Split(@"\");
                        pluginName = pluginNameTemp[pluginNameTemp.Length - 1];
                        PluginMain pluginMain = new PluginMain();
                        pluginMain.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            CreatePlugin createPlugin = new CreatePlugin();
            createPlugin.Show();
            this.Close();
        }
    }
}
