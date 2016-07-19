namespace DevPro_CardManager
{
    public class CDBData
    {
        public CDBData(string name, string dir)
        {
            Dir = dir;
            Name = name;
        }
        public string Dir { get; private set; }
        public string Name { get; private set; }
    }
}
