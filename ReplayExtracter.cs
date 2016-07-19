using DevPro_CardManager.Components;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public partial class ReplayExtracter : Form
    {
        private ReplayReader Extracter = new ReplayReader();

        public ReplayExtracter()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

            deckList.DrawItem += List_DrawItem;
            extraList.DrawItem += List_DrawItem;
            playerSelect.SelectedIndexChanged += PlayerChange;
        }

        private void PlayerChange(object sender, EventArgs e)
        {
            SetDeckinfo(playerSelect.SelectedItem.ToString());
        }

        private void List_DrawItem(object sender, DrawItemEventArgs e)
        {
            var list = (ListBox)sender;
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list.Items.Count)
            {
                string text = list.Items[index].ToString();
                Graphics g = e.Graphics;

                CardInfos card = null;

                if (CardManager.ContainsCard(Int32.Parse(text)))
                    card = CardManager.GetCard(Int32.Parse(text));

                g.FillRectangle((selected) ? new SolidBrush(Color.Blue) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString((card == null ? text : card.Name), e.Font,
                    (selected) ? Brushes.White : Brushes.Black,
                    list.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void BtnLoadDeck_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog replayFile = new OpenFileDialog();
            replayFile.Filter = "YGOPro Replay (*.yrp)|*.yrp|All Files (*.*)|*.*";
            if(replayFile.ShowDialog() == DialogResult.OK)
            {
                Stream filestream = null;
                if ((filestream = replayFile.OpenFile()) != null)
                {
                    if(Extracter.FromFile(CopyStream(filestream)))
                    {
                        playerSelect.Items.Clear();
                        playerSelect.Items.AddRange(Extracter.GetPlayers());
                        playerSelect.SelectedIndex = 0;
                        SetDeckinfo(playerSelect.SelectedItem.ToString());
                    }
                }
            }
        }

        public void SetDeckinfo(string name)
        {
            deckList.Items.Clear();
            extraList.Items.Clear();
            deckList.Items.AddRange(Extracter.GetMainDeck(name));
            extraList.Items.AddRange(Extracter.GetExtraDeck(name));
        }
        public MemoryStream CopyStream(Stream input)
        {
            MemoryStream output = new MemoryStream();
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
            return output;
        }

        private void BtnSaveDeck_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDeck = new SaveFileDialog();
            saveDeck.Filter = "YGOPro Deck (*.ydk)|*.ydk";
            if(saveDeck.ShowDialog() == DialogResult.OK)
            {
                //savedeck
                string dir = saveDeck.FileName;
                if (!string.IsNullOrEmpty(dir))
                {
                    StreamWriter writer = new StreamWriter(dir);
                    writer.WriteLine("#Created by YGOPro CardManager - Buttys");
                    writer.WriteLine("#main");
                    string[] maindeck = Extracter.GetMainDeck(playerSelect.SelectedItem.ToString());
                    foreach (string card in maindeck)
                        writer.WriteLine(card);
                    writer.WriteLine("#extra");
                    string[] extradeck = Extracter.GetExtraDeck(playerSelect.SelectedItem.ToString());
                    foreach (string card in extradeck)
                        writer.WriteLine(card);
                    writer.WriteLine("!side");
                    writer.Close();
                }
            }
        }
    }
}
