using System.Collections.Generic;
namespace XHSystem
{
    public interface IDataInitialization
    {
        Dictionary<string , object> DataDic { get; set; }
        string SavePath { get; }
        string SavePassword { get; }
        // PathStatus SaveStatus { get; }
        void Initialization(Dictionary<string, object> objectDic);
    }
}