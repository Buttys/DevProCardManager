using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using YGOPro_Launcher.CardDatabase;

namespace DevPro_CardManager.Components
{
    public class SearchBox : GroupBox
    {
        private object Searchlock = new object();
        private ListBox SearchList = new ListBox() 
        { 
            Dock = DockStyle.Fill, 
            IntegralHeight = false, 
            DrawMode = DrawMode.OwnerDrawFixed 
        };
        private TextBox SearchInput = new TextBox()
        {
            Dock = DockStyle.Fill,
            Text = "Search",
            TextAlign = HorizontalAlignment.Center, 
            ForeColor = SystemColors.WindowFrame 
        };

        public SearchBox()
        {
            this.Dock = DockStyle.Fill;
            TableLayoutPanel panel = new TableLayoutPanel();
            panel.ColumnCount = 1;
            panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            panel.Controls.Add(SearchList, 0, 0);
            panel.Controls.Add(SearchInput, 0, 1);
            panel.Dock = System.Windows.Forms.DockStyle.Fill;
            panel.RowCount = 2;
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.60714F));
            panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));

            SearchInput.Enter += new EventHandler(SearchInput_Enter);
            SearchInput.Leave += new EventHandler(SearchInput_Leave);
            SearchInput.TextChanged += new EventHandler(SearchInput_TextChanged);

            SearchList.DrawItem += new DrawItemEventHandler(SearchList_DrawItem);

            this.Controls.Add(panel);
        }

        private void SearchInput_Enter(object sender, EventArgs e)
        {
            if (SearchInput.Text == "Search")
            {
                SearchInput.Text = "";
                SearchInput.ForeColor = SystemColors.WindowText;
            }
        }

        private void SearchInput_Leave(object sender, EventArgs e)
        {
            if (SearchInput.Text == "")
            {
                SearchInput.Text = "Search";
                SearchInput.ForeColor = SystemColors.WindowFrame;
            }
        }

        private void SearchInput_TextChanged(object sender, EventArgs e)
        {
            lock (Searchlock)
            {
                if (SearchInput.Text == "")
                {
                    SearchList.Items.Clear();
                    return;
                }
                if (SearchInput.Text != "Search")
                {
                    SearchList.Items.Clear();
                    foreach (int card in Program.CardData.Keys)
                    {
                        if (Program.CardData[card].Id.ToString().ToLower().StartsWith(SearchInput.Text.ToLower()) ||
                            Program.CardData[card].Name.ToLower().Contains(SearchInput.Text.ToLower()))
                        {
                            SearchList.Items.Add(Program.CardData[card].Id.ToString());
                        }
                    }

                }

            }
        }

        private void SearchList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < SearchList.Items.Count)
            {
                string text = SearchList.Items[index].ToString();
                Graphics g = e.Graphics;

                CardInfos card = Program.CardData[Int32.Parse(text)];

                g.FillRectangle((selected) ? new SolidBrush(Color.Blue) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString((card.Name == "" ? card.Id.ToString() : card.Name), e.Font, (selected) ? Brushes.White : Brushes.Black,
                    SearchList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }
        public ListBox List
        {
            get { return SearchList; }
        }
    }
}
