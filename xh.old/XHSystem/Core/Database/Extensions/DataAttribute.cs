using System;
namespace XHSystem
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataAttribute : Attribute
    {
        public int ID;
        public string saveFile;
        public string savePath;
        public string password;

        public DataAttribute(string saveFile = "", string savePath = "", string password = "", int ID = -1)
        {
            this.ID = ID;
            this.saveFile = saveFile;
            this.savePath = savePath;
            this.password = password;
        }
    }
}