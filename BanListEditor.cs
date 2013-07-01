using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YGOPro_Launcher.CardDatabase;

namespace DevPro_CardManager
{
    public partial class BanListEditor : Form
    {
        public BanListEditor()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            LoadBanList();
     
            BanList.SelectedIndexChanged += new EventHandler(BanList_SelectedIndexChanged);
            if (BanList.Items.Count > 0)
                BanList.SelectedIndex = 0;

            BannedList.AllowDrop = true;
            LimitedList.AllowDrop = true;
            SemiLimitedList.AllowDrop = true;
            
            SearchBox.List.MouseDown += new MouseEventHandler(SearchList_MouseDown);
            BannedList.DragEnter += new DragEventHandler(List_DragEnter);
            LimitedList.DragEnter += new DragEventHandler(List_DragEnter);
            SemiLimitedList.DragEnter += new DragEventHandler(List_DragEnter);
            BannedList.DragDrop += new DragEventHandler(List_DragDrop);
            LimitedList.DragDrop += new DragEventHandler(List_DragDrop);
            SemiLimitedList.DragDrop += new DragEventHandler(List_DragDrop);
            BannedList.DrawItem += new DrawItemEventHandler(List_DrawItem);
            LimitedList.DrawItem += new DrawItemEventHandler(List_DrawItem);
            SemiLimitedList.DrawItem += new DrawItemEventHandler(List_DrawItem);

        }

        Dictionary<string, List<BanListCard>> Banlists;

        private void LoadBanList()
        {
            Banlists = new Dictionary<string, List<BanListCard>>();
            if (!File.Exists("lflist.conf"))
                return;

            StreamReader reader = new StreamReader(File.OpenRead("lflist.conf"));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null || line == "" || line.StartsWith("#")) continue;
                if (line.StartsWith("!"))
                {
                    BanList.Items.Add(line.Substring(1));
                }
                else
                {
                    string[] parts = line.Split(' ');
                    if (!Program.CardData.ContainsKey(Int32.Parse(parts[0])))
                        continue;

                    if (Program.CardData[Int32.Parse(parts[0])].Name == "")
                        continue;


                    if (!Banlists.ContainsKey(BanList.Items[BanList.Items.Count - 1].ToString()))
                    {
                        Banlists.Add(BanList.Items[BanList.Items.Count - 1].ToString(), new List<BanListCard>());
                        Banlists[BanList.Items[BanList.Items.Count - 1].ToString()].Add(
                            new BanListCard() { id = Int32.Parse(parts[0]), banvalue = Int32.Parse(parts[1]), name = Program.CardData[Int32.Parse(parts[0])].Name });
                    }
                    else
                    {
                        Banlists[BanList.Items[BanList.Items.Count - 1].ToString()].Add(
                            new BanListCard() { id = Int32.Parse(parts[0]), banvalue = Int32.Parse(parts[1]), name = Program.CardData[Int32.Parse(parts[0])].Name });
                    }
                }
            }
        }

        private void SaveBanList()
        {
            using (StreamWriter writer = new StreamWriter(File.OpenWrite("banlist.txt")))
            {
                writer.WriteLine("#Built using DevPro card editor.");
                for (int i = 0; i < BanList.Items.Count; i++)
                {
                    writer.WriteLine("!{0}", BanList.Items[i].ToString());
                    try
                    {
                        var forbidden = Banlists[BanList.Items[i].ToString()].FindAll(x => x.banvalue == 0);
                        var limited = Banlists[BanList.Items[i].ToString()].FindAll(x => x.banvalue == 1);
                        var semiLimited = Banlists[BanList.Items[i].ToString()].FindAll(x => x.banvalue == 2);

                        writer.WriteLine("#forbidden");
                        foreach (var banListCard in forbidden)
                        {
                            writer.WriteLine("{0} {1}", banListCard.id, banListCard.banvalue);
                        }

                        writer.WriteLine("#limit");
                        foreach (var banListCard in limited)
                        {
                            writer.WriteLine("{0} {1}", banListCard.id, banListCard.banvalue);
                        }

                        writer.WriteLine("#semi limit");
                        foreach (var banListCard in semiLimited)
                        {
                            writer.WriteLine("{0} {1}", banListCard.id, banListCard.banvalue);
                        }
                    }
                    catch (KeyNotFoundException)
                    {
                        Debug.WriteLine("Unlimited was probably hit, good idea to check it out.");
                    }
                }
            }

        }

        private void SearchList_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox list = (ListBox)sender;
            int indexOfItem = list.IndexFromPoint(e.X, e.Y);
            if (indexOfItem >= 0 && indexOfItem < list.Items.Count)
            {
                list.DoDragDrop(list.Items[indexOfItem], DragDropEffects.Copy);
            }
        }
        private void List_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy; 
        }
        private void List_DragDrop(object sender, DragEventArgs e)
        {
            ListBox list = (ListBox)sender;
            int indexOfItemUnderMouseToDrop = list.IndexFromPoint(list.PointToClient(new Point(e.X, e.Y)));
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                if (!BannedList.Items.Contains(e.Data.GetData(DataFormats.Text)) && !LimitedList.Items.Contains(e.Data.GetData(DataFormats.Text))
                        && !SemiLimitedList.Items.Contains(e.Data.GetData(DataFormats.Text)))
                {
                    if (indexOfItemUnderMouseToDrop >= 0 && indexOfItemUnderMouseToDrop < list.Items.Count)
                        list.Items.Insert(indexOfItemUnderMouseToDrop, e.Data.GetData(DataFormats.Text));
                    else
                        list.Items.Add(e.Data.GetData(DataFormats.Text));
                }
                else
                {
                    if (BannedList.Items.Contains(e.Data.GetData(DataFormats.Text)))
                        MessageBox.Show(e.Data.GetData(DataFormats.Text) + " is already contained in the Banned list.");
                    else if (LimitedList.Items.Contains(e.Data.GetData(DataFormats.Text)))
                        MessageBox.Show(e.Data.GetData(DataFormats.Text) + " is already contained in the Limited list.");
                    else if (SemiLimitedList.Items.Contains(e.Data.GetData(DataFormats.Text)))
                        MessageBox.Show(e.Data.GetData(DataFormats.Text) + " is already contained in the SemiLimited list.");
                }
            }
        } 

        private void BanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BanList.SelectedItem == null) return;
            BannedList.Items.Clear();
            LimitedList.Items.Clear();
            SemiLimitedList.Items.Clear();
            if (Banlists.ContainsKey(BanList.SelectedItem.ToString()))
            {
                foreach (BanListCard card in Banlists[BanList.SelectedItem.ToString()])
                {
                    if (card.banvalue == 0)
                        BannedList.Items.Add(card.id);
                    else if (card.banvalue == 1)
                        LimitedList.Items.Add(card.id);
                    else if (card.banvalue == 2)
                        SemiLimitedList.Items.Add(card.id);
                }
            }
        }

        private void List_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox list = (ListBox)sender;
            e.DrawBackground();

            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < list.Items.Count)
            {
                string text = list.Items[index].ToString();
                Graphics g = e.Graphics;

                CardInfos card = Program.CardData[Int32.Parse(text)];

                g.FillRectangle((selected) ? new SolidBrush(Color.Blue) : new SolidBrush(Color.White), e.Bounds);

                // Print text
                g.DrawString((card.Name == "" ? card.Id.ToString() : card.Name), e.Font, (selected) ? Brushes.White : Brushes.Black,
                    list.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void BanAnimeCardsBtn_Click(object sender, EventArgs e)
        {
            foreach (int id in Program.CardData.Keys)
            {
                if (Program.CardData[id].Ot == 4)
                    if (!BannedList.Items.Contains(id))
                    {
                        BannedList.Items.Add(id);
                        Banlists[BanList.Items[BanList.Items.Count - 1].ToString()].Add(
                            new BanListCard() { id = id, banvalue = 0, name = Program.CardData[id].Name });
                    }
            }
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {
            SaveBanList();
        }
    }
    public class BanListCard
    {
        public int id;
        public string name;
        public int banvalue;
    }
}
