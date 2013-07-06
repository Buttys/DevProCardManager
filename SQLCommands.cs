using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public static class SQLiteCommands
    {
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic,
                                      TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        public static bool UpdateCardId(string cardId, string updatedId, SQLiteConnection connection)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand("UPDATE datas SET id=@updatedId WHERE id=@cardId", connection);
                SQLiteCommand command2 = new SQLiteCommand("UPDATE texts SET id=@updatedId WHERE id=@cardId", connection);

                command.Parameters.Add(new SQLiteParameter("@updatedId", updatedId));
                command2.Parameters.Add(new SQLiteParameter("@updatedId", updatedId));

                command.Parameters.Add(new SQLiteParameter("@cardId", cardId));
                command2.Parameters.Add(new SQLiteParameter("@cardId", cardId));

                DatabaseHelper.ExecuteNonCommand(command);
                DatabaseHelper.ExecuteNonCommand(command2);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool SaveCard
            (int updateid,int cardid,int ot,int cardalias,int atk,int def,
            int setcode,int type, int lvl,int race,int attribute, int category)
        {
            try
            {
                SQLiteCommand command = null;
                if (updateid != cardid)
                {
                    command = new SQLiteCommand("INSERT INTO datas(id,ot,alias,setcode,type,atk,def,level,race,attribute,category)" +
                    " VALUES (@id, @ot, @alias, @setcode, @type, @atk, @def, @level, @race, @attribute, @category)" +
                    " ON DUPLICATE KEY UPDATE id=@id, ot=@ot, alias=@alias, setcode=@setcode, type=@type, atk=@atk, def=@def, level=@level, race=@race, attribute=@attribute, category=@category"
                    );
                }
                else
                {
                    command = new SQLiteCommand("UPDATE id=@id, ot=@ot, alias=@alias, setcode=@setcode, type=@type, atk=@atk, def=@def, level=@level, race=@race, attribute=@attribute, category=@category WHERE id=@loadedid");
                }

                command.Parameters.Add(new SQLiteParameter("@loadedid", updateid));
                command.Parameters.Add(new SQLiteParameter("@id", cardid));
                command.Parameters.Add(new SQLiteParameter("@ot", ot));
                command.Parameters.Add(new SQLiteParameter("@alias", cardalias));
                command.Parameters.Add(new SQLiteParameter("@setcode", setcode));
                command.Parameters.Add(new SQLiteParameter("@type", type));
                command.Parameters.Add(new SQLiteParameter("@atk", atk));
                command.Parameters.Add(new SQLiteParameter("@def", def));
                command.Parameters.Add(new SQLiteParameter("@level", lvl));
                command.Parameters.Add(new SQLiteParameter("@race",race));
                command.Parameters.Add(new SQLiteParameter("@attribute", attribute));
                command.Parameters.Add(new SQLiteParameter("@category", category));
                DatabaseHelper.ExecuteNonCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }

        public static bool ExecuteSqlCommand(string commandstring)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(commandstring);
                DatabaseHelper.ExecuteNonCommand(command);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK);
                return false;
            }
        }
    }
}
