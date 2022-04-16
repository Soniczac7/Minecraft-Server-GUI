using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        /*
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
        */
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
                // If there is no server specified then finish loading
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripStatusLabel1.Text = "Done!";
                return;
            }
            // A server has been specified and loading will continue
            toolStripStatusLabel1.Text = "Loading: " + Settings1.Default.serverPath + "server.properties";
            console.AppendText("[Server] Reading server.properties");
            try
            {
                // Read all lines of server.properties
                string[] serverProperties = File.ReadAllLines(Settings1.Default.serverPath + "server.properties");
                // Process spawn-protection
                string spawnprotectionSetting = serverProperties[2];
                if (spawnprotectionSetting.Contains("spawn-protection"))
                {
                    Debug.WriteLine("Found spawn-protection");
                    string spawnprotection;
                    spawnprotection = spawnprotectionSetting.Remove(0, 17);
                    Debug.WriteLine("spawn-protection value is " + spawnprotection);
                    decimal spawnprotectionValue = Convert.ToDecimal(spawnprotection);
                    numericUpDown9.Value = spawnprotectionValue;
                }
                else
                {
                    // spawn-protection line did not contain "spawn-protection"
                    Debug.WriteLine("Failed to find spawn-protection");
                    console.AppendText("[Error] Failed to find spawn-protection in server.properties");
                }
                // Process generator-settings
                string generatorsettingsSetting = serverProperties[3];
                if (generatorsettingsSetting.Contains("generator-settings"))
                {
                    Debug.WriteLine("Found generator-settings");
                    string generatorsettings;
                    generatorsettings = generatorsettingsSetting.Remove(0, 19);
                    Debug.WriteLine("generator-settings value is " + generatorsettings);
                    textBox3.Text = generatorsettings;
                }
                else
                {
                    // generator-settings line did not contain "generator-settings"
                    Debug.WriteLine("Failed to find generator-settings");
                    console.AppendText("[Error] Failed to find generator-settings in server.properties");
                }
                // Proccess force-gamemode
                string forcegamemodeSetting = serverProperties[4];
                if (forcegamemodeSetting.Contains("force-gamemode"))
                {
                    Debug.WriteLine("Found force-gamemode");
                    string forcegamemode;
                    forcegamemode = forcegamemodeSetting.Remove(0, 15);
                    Debug.WriteLine("force-gamemode value is " + forcegamemode);
                    if(forcegamemode == "true")
                    {
                        comboBox17.SelectedIndex = 0;
                    }
                    else if(forcegamemode == "false")
                    {
                        comboBox17.SelectedIndex = 1;
                    }
                    else
                    {
                        // force-gamemode is invalid
                        console.AppendText("[Error] force-gamemode in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // force-gamemode line did not contain "force-gamemode"
                    Debug.WriteLine("Failed to find force-gamemode");
                    console.AppendText("[Error] Failed to find force-gamemode in server.properties");
                }
            }
            catch(Exception ex)
            {
                console.AppendText("[Error] " + ex.Message);
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Show the settings form
            settings.Show();
        }
    }
}
