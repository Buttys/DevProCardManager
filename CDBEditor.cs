using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevPro_CardManager.Enums;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using DevPro_CardManager.Properties;

namespace DevPro_CardManager
{
    public sealed partial class CDBEditor : Form
    {
        private const string Cdbdir = @"cards.cdb";
        string m_loadedImage = "";
        Dictionary<int,string> m_setCodes;
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
            SearchBox.List.DoubleClick +=CardList_DoubleClick;
            SetDataTypes();
            LoadData(Cdbdir);
        }

        private void SetDataTypes()
        {
            LoadCardFormatsFromFile("Assets\\cardformats.txt");
            LoadCardRacesFromFile("Assets\\cardraces.txt");
            LoadCardAttributesFromFile("assets\\cardattributes.txt");
            for (int i = 1; i < 13; i++)
            {
                Level.Items.Add("★" + i);
            }
            LoadSetCodesFromFile("Assets\\setname.txt");
            CardTypeList.Items.AddRange(Enum.GetNames(typeof(CardType)));
        }

        private void LoadSetCodesFromFile(string filedir)
        {
            m_setCodes = new Dictionary<int,string>();
            List<string> setnames = new List<string>();

            if (!File.Exists(filedir))
            {
                return;
            }

            var reader = new StreamReader(File.OpenRead(filedir));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string setname = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();
                int setcode = Convert.ToInt32(parts[0], 16);

                if (!m_setCodes.ContainsKey(setcode))
                {
                    setnames.Add(setname);
                    m_setCodes.Add(setcode, setname);
                }
            }
            setnames.Sort();
            SetCodeLst.Items.AddRange(setnames.ToArray());
            OtherSetCodeLst.Items.AddRange(setnames.ToArray());
        }

        private void LoadCardFormatsFromFile(string filedir)
        {
            m_formats = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            var reader = new StreamReader(File.OpenRead(filedir));
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

        private void LoadCardRacesFromFile(string filedir)
        {
            m_cardRaces = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            var reader = new StreamReader(File.OpenRead(filedir));
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

        private void LoadCardAttributesFromFile(string filedir)
        {
            m_cardAttributes = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            var reader = new StreamReader(File.OpenRead(filedir));
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
            if (!Program.CardData.ContainsKey(cardid))
                return false;

            Clearbtn_Click(null, EventArgs.Empty);
            CardInfos info = Program.CardData[cardid];

            CardID.Text = info.Id.ToString(CultureInfo.InvariantCulture);
            Alias.Text = info.AliasId.ToString(CultureInfo.InvariantCulture);
            for (int i = 0; i < m_formats.Count; i++)
            {
                if (m_formats[i] == info.Ot)
                {
                    CardFormats.SelectedIndex = i;
                    break;
                }
            }
            Level.SelectedIndex = info.Level - 1;
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
            CardName.Text = info.Name;
            CardDescription.Text = info.Description;
            foreach (string effect in info.EffectStrings)
                EffectList.Items.Add(effect);
            SetCardTypes(info.GetCardTypes());

            int setcode = info.SetCode & 0xffff;
            if (m_setCodes.ContainsKey(setcode))
                SetCodeLst.SelectedItem = m_setCodes[setcode];
            setcode = info.SetCode >> 16;
            if (m_setCodes.ContainsKey(setcode))
                OtherSetCodeLst.SelectedItem = m_setCodes[setcode];

            SetCategoryCheckBoxs(info.Category);

            m_loadedCard = info.Id;

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

        private void SetCategoryCheckBoxs(int categorynumber)
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

        private void LoadData(string dataloc)
        {
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, dataloc);
            if (!File.Exists(str2))
            {
                MessageBox.Show(dataloc + " not found.");
                return;
            }
            //LoadData(cdbdir, "SELECT id, ot, alias, setcode, type, level, race, attribute, atk, def, category FROM datas", cdbdata);
            //LoadData(cdbdir, "SELECT id, name, desc, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16 FROM texts", cdbenglishtext);



            var connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();

            var datacommand = new SQLiteCommand("SELECT id, ot, alias, setcode, type, level, race, attribute, atk, def, category FROM datas", connection);
            var textcommand = new SQLiteCommand("SELECT id, name, desc, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16 FROM texts", connection);
            List<string[]> datas = DatabaseHelper.ExecuteStringCommand(datacommand, 11);
            List<string[]> texts = DatabaseHelper.ExecuteStringCommand(textcommand, 19);

            foreach (string[] row in datas)
            {
                if (!Program.CardData.ContainsKey(Int32.Parse(row[0])))
                    Program.CardData.Add(Int32.Parse(row[0]), new CardInfos(row));
            }
            foreach (string[] row in texts)
            {
                if (Program.CardData.ContainsKey(Int32.Parse(row[0])))
                    Program.CardData[Int32.Parse(row[0])].SetCardText(row);
            }
            connection.Close();
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
            {
                CardImg.Image = Resources.unknown;
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {

            CardID.Clear();
            Alias.Text = "0";
            CardFormats.SelectedIndex = -1;
            SetCodeLst.SelectedIndex = -1;
            OtherSetCodeLst.SelectedIndex = -1;
            Level.SelectedIndex = -1;
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
            m_loadedImage = "";
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

        private int GetSetCode()
        {
            int code = (SetCodeLst.SelectedIndex > 0) ? GetSetCodeFromString(SetCodeLst.SelectedItem.ToString()) : 0;
            code += ((OtherSetCodeLst.SelectedIndex > 0) ? GetSetCodeFromString(OtherSetCodeLst.SelectedItem.ToString()) : 0) << 0x10;
            return code;
        }

        private int GetSetCodeFromString(string name)
        {
            foreach(var item in m_setCodes)
                if(item.Value == name)
                    return item.Key;
            return 0;
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
            return code;
        }

        private void SaveCardbtn_Click(object sender, EventArgs e)
        {
            if (SaveCardtoCDB(Cdbdir))
            {
                SaveImage(CardID.Text);
                m_loadedCard = Convert.ToInt32(CardID.Text);
            }

        }

        private bool SaveCardtoCDB(string cdbpath)
        {
            int cardid;
            int cardalias;
            int atk;
            int def;
            bool overwrite = false;


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
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, cdbpath);
            if (!File.Exists(str2))
            {
                SQLiteConnection.CreateFile(cdbpath);
            }
            var connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();

            //check if card id exsists

            SQLiteCommand checkcommand = DatabaseHelper.CreateCommand("SELECT COUNT(*) FROM datas WHERE id= @id", connection);
            checkcommand.Parameters.Add(new SQLiteParameter("@id", updatecard));
            if (DatabaseHelper.ExecuteIntCommand(checkcommand) == 1)
            {
                if (MessageBox.Show("Overwrite current card?", "Found", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    overwrite = true;
                }
                else
                {
                    return false;
                }
            }


            SQLiteCommand command;
            if (overwrite)
            {
                command = DatabaseHelper.CreateCommand("UPDATE datas" +
         " SET id= @id, ot = @ot, alias= @alias, setcode= @setcode, type= @type, atk= @atk, def= @def, level= @level, race= @race, attribute= @attribute, category= @category WHERE id = @loadedid", connection);
            }
            else
            {
                command = DatabaseHelper.CreateCommand("INSERT INTO datas (id,ot,alias,setcode,type,atk,def,level,race,attribute,category)" +
                         " VALUES (@id, @ot, @alias, @setcode, @type, @atk, @def, @level, @race, @attribute, @category)", connection);
            }
            command.Parameters.Add(new SQLiteParameter("@loadedid", updatecard));
            command.Parameters.Add(new SQLiteParameter("@id", cardid));
            command.Parameters.Add(new SQLiteParameter("@ot", (CardFormats.SelectedItem == null ? 0 : GetCardFormat())));
            command.Parameters.Add(new SQLiteParameter("@alias", cardalias));
            command.Parameters.Add(new SQLiteParameter("@setcode", GetSetCode()));
            command.Parameters.Add(new SQLiteParameter("@type", GetTypeCode()));
            command.Parameters.Add(new SQLiteParameter("@atk", atk));
            command.Parameters.Add(new SQLiteParameter("@def", def));
            command.Parameters.Add(new SQLiteParameter("@level", (Level.SelectedItem == null ? 0 : Int32.Parse(Level.SelectedItem.ToString().Substring(1)))));
            command.Parameters.Add(new SQLiteParameter("@race", (Race.SelectedItem == null ? 0 : (Race.SelectedItem == null ? 0 : m_cardRaces[Race.SelectedIndex]))));
            command.Parameters.Add(new SQLiteParameter("@attribute", (CardAttribute.SelectedItem == null ? 0 : (CardAttribute.SelectedItem == null ? 0 : m_cardAttributes[CardAttribute.SelectedIndex]))));
            command.Parameters.Add(new SQLiteParameter("@category", GetCategoryNumber()));
            DatabaseHelper.ExecuteNonCommand(command);
            if (overwrite)
            {
                command = DatabaseHelper.CreateCommand("UPDATE texts" +
                    " SET id= @id,name= @name,desc= @des,str1= @str1,str2= @str2,str3= @str3,str4= @str4,str5= @str5,str6= @str6,str7= @str7,str8= @str8,str9= @str9,str10= @str10,str11= @str11,str12= @str12,str13= @str13,str14= @str14,str15= @str15,str16= @str16 WHERE id= @loadedid", connection);
            }
            else
            {
                command = DatabaseHelper.CreateCommand("INSERT INTO texts (id,name,desc,str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str11,str12,str13,str14,str15,str16)" +
                    " VALUES (@id,@name,@des,@str1,@str2,@str3,@str4,@str5,@str6,@str7,@str8,@str9,@str10,@str11,@str12,@str13,@str14,@str15,@str16)", connection);
            }
            command.Parameters.Add(new SQLiteParameter("@loadedid", updatecard));
            command.Parameters.Add(new SQLiteParameter("@id", cardid));
            command.Parameters.Add(new SQLiteParameter("@name", CardName.Text));
            command.Parameters.Add(new SQLiteParameter("@des", CardDescription.Text));
            var parameters = new List<SQLiteParameter>();
            for (int i = 0; i < 16; i++)
            {
                parameters.Add(new SQLiteParameter("@str" + (i + 1), (i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty)));
            }
            command.Parameters.AddRange(parameters.ToArray());
            DatabaseHelper.ExecuteNonCommand(command);
            connection.Close();
            MessageBox.Show("Card Saved");

            if (Program.CardData.ContainsKey(cardid))
            {
                Program.CardData[cardid] = new CardInfos(new [] { cardid.ToString(CultureInfo.InvariantCulture), (CardFormats.SelectedItem == null ? "0" : GetCardFormat().ToString(CultureInfo.InvariantCulture)),cardalias.ToString(CultureInfo.InvariantCulture),GetSetCode().ToString(CultureInfo.InvariantCulture),GetTypeCode().ToString(CultureInfo.InvariantCulture),
                    (Level.SelectedItem == null ? "0" : Level.SelectedItem.ToString().Substring(1)), (Race.SelectedItem == null ? "0" : (Race.SelectedItem == null ? "0" : m_cardRaces[Race.SelectedIndex].ToString(CultureInfo.InvariantCulture))),
                (CardAttribute.SelectedItem == null ? "0" : (CardAttribute.SelectedItem == null ? "0" : m_cardAttributes[CardAttribute.SelectedIndex].ToString(CultureInfo.InvariantCulture))),atk.ToString(CultureInfo.InvariantCulture),def.ToString(CultureInfo.InvariantCulture),GetCategoryNumber().ToString(CultureInfo.InvariantCulture)});

                var cardtextarray = new List<string> {cardid.ToString(CultureInfo.InvariantCulture), CardName.Text, CardDescription.Text};

                for (var i = 0; i < 17; i++)
                {
                    cardtextarray.Add((i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty));
                }

                Program.CardData[cardid].SetCardText(cardtextarray.ToArray());
            }
            else
            {
                Program.CardData.Add(cardid, new CardInfos(new [] { cardid.ToString(CultureInfo.InvariantCulture), (CardFormats.SelectedItem == null ? "0" : GetCardFormat().ToString(CultureInfo.InvariantCulture)),cardalias.ToString(CultureInfo.InvariantCulture),GetSetCode().ToString(CultureInfo.InvariantCulture),GetTypeCode().ToString(CultureInfo.InvariantCulture),
                    (Level.SelectedItem == null ? "0" : Level.SelectedItem.ToString().Substring(1)), (Race.SelectedItem == null ? "0" : (Race.SelectedItem == null ? "0" : m_cardRaces[Race.SelectedIndex].ToString(CultureInfo.InvariantCulture))),
                (CardAttribute.SelectedItem == null ? "0" : (CardAttribute.SelectedItem == null ? "0" : m_cardAttributes[CardAttribute.SelectedIndex].ToString(CultureInfo.InvariantCulture))),atk.ToString(CultureInfo.InvariantCulture),def.ToString(CultureInfo.InvariantCulture),GetCategoryNumber().ToString(CultureInfo.InvariantCulture)}));

                var cardtextarray = new List<string> {cardid.ToString(CultureInfo.InvariantCulture), CardName.Text, CardDescription.Text};

                for (int i = 0; i < 17; i++)
                {
                    cardtextarray.Add((i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty));
                }

                Program.CardData[cardid].SetCardText(cardtextarray.ToArray());
            }
            return true;
        }
        public void SaveImage(string cardid)
        {
            if (m_loadedImage != "")
            {
                // Save card image
                ImageResizer.SaveImage(CardImg.Image,
                        "pics\\" + cardid + ".jpg", 177, 254);
                //Save card thumbnail
                ImageResizer.SaveImage(CardImg.Image,
                        "pics\\thumbnail\\" + cardid + ".jpg", 44, 64);
            }
        }

        private void LoadImageBtn_Click(object sender, EventArgs e)
        {
            m_loadedImage = "";
            string imagepath = ImageResizer.OpenFileWindow("Set Image ", "", "Images|*.jpg;*.jpeg;*.png;");
            if (imagepath != null)
            {
                if (File.Exists(imagepath))
                {
                    using (var stream = new FileStream(imagepath, FileMode.Open, FileAccess.Read))
                    {
                        CardImg.Image = Image.FromStream(stream);
                    }
                    m_loadedImage = imagepath;
                }
                else
                {
                    CardImg.Image = Resources.unknown;
                }
            }
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

            if (!Program.CardData.ContainsKey(cardid))
            {
                MessageBox.Show("Unable to find card to delete.", "Error", MessageBoxButtons.OK);
                return;
            }

            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, Cdbdir);
            if (!File.Exists(str2))
            {
                SQLiteConnection.CreateFile(Cdbdir);
            }
            var connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();

            SQLiteCommand checkcommand = DatabaseHelper.CreateCommand("SELECT COUNT(*) FROM datas WHERE id= @id", connection);
            checkcommand.Parameters.Add(new SQLiteParameter("@id", cardid));
            if (DatabaseHelper.ExecuteIntCommand(checkcommand) == 1)
            {
                if (MessageBox.Show("Are you sure you want to delete " + Program.CardData[cardid].Name + "?", "Found", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            SQLiteCommand command = DatabaseHelper.CreateCommand("DELETE FROM datas WHERE id= @id", connection);
            command.Parameters.Add(new SQLiteParameter("@id", cardid));
            DatabaseHelper.ExecuteIntCommand(command);

            command = DatabaseHelper.CreateCommand("DELETE FROM texts WHERE id= @id", connection);
            command.Parameters.Add(new SQLiteParameter("@id", cardid));
            DatabaseHelper.ExecuteIntCommand(command);

            Program.CardData.Remove(cardid);
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
                    if (Program.CardData.ContainsKey(m_loadedCard))
                    {
                        CardInfos card = Program.CardData[m_loadedCard];
                        string[] lines = { "--" + card.Name, "function c"+ m_loadedCard + ".initial_effect(c)", string.Empty, "end"};
                        File.WriteAllLines(file, lines);
                        Process.Start(file);
                    }
                }
            }
        }

    }
}
