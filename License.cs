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
    public partial class License : Form
    {
        public License()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Save license settings
            Settings1.Default.licenseShown = true;
            Settings1.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Exit the application
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Checkbox toggle check
            if(checkBox1.Checked == true)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
