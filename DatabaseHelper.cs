using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DevPro_CardManager
{
    public static class DatabaseHelper
    {
        public static SQLiteCommand CreateCommand(string statement, SQLiteConnection connection)
        {
            return new SQLiteCommand
            {
                CommandText = statement,
                CommandType = CommandType.Text,
                Connection = connection
            };
        }

        public static bool ExecuteNonCommand(SQLiteCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public static List<string[]> ExecuteStringCommand(SQLiteCommand command, int columncount)
        {
            try
            {
                var values = new List<string[]>();
                SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new List<string>();
                        for(int i = 0;i < reader.FieldCount; i++)
                        {
                            row.Add(reader[i].ToString());
                        }
                        values.Add(row.ToArray());
                    }
                    reader.Close();
                return values;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<string[]>();
            }
        }

        public static int ExecuteIntCommand(SQLiteCommand command)
        {
            try
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
        }
    }
}
