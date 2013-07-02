using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public partial class IDConverter : Form
    {
        //string[] definition
        //[0] = Old card ID
        //[1] = New card ID
        List<string[]> updateCards = new List<string[]>(); 
        public IDConverter()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            int selectedCardId = Convert.ToInt32(SearchBox.List.SelectedItem.ToString());
            if (updateCards.Exists(x => x[0] == selectedCardId.ToString()))
            {
                MessageBox.Show("Card already in list to be changed", "Error!", MessageBoxButtons.OK);
                return;
            }
            string newId = NewId.Text;

            string[] cardToUpdate = new string[2];
            cardToUpdate[0] = selectedCardId.ToString();
            cardToUpdate[1] = newId;

            updateCards.Add(cardToUpdate);

            UpdateCardsList.Items.Add(Program.CardData[selectedCardId].Name);
        }
        
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            bool updateCdb = cdbchk.Checked;
            bool updateScript = scriptchk.Checked;
            bool updateImage = imagechk.Checked;

            foreach (var updateCard in updateCards)
            {
                if (updateCdb)
                {
                    string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
                    string str2 = Path.Combine(str, "cards.cdb");
                    if (!File.Exists(str2))
                    {
                        MessageBox.Show("cards.cdb not found.");
                        return;
                    }

                    SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
                    connection.Open();

                    SQLiteCommands.UpdateCardId(updateCard[0], updateCard[1], connection);

                    connection.Close();
                }

                if (updateImage)
                {
                    string mainDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
                    string picFolderName = "pics";
                    string picName = updateCard[0] + ".jpg";
                    string newPicName = updateCard[1] + ".jpg";

                    string imagePath = Path.Combine(mainDir, picFolderName, picName);
                    string newImagePath = Path.Combine(mainDir, picFolderName, newPicName);

                    File.Move(imagePath, newImagePath);
                }

                if (updateScript)
                {
                    string mainDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
                    string scriptFolderName = "script";
                    string scriptName = "c" + updateCard[0] + ".lua";
                    string newScriptName = "c" + updateCard[1] + ".lua";

                    string scriptPath = Path.Combine(mainDir, scriptFolderName, scriptName);
                    string newScriptPath = Path.Combine(mainDir, scriptFolderName, newScriptName);

                    File.Move(scriptPath, newScriptPath);
                }

                Program.CardData.RenameKey<int, YGOPro_Launcher.CardDatabase.CardInfos>(Convert.ToInt32(updateCard[0]), Convert.ToInt32(updateCard[1]));
            }
        }
    }
}
