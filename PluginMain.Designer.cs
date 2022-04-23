namespace Minecraft_Server_GUI
{
    partial class PluginMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AddElement = new System.Windows.Forms.TabPage();
            this.ViewElements = new System.Windows.Forms.TabPage();
            this.PluginInfo = new System.Windows.Forms.TabPage();
            this.PvInput = new System.Windows.Forms.TextBox();
            this.PluginVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.PluginInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(166, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "[PluginName]";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AddElement);
            this.tabControl1.Controls.Add(this.ViewElements);
            this.tabControl1.Controls.Add(this.PluginInfo);
            this.tabControl1.Location = new System.Drawing.Point(12, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(493, 426);
            this.tabControl1.TabIndex = 1;
            // 
            // AddElement
            // 
            this.AddElement.Location = new System.Drawing.Point(4, 24);
            this.AddElement.Name = "AddElement";
            this.AddElement.Padding = new System.Windows.Forms.Padding(3);
            this.AddElement.Size = new System.Drawing.Size(485, 398);
            this.AddElement.TabIndex = 0;
            this.AddElement.Text = "Add Element";
            this.AddElement.UseVisualStyleBackColor = true;
            // 
            // ViewElements
            // 
            this.ViewElements.Location = new System.Drawing.Point(4, 24);
            this.ViewElements.Name = "ViewElements";
            this.ViewElements.Padding = new System.Windows.Forms.Padding(3);
            this.ViewElements.Size = new System.Drawing.Size(485, 398);
            this.ViewElements.TabIndex = 1;
            this.ViewElements.Text = "View Elements";
            this.ViewElements.UseVisualStyleBackColor = true;
            // 
            // PluginInfo
            // 
            this.PluginInfo.Controls.Add(this.PvInput);
            this.PluginInfo.Controls.Add(this.PluginVersion);
            this.PluginInfo.Controls.Add(this.label2);
            this.PluginInfo.Location = new System.Drawing.Point(4, 24);
            this.PluginInfo.Name = "PluginInfo";
            this.PluginInfo.Size = new System.Drawing.Size(485, 398);
            this.PluginInfo.TabIndex = 2;
            this.PluginInfo.Text = "Plugin Info";
            this.PluginInfo.UseVisualStyleBackColor = true;
            // 
            // PvInput
            // 
            this.PvInput.Location = new System.Drawing.Point(100, 47);
            this.PvInput.Name = "PvInput";
            this.PvInput.Size = new System.Drawing.Size(126, 23);
            this.PvInput.TabIndex = 5;
            this.PvInput.TextChanged += new System.EventHandler(this.PvInput_TextChanged);
            // 
            // PluginVersion
            // 
            this.PluginVersion.AutoSize = true;
            this.PluginVersion.Location = new System.Drawing.Point(12, 50);
            this.PluginVersion.Name = "PluginVersion";
            this.PluginVersion.Size = new System.Drawing.Size(82, 15);
            this.PluginVersion.TabIndex = 2;
            this.PluginVersion.Text = "Plugin Version";
            this.PluginVersion.Click += new System.EventHandler(this.PluginVersion_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "plugin.yml";
            // 
            // PluginMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 487);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Name = "PluginMain";
            this.Text = "PluginMain";
            this.tabControl1.ResumeLayout(false);
            this.PluginInfo.ResumeLayout(false);
            this.PluginInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private TabControl tabControl1;
        private TabPage AddElement;
        private TabPage ViewElements;
        private TabPage PluginInfo;
        private Label label2;
        private Label PluginVersion;
        private TextBox PvInput;
    }
}