using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DevPro_CardManager
{
    public static class SQLiteCommands
    {
        //private void SaveCardtoCDB(string cdbpath)
        //{
        //    int updatecard = 0;
        //    int cardid = 0;
        //    int ot = 0;
        //    int cardalias = 0;
        //    int atk = 0;
        //    int def = 0;
        //    bool overwrite = false;


        //    if (!Int32.TryParse(CardID.Text, out cardid))
        //    {
        //        MessageBox.Show("Invalid card id");
        //        return;
        //    }

        //    if (LoadedCard == 0)
        //        updatecard = cardid;
        //    else
        //        updatecard = LoadedCard;

        //    if (!Int32.TryParse(Alias.Text, out cardalias))
        //    {
        //        cardalias = 0;
        //    }
        //    if (!Int32.TryParse(ATK.Text, out atk))
        //    {
        //        MessageBox.Show("Invalid atk value");
        //        return;
        //    }
        //    if (!Int32.TryParse(DEF.Text, out def))
        //    {
        //        MessageBox.Show("Invalid def value");
        //        return;
        //    }
        //    string str = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";
        //    string str2 = Path.Combine(str, cdbpath);
        //    if (!File.Exists(str2))
        //    {
        //        SQLiteConnection.CreateFile(cdbpath);
        //    }
        //    SQLiteConnection connection = new SQLiteConnection("Data Source=" + str2);
        //    connection.Open();

        //    //check if card id exsists

        //    SQLiteCommand Checkcommand = DatabaseHelper.CreateCommand("SELECT COUNT(*) FROM datas WHERE id= @id", connection);
        //    Checkcommand.Parameters.Add(new SQLiteParameter("@id", updatecard));
        //    if (DatabaseHelper.ExecuteIntCommand(Checkcommand) == 1)
        //    {
        //        if (MessageBox.Show("Overwrite current card?", "Found", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        //        {
        //            overwrite = true;
        //        }
        //        else
        //        {
        //            return;
        //        }
        //    }


        //    SQLiteCommand command = null;
        //    if (overwrite)
        //    {
        //        command = DatabaseHelper.CreateCommand("UPDATE datas" +
        // " SET id= @id, ot = @ot, alias= @alias, setcode= @setcode, type= @type, atk= @atk, def= @def, level= @level, race= @race, attribute= @attribute, category= @category WHERE id = @loadedid", connection);
        //    }
        //    else
        //    {
        //        command = DatabaseHelper.CreateCommand("INSERT INTO datas (id,ot,alias,setcode,type,atk,def,level,race,attribute,category)" +
        //                 " VALUES (@id, @ot, @alias, @setcode, @type, @atk, @def, @level, @race, @attribute, @category)", connection);
        //    }
        //    command.Parameters.Add(new SQLiteParameter("@loadedid", updatecard));
        //    command.Parameters.Add(new SQLiteParameter("@id", cardid));
        //    command.Parameters.Add(new SQLiteParameter("@ot", (CardFormats.SelectedItem == null ? ot : GetCardFormat())));
        //    command.Parameters.Add(new SQLiteParameter("@alias", cardalias));
        //    command.Parameters.Add(new SQLiteParameter("@setcode", GetSetCode()));
        //    command.Parameters.Add(new SQLiteParameter("@type", GetTypeCode()));
        //    command.Parameters.Add(new SQLiteParameter("@atk", atk));
        //    command.Parameters.Add(new SQLiteParameter("@def", def));
        //    command.Parameters.Add(new SQLiteParameter("@level", (Level.SelectedItem == null ? 0 : Int32.Parse(Level.SelectedItem.ToString().Substring(1)))));
        //    command.Parameters.Add(new SQLiteParameter("@race", (Race.SelectedItem == null ? 0 : (Race.SelectedItem == null ? 0 : CardRaces[Race.SelectedIndex]))));
        //    command.Parameters.Add(new SQLiteParameter("@attribute", (CardAttribute.SelectedItem == null ? 0 : (CardAttribute.SelectedItem == null ? 0 : CardAttributes[CardAttribute.SelectedIndex]))));
        //    command.Parameters.Add(new SQLiteParameter("@category", GetCategoryNumber()));
        //    DatabaseHelper.ExecuteNonCommand(command);
        //    if (overwrite)
        //    {
        //        command = DatabaseHelper.CreateCommand("UPDATE texts" +
        //            " SET id= @id,name= @name,desc= @des,str1= @str1,str2= @str2,str3= @str3,str4= @str4,str5= @str5,str6= @str6,str7= @str7,str8= @str8,str9= @str9,str10= @str10,str11= @str11,str12= @str12,str13= @str13,str14= @str14,str15= @str15,str16= @str16 WHERE id= @loadedid", connection);
        //    }
        //    else
        //    {
        //        command = DatabaseHelper.CreateCommand("INSERT INTO texts (id,name,desc,str1,str2,str3,str4,str5,str6,str7,str8,str9,str10,str11,str12,str13,str14,str15,str16)" +
        //            " VALUES (@id,@name,@des,@str1,@str2,@str3,@str4,@str5,@str6,@str7,@str8,@str9,@str10,@str11,@str12,@str13,@str14,@str15,@str16)", connection);
        //    }
        //    command.Parameters.Add(new SQLiteParameter("@loadedid", updatecard));
        //    command.Parameters.Add(new SQLiteParameter("@id", cardid));
        //    command.Parameters.Add(new SQLiteParameter("@name", CardName.Text));
        //    command.Parameters.Add(new SQLiteParameter("@des", CardDescription.Text));
        //    List<SQLiteParameter> parameters = new List<SQLiteParameter>();
        //    for (int i = 0; i < 16; i++)
        //    {
        //        parameters.Add(new SQLiteParameter("@str" + (i + 1), (i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty)));
        //    }
        //    command.Parameters.AddRange(parameters.ToArray());
        //    DatabaseHelper.ExecuteNonCommand(command);
        //    connection.Close();
        //    MessageBox.Show("Card Saved");

        //    if (Program.CardData.ContainsKey(cardid))
        //    {
        //        Program.CardData[cardid] = new CardInfos(new string[] { cardid.ToString(), (CardFormats.SelectedItem == null ? ot.ToString() : GetCardFormat().ToString()),cardalias.ToString(),GetSetCode().ToString(),GetTypeCode().ToString(),
        //            (Level.SelectedItem == null ? "0" : Level.SelectedItem.ToString().Substring(1)), (Race.SelectedItem == null ? "0" : (Race.SelectedItem == null ? "0" : CardRaces[Race.SelectedIndex].ToString())),
        //        (CardAttribute.SelectedItem == null ? "0" : (CardAttribute.SelectedItem == null ? "0" : CardAttributes[CardAttribute.SelectedIndex].ToString())),atk.ToString(),def.ToString(),GetCategoryNumber().ToString()});

        //        List<string> cardtextarray = new List<string>();
        //        cardtextarray.Add(cardid.ToString());
        //        cardtextarray.Add(CardName.Text);
        //        cardtextarray.Add(CardDescription.Text);

        //        for (int i = 0; i < 17; i++)
        //        {
        //            cardtextarray.Add((i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty));
        //        }

        //        Program.CardData[cardid].SetCardText(cardtextarray.ToArray());
        //    }
        //    else
        //    {
        //        Program.CardData.Add(cardid, new CardInfos(new string[] { cardid.ToString(), (CardFormats.SelectedItem == null ? ot.ToString() : GetCardFormat().ToString()),cardalias.ToString(),GetSetCode().ToString(),GetTypeCode().ToString(),
        //            (Level.SelectedItem == null ? "0" : Level.SelectedItem.ToString().Substring(1)), (Race.SelectedItem == null ? "0" : (Race.SelectedItem == null ? "0" : CardRaces[Race.SelectedIndex].ToString())),
        //        (CardAttribute.SelectedItem == null ? "0" : (CardAttribute.SelectedItem == null ? "0" : CardAttributes[CardAttribute.SelectedIndex].ToString())),atk.ToString(),def.ToString(),GetCategoryNumber().ToString()}));

        //        List<string> cardtextarray = new List<string>();
        //        cardtextarray.Add(cardid.ToString());
        //        cardtextarray.Add(CardName.Text);
        //        cardtextarray.Add(CardDescription.Text);

        //        for (int i = 0; i < 17; i++)
        //        {
        //            cardtextarray.Add((i < EffectList.Items.Count ? EffectList.Items[i].ToString() : string.Empty));
        //        }

        //        Program.CardData[cardid].SetCardText(cardtextarray.ToArray());
        //    }
        //}

    }
}
