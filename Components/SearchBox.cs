using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DevPro_CardManager.Components
{
    public sealed class SearchBox : GroupBox
    {
        private readonly object _searchlock = new object();
        private readonly ListBox _searchList = new ListBox 
        { 
            Dock = DockStyle.Fill, 
            IntegralHeight = false, 
            DrawMode = DrawMode.OwnerDrawFixed 
        };
        private readonly TextBox _searchInput = new TextBox
        {
            Dock = DockStyle.Fill,
            Text = "Search",
            TextAlign = HorizontalAlignment.Center, 
            ForeColor = SystemColors.WindowFrame 
        };

        public SearchBox()
        {
            Dock = DockStyle.Fill;
            var panel = new TableLayoutPanel {ColumnCount = 1};
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            panel.Controls.Add(_searchList, 0, 0);
            panel.Controls.Add(_searchInput, 0, 1);
            panel.Dock = DockStyle.Fill;
            panel.RowCount = 2;
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 86.60714F));
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));

            _searchInput.Enter += SearchInput_Enter;
            _searchInput.Leave += SearchInput_Leave;
            _searchInput.TextChanged += SearchInput_TextChanged;

            _searchList.DrawItem += SearchList_DrawItem;

            Controls.Add(panel);
        }

        private void SearchInput_Enter(object sender, EventArgs e)
        {
            if (_searchInput.Text == "Search")
            {
                _searchInput.Text = "";
                _searchInput.ForeColor = SystemColors.WindowText;
            }
        }

        private void SearchInput_Leave(object sender, EventArgs e)
        {
            if (_searchInput.Text == "")
            {
                _searchInput.Text = "Search";
                _searchInput.ForeColor = SystemColors.WindowFrame;
            }
        }

        private void SearchInput_TextChanged(object sender, EventArgs e)
        {
            lock (_searchlock)
            {
                if (_searchInput.Text == "")
                {
                    _searchList.Items.Clear();
                    return;
                }
                if (_searchInput.Text != "Search")
                {
                    _searchList.Items.Clear();
                    foreach (int card in Program.CardData.Keys.Where(card => Program.CardData[card].Id.ToString(CultureInfo.InvariantCulture).ToLower().StartsWith(_searchInput.Text.ToLower()) ||
                                                                             Program.CardData[card].Name.ToLower().Contains(_searchInput.Text.ToLower())))
                    {
                        AddCardToList(Program.CardData[card].Id.ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
        }

        private void AddCardToList(string id)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddCardToList), id);
                return;
            }

            _searchList.Items.Add(id);

        }

        private void SearchList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < _searchList.Items.Count)
            {
                string text = _searchList.Items[index].ToString();
                Graphics g = e.Graphics;

                CardInfos card = Program.CardData[Int32.Parse(text)];

                g.FillRectangle((selected) ? new SolidBrush(Color.Blue) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString((card.Name == "" ? card.Id.ToString(CultureInfo.InvariantCulture) : card.Name), e.Font, (selected) ? Brushes.White : Brushes.Black,
                    _searchList.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }
        public ListBox List
        {
            get { return _searchList; }
        }
    }
}
