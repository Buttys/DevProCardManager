using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public static class SQLiteCommands
    {
        public static List<string[]> LoadData(SQLiteConnection connection)
        {
            SQLiteCommand datacommand = new SQLiteCommand("SELECT id, ot, alias, setcode, type, level, race, attribute, atk, def, category FROM datas", connection);
            return DatabaseHelper.ExecuteStringCommand(datacommand, 11);
        }

        public static List<string[]> LoadText(SQLiteConnection connection)
        {
            SQLiteCommand textcommand = new SQLiteCommand("SELECT id, name, desc, str1, str2, str3, str4, str5, str6, str7, str8, str9, str10, str11, str12, str13, str14, str15, str16 FROM texts", connection);
            return DatabaseHelper.ExecuteStringCommand(textcommand,19);
        }

        public static bool UpdateCardId(string cardId, string updatedId, SQLiteConnection connection)
        {
            try
            {
                SQLiteCommand command = DatabaseHelper.CreateCommand("UPDATE datas SET id=@updatedId WHERE id=@cardId", connection);

                command.Parameters.Add(new SQLiteParameter("@updatedId", updatedId));
                command.Parameters.Add(new SQLiteParameter("@cardId", cardId));
                DatabaseHelper.ExecuteNonCommand(command);

                command = DatabaseHelper.CreateCommand("UPDATE texts SET id=@updatedId WHERE id=@cardId", connection);
                command.Parameters.Add(new SQLiteParameter("@updatedId", updatedId));
                command.Parameters.Add(new SQLiteParameter("@cardId", cardId));
                DatabaseHelper.ExecuteNonCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool UpdateCardOt(string cardId, string updatedOt, SQLiteConnection connection)
        {
            try
            {
                SQLiteCommand command = DatabaseHelper.CreateCommand("UPDATE datas SET ot=@updatedOt WHERE id=@cardId", connection);
                command.Parameters.Add(new SQLiteParameter("@updatedOt", updatedOt));
                command.Parameters.Add(new SQLiteParameter("@cardId", cardId));

                return DatabaseHelper.ExecuteNonCommand(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool SaveCard(CardInfos card, SQLiteConnection connection, int loadedid, bool overwrite)
        {
            try
            {
                SQLiteCommand command;
                if(overwrite)
                {
                    command = DatabaseHelper.CreateCommand("UPDATE datas" +
                  " SET id= @id, ot = @ot, alias= @alias, setcode= @setcode, type= @type, atk= @atk, def= @def, level= @level, race= @race, attribute= @attribute, category= @category WHERE id = @loadedid", connection);
                }
                else
                {
                    command = DatabaseHelper.CreateCommand("INSERT INTO datas (id,ot,alias,setcode,type,atk,def,level,race,attribute,category)" +
                        " VALUES (@id, @ot, @alias, @setcode, @type, @atk, @def, @level, @race, @attribute, @category)", connection);
                }

                command.Parameters.Add(new SQLiteParameter("@loadedid", loadedid));
                command.Parameters.Add(new SQLiteParameter("@id", card.Id));
                command.Parameters.Add(new SQLiteParameter("@ot", card.Ot));
                command.Parameters.Add(new SQLiteParameter("@alias", card.AliasId));
                command.Parameters.Add(new SQLiteParameter("@setcode", card.SetCode));
                command.Parameters.Add(new SQLiteParameter("@type", card.Type));
                command.Parameters.Add(new SQLiteParameter("@atk", card.Atk));
                command.Parameters.Add(new SQLiteParameter("@def", card.Def));
                command.Parameters.Add(new SQLiteParameter("@level", card.GetLevelCode()));
                command.Parameters.Add(new SQLiteParameter("@race", card.Race));
                command.Parameters.Add(new SQLiteParameter("@attribute", card.Attribute));
                command.Parameters.Add(new SQLiteParameter("@category", card.Category));
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
                command.Parameters.Add(new SQLiteParameter("@loadedid", loadedid));
                command.Parameters.Add(new SQLiteParameter("@id", card.Id));
                command.Parameters.Add(new SQLiteParameter("@name", card.Name));
                command.Parameters.Add(new SQLiteParameter("@des", card.Description));
                var parameters = new List<SQLiteParameter>();
                for (int i = 0; i < 16; i++)
                {
                    parameters.Add(new SQLiteParameter("@str" + (i + 1), (i < card.EffectStrings.Length ? card.EffectStrings[i].ToString() : string.Empty)));
                }
                command.Parameters.AddRange(parameters.ToArray());
                return DatabaseHelper.ExecuteNonCommand(command);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool DeleteCard(int cardid, SQLiteConnection connection)
        {
            try
            {
                SQLiteCommand command = DatabaseHelper.CreateCommand("DELETE FROM datas WHERE id= @id", connection);
                command.Parameters.Add(new SQLiteParameter("@id", cardid));
                DatabaseHelper.ExecuteIntCommand(command);
                command = DatabaseHelper.CreateCommand("DELETE FROM texts WHERE id= @id", connection);
                command.Parameters.Add(new SQLiteParameter("@id", cardid));

                return DatabaseHelper.ExecuteNonCommand(command);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool ContainsCard(int cardid, SQLiteConnection connection)
        {
            try
            {
                SQLiteCommand checkcommand = DatabaseHelper.CreateCommand("SELECT COUNT(*) FROM datas WHERE id= @id", connection);
                checkcommand.Parameters.Add(new SQLiteParameter("@id", cardid));
                return DatabaseHelper.ExecuteIntCommand(checkcommand) > 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
