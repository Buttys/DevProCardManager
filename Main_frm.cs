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

            TabPage animegen = new TabPage() { Name = "Anime List ID Gen", Text = "Anime List ID Gen" };
            animegen.Controls.Add(new AnimeCardListGen());

            TabPage IDConverter = new TabPage() { Name = "ID Converter", Text = "ID Converter" };
            IDConverter.Controls.Add(new IDConverter());

            TabControl.TabPages.AddRange(new TabPage[] { editor, banlisted, animegen,IDConverter });
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Damien Lawford AKA Buttys" + Environment.NewLine + "Modifications by Rahul Parkar AKA Idiot211" + Environment.NewLine + "Email: killerdamo@virginmedia.com" + Environment.NewLine+
                "Creator of DevPro Launcher/Server Software - http://dev.ygopro-online.net/" + Environment.NewLine + "CardManager Source code: https://github.com/Buttys/DevProCardManager" 
                , "About", MessageBoxButtons.OK);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private CDBEditor GetEditor()
        {
            foreach (TabPage tab in TabControl.TabPages)
            {
                foreach (Control control in tab.Controls)
                {
                    if (control is CDBEditor)
                    {
                        return (CDBEditor)control;
                    }
                }
            }
            return null;
        }

        private void enableDevProModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            enableDevProModeToolStripMenuItem.Checked = (enableDevProModeToolStripMenuItem.Checked ? false : true);
        }

        private void applyChangesToPrimaryCDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyChangesToPrimaryCDBToolStripMenuItem.Checked = (applyChangesToPrimaryCDBToolStripMenuItem.Checked ? false : true);
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (englishToolStripMenuItem.Checked)
                return;

            englishToolStripMenuItem.Checked = true;
            frenchToolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = false;
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frenchToolStripMenuItem.Checked)
                return;

            englishToolStripMenuItem.Checked = false;
            frenchToolStripMenuItem.Checked = true;
            germanToolStripMenuItem.Checked = false;
        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (germanToolStripMenuItem.Checked)
                return;

            englishToolStripMenuItem.Checked = false;
            frenchToolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = true;
        }

        private void ImportSQL_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader("Test.sql");
            string file = reader.ReadToEnd();
            string[] commands = file.Split(';');

            SQLiteConnection.CreateFile("temp.cdb");
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, "temp.cdb");
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();
            
            SQLiteCommand command = new SQLiteCommand(file, connection);
            DatabaseHelper.ExecuteNonCommand(command);
            
            //foreach (string commandstring in commands)
            //{
            //    try
            //    {
            //        SQLiteCommand command = new SQLiteCommand(commandstring, connection);
            //        DatabaseHelper.ExecuteNonCommand(command);
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Test");
            //    }
            //}

            connection.Close();
        }

        private void ImportCBD_Click(object sender, EventArgs e)
        {

        }

        private void CreateSQLiteCardDB(string path)
        {
            SQLiteConnection.CreateFile(path);
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, path);
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();
            SQLiteCommand datacommand = new SQLiteCommand("CREATE TABLE datas(id integer primary key, ot integer, alias integet, setcode integer, type integer, atk integer, def integer, level integer, race integer, attribute integer, category integer);", connection);
            SQLiteCommand textcommand = new SQLiteCommand("CREATE TABLE texts(id integer primary key, name varchar(128), desc varchar(1024), str1 varchar(256), str2 varchar(256), str3 varchar(256),  str4 varchar(256),  str5 varchar(256),  str6 varchar(256),  str7 varchar(256),  str8 varchar(256),  str9 varchar(256), str10 varchar(256),  str11 varchar(256),  str12 varchar(256),  str13 varchar(256),  str14 varchar(256),  str15 varchar(256),  str16 varchar(256) );", connection);

            connection.Close();
        }

        private void ExportSQLFile(string filepath)
        {
            if (File.Exists(filepath))
                File.Delete(filepath);

            StreamWriter writer = new StreamWriter(filepath);
            writer.WriteLine("DROP TABLE IF EXISTS \"datas\";");
            writer.WriteLine("DROP TABLE IF EXISTS \"texts\";");
            writer.WriteLine("CREATE TABLE datas(id integer primary key, ot integer, alias integet, setcode integer, type integer, atk integer, def integer, level integer, race integer, attribute integer, category integer);");
            writer.WriteLine("CREATE TABLE texts(id integer primary key, name varchar(128), desc varchar(1024), str1 varchar(256), str2 varchar(256), str3 varchar(256),  str4 varchar(256),  str5 varchar(256),  str6 varchar(256),  str7 varchar(256),  str8 varchar(256),  str9 varchar(256), str10 varchar(256),  str11 varchar(256),  str12 varchar(256),  str13 varchar(256),  str14 varchar(256),  str15 varchar(256),  str16 varchar(256) );");
            Dictionary<int,CardInfos> data = Program.CardData;

            foreach (int card in data.Keys)
            {
                if (data[card].Name == "")
                    continue;
                writer.WriteLine("INSERT INTO \"datas\" VALUES(" + data[card].Id + "," + data[card].Ot + "," + data[card].AliasId + "," + data[card].SetCode + "," + data[card].Type + "," + data[card].Atk + "," + data[card].Def + "," + data[card].Level + "," + data[card].Race + "," + data[card].Attribute + "," + data[card].Category + ");");

                string[] effects = new string[16];

                if (data[card].EffectStrings != null)
                {
                    for (int i = 0; i < data[card].EffectStrings.Length; i++)
                    {
                        effects[i] = data[card].EffectStrings[i];
                    }
                }
                
                string CleanedDescription = data[card].Description.Replace(';', '.');

                writer.WriteLine("INSERT INTO \"texts\" VALUES(" + data[card].Id + ",'" + data[card].Name + "','" + CleanedDescription + "','" + effects[0] + "','" + effects[1] + "','" + effects[2] + "','" + effects[3] + "','" + effects[4] + "','" + effects[5] + "','" + effects[6] + "','" + effects[7] + "','" + effects[8] + "','" + effects[9] + "','" + effects[10] + "','" + effects[11] + "','" + effects[12] + "','" + effects[13] + "','" + effects[14] + "','" + effects[15] + "');");
            }
            writer.Close();

        }

        private void ExportToSql_Click(object sender, EventArgs e)
        {
            ExportSQLFile("Test.sql");
        }

    }
}
