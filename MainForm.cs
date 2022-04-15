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

        public MainForm()
        {
            InitializeComponent();
            if(Settings1.Default.licenseShown == false)
            {
                license.ShowDialog();
                
            }
            if (Settings1.Default.serverPath == null)
            {
                getServer.ShowDialog();
            }
        }
    }
}
