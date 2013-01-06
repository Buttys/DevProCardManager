using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

        }

        Dictionary<string, List<BanListCard>> Banlists;

        private void LoadBanList()
        {
            Banlists = new Dictionary<string, List<BanListCard>>();
            if (!File.Exists("lflist.conf"))
            {
                return;
            }

            StreamReader reader = new StreamReader(File.OpenRead("lflist.conf"));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null || line == "") continue;
                if (line.StartsWith("!"))
                {
                    BanList.Items.Add(line.Substring(1));
                }
                else if (line.StartsWith("#"))
                {

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
                        BannedList.Items.Add(card.name);
                    else if (card.banvalue == 1)
                        LimitedList.Items.Add(card.name);
                    else if (card.banvalue == 2)
                        SemiLimitedList.Items.Add(card.name);
                }
            }
        }

    }
    public class BanListCard
    {
        public int id;
        public string name;
        public int banvalue;
    }
}
