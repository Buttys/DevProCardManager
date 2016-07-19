using Decoder = SevenZip.Compression.LZMA.Decoder;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace DevPro_CardManager.Components
{
    public class ReplayReader
    {
        public struct ReplayHeader
        {
            public uint Id;
            public uint Version;
            public uint Flag;
            public uint Seed;
            public uint DataSize;
            public uint Hash;
            public byte[] Props;
        }

        public class DeckInfo
        {
            public List<string> MainDeck = new List<string>();
            public List<string> ExtraDeck = new List<string>();
        }

        public const int ReplaySize = 32;

        public ReplayHeader Header;
        public BinaryReader DataReader;

        private byte[] _mFileContent;
        private byte[] _mData;

        private List<string> _players = new List<string>();
        private List<DeckInfo> _deckData = new List<DeckInfo>();

        public bool Compressed
        {
            get { return (Header.Flag & 0x1) != 0; }
        }

        public bool Tag
        {
            get { return (Header.Flag & 0x2) != 0; }
        }

        public bool FromFile(MemoryStream file)
        {
            try
            {
                _players.Clear();
                _deckData.Clear();
                _mFileContent = file.ToArray();
                file.Position = 0;
                BinaryReader reader = new BinaryReader(file); 
                HandleHeader(reader);
                HandleData(reader);
                reader.Close();
                ExtractDeckData();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool FromFile(string fileName)
        {
            try
            {
                _mFileContent = File.ReadAllBytes(fileName);
                FromFile(new MemoryStream(_mFileContent));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public string ReadString(int length)
        {
            string value = Encoding.Unicode.GetString(DataReader.ReadBytes(length));
            return value.Substring(0, value.IndexOf("\0", StringComparison.Ordinal));
        }

        public string[] GetPlayers()
        {
            return _players.ToArray();
        }

        public string[] GetMainDeck(string name)
        {
            int index = _players.IndexOf(name);
            if (index >= 0)
                return _deckData[index].MainDeck.ToArray();
            return new string[0];
        }
        public string[] GetExtraDeck(string name)
        {
            int index = _players.IndexOf(name);
            if (index >= 0)
                return _deckData[index].ExtraDeck.ToArray();
            return new string[0];
        }

        private void HandleHeader(BinaryReader reader)
        {
            Header.Id = reader.ReadUInt32();
            Header.Version = reader.ReadUInt32();
            Header.Flag = reader.ReadUInt32();
            Header.Seed = reader.ReadUInt32();
            Header.DataSize = reader.ReadUInt32();
            Header.Hash = reader.ReadUInt32();
            Header.Props = reader.ReadBytes(8);
        }

        private void HandleData(BinaryReader reader)
        {
            int compressedSize = _mFileContent.Length - ReplaySize;
            if (!Compressed)
            {
                _mData = reader.ReadBytes(compressedSize);
                DataReader = new BinaryReader(new MemoryStream(_mData));
                return;
            }

            Byte[] inData = new byte[compressedSize];
            Array.Copy(_mFileContent, ReplaySize, inData, 0, compressedSize);
            Byte[] outData = new byte[Header.DataSize];

            Decoder lzma = new Decoder();
            lzma.SetDecoderProperties(Header.Props);
            lzma.Code(new MemoryStream(inData), new MemoryStream(outData), compressedSize, Header.DataSize, null);

            _mData = outData;
            DataReader = new BinaryReader(new MemoryStream(_mData));
        }

        private bool ExtractDeckData()
        {
            try
            {
                _players.Add(ReadString(40));
                _players.Add(ReadString(40));
                if (Tag)
                {
                    _players.Add(ReadString(40));
                    _players.Add(ReadString(40));
                }

                DataReader.ReadInt32();//lifepoints
                DataReader.ReadInt32();//handcount
                DataReader.ReadInt32();//drawcount
                DataReader.ReadInt32();//other duel settings

                
                for (int i = 0; i < _players.Count; i++)
                {
                    DeckInfo deck = new DeckInfo();
                    //main deck
                    int count = DataReader.ReadInt32();
                    for (int main = 0; main < count; main++)
                        deck.MainDeck.Add(DataReader.ReadInt32().ToString());
                    //extra deck
                    count = DataReader.ReadInt32();
                    for (int extra = 0; extra < count; extra++)
                        deck.ExtraDeck.Add(DataReader.ReadInt32().ToString());
                    _deckData.Add(deck);
                }
            }
            catch(Exception)
            {
                //failed invalid data?
                return false;
            }

            return true;

        }

    }
}
