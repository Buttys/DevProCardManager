using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public partial class IDConverter : Form
    {
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
    }
}
