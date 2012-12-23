namespace YGOPro_Launcher.CardDatabase
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using DevPro.Data.Enums;

    public class CardInfos: ICloneable
    {

        public CardInfos(DataGridViewRow data,DataGridViewRow text)
        {
            if (data != null)
            {
                this.Id = Int32.Parse(data.Cells["id"].Value.ToString());
                this.Ot = Int32.Parse(data.Cells["ot"].Value.ToString());
                this.AliasId = Int32.Parse(data.Cells["alias"].Value.ToString());
                this.Type = Int32.Parse(data.Cells["type"].Value.ToString());
                this.Atk = Int32.Parse(data.Cells["atk"].Value.ToString());
                this.Def = Int32.Parse(data.Cells["def"].Value.ToString());
                this.Level = Int32.Parse(data.Cells["level"].Value.ToString());
                this.Race = Int32.Parse(data.Cells["race"].Value.ToString());
                this.Attribute = Int32.Parse(data.Cells["attribute"].Value.ToString());
                this.Category = Int32.Parse(data.Cells["category"].Value.ToString());
                this.SetCode = Int32.Parse(data.Cells["setcode"].Value.ToString());
                
            }
            if (text != null)
            {
                this.Id = Int32.Parse(text.Cells["id"].Value.ToString());
                this.Name = text.Cells["name"].Value.ToString();
                this.Description = text.Cells["desc"].Value.ToString();
                List<string> strs = new List<string>();
                for (int i = 1; i < 17; i++)
                {
                    if (text.Cells["str" + i].Value.ToString() == "") continue;
                    strs.Add(text.Cells["str" + i].Value.ToString());
                }
                this.EffectStrings = strs.ToArray();

            }
        }

        public CardType[] GetCardTypes()
        {
            List<CardType> types = new List<CardType>();
            var typeArray = Enum.GetValues(typeof(CardType));
            foreach (CardType type in typeArray)
            {
                if (((this.Type & (int)type) != 0))
                {
                    types.Add(type);
                }
            }
            return types.ToArray();
        }

        public static CardType[] GetCardTypes(int Type)
        {
            List<CardType> types = new List<CardType>();
            var typeArray = Enum.GetValues(typeof(CardType));
            foreach (CardType type in typeArray)
            {
                if (((Type & (int)type) != 0))
                {
                    types.Add(type);
                }
            }
            return types.ToArray();
        }
        public int[] GetCardSets(List<int>setArray)
        {
            List<int> sets = new List<int>();

            sets.Add(setArray.IndexOf(SetCode & 0xffff));
            sets.Add(setArray.IndexOf(SetCode >> 0x10));

            return sets.ToArray();
        }

        public object Clone()
        {
            return  (CardInfos)this.MemberwiseClone();
        }

        public int AliasId { get; set; }

        public int Atk { get; set; }

        public int Attribute { get; set; }

        public int Def { get; set; }

        public string Description { get; set; }

        public int Id { get; private set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public int Race { get; set; }

        public int Type { get; set; }

        public int Category { get; set; }

        public int Ot { get; set; }

        public string[] EffectStrings { get; set; }

        public int SetCode { get; set; }

    }
}