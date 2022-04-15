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
        #region Form Definition
        License license = new License();
        GetServer getServer = new GetServer();
        Settings settings = new Settings();
        #endregion

        #region Server Settings
        string spawnprotection = "16";
        string generatorsettings = "";
        string forcegamemode = "false";
        string allownether = "true";
        string gamemode = "0";
        string broadcastconsoletoops = "true";
        string enablequery = "false";
        string playeridletimeout = "0";
        string difficulty = "1";
        string spawnmonsters = "true";
        string oppermissionlevel = "4";
        string resourcepackhash = "";
        string announceplayerachievements = "true";
        string pvp = "true";
        string snooperenabled = "true";
        string leveltype = "DEFAULT";
        string hardcore = "false";
        string enablecommandblock = "false";
        string maxplayers = "20";
        string networkcompressionthreshold = "256";
        string maxworldsize = "29999984";
        string serverport = "25565";
        string debug = "false";
        string serverip = "";
        string spawnnpcs = "true";
        string allowflight = "false";
        string levelname = "world";
        string viewdistance = "10";
        string resourcepack = "";
        string spawnanimals = "true";
        string whitelist = "false";
        string generatestructures = "true";
        string onlinemode = "true";
        string maxbuildheight = "256";
        string levelseed = "";
        string enablercon = "false";
        string motd = "";
        #endregion

        public MainForm()
        {
            InitializeComponent();
            if(Settings1.Default.licenseShown == false)
            {
                // Show the license dialogue if the user has not seen the license dialogue yet
                license.ShowDialog();
            }
            if (Settings1.Default.serverPath == null || Settings1.Default.serverPath == "")
            {
                // If the user has not navigated to a server before then show open server dialogue
                getServer.ShowDialog();
            }
            if(Settings1.Default.serverPath == null || Settings1.Default.serverPath == "")
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripStatusLabel1.Text = "Done!";
                return;
            }
            toolStripStatusLabel1.Text = "Loading: " + Settings1.Default.serverPath + "server.properties";


        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Show the settings form
            settings.Show();
        }
    }
}
