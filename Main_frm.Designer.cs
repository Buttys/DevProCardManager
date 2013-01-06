namespace DevPro_CardManager
{
    partial class Main_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_frm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromsqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cdbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cdbToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devProSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableDevProModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primaryCDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applyChangesToPrimaryCDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.devProSettingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.loadToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromsqlToolStripMenuItem,
            this.cdbToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importToolStripMenuItem.Text = "Import";
            // 
            // fromsqlToolStripMenuItem
            // 
            this.fromsqlToolStripMenuItem.Name = "fromsqlToolStripMenuItem";
            this.fromsqlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fromsqlToolStripMenuItem.Text = ".sql";
            this.fromsqlToolStripMenuItem.Click += new System.EventHandler(this.ImportSQL_Click);
            // 
            // cdbToolStripMenuItem
            // 
            this.cdbToolStripMenuItem.Name = "cdbToolStripMenuItem";
            this.cdbToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cdbToolStripMenuItem.Text = "cdb";
            this.cdbToolStripMenuItem.Click += new System.EventHandler(this.ImportCBD_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sqlToolStripMenuItem,
            this.cdbToolStripMenuItem1});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // sqlToolStripMenuItem
            // 
            this.sqlToolStripMenuItem.Name = "sqlToolStripMenuItem";
            this.sqlToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sqlToolStripMenuItem.Text = ".sql";
            this.sqlToolStripMenuItem.Click += new System.EventHandler(this.ExportToSql_Click);
            // 
            // cdbToolStripMenuItem1
            // 
            this.cdbToolStripMenuItem1.Name = "cdbToolStripMenuItem1";
            this.cdbToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.cdbToolStripMenuItem1.Text = "cdb";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // devProSettingsToolStripMenuItem
            // 
            this.devProSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableDevProModeToolStripMenuItem,
            this.primaryCDBToolStripMenuItem,
            this.applyChangesToPrimaryCDBToolStripMenuItem});
            this.devProSettingsToolStripMenuItem.Name = "devProSettingsToolStripMenuItem";
            this.devProSettingsToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.devProSettingsToolStripMenuItem.Text = "DevPro Settings";
            // 
            // enableDevProModeToolStripMenuItem
            // 
            this.enableDevProModeToolStripMenuItem.Name = "enableDevProModeToolStripMenuItem";
            this.enableDevProModeToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.enableDevProModeToolStripMenuItem.Text = "Enable DevPro Mode";
            this.enableDevProModeToolStripMenuItem.Click += new System.EventHandler(this.enableDevProModeToolStripMenuItem_Click);
            // 
            // primaryCDBToolStripMenuItem
            // 
            this.primaryCDBToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.frenchToolStripMenuItem,
            this.germanToolStripMenuItem});
            this.primaryCDBToolStripMenuItem.Name = "primaryCDBToolStripMenuItem";
            this.primaryCDBToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.primaryCDBToolStripMenuItem.Text = "Primary CDB";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Checked = true;
            this.englishToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            this.frenchToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.frenchToolStripMenuItem.Text = "French";
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.frenchToolStripMenuItem_Click);
            // 
            // germanToolStripMenuItem
            // 
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            this.germanToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.germanToolStripMenuItem.Text = "German";
            this.germanToolStripMenuItem.Click += new System.EventHandler(this.germanToolStripMenuItem_Click);
            // 
            // applyChangesToPrimaryCDBToolStripMenuItem
            // 
            this.applyChangesToPrimaryCDBToolStripMenuItem.Name = "applyChangesToPrimaryCDBToolStripMenuItem";
            this.applyChangesToPrimaryCDBToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.applyChangesToPrimaryCDBToolStripMenuItem.Text = "Apply Changes to Primary Only";
            this.applyChangesToPrimaryCDBToolStripMenuItem.Click += new System.EventHandler(this.applyChangesToPrimaryCDBToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // TabControl
            // 
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 24);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(794, 586);
            this.TabControl.TabIndex = 1;
            // 
            // Main_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 610);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main_frm";
            this.Text = "DevPro CardManager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devProSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primaryCDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem germanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableDevProModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applyChangesToPrimaryCDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromsqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cdbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cdbToolStripMenuItem1;

    }
}

