using System.Diagnostics;

namespace Minecraft_Server_GUI
{
    public partial class MainForm : Form
    {
        #region Form Definition
        AboutBox about = new AboutBox();
        License license = new License();
        GetServer getServer = new GetServer();
        #endregion

        #region Other Definitions
        StreamWriter? inputWriter;
        public static bool newServer = false;
        private static bool startServerOnStart = false;
        public static string serverPath = Settings1.Default.serverPath;
        #endregion

        public MainForm(bool forceGetValue)
        {
            InitializeComponent();
            if (!Settings1.Default.licenseShown)
            {
                // Show the license dialog if the user has not seen the license dialog yet
                license.ShowDialog();
            }
            if (!Settings1.Default.startServerOnStart)
            {
                MessageBox.Show("This application is very early in development.\nExpect missing functionality and bugs.\nIf you have any issues make sure to open an issue in the github at:\nhttps://github.com/Soniczac7/Minecraft-Server-GUI/issues", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                startServerOnStart = true;
            }
            if (serverPath == null || serverPath == "")
            {
                // If the user has not navigated to a server before then show open server dialog
                getServer.ShowDialog();
                GetServer.serverPath = serverPath;
                GetServer.newServer = newServer;
            }
            Initialize();
        }

        internal void Initialize()
        {
            if (newServer)
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
                    FileStream eulaFile = File.Create(serverPath + "eula.txt");
                    eulaFile.Close();
                    FileStream propertiesFile = File.Create(serverPath + "server.properties");
                    propertiesFile.Close();
                    File.WriteAllLines(serverPath + "eula.txt", eula);
                    File.WriteAllLines(serverPath + "server.properties", defaultProperties);
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
                    startServerOnStart = true;
                }
            }
            if (serverPath == null || serverPath == "")
            {
                // If there is no server specified then finish loading
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                toolStripStatusLabel1.Text = "Done!";
                return;
            }
            // A server has been specified and loading will continue
            toolStripStatusLabel1.Text = "Loading: " + serverPath + "server.properties";
            console.AppendText("[Server] Reading server.properties");
            try
            {
                // Read all lines of server.properties
                string[] serverProperties = File.ReadAllLines(serverPath + "server.properties");
                // Process spawn-protection
                string spawnprotectionSetting = serverProperties[2];
                if (spawnprotectionSetting.Contains("spawn-protection"))
                {
                    Debug.WriteLine("Found spawn-protection");
                    string spawnprotection;
                    spawnprotection = spawnprotectionSetting.Remove(0, 17);
                    Debug.WriteLine("spawn-protection value is " + spawnprotection);
                    decimal spawnprotectionValue = Convert.ToDecimal(spawnprotection);
                    spawnProtection.Value = spawnprotectionValue;
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
                    generatorSettings.Text = generatorsettings;
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
                        forceGamemode.SelectedIndex = 0;
                    }
                    else if (forcegamemode == "false")
                    {
                        forceGamemode.SelectedIndex = 1;
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
                        allowNether.SelectedIndex = 0;
                    }
                    else if (allownether == "false")
                    {
                        allowNether.SelectedIndex = 1;
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
                        this.gamemode.Value = 0;
                    }
                    else if (gamemode == "1")
                    {
                        this.gamemode.Value = 1;
                    }
                    else if (gamemode == "2")
                    {
                        this.gamemode.Value = 2;
                    }
                    else if (gamemode == "3")
                    {
                        this.gamemode.Value = 3;
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
                        broadcastConsoleToOps.SelectedIndex = 0;
                    }
                    else if (consoletoop == "false")
                    {
                        broadcastConsoleToOps.SelectedIndex = 1;
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
                        enableQuery.SelectedIndex = 0;
                    }
                    else if (enablequery == "false")
                    {
                        enableQuery.SelectedIndex = 1;
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
                    playerIdleTimeout.Value = playeridletimeoutValue;
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
                    this.difficulty.Value = difficultyValue;
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
                        spawnMonsters.SelectedIndex = 0;
                    }
                    else if (spawnmonsters == "false")
                    {
                        spawnMonsters.SelectedIndex = 1;
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
                    opPermLvl.Value = oppermissionlevelValue;
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
                    resourcePackHash.Text = resourcepackhash;
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
                        achivements.SelectedIndex = 0;
                    }
                    else if (announceplayerachivements == "false")
                    {
                        achivements.SelectedIndex = 1;
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
                        this.pvp.SelectedIndex = 0;
                    }
                    else if (pvp == "false")
                    {
                        this.pvp.SelectedIndex = 1;
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
                        snooperEnabled.SelectedIndex = 0;
                    }
                    else if (snooperenabled == "false")
                    {
                        snooperEnabled.SelectedIndex = 1;
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
                        worldType.SelectedIndex = 0;
                    }
                    else if (leveltype == "FLAT")
                    {
                        worldType.SelectedIndex = 1;
                    }
                    else if (leveltype == "AMPLIFIED")
                    {
                        worldType.SelectedIndex = 2;
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
                        this.hardcore.SelectedIndex = 0;
                    }
                    else if (hardcore == "false")
                    {
                        this.hardcore.SelectedIndex = 1;
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
                // Process enable-command-block
                string commandblocksSetting = serverProperties[19];
                if (commandblocksSetting.Contains("enable-command-block"))
                {
                    Debug.WriteLine("Found enable-command-block");
                    string commandblocks;
                    commandblocks = commandblocksSetting.Remove(0, 21);
                    Debug.WriteLine("enable-command-block value is " + commandblocks);
                    if (commandblocks == "true")
                    {
                        commandBlocks.SelectedIndex = 0;
                    }
                    else if (commandblocks == "false")
                    {
                        commandBlocks.SelectedIndex = 1;
                    }
                    else
                    {
                        // enable-command-block is invalid
                        console.AppendText("\n[Error] enable-command-block in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // enable-command-block line did not contain "enable-command-block"
                    Debug.WriteLine("Failed to find enable-command-block");
                    console.AppendText("\n[Error] Failed to find enable-command-block in server.properties");
                }
                // Process max-players
                string maxplayersSetting = serverProperties[20];
                if (maxplayersSetting.Contains("max-players"))
                {
                    Debug.WriteLine("Found max-players");
                    string maxplayers;
                    maxplayers = maxplayersSetting.Remove(0, 12);
                    Debug.WriteLine("max-players value is " + maxplayers);
                    decimal maxplayersValue = Convert.ToDecimal(maxplayers);
                    maxPlayers.Value = maxplayersValue;
                }
                else
                {
                    // max-players line did not contain "max-players"
                    Debug.WriteLine("Failed to find max-players");
                    console.AppendText("\n[Error] Failed to find max-players in server.properties");
                }
                // Process network-compression-threshold
                string networkcompressionthresholdSetting = serverProperties[21];
                if (networkcompressionthresholdSetting.Contains("network-compression-threshold"))
                {
                    Debug.WriteLine("Found network-compression-threshold");
                    string networkcompressionthreshold;
                    networkcompressionthreshold = networkcompressionthresholdSetting.Remove(0, 30);
                    Debug.WriteLine("network-compression-threshold value is " + networkcompressionthreshold);
                    decimal networkcompressionthresholdValue = Convert.ToDecimal(networkcompressionthreshold);
                    netCompression.Value = networkcompressionthresholdValue;
                }
                else
                {
                    // network-compression-threshold line did not contain "network-compression-threshold"
                    Debug.WriteLine("Failed to find network-compression-threshold");
                    console.AppendText("\n[Error] Failed to find network-compression-threshold in server.properties");
                }
                // Process max-world-size
                string maxworldsizeSetting = serverProperties[22];
                if (maxworldsizeSetting.Contains("max-world-size"))
                {
                    Debug.WriteLine("Found max-world-size");
                    string maxworldsize;
                    maxworldsize = maxworldsizeSetting.Remove(0, 15);
                    Debug.WriteLine("max-world-size value is " + maxworldsize);
                    decimal maxworldsizeValue = Convert.ToDecimal(maxworldsize);
                    maxWorldSize.Value = maxworldsizeValue;
                }
                else
                {
                    // max-world-size line did not contain "max-world-size"
                    Debug.WriteLine("Failed to find max-world-size");
                    console.AppendText("\n[Error] Failed to find max-world-size in server.properties");
                }
                // Process server-port
                string portSetting = serverProperties[23];
                if (portSetting.Contains("server-port"))
                {
                    Debug.WriteLine("Found server-port");
                    string port;
                    port = portSetting.Remove(0, 12);
                    Debug.WriteLine("server-port value is " + port);
                    decimal portValue = Convert.ToDecimal(port);
                    serverPort.Value = portValue;
                }
                else
                {
                    // server-port line did not contain "server-port"
                    Debug.WriteLine("Failed to find server-port");
                    console.AppendText("\n[Error] Failed to find server-port in server.properties");
                }
                // Process debug
                string debugSetting = serverProperties[24];
                if (debugSetting.Contains("debug"))
                {
                    Debug.WriteLine("Found debug");
                    string debug;
                    debug = debugSetting.Remove(0, 6);
                    Debug.WriteLine("debug value is " + debug);
                    if (debug == "true")
                    {
                        this.debug.SelectedIndex = 0;
                    }
                    else if (debug == "false")
                    {
                        this.debug.SelectedIndex = 1;
                    }
                    else
                    {
                        // debug is invalid
                        console.AppendText("\n[Error] debug in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // debug line did not contain "debug"
                    Debug.WriteLine("Failed to find debug");
                    console.AppendText("\n[Error] Failed to find debug in server.properties");
                }
                // Process server-ip
                string ipSetting = serverProperties[25];
                if (ipSetting.Contains("server-ip"))
                {
                    Debug.WriteLine("Found server-ip");
                    string ip;
                    ip = ipSetting.Remove(0, 10);
                    Debug.WriteLine("server-ip value is " + ip);
                    serverIP.Text = ip;
                }
                else
                {
                    // server-ip line did not contain "server-ip"
                    Debug.WriteLine("Failed to find server-ip");
                    console.AppendText("\n[Error] Failed to find server-ip in server.properties");
                }
                // Process spawn-npcs
                string npcsSetting = serverProperties[26];
                if (npcsSetting.Contains("spawn-npcs"))
                {
                    Debug.WriteLine("Found spawn-npcs");
                    string npcs;
                    npcs = npcsSetting.Remove(0, 11);
                    Debug.WriteLine("spawn-npcs value is " + npcs);
                    if (npcs == "true")
                    {
                        spawnNpcs.SelectedIndex = 0;
                    }
                    else if (npcs == "false")
                    {
                        spawnNpcs.SelectedIndex = 1;
                    }
                    else
                    {
                        // spawn-npcs is invalid
                        console.AppendText("\n[Error] spawn-npcs in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // spawn-npcs line did not contain "spawn-npcs"
                    Debug.WriteLine("Failed to find spawn-npcs");
                    console.AppendText("\n[Error] Failed to find spawn-npcs in server.properties");
                }
                // Process allow-flight
                string allowflightSetting = serverProperties[27];
                if (allowflightSetting.Contains("allow-flight"))
                {
                    Debug.WriteLine("Found allow-flight");
                    string allowflight;
                    allowflight = allowflightSetting.Remove(0, 13);
                    Debug.WriteLine("allow-flight value is " + allowflight);
                    if (allowflight == "true")
                    {
                        allowFlight.SelectedIndex = 0;
                    }
                    else if (allowflight == "false")
                    {
                        allowFlight.SelectedIndex = 1;
                    }
                    else
                    {
                        // allow-flight is invalid
                        console.AppendText("\n[Error] allow-flight in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // allow-flight line did not contain "allow-flight"
                    Debug.WriteLine("Failed to find allow-flight");
                    console.AppendText("\n[Error] Failed to find allow-flight in server.properties");
                }
                // Process level-name
                string levelnameSetting = serverProperties[28];
                if (levelnameSetting.Contains("level-name"))
                {
                    Debug.WriteLine("Found level-name");
                    string levelname;
                    levelname = levelnameSetting.Remove(0, 11);
                    Debug.WriteLine("level-name value is " + levelname);
                    worldName.Text = levelname;
                }
                else
                {
                    // level-name line did not contain "level-name"
                    Debug.WriteLine("Failed to find level-name");
                    console.AppendText("\n[Error] Failed to find level-name in server.properties");
                }
                // Process view-distance
                string viewSetting = serverProperties[29];
                if (viewSetting.Contains("view-distance"))
                {
                    Debug.WriteLine("Found view-distance");
                    string view;
                    view = viewSetting.Remove(0, 14);
                    Debug.WriteLine("view-distance value is " + view);
                    decimal viewValue = Convert.ToDecimal(view);
                    viewDistance.Value = viewValue;
                }
                else
                {
                    // view-distance line did not contain "view-distance"
                    Debug.WriteLine("Failed to find view-distance");
                    console.AppendText("\n[Error] Failed to find view-distance in server.properties");
                }
                // Process resource-pack
                string resourceSetting = serverProperties[30];
                if (resourceSetting.Contains("resource-pack"))
                {
                    Debug.WriteLine("Found resource-pack");
                    string resource;
                    resource = resourceSetting.Remove(0, 14);
                    Debug.WriteLine("resource-pack value is " + resource);
                    resourcePack.Text = resource;
                }
                else
                {
                    // resource-pack line did not contain "resource-pack"
                    Debug.WriteLine("Failed to find resource-pack");
                    console.AppendText("\n[Error] Failed to find resource-pack in server.properties");
                }
                // Process spawn-animals
                string spawnanimalsSetting = serverProperties[31];
                if (spawnanimalsSetting.Contains("spawn-animals"))
                {
                    Debug.WriteLine("Found spawn-animals");
                    string spawnanimals;
                    spawnanimals = spawnanimalsSetting.Remove(0, 14);
                    Debug.WriteLine("spawn-animals value is " + spawnanimals);
                    if (spawnanimals == "true")
                    {
                        spawnAnimals.SelectedIndex = 0;
                    }
                    else if (spawnanimals == "false")
                    {
                        spawnAnimals.SelectedIndex = 1;
                    }
                    else
                    {
                        // spawn-animals is invalid
                        console.AppendText("\n[Error] spawn-animals in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // spawn-animals line did not contain "spawn-animals"
                    Debug.WriteLine("Failed to find spawn-animals");
                    console.AppendText("\n[Error] Failed to find spawn-animals in server.properties");
                }
                // Process white-list
                string whitelistSetting = serverProperties[32];
                if (whitelistSetting.Contains("white-list"))
                {
                    Debug.WriteLine("Found white-list");
                    string whitelist;
                    whitelist = whitelistSetting.Remove(0, 11);
                    Debug.WriteLine("white-list value is " + whitelist);
                    if (whitelist == "true")
                    {
                        this.whitelist.SelectedIndex = 0;
                    }
                    else if (whitelist == "false")
                    {
                        this.whitelist.SelectedIndex = 1;
                    }
                    else
                    {
                        // white-list is invalid
                        console.AppendText("\n[Error] white-list in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // white-list line did not contain "white-list"
                    Debug.WriteLine("Failed to find white-list");
                    console.AppendText("\n[Error] Failed to find white-list in server.properties");
                }
                // Process generate-structures
                string generatestructuresSetting = serverProperties[33];
                if (generatestructuresSetting.Contains("generate-structures"))
                {
                    Debug.WriteLine("Found generate-structures");
                    string generatestructures;
                    generatestructures = generatestructuresSetting.Remove(0, 20);
                    Debug.WriteLine("generate-structures value is " + generatestructures);
                    if (generatestructures == "true")
                    {
                        generateStructures.SelectedIndex = 0;
                    }
                    else if (generatestructures == "false")
                    {
                        generateStructures.SelectedIndex = 1;
                    }
                    else
                    {
                        // generate-structures is invalid
                        console.AppendText("\n[Error] generate-structures in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // generate-structures line did not contain "generate-structures"
                    Debug.WriteLine("Failed to find generate-structures");
                    console.AppendText("\n[Error] Failed to find generate-structures in server.properties");
                }
                // Process online-mode
                string onlineSetting = serverProperties[34];
                if (onlineSetting.Contains("online-mode"))
                {
                    Debug.WriteLine("Found online-mode");
                    string online;
                    online = onlineSetting.Remove(0, 12);
                    Debug.WriteLine("online-mode value is " + online);
                    if (online == "true")
                    {
                        onlineMode.SelectedIndex = 0;
                    }
                    else if (online == "false")
                    {
                        onlineMode.SelectedIndex = 1;
                    }
                    else
                    {
                        // online-mode is invalid
                        console.AppendText("\n[Error] online-mode in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // online-mode line did not contain "online-mode"
                    Debug.WriteLine("Failed to find online-mode");
                    console.AppendText("\n[Error] Failed to find online-mode in server.properties");
                }
                // Process max-build-height
                string buildheightSetting = serverProperties[35];
                if (buildheightSetting.Contains("max-build-height"))
                {
                    Debug.WriteLine("Found max-build-height");
                    string buildheight;
                    buildheight = buildheightSetting.Remove(0, 17);
                    Debug.WriteLine("max-build-height value is " + buildheight);
                    decimal buildheightValue = Convert.ToDecimal(buildheight);
                    maxBuildHeight.Value = buildheightValue;
                }
                else
                {
                    // max-build-height line did not contain "max-build-height"
                    Debug.WriteLine("Failed to find max-build-height");
                    console.AppendText("\n[Error] Failed to find max-build-height in server.properties");
                }
                // Process level-seed
                string seedSetting = serverProperties[36];
                if (seedSetting.Contains("level-seed"))
                {
                    Debug.WriteLine("Found level-seed");
                    string seed;
                    seed = seedSetting.Remove(0, 11);
                    Debug.WriteLine("level-seed value is " + seed);
                    worldSeed.Text = seed;
                }
                else
                {
                    // level-seed line did not contain "level-seed"
                    Debug.WriteLine("Failed to find level-seed");
                    console.AppendText("\n[Error] Failed to find level-seed in server.properties");
                }
                // Process motd
                string motdSetting = serverProperties[37];
                if (motdSetting.Contains("motd"))
                {
                    Debug.WriteLine("Found motd");
                    string motd;
                    motd = motdSetting.Remove(0, 5);
                    Debug.WriteLine("motd value is " + motd);
                    this.motd.Text = motd;
                }
                else
                {
                    // motd line did not contain "motd"
                    Debug.WriteLine("Failed to find motd");
                    console.AppendText("\n[Error] Failed to find motd in server.properties");
                }
                // Process enable-rcon
                string rconSetting = serverProperties[38];
                if (rconSetting.Contains("enable-rcon"))
                {
                    Debug.WriteLine("Found enable-rcon");
                    string rcon;
                    rcon = rconSetting.Remove(0, 12);
                    Debug.WriteLine("enable-rcon value is " + rcon);
                    if (rcon == "true")
                    {
                        enableRcon.SelectedIndex = 0;
                    }
                    else if (rcon == "false")
                    {
                        enableRcon.SelectedIndex = 1;
                    }
                    else
                    {
                        // enable-rcon is invalid
                        console.AppendText("\n[Error] enable-rcon in server.properties contains an invalid value");
                    }
                }
                else
                {
                    // enable-rcon line did not contain "enable-rcon"
                    Debug.WriteLine("Failed to find enable-rcon");
                    console.AppendText("\n[Error] Failed to find enable-rcon in server.properties");
                }
            }
            catch (Exception ex)
            {
                console.AppendText("\n[Error] " + ex.Message);
            }
            toolStripStatusLabel1.Text = "Done!";
            toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            if (startServerOnStart)
            {
                StartServer();
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            // Show the settings form
            Settings settings = new Settings();
            settings.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            // Start server
            StartServer();
        }

        void StartServer()
        {
            toolStripStatusLabel1.Text = "Running Server...";
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            console.AppendText("\nStarting server...");
            stopButton.Enabled = true;
            startButton.Enabled = false;
            commandButton.Enabled = true;
            ThreadStart threadStart = new ThreadStart(ServerThread);
            Thread serverThread = new Thread(threadStart);
            serverThread.Start();
        }

        void ServerThread()
        {
            // Create start server script
            string[] lines = new string[] { Properties.Resources.startServer };
            File.WriteAllLines(serverPath + "start.bat", lines);
            // Set up server process
            ProcessStartInfo serverProcessInfo = new ProcessStartInfo();
            Process serverProcess = new Process();
            serverProcessInfo.FileName = serverPath + "start.bat";
            serverProcessInfo.CreateNoWindow = true;
            serverProcessInfo.UseShellExecute = false;
            serverProcessInfo.RedirectStandardOutput = true;
            serverProcessInfo.RedirectStandardError = true;
            serverProcessInfo.RedirectStandardInput = true;
            serverProcessInfo.ErrorDialog = true;
            serverProcess.StartInfo = serverProcessInfo;
            serverProcess.OutputDataReceived += outputDataRecieved;
            serverProcess.ErrorDataReceived += outputDataRecieved;
            serverProcessInfo.WorkingDirectory = serverPath;
            // Start server and begin reading output
            try
            {
                serverProcess.Start();
                Action defineInputWriter = () => inputWriter = serverProcess.StandardInput;
                this.Invoke(defineInputWriter);
                serverProcess.BeginOutputReadLine();
                serverProcess.BeginErrorReadLine();
                serverProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                console.AppendText("\n" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            Action a = () => inputWriter.Close();
            Action a1 = () => stopButton.Enabled = false;
            Action a2 = () => startButton.Enabled = true;
            Action a3 = () => commandButton.Enabled = false;
            Action a4 = () => toolStripStatusLabel1.Text = "Server Stopped!";
            Action a5 = () => toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            this.Invoke(a);
            this.Invoke(a1);
            this.Invoke(a2);
            this.Invoke(a3);
            this.Invoke(a4);
            this.Invoke(a5);
        }

        void outputDataRecieved(object sender, DataReceivedEventArgs args)
        {
            Action writeToConsole = () => console.AppendText("\n" + args.Data);
            this.Invoke(writeToConsole);
            Action scrollConsole = () => console.ScrollToCaret();
            this.Invoke(scrollConsole);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // About
            about.ShowDialog();
        }

        private void commandButton_Click(object sender, EventArgs e)
        {
            // Input command
            CommandInput commandInput = new CommandInput();
            commandInput.ShowDialog();
            string inputText;
            console.AppendText("\n > " + CommandInput.input.textBox1.Text);
            inputText = CommandInput.input.textBox1.Text;
            if (inputText.Length > 0)
            {
                inputWriter.WriteLine(inputText);
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            // Stop server
            toolStripStatusLabel1.Text = "Stopping Server...";
            string inputText;
            console.AppendText("\n > stop");
            inputText = "stop";
            if (inputText.Length > 0)
            {
                inputWriter.WriteLine(inputText);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                saveSettings.Visible = true;
            }
            else
            {
                saveSettings.Visible = false;
            }
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            string[] lines = new string[]
            {
                "#Minecraft server properties",
                "#" + DateTime.Now.ToString(),
                "spawn-protection=" + spawnProtection.Value,
                "generator-settings=" + generatorSettings.Text,
                "force-gamemode=" + forceGamemode.Text.ToString().ToLower(),
                "allow-nether=" + allowNether.Text.ToString().ToLower(),
                "gamemode=" + gamemode.Value,
                "broadcast-console-to-ops=" + broadcastConsoleToOps.Text.ToString().ToLower(),
                "enable-query=" + enableQuery.Text.ToString().ToLower(),
                "player-idle-timeout=" + playerIdleTimeout.Text,
                "difficulty=" + difficulty.Text,
                "spawn-monsters=" + spawnMonsters.Text.ToString().ToLower(),
                "op-permission-level=" + opPermLvl.Text,
                "resource-pack-hash=" + resourcePackHash.Text,
                "announce-player-achievements=" + achivements.Text.ToString().ToLower(),
                "pvp=" + pvp.Text.ToString().ToLower(),
                "snooper-enabled=" + snooperEnabled.Text.ToString().ToLower(),
                "level-type=" + worldType.Text,
                "hardcore=" + hardcore.Text.ToString().ToLower(),
                "enable-command-block=" + commandBlocks.Text.ToString().ToLower(),
                "max-players=" + maxPlayers.Value,
                "network-compression-threshold=" + netCompression.Value,
                "max-world-size=" + maxWorldSize.Value,
                "server-port=" + serverPort.Value,
                "debug=" + debug.Text.ToString().ToLower(),
                "server-ip=" + serverIP.Text,
                "spawn-npcs=" + spawnNpcs.Text.ToString().ToLower(),
                "allow-flight=" + allowFlight.Text.ToString().ToLower(),
                "level-name=" + worldName.Text,
                "view-distance=" + viewDistance.Text,
                "resource-pack=" + resourcePack.Text,
                "spawn-animals=" + spawnAnimals.Text.ToString().ToLower(),
                "white-list=" + whitelist.Text.ToString().ToLower(),
                "generate-structures=" + generateStructures.Text.ToString().ToLower(),
                "online-mode=" + onlineMode.Text.ToString().ToLower(),
                "max-build-height=" + maxBuildHeight.Text,
                "level-seed=" + worldSeed.Text,
                "motd=" + motd.Text,
                "enable-rcon=" + enableRcon.Text.ToString().ToLower()

            };
            try
            {
                File.WriteAllLines(serverPath + "server.properties", lines);
                MessageBox.Show("Successfully edited server.properties!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Open server
            getServer.ShowDialog();
            serverPath = GetServer.serverPath;
            newServer = GetServer.newServer;
            if (serverPath == "")
            {
                return;
            }
            ProcessStartInfo killInfo = new ProcessStartInfo();
            killInfo.UseShellExecute = false;
            killInfo.CreateNoWindow = true;
            killInfo.FileName = "taskkill.exe";
            killInfo.Arguments = "/f /IM java.exe";
            Process kill = new Process();
            kill.StartInfo = killInfo;
            kill.Start();
            console.Text = "";
            Initialize();
        }
    }
}
