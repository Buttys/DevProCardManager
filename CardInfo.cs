using System.Linq;
using DevPro_CardManager.Enums;
using System;
using System.Collections.Generic;

namespace DevPro_CardManager
{
    public class CardInfos: ICloneable
    {

        public CardInfos(IList<string> carddata)
        {
            Id = Int32.Parse(carddata[0]);
            Ot = Int32.Parse(carddata[1]);
            AliasId = Int32.Parse(carddata[2]);
            SetCode = Int64.Parse(carddata[3]);
            Type = Int32.Parse(carddata[4]);
            uint level = UInt32.Parse(carddata[5]);
            Level = level & 0xff;
            LScale = (level >> 24) & 0xff;
            RScale = (level >> 16) & 0xff;
            Race = Int32.Parse(carddata[6]);
            Attribute = Int32.Parse(carddata[7]);
            Atk = Int32.Parse(carddata[8]);
            Def = Int32.Parse(carddata[9]);
            Category =  Int64.Parse(carddata[10]);
        }

        public void SetCardText(string[] cardtext)
        {
            Name = cardtext[1];
            Description = cardtext[2];
            var effects = new List<string>();

            for (var i = 3; i < cardtext.Length; i++)
            {
                if(cardtext[i] != "")
                    effects.Add(cardtext[i]);
            }
            EffectStrings = effects.ToArray();

        }

        public CardType[] GetCardTypes()
        {
            var typeArray = Enum.GetValues(typeof(CardType));
            return typeArray.Cast<CardType>().Where(type => ((Type & (int) type) != 0)).ToArray();
        }

        public long[] GetCardSets(List<long> setArray)
        {
            var sets = new List<long> {setArray.IndexOf(SetCode & 0xffff), setArray.IndexOf(SetCode >> 0x10)};
            return sets.ToArray();
        }

        public object Clone()
        {
            return  MemberwiseClone();
        }

        public int AliasId { get; set; }

        public int Atk { get; set; }

        public int Attribute { get; set; }

        public int Def { get; set; }

        public string Description { get; set; }

        public int Id { get; private set; }

        public uint Level { get; set; }

        public uint LScale { get; set; }

        public uint RScale { get; set; }

        public string Name = "";

        public int Race { get; set; }

        public int Type { get; set; }

        public long Category { get; set; }

        public int Ot { get; set; }

        public string[] EffectStrings { get; set; }

        public long SetCode { get; set; }

    }
}