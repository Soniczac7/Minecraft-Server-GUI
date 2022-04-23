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
    public partial class CreatePlugin : Form
    {
        public string pluginName;
        public string pluginAuthor;
        public string pluginPath;
        public CreatePlugin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pluginName = textBox1.Text;
            pluginAuthor = textBox2.Text;
            pluginPath = textBox3.Text;
            //Make All Plugin Files + Folders
            try
            {
                System.IO.Directory.CreateDirectory(pluginPath + pluginName);
                System.IO.Directory.CreateDirectory(pluginPath + "Dependencies");
                FileStream fileStream = System.IO.File.Create(pluginPath + @"Dependencies\spigot-api-1.18.1.jar");
                fileStream.Close();
                string[] content = new string[]
                {
                Properties.Resources.spigot_api_1_18_1
                };
                System.IO.File.WriteAllLines(pluginPath + @"Dependencies\spigot-api-1.18.1.jar", content);
                fileStream = System.IO.File.Create(pluginPath + pluginName + @"\" + pluginName + ".iml");
                fileStream.Close();
                content = new string[]
                {
                Properties.Resources.PluginName
                };
                System.IO.File.WriteAllLines(pluginPath + pluginName + @"\" + pluginName + ".iml", content);
                System.IO.Directory.CreateDirectory(pluginPath + pluginName + @"\src");
                fileStream = System.IO.File.Create(pluginPath + pluginName + @"\src\plugin.yml");
                fileStream.Close();
                content = new string[]
                {
                "name: " + pluginName,
                "version: 1.0.0",
                "author: " + pluginAuthor,
                "main: com." + pluginAuthor + "." + pluginName + "." + pluginName,
                "api-version: 1.18",
                "commands:"
                };
                System.IO.File.WriteAllLines(pluginPath + pluginName + @"\src\plugin.yml", content);
                System.IO.Directory.CreateDirectory(pluginPath + pluginName + @"\src\com\" + pluginAuthor + @"\" + pluginName);
                fileStream = System.IO.File.Create(pluginPath + pluginName + @"\src\com\" + pluginAuthor + @"\" + pluginName + @"\" + pluginName + ".java");
                fileStream.Close();
                content = new string[]
                {
                    "package com." + pluginAuthor + "." + pluginName + ";",
                    "",
                    "import org.bukkit.plugin.java.JavaPlugin;",
                    "import org.bukkit.ChatColor;",
                    "",
                    "public class " + pluginName + " extends JavaPlugin {",
                    "",
                    "\t@Override",
                    "\tpublic void onEnable() {",
                    "\t\tgetServer().getConsoleSender().sendMessage(ChatColor.GREEN + '[" + pluginName + "]: Plugin Loaded');",
                    "\t}",
                    "}"
                };
                System.IO.File.WriteAllLines(pluginPath + pluginName + @"\src\com\" + pluginAuthor + @"\" + pluginName + @"\" + pluginName + ".java", content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //set plugin path
            DialogResult dialogResult = folderBrowserDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                pluginPath = folderBrowserDialog1.SelectedPath + @"\";
            }

            textBox3.Text = pluginPath;
        }
    }
}
