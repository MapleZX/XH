using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace XHSystem
{
    public static class DataExtensions
    {
        public static Dictionary<string, object> InitializationData(this IDataInitialization _data)
        {
            return _data.InitializationData(_data.SavePath, _data.SavePassword, null/*, _data.SaveStatus*/);
        }

        public static Dictionary<string, object> InitializationData(this IDataInitialization _data, FieldInfo[] fieldInfo)
        {
            return _data.InitializationData(_data.SavePath, _data.SavePassword, fieldInfo/*, _data.SaveStatus*/);
        }

        public static Dictionary<string, object> InitializationData(this IDataInitialization _data, string path, string password = "", FieldInfo[] fieldInfo = null /*, PathStatus status = PathStatus.User*/)
        {
            Type dataType = _data.GetType();
            var node = _data as Node;
 
            var binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            // FieldInfo[] info = dataType.GetFields(binding);
            FieldInfo[] infos = fieldInfo == null ? dataType.GetFields(binding) : fieldInfo;

            Type attrType = typeof(DataAttribute);

            Dictionary<string, object> objectDic = new Dictionary<string, object>();
            _data.Initialization(objectDic);
            foreach (var fo in infos)
            {
                bool isDataAttribute = Attribute.IsDefined(fo, attrType);
                if (isDataAttribute)
                {
                    // DataAttribute attr = (DataAttribute)Attribute.GetCustomAttribute(fo, attrType);
                    // var saveFile = attr.saveFile == "" ? fo.Name  : attr.saveFile;
                    // var savePath = attr.savePath == "" ? path : attr.savePath;
                    // var savePassword = attr.password == "" ? password : attr.password;
                    // var initiData = objectDic[saveFile];
                    // var data = initiData;
                    // if (savePassword == "")
                    //     data = node.LoadData(savePath, saveFile + ".save", initiData, fo.FieldType, status);
                    // else if (savePassword != "")
                    //     data = node.LoadData(savePath, saveFile + ".save", initiData, password, fo.FieldType, status);
                    // fo.SetValue(_data, data);
                    // objectDic[saveFile] = data;
                }
            }
            return objectDic;
        }

        public static Error Save(this IDataInitialization _data)
        {
            return _data.Save(_data.SavePath, _data.SavePassword, _data.DataDic, null/*, _data.SaveStatus*/);
        }

        public static Error Save(this IDataInitialization _data, FieldInfo[] fieldInfo)
        {
            return _data.Save(_data.SavePath, _data.SavePassword, null, fieldInfo/*, _data.SaveStatus*/);
        }

        public static Error Save(this IDataInitialization _data, string path, string password = "", Dictionary<string, object> dataDic = null, FieldInfo[] fieldInfo = null/*, PathStatus status = PathStatus.User*/)
        {
            var node = _data as Node;
            if (dataDic != null)
            {
                foreach(var data in dataDic)
                {
                //    if (password == "")
                //         return node.SaveData(path, data.Key + ".save", data.Value, status);
                //    else if (password != "")
                //         return node.SaveData(path, data.Key + ".save", data.Value, password, status);
                }
            }

            Type dataType = _data.GetType();

            var binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            // FieldInfo[] info = dataType.GetFields(binding);
            FieldInfo[] infos = fieldInfo == null ? dataType.GetFields(binding) : fieldInfo;

            Type attrType = typeof(DataAttribute);
            Error error = Error.Failed;
            foreach (var fo in infos)
            {
                bool isDataAttribute = Attribute.IsDefined(fo, attrType);
                if (isDataAttribute)
                {
                    // DataAttribute attr = (DataAttribute)Attribute.GetCustomAttribute(fo, attrType);
                    // var saveFile = attr.saveFile == "" ? fo.Name  : attr.saveFile;
                    // var savePath = attr.savePath == "" ? path : attr.savePath;
                    // var savePassword = attr.password == "" ? password : attr.password;
                    // var data = fo.GetValue(_data);
                    // if (password == "")
                    //     error = node.SaveData(savePath, saveFile + ".save", data, status);
                    // else if (password != "")
                    //     error = node.SaveData(savePath, saveFile + ".save", data, savePassword, status);
                }
            }
            return error;
        }
    }
}