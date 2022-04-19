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

        public static bool newServer = false;

        public MainForm()
        {
            InitializeComponent();
            if (Settings1.Default.licenseShown == false)
            {
                // Show the license dialog if the user has not seen the license dialog yet
                license.ShowDialog();
            }
            MessageBox.Show("This application is very early in development.\nExpect missing functionality and bugs.\nIf you have any issues make sure to open an issue in the github at:\nhttps://github.com/Soniczac7/Minecraft-Server-GUI/issues", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (Settings1.Default.serverPath == null || Settings1.Default.serverPath == "")
            {
                // If the user has not navigated to a server before then show open server dialog
                getServer.ShowDialog();
            }
            if (newServer == true)
            {
                string[] eula = new string[]{
                    "#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).",
                    "#Wed Apr 13 18:25:21 BST 2022",
                    "eula = true"
                };
                string[] defaultProperties = new string[]
                {
                    "#Minecraft server properties",
                    "#Tue Apr 19 14:56:11 BST 2022",
                    "spawn-protection=0",
                    "generator-settings=",
                    "force-gamemode=false",
                    "allow-nether=true",
                    "gamemode=0",
                    "broadcast-console-to-ops=true",
                    "enable-query=false",
                    "player-idle-timeout=0",
                    "difficulty=1",
                    "spawn-monsters=true",
                    "op-permission-level=4",
                    "resource-pack-hash=",
                    "announce-player-achievements=true",
                    "pvp=true",
                    "snooper-enabled=true",
                    "level-type=DEFAULT",
                    "hardcore=false",
                    "enable-command-block=false",
                    "max-players=16",
                    "network-compression-threshold=256",
                    "max-world-size=29999984",
                    "server-port=25565",
                    "debug=false",
                    "server-ip=",
                    "spawn-npcs=true",
                    "allow-flight=false",
                    "level-name=world",
                    "view-distance=10",
                    "resource-pack=",
                    "spawn-animals=true",
                    "white-list=false",
                    "generate-structures=true",
                    "online-mode=true",
                    "max-build-height=256",
                    "level-seed=",
                    "enable-rcon=false",
                    "motd=A Minecraft Server!"

                };
                try
                {
                    FileStream eulaFile = File.Create(Settings1.Default.serverPath + "eula.txt");
                    eulaFile.Close();
                    FileStream propertiesFile = File.Create(Settings1.Default.serverPath + "server.properties");
                    propertiesFile.Close();
                    File.WriteAllLines(Settings1.Default.serverPath + "eula.txt", eula);
                    File.WriteAllLines(Settings1.Default.serverPath + "server.properties", defaultProperties);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    return;
                }
                DialogResult result = MessageBox.Show("Would you like to start your server with default settings?", "Start Server?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    throw new NotImplementedException();
                }
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
                // Process force-gamemode
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
                // Process broadcast-console-to-ops
                string consoletoopSetting = serverProperties[7];
                if (consoletoopSetting.Contains("broadcast-console-to-ops"))
                {
                    Debug.WriteLine("Found broadcast-console-to-ops");
                    string consoletoop;
                    consoletoop = consoletoopSetting.Remove(0, 25);
                    Debug.WriteLine("broadcast-console-to-ops value is " + consoletoop);
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
                // Process enable-query
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
                // Process announce-player-achievements
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
                    // announce-player-achievements is invalid
                    console.AppendText("\n[Error] announce-player-achievements in server.properties contains an invalid value");
                }
                // Process pvp
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
                // Process snooper-enabled
                string snooperenabledSetting = serverProperties[16];
                if (snooperenabledSetting.Contains("snooper-enabled"))
                {
                    Debug.WriteLine("Found snooper-enabled");
                    string snooperenabled;
                    snooperenabled = snooperenabledSetting.Remove(0, 16);
                    Debug.WriteLine("snooper-enabled value is " + snooperenabled);
                    if (snooperenabled == "true")
                    {
                        comboBox13.SelectedIndex = 0;
                    }
                    else if (snooperenabled == "false")
                    {
                        comboBox13.SelectedIndex = 1;
                    }
                    else
                    {
                        // snooper-enabled is invalid
                        console.AppendText("\n[Error] snooper-enabled in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // snooper-enabled line did not contain "snooper-enabled"
                    Debug.WriteLine("Failed to find snooper-enabled");
                    console.AppendText("\n[Error] Failed to find snooper-enabled in server.properties");
                }
                // Process level-type
                string leveltypeSetting = serverProperties[17];
                if (leveltypeSetting.Contains("level-type"))
                {
                    Debug.WriteLine("Found leveltype");
                    string leveltype;
                    leveltype = leveltypeSetting.Remove(0, 11);
                    Debug.WriteLine("level-type value is " + leveltype);
                    if (leveltype == "DEFAULT")
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (leveltype == "FLAT")
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    else if (leveltype == "AMPLIFIED")
                    {
                        comboBox1.SelectedIndex = 2;
                    }
                    else
                    {
                        // level-type is invalid
                        console.AppendText("\n[Error] level-type in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // level-type line did not contain "level-type"
                    Debug.WriteLine("Failed to find level-type");
                    console.AppendText("\n[Error] Failed to find level-type in server.properties");
                }
                // Process hardcore
                string hardcoreSetting = serverProperties[18];
                if (hardcoreSetting.Contains("hardcore"))
                {
                    Debug.WriteLine("Found hardcore");
                    string hardcore;
                    hardcore = hardcoreSetting.Remove(0, 9);
                    Debug.WriteLine("hardcore value is " + hardcore);
                    if (hardcore == "true")
                    {
                        comboBox7.SelectedIndex = 0;
                    }
                    else if (hardcore == "false")
                    {
                        comboBox7.SelectedIndex = 1;
                    }
                    else
                    {
                        // hardcore is invalid
                        console.AppendText("\n[Error] hardcore in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // hardcore line did not contain "hardcore"
                    Debug.WriteLine("Failed to find hardcore");
                    console.AppendText("\n[Error] Failed to find hardcore in server.properties");
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
