using Godot;
using System;
using System.Collections.Generic;

namespace XHSystem.old
{
    public static class ConfigurationExtension
    {
        // # region 配置文件
        // public static Dictionary<string, Dictionary<string, object>> LoadConfig(this object ojc, string path, string file, string default_path, PathStatus status = PathStatus.User)
        // {
        //     ConfigFile config = new ConfigFile();
        //     var dirPath = node.GetPathPrefix(status) + path + "/" + file;      
        //     Error error = config.Load(dirPath);
        //     if (error == Error.Ok)
        //     {
        //         return LoadConfig(config);
        //     }

        //     var dir_default = default_path;
        //     Error deferror = config.Load(dir_default);

        //     if (deferror == Error.Ok)
        //     {
        //         node.SaveConfig(path, file, LoadConfig(config), status);
        //         return LoadConfig(config);
        //     }

        //     return null;
        // }
        // public static Error SaveConfig(this Node node, string path, string file, Dictionary<string, Dictionary<string, object>> config_dic, PathStatus status = PathStatus.User)
        // {
        //     ConfigFile config = new ConfigFile();
        //     Directory dir = new Directory();
        //     node.RemoveFile(dir, path + "/" + file, status);
        //     var dirPath = node.DirNewFile(dir, path, status);
        //     Error error = config.Load(dirPath + "/" + file);

        //     if (error != Error.Ok) 
        //     {
        //         config.Save(dirPath + "/" + file);
        //         error = config.Load(dirPath + "/" + file);
        //     }

        //     if (error == Error.Ok)
        //     {
        //         foreach (var section in config_dic)
        //         {
        //             var sectionKey = section.Value;
        //             foreach (var value in sectionKey)
        //             {
        //                 config.SetValue(section.Key, value.Key, value.Value);
        //             }
        //         }
        //         config.Save(dirPath + "/" + file);
        //     }
        //     return error;
        // }
        // private static ConfigFile GetConfigFile(ConfigFile config)
        // {
        //     var fig = config == null ? new ConfigFile() : config;
        //     return fig;
        // }
        // private static Dictionary<string, Dictionary<string, object>> LoadConfig(ConfigFile config)
        // {
        //     var sectionDic = new Dictionary<string, Dictionary<string, object>>();
        //     var sections = config.GetSections();
        //     foreach (var section in sections)
        //     {
        //         var sectionKeys = config.GetSectionKeys(section);
        //         var keyDic = new Dictionary<string, object>();
        //         foreach (var key in sectionKeys)
        //         {
        //             var value = config.GetValue(section, key);
        //             keyDic[key] = value;
        //         }
        //         sectionDic[section] = keyDic;
        //     }
        //     return sectionDic;
        // }
        // # endregion
    }
}