using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
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
                            if (!CardManager.ContainsCard(id))
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
