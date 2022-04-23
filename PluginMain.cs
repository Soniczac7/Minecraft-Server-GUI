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
    public partial class PluginMain : Form
    {
        public string pluginName;
        public string pluginVersion;
        public string pluginAuthor;
        public PluginMain()
        {
            InitializeComponent();
            pluginName = GetPlugin.getPlugin.pluginName;
            label1.Text = pluginName;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void PvInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void PluginVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
