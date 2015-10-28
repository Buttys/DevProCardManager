using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using DevPro_CardManager.Components;

namespace DevPro_CardManager
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
            var editor = new TabPage {Name = "Editor", Text = "Card Editor" };
            editor.Controls.Add(new CDBEditor());

            var banlisted = new TabPage { Name = "Banlist Editor", Text = "Banlist Editor" };
            banlisted.Controls.Add(new BanListEditor());

            var idConverter = new TabPage { Name = "ID Converter", Text = "ID Converter" };
            idConverter.Controls.Add(new IDConverter());

            var formatConverter = new TabPage { Name = "Format Converter", Text = "Format Converter" };
            formatConverter.Controls.Add(new FormatConverter());
            formatConverter.Controls[0].Dock = DockStyle.Fill;

            TabControl.TabPages.AddRange(new [] { editor, banlisted,idConverter, formatConverter });
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

        private void ImportSQL_Click(object sender, EventArgs e)
        {
            var reader = new StreamReader("Test.sql");
            string file = reader.ReadToEnd();

            SQLiteConnection.CreateFile("temp.cdb");
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, "temp.cdb");
            var connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();
            
            var command = new SQLiteCommand(file, connection);
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

        //private void CreateSQLiteCardDB(string path)
        //{
        //    SQLiteConnection.CreateFile(path);
        //    string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        //    string str2 = Path.Combine(str, path);
        //    SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
        //    connection.Open();
        //    SQLiteCommand datacommand = new SQLiteCommand("CREATE TABLE datas(id integer primary key, ot integer, alias integet, setcode integer, type integer, atk integer, def integer, level integer, race integer, attribute integer, category integer);", connection);
        //    SQLiteCommand textcommand = new SQLiteCommand("CREATE TABLE texts(id integer primary key, name varchar(128), desc varchar(1024), str1 varchar(256), str2 varchar(256), str3 varchar(256),  str4 varchar(256),  str5 varchar(256),  str6 varchar(256),  str7 varchar(256),  str8 varchar(256),  str9 varchar(256), str10 varchar(256),  str11 varchar(256),  str12 varchar(256),  str13 varchar(256),  str14 varchar(256),  str15 varchar(256),  str16 varchar(256) );", connection);

        //    connection.Close();
        //}

        private void ExportSQLFile(string filepath)
        {
            if (File.Exists(filepath))
                File.Delete(filepath);

            var writer = new StreamWriter(filepath);
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

                var effects = new string[16];

                if (data[card].EffectStrings != null)
                {
                    for (int i = 0; i < data[card].EffectStrings.Length; i++)
                    {
                        effects[i] = data[card].EffectStrings[i];
                    }
                }
                
                string cleanedDescription = data[card].Description.Replace(';', '.');

                writer.WriteLine("INSERT INTO \"texts\" VALUES(" + data[card].Id + ",'" + data[card].Name + "','" + cleanedDescription + "','" + effects[0] + "','" + effects[1] + "','" + effects[2] + "','" + effects[3] + "','" + effects[4] + "','" + effects[5] + "','" + effects[6] + "','" + effects[7] + "','" + effects[8] + "','" + effects[9] + "','" + effects[10] + "','" + effects[11] + "','" + effects[12] + "','" + effects[13] + "','" + effects[14] + "','" + effects[15] + "');");
            }
            writer.Close();

        }

        private void ExportToSql_Click(object sender, EventArgs e)
        {
            ExportSQLFile("Test.sql");
        }

        private void CleanDevPro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will clean out any unused images/scripts that are currently not been used by the game, do you want to continue?", "Clean Game Files", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                FileInfo[] unusedPics = GetUnusedFiles("pics", "*.jpg", new string[0]);
                FileInfo[] unusedThumbs = GetUnusedFiles("pics\\thumbnail", "*.jpg", new string[0]);
                FileInfo[] unusedField = GetUnusedFiles("pics\\field", "*.png", new string[0]);
                FileInfo[] unusedField2 = GetUnusedFiles ("pics\\field", "*.jpg", new string[0]);
                FileInfo[] unusedScripts = GetUnusedFiles("script", "*.lua", new string[] { "constant.lua", "utility.lua" }, true);

                try
                {
                    foreach (FileInfo file in unusedPics)
                        file.Delete();
                    foreach (FileInfo file in unusedThumbs)
                        file.Delete();
                    foreach (FileInfo File in unusedField)
                        File.Delete();
                    foreach (FileInfo File in unusedField2)
                        File.Delete();
                    foreach (FileInfo file in unusedScripts)
                        file.Delete();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Admin rights required.");
                    return;
                }
                catch (IOException)
                {
                    MessageBox.Show("Some files are in use and we was unable to complete the removal.");
                    return;
                }

                MessageBox.Show((unusedPics.Length + unusedThumbs.Length + unusedField2.Length + unusedField.Length + unusedScripts.Length) + " files were removed!");
            }
        }

        private FileInfo[] GetUnusedFiles(string dir,string filetype,string[] exceptions, bool script = false)
        {
            if (Directory.Exists(dir))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                FileInfo[] files = dirInfo.GetFiles(filetype);
                List<FileInfo> toRemove = new List<FileInfo>();
                List<string> fileExceptions = new List<string>(exceptions);

                foreach (FileInfo file in files)
                {
                    if (!fileExceptions.Contains(file.Name))
                    {
                        int id = -1;

                        if (!Int32.TryParse(script ? Path.GetFileNameWithoutExtension(file.Name).Substring(1) : Path.GetFileNameWithoutExtension(file.Name), out id))
                        {
                            //not required
                            toRemove.Add(file);
                            continue;
                        }
                        else
                        {
                            if (!Program.CardData.ContainsKey(id))
                                toRemove.Add(file);
                        }
                    }
                }

                return toRemove.ToArray();
            }

            return new FileInfo[0];
        }

    }
}
