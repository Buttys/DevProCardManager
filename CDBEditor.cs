using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevPro_CardManager.Enums;
using System.IO;
using System.Data.SQLite;
using DevPro_CardManager.Properties;

namespace DevPro_CardManager
{
    public sealed partial class CDBEditor : Form
    {
        Dictionary<int,string> m_setCodes = new Dictionary<int, string>();
        List<int> m_formats;
        List<int> m_cardRaces;
        List<int> m_cardAttributes;
        int m_loadedCard;

        public CDBEditor()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;
            SetCodeOne.DrawItem += DrawList;
            SetCodeTwo.DrawItem += DrawList;
            SetCodeThree.DrawItem += DrawList;
            SetCodeFour.DrawItem += DrawList;
            SearchBox.List.DoubleClick += CardList_DoubleClick;
            m_setCodes.Add(0, "None");
            SetDataTypes();
            UpdateSetCodes();
            LScale.SelectedIndex = 0;
            RScale.SelectedIndex = 0;
            UpdateDatabases();
            if (CDBSelect.Items.Count > 0)
                CDBSelect.SelectedIndex = 0;
        }

        private void SetDataTypes()
        {
            LoadCardFormatsFromFile(CreateFileStreamFromString(Resources.cardformats));
            LoadCardRacesFromFile(CreateFileStreamFromString(Resources.cardraces));
            LoadCardAttributesFromFile(CreateFileStreamFromString(Resources.cardattributes));
            LoadSetCodesFromFile(CreateFileStreamFromString(Resources.setname));
            if (File.Exists("cardformats.txt"))
                LoadCardFormatsFromFile(CreateFileStreamFromString(File.ReadAllText("cardformats.txt")));
            if (File.Exists("cardraces.txt"))
                LoadCardRacesFromFile(CreateFileStreamFromString(File.ReadAllText("cardraces.txt")));
            if (File.Exists("cardattributes.txt"))
                LoadCardAttributesFromFile(CreateFileStreamFromString(File.ReadAllText("cardattributes.txt")));
            if (File.Exists("setname.txt"))
                LoadSetCodesFromFile(CreateFileStreamFromString(File.ReadAllText("setname.txt")));
            if (File.Exists("strings.conf"))
                LoadSetCodesFromFile(CreateFileStreamFromString(File.ReadAllText("strings.conf")));
            for (int i = 1; i < 13; i++)
                Level.Items.Add("★" + i);
            for (int i = 0; i < 14; i++)
                LScale.Items.Add(i);
            for (int i = 0; i < 14; i++)
                RScale.Items.Add(i);
            CardTypeList.Items.AddRange(Enum.GetNames(typeof(CardType)));
        }

        private Stream CreateFileStreamFromString(string file)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(file);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private bool LoadSetCodesFromFile(Stream file)
        {
            try
            {
                var reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    //!setcode 0x8d Ghostrick
                    string line = reader.ReadLine();
                    if (line == null || !line.StartsWith("!setname")) continue;
                    string[] parts = line.Split(' ');
                    if (parts.Length == 1) continue;

                    int setcode = Convert.ToInt32(parts[1], 16);
                    string setname = line.Split(new string[] { parts[1] }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();

                    if (!m_setCodes.ContainsKey(setcode))
                        m_setCodes.Add(setcode, setname);
                    else
                        m_setCodes[setcode] = setname;
                }
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        private void UpdateSetCodes()
        {
            List<object> setnames = new List<object>();
            Dictionary<string, int> setstrings = new Dictionary<string, int>();
            List<string> sortedSetNames = new List<string>();

            foreach (int set in m_setCodes.Keys)
                if(!setstrings.ContainsKey(m_setCodes[set]))
                    setstrings.Add(m_setCodes[set], set);

            foreach (string set in setstrings.Keys)
                sortedSetNames.Add(set);

            sortedSetNames.Sort();


            foreach (string setname in sortedSetNames)
                if(setstrings[setname] != 0)
                    setnames.Add(setstrings[setname]);

            SetCodeOne.Items.Add(0);
            SetCodeTwo.Items.Add(0);
            SetCodeThree.Items.Add(0);
            SetCodeFour.Items.Add(0);

            SetCodeOne.Items.AddRange(setnames.ToArray());
            SetCodeTwo.Items.AddRange(setnames.ToArray());
            SetCodeThree.Items.AddRange(setnames.ToArray());
            SetCodeFour.Items.AddRange(setnames.ToArray());

        }

        private void LoadCardFormatsFromFile(Stream file)
        {
            m_formats = new List<int>();

            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string formatname = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();

                CardFormats.Items.Add(formatname);
                m_formats.Add(Convert.ToInt32(parts[0], 16));

            }
        }

        private void LoadCardRacesFromFile(Stream file)
        {
            m_cardRaces = new List<int>();

            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string racename = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();

                Race.Items.Add(racename);
                m_cardRaces.Add(Convert.ToInt32(parts[0], 16));

            }
        }

        private void LoadCardAttributesFromFile(Stream file)
        {
            m_cardAttributes = new List<int>();

            var reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string attributename = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();

                CardAttribute.Items.Add(attributename);
                m_cardAttributes.Add(Convert.ToInt32(parts[0], 16));

            }
        }

        private bool LoadCard(int cardid)
        {
            if (!CardManager.ContainsCard(cardid))
                return false;

            Clearbtn_Click(null, EventArgs.Empty);
            CardInfos info = CardManager.GetCard(cardid);

            CardID.Text = info.Id.ToString(CultureInfo.InvariantCulture);
            Alias.Text = info.AliasId.ToString(CultureInfo.InvariantCulture);
            for (int i = 0; i < m_formats.Count; i++)
            {
                if (m_formats[i] == (info.Ot & 0x3))
                {
                    CardFormats.SelectedIndex = i;
                    break;
                }
            }
            Level.SelectedIndex = (int)info.Level - 1;
            RScale.SelectedIndex = (int)info.RScale;
            LScale.SelectedIndex = (int)info.LScale;
            for (int i = 0; i < m_cardRaces.Count; i++)
            {
                if (m_cardRaces[i] == info.Race)
                {
                    Race.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < m_cardAttributes.Count; i++)
            {
                if (m_cardAttributes[i] == info.Attribute)
                {
                    CardAttribute.SelectedIndex = i;
                    break;
                }
            }
            ATK.Text = info.Atk.ToString(CultureInfo.InvariantCulture);
            DEF.Text = info.Def.ToString(CultureInfo.InvariantCulture);
            chkPre.Checked = (info.Ot & 0x4) > 0;

            CardName.Text = info.Name;
            CardDescription.Text = info.Description;
            foreach (string effect in info.EffectStrings)
                EffectList.Items.Add(effect);
            SetCardTypes(info.GetCardTypes());

            long setcode = info.SetCode & 0xffff;
            if (m_setCodes.ContainsKey((int)setcode))
                SetCodeOne.SelectedItem = (int)setcode;
            else
                SetCodeOne.SelectedItem = m_setCodes[0];
            setcode = info.SetCode >> 16 & 0xffff;
            if (m_setCodes.ContainsKey((int)setcode))
                SetCodeTwo.SelectedItem = (int)setcode;
            else
                SetCodeTwo.SelectedItem = m_setCodes[0];
            setcode = info.SetCode >> 32 & 0xffff;
            if (m_setCodes.ContainsKey((int)setcode))
                SetCodeThree.SelectedItem = (int)setcode;
            else
                SetCodeThree.SelectedItem = 0;
            setcode = info.SetCode >> 48 & 0xffff;
            if (m_setCodes.ContainsKey((int)setcode))
                SetCodeFour.SelectedItem = (int)setcode;
            else
                SetCodeFour.SelectedItem = m_setCodes[0];
            SetCategoryCheckBoxs(info.Category);

            m_loadedCard = info.Id;

            CDBSelect.SelectedIndex = info.source - 1;

            return true;
        }

        private void CardList_DoubleClick(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            if (list.SelectedIndex < 0) return;
            if (!LoadCard(Int32.Parse(list.SelectedItem.ToString())))
            {
                MessageBox.Show("Error Loading card", "Error", MessageBoxButtons.OK);
            }
            else
            {
                LoadCardImage(Int32.Parse(list.SelectedItem.ToString()));
            }
        }

        private void SetCardTypes(IEnumerable<CardType> types)
        {
            foreach (var cardtype in types)
            {
                switch (cardtype)
                {
                    case CardType.Monster:
                        CardTypeList.SetItemCheckState(0, CheckState.Checked);
                        break;
                    case CardType.Spell:
                        CardTypeList.SetItemCheckState(1, CheckState.Checked);
                        break;
                    case CardType.Trap:
                        CardTypeList.SetItemCheckState(2, CheckState.Checked);
                        break;
                    case CardType.Normal:
                        CardTypeList.SetItemCheckState(3, CheckState.Checked);
                        break;
                    case CardType.Effect:
                        CardTypeList.SetItemCheckState(4, CheckState.Checked);
                        break;
                    case CardType.Fusion:
                        CardTypeList.SetItemCheckState(5, CheckState.Checked);
                        break;
                    case CardType.Ritual:
                        CardTypeList.SetItemCheckState(6, CheckState.Checked);
                        break;
                    case CardType.TrapMonster:
                        CardTypeList.SetItemCheckState(7, CheckState.Checked);
                        break;
                    case CardType.Spirit:
                        CardTypeList.SetItemCheckState(8, CheckState.Checked);
                        break;
                    case CardType.Union:
                        CardTypeList.SetItemCheckState(9, CheckState.Checked);
                        break;
                    case CardType.Gemini:
                        CardTypeList.SetItemCheckState(10, CheckState.Checked);
                        break;
                    case CardType.Tuner:
                        CardTypeList.SetItemCheckState(11, CheckState.Checked);
                        break;
                    case CardType.Synchro:
                        CardTypeList.SetItemCheckState(12, CheckState.Checked);
                        break;
                    case CardType.Token:
                        CardTypeList.SetItemCheckState(13, CheckState.Checked);
                        break;
                    case CardType.QuickPlay:
                        CardTypeList.SetItemCheckState(14, CheckState.Checked);
                        break;
                    case CardType.Continuous:
                        CardTypeList.SetItemCheckState(15, CheckState.Checked);
                        break;
                    case CardType.Equip:
                        CardTypeList.SetItemCheckState(16, CheckState.Checked);
                        break;
                    case CardType.Field:
                        CardTypeList.SetItemCheckState(17, CheckState.Checked);
                        break;
                    case CardType.Counter:
                        CardTypeList.SetItemCheckState(18, CheckState.Checked);
                        break;
                    case CardType.Flip:
                        CardTypeList.SetItemCheckState(19, CheckState.Checked);
                        break;
                    case CardType.Toon:
                        CardTypeList.SetItemCheckState(20, CheckState.Checked);
                        break;
                    case CardType.Xyz:
                        CardTypeList.SetItemCheckState(21, CheckState.Checked);
                        break;
                    case CardType.Pendulum:
                        CardTypeList.SetItemCheckState(22, CheckState.Checked);
                        break;
                    case CardType.SpecialSummon:
                        CardTypeList.SetItemCheckState(23, CheckState.Checked);
                        break;
                }
            }
        }

        private int GetCategoryNumber()
        {
            int selectedIndex = 0;
            int num2 = 1;
            int num3 = 0;
            while (num3 < CategoryList.Items.Count)
            {
                if (CategoryList.GetItemCheckState(num3) == CheckState.Checked)
                {
                    selectedIndex |= num2;
                }
                num3++;
                num2 = num2 << 1;
            }

            return selectedIndex;
        }

        private void SetCategoryCheckBoxs(long categorynumber)
        {
            int index = 0;
            int num;
            for (num = 1; index < CategoryList.Items.Count; num = num << 1)
            {
                CategoryList.SetItemCheckState(index,
                                                    (num & categorynumber) != 0L
                                                        ? CheckState.Checked
                                                        : CheckState.Unchecked);
                index++;
            }
        }

        private void LoadCardImage(int id)
        {
            if (File.Exists("pics//" + id + ".jpg"))
            {
                using (var stream = new FileStream("pics//" + id + ".jpg", FileMode.Open, FileAccess.Read))
                {
                    CardImg.Image = Image.FromStream(stream);
                }
            }
            else
            if (File.Exists("pics//" + id + ".png"))
            {
                using (var stream = new FileStream("pics//" + id + ".png", FileMode.Open, FileAccess.Read))
                    {
                    CardImg.Image = Image.FromStream(stream);
                    }
            }
            else
            {
                CardImg.Image = Resources.unknown;
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {

            CardID.Clear();
            Alias.Text = "0";
            CardFormats.SelectedIndex = -1;
            SetCodeOne.SelectedIndex = -1;
            SetCodeTwo.SelectedIndex = -1;
            SetCodeThree.SelectedIndex = -1;
            SetCodeFour.SelectedIndex = -1;
            Level.SelectedIndex = -1;
            RScale.SelectedIndex = 0;
            LScale.SelectedIndex = 0;
            Race.SelectedIndex = -1;
            CardAttribute.SelectedIndex = -1;
            ATK.Text = "0";
            DEF.Text = "0";
            CardName.Clear();
            CardDescription.Clear();
            EffectList.Items.Clear();

            for (int i = 0; i < CardTypeList.Items.Count; i++)
            {
                CardTypeList.SetItemCheckState(i, CheckState.Unchecked);
            }
            for (int i = 0; i < CategoryList.Items.Count; i++)
            {
                CategoryList.SetItemCheckState(i, CheckState.Unchecked);
            }
            m_loadedCard = 0;
            CardImg.Image = Resources.unknown;
        }

        private void DeleteEffectbtn_Click(object sender, EventArgs e)
        {
            if (EffectList.SelectedItem == null)
                return;

            EffectList.Items.Remove(EffectList.SelectedItem);
        }

        private void MoveEffectUp_Click(object sender, EventArgs e)
        {
            if (EffectList.SelectedItem == null)
                return;
            int index = EffectList.SelectedIndex;
            string value = EffectList.SelectedItem.ToString();

            if (EffectList.SelectedIndex == 0)
                return;

            EffectList.Items.RemoveAt(index);
            EffectList.Items.Insert(index - 1, value);
            EffectList.SelectedIndex = index - 1;
            EffectInput.Clear();
        }

        private void MoveEffectDown_Click(object sender, EventArgs e)
        {
            if (EffectList.SelectedItem == null)
                return;


            int index = EffectList.SelectedIndex;
            string value = EffectList.SelectedItem.ToString();

            if (EffectList.SelectedIndex == EffectList.Items.Count - 1)
                return;

            EffectList.Items.RemoveAt(index);
            EffectList.Items.Insert(index + 1, value);
            EffectList.SelectedIndex = index + 1;
            EffectInput.Clear();
        }

        private void AddEffectbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EffectInput.Text))
                return;
            if (EffectList.Items.Count == 16)
            {
                MessageBox.Show("No more items can be added.");
                EffectInput.Clear();
                return;
            }

            EffectList.Items.Insert(EffectList.Items.Count, EffectInput.Text);
            EffectList.SelectedIndex = EffectList.Items.Count - 1;
            EffectInput.Clear();
        }

        private int GetCardFormat()
        {
            return (CardFormats.SelectedItem == null ? 0 : m_formats[CardFormats.SelectedIndex]);
        }

        private long GetSetCode()
        {
            MemoryStream m_stream = new MemoryStream();
            BinaryWriter m_writer = new BinaryWriter(m_stream);
            m_writer.Write((short)((SetCodeOne.SelectedIndex > 0) ? (int)SetCodeOne.SelectedItem : 0));
            m_writer.Write((short)((SetCodeTwo.SelectedIndex > 0) ? (int)SetCodeTwo.SelectedItem : 0));
            m_writer.Write((short)((SetCodeThree.SelectedIndex > 0) ? (int)SetCodeThree.SelectedItem : 0));
            m_writer.Write((short)((SetCodeFour.SelectedIndex > 0) ? (int)SetCodeFour.SelectedItem : 0));
            return BitConverter.ToInt64(m_stream.ToArray(),0);
        }

        private int GetSetCodeFromString(string name)
        {
            foreach(var item in m_setCodes)
                if(item.Value == name)
                    return item.Key;
            return 0;
        }

        private int GetLevelCode()
        {
            MemoryStream m_stream = new MemoryStream();
            BinaryWriter m_writer = new BinaryWriter(m_stream);
            m_writer.Write((byte)(Level.SelectedItem == null ? 0 : Int32.Parse(Level.SelectedItem.ToString().Substring(1))));
            m_writer.Write((byte)0);
            m_writer.Write((byte)Int32.Parse(RScale.SelectedItem.ToString()));
            m_writer.Write((byte)Int32.Parse(LScale.SelectedItem.ToString()));
            return BitConverter.ToInt32(m_stream.ToArray(), 0);
        }

        private int GetTypeCode()
        {
            int code = 0;
            if (CardTypeList.GetItemCheckState(0) == CheckState.Checked)
                code += (int)CardType.Monster;
            if (CardTypeList.GetItemCheckState(1) == CheckState.Checked)
                code += (int)CardType.Spell;
            if (CardTypeList.GetItemCheckState(2) == CheckState.Checked)
                code += (int)CardType.Trap;
            if (CardTypeList.GetItemCheckState(3) == CheckState.Checked)
                code += (int)CardType.Normal;
            if (CardTypeList.GetItemCheckState(4) == CheckState.Checked)
                code += (int)CardType.Effect;
            if (CardTypeList.GetItemCheckState(5) == CheckState.Checked)
                code += (int)CardType.Fusion;
            if (CardTypeList.GetItemCheckState(6) == CheckState.Checked)
                code += (int)CardType.Ritual;
            if (CardTypeList.GetItemCheckState(7) == CheckState.Checked)
                code += (int)CardType.TrapMonster;
            if (CardTypeList.GetItemCheckState(8) == CheckState.Checked)
                code += (int)CardType.Spirit;
            if (CardTypeList.GetItemCheckState(9) == CheckState.Checked)
                code += (int)CardType.Union;
            if (CardTypeList.GetItemCheckState(10) == CheckState.Checked)
                code += (int)CardType.Gemini;
            if (CardTypeList.GetItemCheckState(11) == CheckState.Checked)
                code += (int)CardType.Tuner;
            if (CardTypeList.GetItemCheckState(12) == CheckState.Checked)
                code += (int)CardType.Synchro;
            if (CardTypeList.GetItemCheckState(13) == CheckState.Checked)
                code += (int)CardType.Token;
            if (CardTypeList.GetItemCheckState(14) == CheckState.Checked)
                code += (int)CardType.QuickPlay;
            if (CardTypeList.GetItemCheckState(15) == CheckState.Checked)
                code += (int)CardType.Continuous;
            if (CardTypeList.GetItemCheckState(16) == CheckState.Checked)
                code += (int)CardType.Equip;
            if (CardTypeList.GetItemCheckState(17) == CheckState.Checked)
                code += (int)CardType.Field;
            if (CardTypeList.GetItemCheckState(18) == CheckState.Checked)
                code += (int)CardType.Counter;
            if (CardTypeList.GetItemCheckState(19) == CheckState.Checked)
                code += (int)CardType.Flip;
            if (CardTypeList.GetItemCheckState(20) == CheckState.Checked)
                code += (int)CardType.Toon;
            if (CardTypeList.GetItemCheckState(21) == CheckState.Checked)
                code += (int)CardType.Xyz;
            if (CardTypeList.GetItemCheckState(22) == CheckState.Checked)
                code += (int)CardType.Pendulum;
            if (CardTypeList.GetItemCheckState(23) == CheckState.Checked)
                code += (int)CardType.SpecialSummon;
            return code;
        }

        private void SaveCardbtn_Click(object sender, EventArgs e)
        {
            string dir = string.Empty;
            if (CDBSelect.Items.Count > 0)
                dir = CardManager.GetDatabaseDir(CDBSelect.SelectedIndex + 1);
            else
            {
                MessageBox.Show("No cdb selected!");
                return;
            }
            if (SaveCardtoCDB(dir))
                m_loadedCard = Convert.ToInt32(CardID.Text);

        }

        private bool SaveCardtoCDB(string cdbpath)
        {
            int cardid;
            int cardalias;
            int atk;
            int def;
            int ot = (CardFormats.SelectedItem == null ? 0 : GetCardFormat());
            if(chkPre.Checked)
                ot |= 0x4;

            if (!Int32.TryParse(CardID.Text, out cardid))
            {
                MessageBox.Show("Invalid card id");
                return false;
            }

            int updatecard = m_loadedCard == 0 ? cardid : m_loadedCard;

            if (!Int32.TryParse(Alias.Text, out cardalias))
            {
                cardalias = 0;
            }
            if (!Int32.TryParse(ATK.Text, out atk))
            {
                MessageBox.Show("Invalid atk value");
                return false;
            }
            if (!Int32.TryParse(DEF.Text, out def))
            {
                MessageBox.Show("Invalid def value");
                return false;
            }
            if (CDBSelect.Items.Count == 0)
            {
                MessageBox.Show("No loaded database");
                return false;
            }

            CardInfos newCardInfo = new CardInfos(new[] { cardid.ToString(CultureInfo.InvariantCulture), (ot.ToString(CultureInfo.InvariantCulture)),cardalias.ToString(CultureInfo.InvariantCulture),GetSetCode().ToString(CultureInfo.InvariantCulture),GetTypeCode().ToString(CultureInfo.InvariantCulture),
                GetLevelCode().ToString(), (Race.SelectedItem == null ? "0" : (Race.SelectedItem == null ? "0" : m_cardRaces[Race.SelectedIndex].ToString(CultureInfo.InvariantCulture))),
                (CardAttribute.SelectedItem == null ? "0" : (CardAttribute.SelectedItem == null ? "0" : m_cardAttributes[CardAttribute.SelectedIndex].ToString(CultureInfo.InvariantCulture))),atk.ToString(CultureInfo.InvariantCulture),def.ToString(CultureInfo.InvariantCulture),GetCategoryNumber().ToString(CultureInfo.InvariantCulture)}
            , CDBSelect.SelectedIndex + 1);

            var cardtextarray = new List<string> { cardid.ToString(CultureInfo.InvariantCulture), CardName.Text, CardDescription.Text };

            for (var i = 0; i < 17; i++)
            {
                cardtextarray.Add((i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty));
            }

            newCardInfo.SetCardText(cardtextarray.ToArray());

            //check source DB

            if (CardManager.ContainsCard(cardid))
            {
                if (CardManager.GetCard(cardid).source != newCardInfo.source)
                {
                    if(MessageBox.Show("Copy to new database?","",MessageBoxButtons.YesNo) != DialogResult.Yes)
                        return false;
                }
            }


            //save/update card
            var connection = new SQLiteConnection("Data Source=" + CardManager.GetDatabaseDir(newCardInfo.source));
            connection.Open();
            //check if card id exsists
            bool overwrite = SQLiteCommands.ContainsCard(updatecard, connection);

            if (overwrite)
            {
                if (MessageBox.Show("Overwrite current card?", "Found", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    connection.Close();
                    return false;
                }
            }

            SQLiteCommands.SaveCard(newCardInfo, connection, updatecard, overwrite);

            connection.Close();

            if (cardid != updatecard)
                CardManager.RenameKey(updatecard, cardid);

            CardManager.UpdateOrAddCard(newCardInfo);

            MessageBox.Show("Card Saved");
            return true;
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            int cardid;
            Int32.TryParse(CardID.Text, out cardid);

            if (cardid == 0)
            {
                MessageBox.Show("Invalid card id.", "Error", MessageBoxButtons.OK);
                return;
            }

            if (!CardManager.ContainsCard(cardid))
            {
                MessageBox.Show("Unable to find card to delete.", "Error", MessageBoxButtons.OK);
                return;
            }

            string dir = CardManager.GetDatabaseDir(CardManager.GetCard(cardid).source);

            var connection = new SQLiteConnection("Data Source=" + dir);
            connection.Open();

            if (SQLiteCommands.ContainsCard(cardid,connection))
            {
                if (MessageBox.Show("Are you sure you want to delete " + CardManager.GetCard(cardid).Name + "?", "Found", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            SQLiteCommands.DeleteCard(cardid, connection);

            CardManager.RemoveCard(cardid);
            Clearbtn_Click(null, EventArgs.Empty);
        }

        private void OpenScriptBtn_Click(object sender, EventArgs e)
        {
            if (m_loadedCard != 0)
            {
                string file = "script\\c" + m_loadedCard + ".lua";
                if (File.Exists(file))
                    Process.Start(file);
                else
                {
                    if (CardManager.ContainsCard(m_loadedCard))
                    {
                        CardInfos card = CardManager.GetCard(m_loadedCard);
                        string[] lines = { "--" + card.Name, "function c"+ m_loadedCard + ".initial_effect(c)", string.Empty, "end"};
                        File.WriteAllLines(file, lines);
                        Process.Start(file);
                    }
                }
            }
        }

        public void UpdateDatabases()
        {
            CDBSelect.Items.Clear();
            CDBSelect.Items.AddRange(CardManager.GetDatabaseNames());
        }

        private void DrawList(object sender, DrawItemEventArgs e)
        {
            int index = e.Index;

            if (index < 0)
                return;
            ComboBox combobox = (ComboBox)sender;

            e.DrawBackground();
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            e.Graphics.DrawString(m_setCodes[(int)combobox.Items[index]], e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            e.DrawFocusRectangle();
        }
    }
}
