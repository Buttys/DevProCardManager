using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static string ExecuteStringCommand(SQLiteCommand command, int columncount)
        {
            try
            {
                string value = null;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for(int i = 0; i < columncount + 1; i ++)
                        {
                            if (value == null)
                                value = reader.GetString(i);
                            else
                                value += "," + reader.GetString(i);
                        }
                    }
                    reader.Close();
                return (value ?? "not found");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
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
