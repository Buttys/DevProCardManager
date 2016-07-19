using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace DevPro_CardManager.Components
{
    public partial class FormatConverter : UserControl
    {
        /*
         1 OCG
         2 TCG
         3 OCG/TCG
        */
        public FormatConverter()
        {
            InitializeComponent();
            convertList.DrawItem += convertList_DrawItem;
            convertList.KeyDown += DeleteItem;
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

        private void convertList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < convertList.Items.Count)
            {
                var data = (string[])convertList.Items[index];
                Graphics g = e.Graphics;

                CardInfos card = CardManager.GetCard(Int32.Parse(data[0]));

                g.FillRectangle((selected) ? new SolidBrush(Color.Blue) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                string format = (card.Ot & 0x1) > 0 ? "OCG" : (card.Ot & 0x2) > 0 ? "TCG" : (card.Ot & 0x3) > 2 ? "TCG/OCG" : "???";
                g.DrawString((card.Name == "" ? "???" : card.Name) + "   -  " + format + " > " + data[1], e.Font, (selected) ? Brushes.White : Brushes.Black,
                    convertList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string newFormat = rbTCG.Checked ? "TCG" : rbOCG.Checked ? "OCG" : rbTCGOCG.Checked ? "TCG/OCG" : "???";
            if (newFormat == "???")
            {
                MessageBox.Show("No format selected.", "Error!", MessageBoxButtons.OK);
                return;
            }

            List<string[]> updateCards = convertList.Items.OfType<string[]>().ToList();
            if (this.SearchBox.List.SelectedIndex == -1)
            {
                MessageBox.Show("No card selected.", "Error!", MessageBoxButtons.OK);
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
            cardToUpdate[1] = newFormat;

            convertList.Items.Add(cardToUpdate);
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("DatabasePatch"))
                Directory.CreateDirectory("DatabasePatch");
            List<string[]> updateCards = convertList.Items.OfType<string[]>().ToList();
            string str = Directory.GetCurrentDirectory(); ;
            string str2 = Path.Combine(str, "cards.cdb");
            if (!File.Exists(str2))
            {
                MessageBox.Show("cards.cdb not found.");
                return;
            }

            var connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();
            foreach (var updateCard in updateCards)
            {
                string formatInt = updateCard[1] == "OCG" ? "1" : updateCard[1] == "TCG" ? "2" : updateCard[1] == "TCG/OCG" ? "3" : "???";
                if (formatInt == "???")
                {
                    MessageBox.Show("Error occured.", "Error!", MessageBoxButtons.OK);
                    return;
                }
                SQLiteCommands.UpdateCardOt(updateCard[0], formatInt, connection);
            }

            connection.Close();
            File.Copy(str2, "DatabasePatch\\cards.cdb", true);
            convertList.Items.Clear();
            MessageBox.Show("Complete.");
        }
    }
}
