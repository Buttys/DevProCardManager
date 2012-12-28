using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using YGOPro_Launcher.CardDatabase;
using DevPro_CardManager.Properties;
using DevPro.Data.Enums;

namespace DevPro_CardManager
{
    public partial class Main_frm : Form
    {
        public Main_frm()
        {
            InitializeComponent();
            TabPage editor = new TabPage() {Name = "Editor", Text = "Card Editor" };
            editor.Controls.Add(new CDBEditor());

            TabPage banlisted = new TabPage() { Name = "Banlist Editor", Text = "Banlist Editor" };
            banlisted.Controls.Add(new BanListEditor());
            TabControl.TabPages.AddRange(new TabPage[]{ editor });
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Damien Lawford AKA Buttys" + Environment.NewLine + "Email: killerdamo@virginmedia.com" + Environment.NewLine+
                "Creator of DevPro Launcher/Server Software - http://dev.ygopro-online.net/" + Environment.NewLine + "CardManager Source code: https://github.com/Buttys/DevProCardManager" 
                , "About", MessageBoxButtons.OK);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
