using System.Diagnostics;

namespace Minecraft_Server_GUI
{
    public partial class MainForm : Form
    {
        #region Form Definition
        License license = new License();
        GetServer getServer = new GetServer();
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
            if (Settings1.Default.licenseShown == false)
            {
                // Show the license dialogue if the user has not seen the license dialogue yet
                license.ShowDialog();
            }
            MessageBox.Show("This application is very early in development.\nExpect missing functionality and bugs.\nIf you have any issues make sure to open an issue in the github at:\nhttps://github.com/Soniczac7/Minecraft-Server-GUI/issues", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (Settings1.Default.serverPath == null || Settings1.Default.serverPath == "")
            {
                // If the user has not navigated to a server before then show open server dialogue
                getServer.ShowDialog();
            }
            if (Settings1.Default.serverPath == null || Settings1.Default.serverPath == "")
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
                    console.AppendText("\n[Error] Failed to find spawn-protection in server.properties");
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
                    console.AppendText("\n[Error] Failed to find generator-settings in server.properties");
                }
                // Proccess force-gamemode
                string forcegamemodeSetting = serverProperties[4];
                if (forcegamemodeSetting.Contains("force-gamemode"))
                {
                    Debug.WriteLine("Found force-gamemode");
                    string forcegamemode;
                    forcegamemode = forcegamemodeSetting.Remove(0, 15);
                    Debug.WriteLine("force-gamemode value is " + forcegamemode);
                    if (forcegamemode == "true")
                    {
                        comboBox17.SelectedIndex = 0;
                    }
                    else if (forcegamemode == "false")
                    {
                        comboBox17.SelectedIndex = 1;
                    }
                    else
                    {
                        // force-gamemode is invalid
                        console.AppendText("\n[Error] force-gamemode in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // force-gamemode line did not contain "force-gamemode"
                    Debug.WriteLine("Failed to find force-gamemode");
                    console.AppendText("\n[Error] Failed to find force-gamemode in server.properties");
                }
                // Process allow-nether
                string allownetherSetting = serverProperties[5];
                if (allownetherSetting.Contains("allow-nether"))
                {
                    Debug.WriteLine("Found allow-nether");
                    string allownether;
                    allownether = allownetherSetting.Remove(0, 13);
                    Debug.WriteLine("allow-nether value is " + allownether);
                    if (allownether == "true")
                    {
                        comboBox3.SelectedIndex = 0;
                    }
                    else if (allownether == "false")
                    {
                        comboBox3.SelectedIndex = 1;
                    }
                    else
                    {
                        // allow-nether is invalid
                        console.AppendText("\n[Error] allow-nether in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // allow-nether line did not contain "allow-nether"
                    Debug.WriteLine("Failed to find allow-nether");
                    console.AppendText("\n[Error] Failed to find allow-nether in server.properties");
                }
                // Process gamemode
                string gamemodeSetting = serverProperties[6];
                if (gamemodeSetting.Contains("gamemode"))
                {
                    Debug.WriteLine("Found gamemode");
                    string gamemode;
                    gamemode = gamemodeSetting.Remove(0, 9);
                    Debug.WriteLine("gamemode value is " + gamemode);
                    if (gamemode == "0")
                    {
                        numericUpDown10.Value = 0;
                    }
                    else if (gamemode == "1")
                    {
                        numericUpDown10.Value = 1;
                    }
                    else if (gamemode == "2")
                    {
                        numericUpDown10.Value = 2;
                    }
                    else if (gamemode == "3")
                    {
                        numericUpDown10.Value = 3;
                    }
                    else
                    {
                        // gamemode is invalid
                        console.AppendText("\n[Error] gamemode in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // gamemode line did not contain "gamemode"
                    Debug.WriteLine("Failed to find gamemode");
                    console.AppendText("\n[Error] Failed to find gamemode in server.properties");
                }
                // Proccess broadcast-console-to-ops
                string consoletoopSetting = serverProperties[7];
                if (consoletoopSetting.Contains("broadcast-console-to-ops"))
                {
                    Debug.WriteLine("Found broadcast-console-to-ops");
                    string consoletoop;
                    consoletoop = consoletoopSetting.Remove(0, 25);
                    Debug.WriteLine("force-gamemode value is " + consoletoop);
                    if (consoletoop == "true")
                    {
                        comboBox14.SelectedIndex = 0;
                    }
                    else if (consoletoop == "false")
                    {
                        comboBox14.SelectedIndex = 1;
                    }
                    else
                    {
                        // broadcast-console-to-ops is invalid
                        console.AppendText("\n[Error] broadcast-console-to-ops in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // broadcast-console-to-ops line did not contain "broadcast-console-to-ops"
                    Debug.WriteLine("Failed to find broadcast-console-to-ops");
                    console.AppendText("\n[Error] Failed to find broadcast-console-to-ops in server.properties");
                }
                // Proccess enable-query
                string enablequerySetting = serverProperties[8];
                if (enablequerySetting.Contains("enable-query"))
                {
                    Debug.WriteLine("Found enable-query");
                    string enablequery;
                    enablequery = enablequerySetting.Remove(0, 13);
                    Debug.WriteLine("enable-query value is " + enablequery);
                    if (enablequery == "true")
                    {
                        comboBox10.SelectedIndex = 0;
                    }
                    else if (enablequery == "false")
                    {
                        comboBox10.SelectedIndex = 1;
                    }
                    else
                    {
                        // enable-query is invalid
                        console.AppendText("\n[Error] enable-query in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // enable-query line did not contain "enable-query"
                    Debug.WriteLine("Failed to find enable-query");
                    console.AppendText("\n[Error] Failed to find enable-query in server.properties");
                }
                // Process player-idle-timeout
                string playeridletimeoutSetting = serverProperties[9];
                if (playeridletimeoutSetting.Contains("player-idle-timeout"))
                {
                    Debug.WriteLine("Found player-idle-timeout");
                    string playeridletimeout;
                    playeridletimeout = playeridletimeoutSetting.Remove(0, 20);
                    Debug.WriteLine("player-idle-timeout value is " + playeridletimeout);
                    decimal playeridletimeoutValue = Convert.ToDecimal(playeridletimeout);
                    numericUpDown11.Value = playeridletimeoutValue;
                }
                else
                {
                    // player-idle-timeout line did not contain "player-idle-timeout"
                    Debug.WriteLine("Failed to find player-idle-timeout");
                    console.AppendText("\n[Error] Failed to find player-idle-timeout in server.properties");
                }
                // Process difficulty
                string difficultySetting = serverProperties[10];
                if (difficultySetting.Contains("difficulty"))
                {
                    Debug.WriteLine("Found difficulty");
                    string difficulty;
                    difficulty = difficultySetting.Remove(0, 11);
                    Debug.WriteLine("difficulty value is " + difficulty);
                    decimal difficultyValue = Convert.ToDecimal(difficulty);
                    numericUpDown3.Value = difficultyValue;
                }
                else
                {
                    // difficulty line did not contain "difficulty"
                    Debug.WriteLine("Failed to find difficulty");
                    console.AppendText("\n[Error] Failed to find difficulty in server.properties");
                }
                // Process spawn-monsters
                string spawnmonstersSetting = serverProperties[11];
                if (spawnmonstersSetting.Contains("spawn-monsters"))
                {
                    Debug.WriteLine("Found spawn-monsters");
                    string spawnmonsters;
                    spawnmonsters = spawnmonstersSetting.Remove(0, 15);
                    Debug.WriteLine("spawn-monsters value is " + spawnmonsters);
                    if (spawnmonsters == "true")
                    {
                        comboBox4.SelectedIndex = 0;
                    }
                    else if (spawnmonsters == "false")
                    {
                        comboBox4.SelectedIndex = 1;
                    }
                    else
                    {
                        // spawn-monsters is invalid
                        console.AppendText("\n[Error] spawn-monsters in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // spawn-monsters line did not contain "spawn-monsters"
                    Debug.WriteLine("Failed to find spawn-monsters");
                    console.AppendText("\n[Error] Failed to find spawn-monsters in server.properties");
                }
                // Process op-permission-level
                string oppermissionlevelSetting = serverProperties[12];
                if (oppermissionlevelSetting.Contains("op-permission-level"))
                {
                    Debug.WriteLine("Found op-permission-level");
                    string oppermissionlevel;
                    oppermissionlevel = oppermissionlevelSetting.Remove(0, 20);
                    Debug.WriteLine("op-permission-level value is " + oppermissionlevel);
                    decimal oppermissionlevelValue = Convert.ToDecimal(oppermissionlevel);
                    numericUpDown7.Value = oppermissionlevelValue;
                }
                else
                {
                    // op-permission-level is invalid
                    Debug.WriteLine(serverProperties[12]);
                    console.AppendText("\n[Error op-permission-level in server.properties contains an invalid value");
                }
                // Process resource-pack-hash
                string resourcepackhashSetting = serverProperties[13];
                if (resourcepackhashSetting.Contains("resource-pack-hash"))
                {
                    Debug.WriteLine("Found resource-pack-hash");
                    string resourcepackhash;
                    resourcepackhash = resourcepackhashSetting.Remove(0, 19);
                    Debug.WriteLine("resource-pack-hash value is " + resourcepackhash);
                    textBox7.Text = resourcepackhash;
                }
                else
                {
                    // resource-pack-hash line did not contain "resource-pack-hash"
                    Debug.WriteLine("Failed to find resource-pack-hash");
                    console.AppendText("\n[Error] Failed to find resource-pack-hash in server.properties");
                }
                // Proccess announce-player-achivements
                string announceplayerachivementsSetting = serverProperties[14];
                if (announceplayerachivementsSetting.Contains("announce-player-achievements"))
                {
                    Debug.WriteLine("Found announce-player-achievements");
                    string announceplayerachivements;
                    announceplayerachivements = announceplayerachivementsSetting.Remove(0, 29);
                    Debug.WriteLine("announce-player-achievements value is " + announceplayerachivements);
                    if (announceplayerachivements == "true")
                    {
                        comboBox16.SelectedIndex = 0;
                    }
                    else if (announceplayerachivements == "false")
                    {
                        comboBox16.SelectedIndex = 1;
                    }
                }
                else
                {
                    // announce-player-achivements is invalid
                    console.AppendText("\n[Error] announce-player-achivements in server.properties contains an invalid value");
                }
                // Proccess pvp
                string pvpSetting = serverProperties[15];
                if (pvpSetting.Contains("pvp"))
                {
                    Debug.WriteLine("Found pvp");
                    string pvp;
                    pvp = pvpSetting.Remove(0, 4);
                    Debug.WriteLine("pvp value is " + pvp);
                    if (pvp == "true")
                    {
                        comboBox5.SelectedIndex = 0;
                    }
                    else if (pvp == "false")
                    {
                        comboBox5.SelectedIndex = 1;
                    }
                    else
                    {
                        // pvp is invalid
                        console.AppendText("\n[Error] pvp in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // pvp line did not contain "pvp"
                    Debug.WriteLine("Failed to find pvp");
                    console.AppendText("\n[Error] Failed to find pvp in server.properties");
                }
            }
            catch (Exception ex)
            {
                console.AppendText("\n[Error] " + ex.Message);
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Show the settings form
            Settings settings = new Settings();
            settings.Show();
        }
    }
}
