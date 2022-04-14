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
        GetServer getServer = new GetServer();

        public static string? serverPath;

        public MainForm()
        {
            InitializeComponent();
            if(serverPath == null)
            {
                getServer.ShowDialog();
            }
        }
    }
}
