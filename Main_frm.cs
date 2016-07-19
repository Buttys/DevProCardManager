using System;
using System.Windows.Forms;
using DevPro_CardManager.Components;
using System.IO;

namespace DevPro_CardManager
{
    public partial class MainFrm : Form
    {
        private CDBEditor Editor;
        public MainFrm()
        {
            InitializeComponent();

            //load master cdb
            if (File.Exists("cards.cdb"))
                CardManager.LoadCDB("cards.cdb", false, true);
            else
                loadMasterCDBToolStripMenuItem_Click(null, EventArgs.Empty);

            //check for expansions
            if(Directory.Exists("expansions"))
            {
                string[] expansions = Directory.GetFiles("expansions", "*.cdb");
                foreach (string xpack in expansions)
                    CardManager.LoadCDB(xpack, true);
            }

            var editor = new TabPage {Name = "Editor", Text = "Card Editor" };
            Editor = new CDBEditor();
            editor.Controls.Add(Editor);

            var banlisted = new TabPage { Name = "Banlist Editor", Text = "Banlist Editor" };
            banlisted.Controls.Add(new BanListEditor());

            var idConverter = new TabPage { Name = "ID Converter", Text = "ID Converter" };
            idConverter.Controls.Add(new IDConverter());

            var formatConverter = new TabPage { Name = "Format Converter", Text = "Format Converter" };
            formatConverter.Controls.Add(new FormatConverter());
            formatConverter.Controls[0].Dock = DockStyle.Fill;

            var replayExtracter = new TabPage { Name = "Replay Deck Extracter", Text = "Replay Deck Extracter" };
            replayExtracter.Controls.Add(new ReplayExtracter());

            TabControl.TabPages.AddRange(new [] { editor, banlisted,idConverter, formatConverter, replayExtracter });
            this.FormBorderStyle = FormBorderStyle.Sizable;

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Damien Lawford AKA Buttys" + Environment.NewLine + "Modifications by Rahul Parkar AKA Idiot211" + Environment.NewLine + "Email: killerdamo@virginmedia.com" + Environment.NewLine+
                "Creator of DevPro Launcher/Server Software - http://devpro.org/" + Environment.NewLine + "CardManager Source code: https://github.com/Buttys/DevProCardManager" + Environment.NewLine +
                "Updated by sidschingis and Tic-Tac-Toc. Source code up-to-date : https://github.com/Tic-Tac-Toc/DevProCardManager"
                , "About", MessageBoxButtons.OK);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadMasterCDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dataFile = new OpenFileDialog();
            dataFile.Filter = "YGOPro Database (*.cdb)|*.cdb";
            dataFile.Title = "Select master database";
            if (dataFile.ShowDialog() == DialogResult.OK)
            {
                if(CardManager.Count > 0)
                    if (MessageBox.Show("Loading this cdb will reload the database. Are you sure?", "Clear Data", MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return;
                CardManager.LoadCDB(dataFile.FileName, false,true);
                if(Editor != null)
                    Editor.UpdateDatabases();
            }
        }

        private void loadExpansionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dataFile = new OpenFileDialog();
            dataFile.Filter = "YGOPro Database (*.cdb)|*.cdb";
            dataFile.Title = "Load expansion database";
            dataFile.Multiselect = true;
            if (dataFile.ShowDialog() == DialogResult.OK)
            {
                bool overwrite = MessageBox.Show("Overwrite any exsisting cards?", "Load Expansion", MessageBoxButtons.YesNo) == DialogResult.Yes;
                foreach (string file in dataFile.FileNames)
                        CardManager.LoadCDB(file, overwrite);
                Editor.UpdateDatabases();
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Activate(); //bring the form to the front after loading the cdb
        }
    }
}
