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

        public CardInfos(string[] carddata)
        {
            Id = Int32.Parse(carddata[0]);
            Ot = Int32.Parse(carddata[1]);
            AliasId = Int32.Parse(carddata[2]);
            SetCode = Int32.Parse(carddata[3]);
            Type = Int32.Parse(carddata[4]);
            Level = Int32.Parse(carddata[5]);
            Race = Int32.Parse(carddata[6]);
            Attribute = Int32.Parse(carddata[7]);
            Atk = Int32.Parse(carddata[8]);
            Def = Int32.Parse(carddata[9]);
            Category = Int32.Parse(carddata[10]);
        }

        public void SetCardText(string[] cardtext)
        {
            Name = cardtext[1];
            Description = cardtext[2];
            List<string> effects = new List<string>();

            for (int i = 3; i < cardtext.Length; i++)
            {
                if(cardtext[i] != "")
                    effects.Add(cardtext[i]);
            }
            EffectStrings = effects.ToArray();

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

        public string Name = "";

        public int Race { get; set; }

        public int Type { get; set; }

        public int Category { get; set; }

        public int Ot { get; set; }

        public string[] EffectStrings { get; set; }

        public int SetCode { get; set; }

    }
}