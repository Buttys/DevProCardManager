using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace DevPro_CardManager
{
    public static class CardManager
    {
        private static Dictionary<int, CardInfos> CardData = new Dictionary<int, CardInfos>();
        private static Dictionary<int, CDBData> loadedCDB = new Dictionary<int, CDBData>();

        private static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic,
                              TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        public static bool LoadCDB(string dir, bool overwrite,bool clearData = false)
        {
            if (!File.Exists(dir))
                return false;

            if (clearData)
            {
                CardData.Clear();
                loadedCDB.Clear();
            }

            SQLiteConnection connection = new SQLiteConnection("Data Source=" + dir);
            List<string[]> datas = new List<string[]>();
            List<string[]> texts = new List<string[]>();

            try
            {
                connection.Open();
                datas = SQLiteCommands.LoadData(connection);
                texts = SQLiteCommands.LoadText(connection);
                connection.Close();
            }
            catch(Exception)
            {
                connection.Close();
                return false;
            }

            int cdbsource = loadedCDB.Count + 1;
            loadedCDB.Add(cdbsource, new CDBData(clearData ? "Master": Path.GetFileNameWithoutExtension(dir), dir));

            foreach (string[] row in datas)
            {
                if (overwrite)
                    CardManager.UpdateOrAddCard(new CardInfos(row, cdbsource));
                else
                {
                    if (!CardManager.ContainsCard(Int32.Parse(row[0])))
                        CardManager.UpdateOrAddCard(new CardInfos(row, cdbsource));
                }
            }
            foreach (string[] row in texts)
            {
                if (CardManager.ContainsCard(Int32.Parse(row[0])))
                    CardManager.GetCard(Int32.Parse(row[0])).SetCardText(row);
            }

            return true;
        }

        public static CardInfos GetCard(int id)
        {
            if (CardData.ContainsKey(id))
                return CardData[id];
            return null;
        }

        public static bool RemoveCard(int id)
        {
            if (CardData.ContainsKey(id))
                return CardData.Remove(id);
            return false;
        }

        public static void UpdateOrAddCard(CardInfos card)
        {
            if (ContainsCard(card.Id))
                CardData[card.Id] = card;
            else
                CardData.Add(card.Id, card);
        }

        public static bool ContainsCard(int id)
        {
            return CardData.ContainsKey(id);
        }

        public static void RenameKey(int fromkey,int tokey)
        {
            CardData.RenameKey(fromkey, tokey);
        }

        public static Dictionary<int, CardInfos>.KeyCollection GetKeys()
        {
            return CardData.Keys;
        }

        public static int Count
        {
          get { return CardData.Count; }
        }

        public static string GetDatabaseDir(int source)
        {
            if (loadedCDB.ContainsKey(source))
                return loadedCDB[source].Dir;
            return null;
        }

        public static string[] GetDatabaseNames()
        {
            List<string> keys = new List<string>();
            foreach (int key in loadedCDB.Keys)
                keys.Add(loadedCDB[key].Name);
            return keys.ToArray();
        }
    }
}
