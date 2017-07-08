using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public sealed partial class IDConverter : Form
    {
        //string[] definition
        //[0] = Old card ID
        //[1] = New card ID
        public IDConverter()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            NewId.Enter += NewIDInput_Enter;
            NewId.Leave += NewIDInput_Leave;

            UpdateCardsList.DrawItem += NewIDList_DrawItem;
            UpdateCardsList.KeyDown += DeleteItem;
        }

        private void DeleteItem(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                var list = (ListBox)sender;
                if (list.SelectedIndex != -1)
                    list.Items.RemoveAt(list.SelectedIndex);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            List<string[]> updateCards = UpdateCardsList.Items.OfType<string[]>().ToList();
            if (SearchBox.List.SelectedIndex == -1)
            {
                MessageBox.Show("No card selected.", "Error!", MessageBoxButtons.OK);
                return;
            }

            int newId;
            if (!Int32.TryParse(NewId.Text, out newId))
            {
                MessageBox.Show("New Id is invalid.", "Error!", MessageBoxButtons.OK);
                return;
            }

            if (CardManager.ContainsCard(newId) || updateCards.Exists(x => x[1] == newId.ToString(CultureInfo.InvariantCulture)))
            {
                MessageBox.Show("New Id is already been used.", "Error!", MessageBoxButtons.OK);
                return;
            }

            int selectedCardId = Convert.ToInt32(SearchBox.List.SelectedItem.ToString());
            if (updateCards.Exists(x => x[0] == selectedCardId.ToString(CultureInfo.InvariantCulture)))
            {
                MessageBox.Show("Card already in list to be changed", "Error!", MessageBoxButtons.OK);
                return;
            }
            

            var cardToUpdate = new string[2];
            cardToUpdate[0] = selectedCardId.ToString(CultureInfo.InvariantCulture);
            cardToUpdate[1] = newId.ToString(CultureInfo.InvariantCulture);

            UpdateCardsList.Items.Add(cardToUpdate);
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {

            ConvertButton.Enabled = false;
            bool updateCdb = cdbchk.Checked;
            bool updateScript = patchchk.Checked;
            bool updateImage = imagechk.Checked;
            List<string[]> updateCards = UpdateCardsList.Items.OfType<string[]>().ToList();

            if (patchchk.Checked)
            {
                if (!Directory.Exists("DevPatch"))
                    Directory.CreateDirectory("DevPatch");
                if (!Directory.Exists("DevPatch\\script"))
                    Directory.CreateDirectory("DevPatch\\script");
                if (!Directory.Exists("DevPatch\\pics"))
                    Directory.CreateDirectory("DevPatch\\pics");
                if (!Directory.Exists("DevPatch\\pics\\thumbnail"))
                    Directory.CreateDirectory("DevPatch\\pics\\thumbnail");
            }

            string str = "cards.cdb";
            foreach (var updateCard in updateCards)
            {
                
                if (updateCdb)
                {
                    if (!File.Exists(str))
                    {
                        MessageBox.Show("cards.cdb not found.");
                        return;
                    }

                    int cardid = Int32.Parse(updateCard[0]);
                    int newid = Int32.Parse(updateCard[1]);
                    CardManager.RenameKey(cardid, newid);

                    CardInfos card = CardManager.GetCard(newid);
                    card.Id = newid;
                    if (chkremovepre.Checked)
                        card.Ot = card.Ot & 0x03;
                    
                    CardManager.UpdateOrAddCard(card);
                    
                    var connection = new SQLiteConnection("Data Source=" + str);
                    connection.Open();

                    SQLiteCommands.UpdateCardId(updateCard[0], updateCard[1], connection);
                    if(chkremovepre.Checked)
                        SQLiteCommands.UpdateCardOt(updateCard[1], card.Ot.ToString(), connection);

                    connection.Close();

                }

                if (updateImage)
                {
                    string mainDir = Directory.GetCurrentDirectory(); ;
                    const string picFolderName = "pics";
                    const string tumbnailFolderName = "pics\\thumbnail";
                    string picName = updateCard[0] + ".png";
                    string newPicName = updateCard[1] + ".png";

                    string imagePath = Path.Combine(mainDir, picFolderName, picName);
                    string newImagePath = Path.Combine(mainDir, picFolderName, newPicName);
                    string thumbnailImagePath = Path.Combine(mainDir, tumbnailFolderName, picName);
                    string newthumbnailImagePath = Path.Combine(mainDir, tumbnailFolderName, newPicName);

                    if (File.Exists(imagePath) && !File.Exists(newImagePath))
                        File.Move(imagePath, newImagePath);
                    if (File.Exists(thumbnailImagePath) && !File.Exists(newthumbnailImagePath))
                        File.Move(thumbnailImagePath, newthumbnailImagePath);
                    if (patchchk.Checked)
                    {
                        if(File.Exists(newImagePath))
                            File.Copy(newImagePath, Path.Combine("DevPatch\\pics", newPicName), true);
                        if (File.Exists(newthumbnailImagePath))
                            File.Copy(newthumbnailImagePath, Path.Combine("DevPatch\\pics\\thumbnail", newPicName), true);
                    }
                }

                if (updateScript)
                {
                    string mainDir = Directory.GetCurrentDirectory(); ;
                    const string scriptFolderName = "script";
                    string scriptName = "c" + updateCard[0] + ".lua";
                    string newScriptName = "c" + updateCard[1] + ".lua";

                    string scriptPath = Path.Combine(mainDir, scriptFolderName, scriptName);
                    string newScriptPath = Path.Combine(mainDir, scriptFolderName, newScriptName);

                    if (File.Exists(scriptPath))
                    {
                        File.Move(scriptPath, newScriptPath);

                        //needs testing id replacing
                        string scriptFile = File.ReadAllText(newScriptPath);
                        scriptFile = scriptFile.Replace(updateCard[0], updateCard[1]);
                        File.WriteAllText(newScriptPath, scriptFile);

                        if (patchchk.Checked)
                            if(File.Exists(newScriptPath))
                                File.Copy(newScriptPath, Path.Combine("DevPatch\\script", newScriptName), true);
                    }
                }
                

            }
            if (patchchk.Checked)
                File.Copy(str, "DevPatch\\cards.cdb", true);
            UpdateCardsList.Items.Clear();
            MessageBox.Show("Complete.");
            ConvertButton.Enabled = true;
        }

        #region Form Desgin

        private void NewIDInput_Enter(object sender, EventArgs e)
        {
            if (NewId.Text == "New ID")
            {
                NewId.Text = "";
                NewId.ForeColor = SystemColors.WindowText;
            }
        }

        private void NewIDInput_Leave(object sender, EventArgs e)
        {
            if (NewId.Text == "")
            {
                NewId.Text = "New ID";
                NewId.ForeColor = SystemColors.WindowFrame;
            }
        }

        private void NewIDList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < UpdateCardsList.Items.Count)
            {
                var data = (string[])UpdateCardsList.Items[index];
                Graphics g = e.Graphics;

                CardInfos card = CardManager.GetCard(Int32.Parse(data[0]));

                g.FillRectangle((selected) ? new SolidBrush(Color.Blue) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString((card.Name == "" ? "???" : card.Name) + "   -  " + data[0] + " > " + data[1], e.Font, (selected) ? Brushes.White : Brushes.Black,
                    UpdateCardsList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }
        #endregion
    }
}
