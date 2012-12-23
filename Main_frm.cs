using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Data.SQLite;
using YGOPro_Launcher.CardDatabase;
using DevPro_CardManager.Properties;
using DevPro.Data.Enums;

namespace DevPro_CardManager
{
    public partial class Main_frm : Form
    {
        public const string cdbdir = @"Language\\English\\cards.cdb";
        List<int> SetCodes;
        List<int> Formats;
        List<int> CardRaces;
        List<int> CardAttributes;
        int LoadedCard = 0;

        public Main_frm()
        {
            InitializeComponent();
            LoadData(cdbdir, "SELECT id, ot, alias, setcode, type, level, race, attribute, atk, def, category FROM datas", cdbdata);
            LoadData(cdbdir, "SELECT id, name, desc, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16 FROM texts", cdbenglishtext);
            LoadData(@"Language\\French\\cards.cdb", "SELECT id, name, desc, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16 FROM texts", cdbfrenchtext);
            LoadData(@"Language\\German\\cards.cdb", "SELECT id, name, desc, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16 FROM texts", cdbgermantext);
            SetDataTypes();

            BanList.SelectedIndexChanged += new EventHandler(BanList_SelectedIndexChanged);
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
            SetCodes = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            StreamReader reader = new StreamReader(File.OpenRead(filedir));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string setname = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();
                SetCodeLst.Items.Add(setname);
                OtherSetCodeLst.Items.Add(setname);
                SetCodes.Add(Convert.ToInt32(parts[0],16));

            }
        }

        private void LoadCardFormatsFromFile(string filedir)
        {
            Formats = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            StreamReader reader = new StreamReader(File.OpenRead(filedir));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string formatname = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();

                CardFormats.Items.Add(formatname);
                Formats.Add(Convert.ToInt32(parts[0], 16));

            }
        }

        private void LoadCardRacesFromFile(string filedir)
        {
            CardRaces = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            StreamReader reader = new StreamReader(File.OpenRead(filedir));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string racename = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();

                Race.Items.Add(racename);
                CardRaces.Add(Convert.ToInt32(parts[0], 16));

            }
        }

        private void LoadCardAttributesFromFile(string filedir)
        {
            CardAttributes = new List<int>();

            if (!File.Exists(filedir))
            {
                return;
            }

            StreamReader reader = new StreamReader(File.OpenRead(filedir));
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) continue;
                string[] parts = line.Split(' ');
                if (parts.Length == 1) continue;
                string attributename = line.Substring(parts[0].Length, line.Length - parts[0].Length).Trim();

                CardAttribute.Items.Add(attributename);
                CardAttributes.Add(Convert.ToInt32(parts[0], 16));

            }
        }

        private bool LoadCard(int cardid)
        {

            DataGridViewRow datarow = null;
            DataGridViewRow textrow = null;
            foreach (DataGridViewRow row in cdbdata.Rows)
            {
                if (row.Cells["id"].Value == null) continue;
                if (row.Cells["id"].Value.ToString() == cardid.ToString())
                {
                    datarow = row;
                }
            }
            foreach (DataGridViewRow row in cdbenglishtext.Rows)
            {
                if (row.Cells["id"].Value == null) continue;
                if (row.Cells["id"].Value.ToString() == cardid.ToString())
                {
                    textrow = row;
                }
            }

            if (datarow == null && textrow == null)
            {
                MessageBox.Show("Card not found.");
                return false;
            }

            CardInfos info = new CardInfos(datarow, textrow);
            CardID.Text = info.Id.ToString();
            Alias.Text = info.AliasId.ToString();
            for (int i = 0; i < Formats.Count;i++ )
            {
                if (Formats[i] == info.Ot)
                {
                    CardFormats.SelectedIndex = i;
                    break;
                }
            }
            Level.SelectedIndex = info.Level - 1;
            for (int i = 0; i < CardRaces.Count; i++)
            {
                if (CardRaces[i] == info.Race)
                {
                    Race.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < CardAttributes.Count; i++)
            {
                if (CardAttributes[i] == info.Attribute)
                {
                    CardAttribute.SelectedIndex = i;
                    break;
                }
            }
            ATK.Text = info.Atk.ToString();
            DEF.Text = info.Def.ToString();
            CardName.Text = info.Name;
            CardDescription.Text = info.Description;
            foreach (string effect in info.EffectStrings)
                EffectList.Items.Add(effect);
            SetCardTypes(info.GetCardTypes());

            int index = this.SetCodes.IndexOf(info.SetCode & 0xffff);
            this.SetCodeLst.SelectedIndex = index;
            index = this.SetCodes.IndexOf(info.SetCode >> 0x10);
            this.OtherSetCodeLst.SelectedIndex = index;
           
            SetCategoryCheckBoxs(info.Category);

            LoadedCard = info.Id;

            return true;

        }

        private void SetCardTypes(CardType[] types)
        {
            foreach(CardType cardtype in types)
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
            while (num3 < 0x20)
            {
                if (this.CategoryList.GetItemCheckState(num3) == CheckState.Checked)
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
            int num = 0;
            for (num = 1; index < 0x20; num = num << 1)
            {
                if ((num & categorynumber) != 0L)
                {
                    this.CategoryList.SetItemCheckState(index,CheckState.Checked);
                }
                else
                {
                    this.CategoryList.SetItemCheckState(index, CheckState.Unchecked);
                }
                index++;
            }
        }

        private void LoadData(string dataloc, string sqlcommand, DataGridView grid)
        {
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, dataloc);
            if (!File.Exists(str2))
            {
                MessageBox.Show(dataloc + " not found.");
                return;
            }
            SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(sqlcommand, connection);
            SQLiteDataAdapter data = new SQLiteDataAdapter(command);
            DataSet dataset = new DataSet();
            try
            {
                data.Fill(dataset);
                DataTable table = dataset.Tables[0];
                grid.DataSource = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connection.Close();
        }

        private void LoadCardImage(int id)
        {
            if (File.Exists("pics//" + id + ".jpg"))
            {
                CardImg.Image = Image.FromFile("pics//" + id + ".jpg");
            }
            else
            {
                CardImg.Image = Resources.unknown;
            }
        }

        private void LoadCardBtn_Click(object sender, EventArgs e)
        {
            Clearbtn_Click(null, EventArgs.Empty);
            if (LoadCardIDtxt.Text == "")
                MessageBox.Show("Please input card id");
            else
            {
                int cardid = 0;
                if (!Int32.TryParse(LoadCardIDtxt.Text,out cardid))
                {
                    MessageBox.Show("Incorrect ID format");
                    return;
                }

                if (LoadCard(cardid))
                {
                    LoadCardImage(cardid);
                }
            }
        }

        private void Clearbtn_Click(object sender, EventArgs e)
        {

            CardID.Clear();
            Alias.Clear();
            CardFormats.SelectedIndex = -1;
            SetCodeLst.SelectedIndex = -1;
            OtherSetCodeLst.SelectedIndex = -1;
            Level.SelectedIndex = -1;
            Race.SelectedIndex = -1;
            CardAttribute.SelectedIndex = -1;
            ATK.Clear();
            DEF.Clear();
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
            LoadedCard = 0;
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
            if (EffectInput.Text == "")
                return;
            if (EffectList.Items.Count == 16)
            {
                MessageBox.Show("No more items can be added.");
                return;
            }

            EffectList.Items.Insert(EffectList.Items.Count, EffectInput.Text);
            EffectList.SelectedIndex = EffectList.Items.Count -1;
        }

        private int GetCardFormat()
        {
            return (CardFormats.SelectedItem == null ? 0 : Formats[CardFormats.SelectedIndex]);
        }

        private int GetSetCode()
        {
           int code = 0;
           code = (this.SetCodeLst.SelectedIndex > 0) ? this.SetCodes[this.SetCodeLst.SelectedIndex] : 0;
           code += ((this.OtherSetCodeLst.SelectedIndex > 0) ? this.SetCodes[this.OtherSetCodeLst.SelectedIndex] : 0) << 0x10;

           return code;
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
                code += (int)CardType.Monster;
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
            int updatecard = LoadedCard;
            int cardid = 0;
            int ot = 0;
            int cardalias = 0;
            int atk = 0;
            int def = 0;
            bool overwrite = false;
            if(!Int32.TryParse(CardID.Text,out cardid))
            {
                MessageBox.Show("Invalid card id");
                return;
            }
            if(!Int32.TryParse(Alias.Text,out cardalias))
            {
                cardalias = 0;
            }
            if (!Int32.TryParse(ATK.Text, out atk))
            {
                MessageBox.Show("Invalid atk value");
                return;
            }
            if (!Int32.TryParse(ATK.Text, out def))
            {
                MessageBox.Show("Invalid def value");
                return;
            }
            string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
            string str2 = Path.Combine(str, cdbdir);
            if (!File.Exists(str2))
            {
                SQLiteConnection.CreateFile(cdbdir);
            }
                SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
                connection.Open();

            //check if card id exsists
            
            SQLiteCommand Checkcommand = DatabaseHelper.CreateCommand("SELECT COUNT(*) FROM datas WHERE id= @id", connection);
            Checkcommand.Parameters.Add(new SQLiteParameter("@id",cardid));
            if (DatabaseHelper.ExecuteIntCommand(Checkcommand) == 1)
            {
                if (MessageBox.Show("Overwrite current card?", "Found", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    overwrite = true;
                }
                else
                {
                    return;
                }
            }


            SQLiteCommand command = null;
            if (overwrite)
            {
                command = DatabaseHelper.CreateCommand("UPDATE datas" +
         " SET id= @id, ot = @ot, alias= @alias, setcode= @setcode, type= @type, atk= @atk, def= @def, level= @level, race= @race, attribute= @attribute, category= @category WHERE id = @id", connection);
            }
            else
            {
               command = DatabaseHelper.CreateCommand("INSERT INTO datas (id,ot,alias,setcode,type,atk,def,level,race,attribute,category)" +
                        " VALUES (@id, @ot, @alias, @setcode, @type, @atk, @def, @level, @race, @attribute, @category)", connection);
            }
                command.Parameters.Add(new SQLiteParameter("@id", cardid));
                command.Parameters.Add(new SQLiteParameter("@ot", (CardFormats.SelectedItem == null ? ot : GetCardFormat())));
                command.Parameters.Add(new SQLiteParameter("@alias", cardalias));
                command.Parameters.Add(new SQLiteParameter("@setcode", GetSetCode()));
                command.Parameters.Add(new SQLiteParameter("@type", GetTypeCode()));
                command.Parameters.Add(new SQLiteParameter("@atk", atk));
                command.Parameters.Add(new SQLiteParameter("@def", def));
                command.Parameters.Add(new SQLiteParameter("@level", (Level.SelectedItem == null ? 0 : Int32.Parse(Level.SelectedItem.ToString().Substring(1)))));
                command.Parameters.Add(new SQLiteParameter("@race", (Race.SelectedItem == null ? 0:(Race.SelectedItem == null ? 0:CardRaces[Race.SelectedIndex]))));
                command.Parameters.Add(new SQLiteParameter("@attribute", (CardAttribute.SelectedItem == null ? 0 : (CardAttribute.SelectedItem == null ? 0 : CardAttributes[CardAttribute.SelectedIndex]))));
                command.Parameters.Add(new SQLiteParameter("@category", GetCategoryNumber()));
                DatabaseHelper.ExecuteNonCommand(command);
                if (overwrite)
                {
                    command = DatabaseHelper.CreateCommand("UPDATE texts" +
                        " SET id= @id,name= @name,desc= @des,str1= @str1,str2= @str2,str3= @str3,str4= @str4,str5= @str5,str6= @str6,str7= @str7,str8= @str8,str9= @str9,str10= @str10,str11= @str11,str12= @str12,str13= @str13,str14= @str14,str15= @str15,str16= @str16 WHERE id= @id", connection);
                }
                else
                {
                    command = DatabaseHelper.CreateCommand("INSERT INTO texts (id,name,desc,str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str11,str12,str13,str14,str15,str16)" +
                        " VALUES (@id,@name,@des,@str1,@str2,@str3,@str4,@str5,@str6,@str7,@str8,@str9,@str10,@str11,@str12,@str13,@str14,@str15,@str16)", connection);
                }
                command.Parameters.Add(new SQLiteParameter("@id", cardid));
                command.Parameters.Add(new SQLiteParameter("@name", CardName.Text));
                command.Parameters.Add(new SQLiteParameter("@des", CardDescription.Text));
                for (int i = 0; i < 17; i++)
                {
                    command.Parameters.Add(new SQLiteParameter("@str" + i, (i < EffectList.Items.Count ? EffectList.Items[i].ToString():string.Empty)));
                }
                DatabaseHelper.ExecuteNonCommand(command);
                connection.Close();
                MessageBox.Show("Card Saved");
        }

        Dictionary<string, List<BanListCard>> Banlists;

        private void LoadBanListBtn_Click(object sender, EventArgs e)
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
                    if (!Banlists.ContainsKey(BanList.Items[BanList.Items.Count - 1].ToString()))
                    {
                        Banlists.Add(BanList.Items[BanList.Items.Count - 1].ToString(), new List<BanListCard>());
                        Banlists[BanList.Items[BanList.Items.Count - 1].ToString()].Add(
                            new BanListCard() { id = Int32.Parse(parts[0]), banvalue = Int32.Parse(parts[1]), name = GetCardNameFromID(parts[0]) });
                    }
                    else
                    {
                        Banlists[BanList.Items[BanList.Items.Count - 1].ToString()].Add(
                            new BanListCard() { id = Int32.Parse(parts[0]), banvalue = Int32.Parse(parts[1]), name = GetCardNameFromID(parts[0]) });
                    }
                    //CardList.Items.Add(GetCardNameFromID(parts[0]));
                }
            }
        }

        private string GetCardNameFromID(string id)
        {
            foreach (DataGridViewRow row in cdbenglishtext.Rows)
            {
                if (row.Cells["id"].Value == null) continue;
                if (row.Cells["id"].Value.ToString() == id)
                {
                    return row.Cells["name"].Value.ToString();
                }
            }
            return id;
        }
        private int GetCardIDFromName(string name)
        {
            foreach (DataGridViewRow row in cdbenglishtext.Rows)
            {
                if (row.Cells["name"].Value == null) continue;
                if (row.Cells["name"].Value.ToString() == name)
                {
                    return Int32.Parse(row.Cells["id"].Value.ToString());
                }
            }
            return 0;
        }

        private void BanList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BanList.SelectedItem == null) return;
            CardList.Items.Clear();
            if (Banlists.ContainsKey(BanList.SelectedItem.ToString()))
            {
                foreach (BanListCard card in Banlists[BanList.SelectedItem.ToString()])
                {
                    CardList.Items.Add(card.name);
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
