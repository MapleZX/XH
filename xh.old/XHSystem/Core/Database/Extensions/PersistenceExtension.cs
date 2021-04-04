using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace XHSystem
{
	public static class PersistenceExtension
	{    
		# region 配置文件
		public static Dictionary<string, Dictionary<string, object>> LoadConfig(this object ojc, string configPath,
			string defaultPath, ConfigFile configFile = null, Directory directory = null)
		{
			var config = GetConfigFile(configFile);
			Error error = config.Load(configPath);
			if (error == Error.Ok)
			{
				var data = LoadConfig(config);
				return LoadConfig(config);
			}

			error = config.Load(defaultPath);
			if (error == Error.Ok)
			{
				var data = LoadConfig(config);
				ojc.SaveConfig(configPath, data, config, directory);
				return data;
			}
			return null;
		}
		public static Error SaveConfig(this object ojc, string configPath, Dictionary<string, Dictionary<string, object>> configDic, 
			ConfigFile configFile = null, Directory directory = null)
		{
			var config = GetConfigFile(configFile);
			var dir = directory == null ? new Directory() : directory;

			ojc.RemoveDirectoryOrFile(dir, configPath);
			var path = GetSaveDataPath(ojc, configPath, directory);

			Error error = config.Load(path);

			if (error != Error.Ok) 
			{
				config.Save(path);
				error = config.Load(path);
			}

			if (error == Error.Ok) 
			{
				foreach (var section in configDic)
				{
					var sectionKey = section.Value;
					foreach (var value in sectionKey)
					{
						config.SetValue(section.Key, value.Key, value.Value);
					}
				}
				config.Save(path);
			}

			return error;
		}

		private static ConfigFile GetConfigFile(ConfigFile config)
		{
			var fig = config == null ? new ConfigFile() : config;
			return fig;
		}

		private static Dictionary<string, Dictionary<string, object>> LoadConfig(ConfigFile config)
		{
			var sectionDic = new Dictionary<string, Dictionary<string, object>>();
			var sections = config.GetSections();
			foreach (var section in sections)
			{
				var sectionKeys = config.GetSectionKeys(section);
				var keyDic = new Dictionary<string, object>();
				foreach (var key in sectionKeys)
				{
					var value = config.GetValue(section, key);
					keyDic[key] = value;
				}
				sectionDic[section] = keyDic;
			}
			return sectionDic;
		}

		# endregion

		# region 加载Csv
		public static List<string[]> LoadCsv(this object ojc, string savePath, List<string[]> defaultCsv,
			File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultCsv, null, null, SaveStatus.Csv, saveFile, directory);

			if (data == null) return null;

			return (List<string[]>)data;
		}

		public static List<string[]> LoadCsv(this object ojc, string savePath, List<string[]> defaultCsv, byte[] key,
			File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultCsv, null, key, SaveStatus.Csv, saveFile, directory);

			if (data == null) return null;

			return (List<string[]>)data;
		}

		public static List<string[]> LoadCsv(this object ojc, string savePath, List<string[]> defaultCsv, string password,
			File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultCsv, null, password, SaveStatus.Csv, saveFile, directory);

			if (data == null) return null;

			return (List<string[]>)data;
		}
		# endregion

		# region 存储Csv

		public static Error SaveCsv(this object ojc, string csvPath, List<string[]> csv, File saveFile = null,
			Directory directory = null)
		{
			return Save(ojc, csvPath, csv, null, SaveStatus.Csv, saveFile, directory);
		}

		public static Error SaveCsv(this object ojc, string csvPath, List<string[]> csv, byte[] key, File saveFile = null,
			Directory directory = null)
		{
			return Save(ojc, csvPath, csv, key, SaveStatus.Csv, saveFile, directory);
		}

		public static Error SaveCsv(this object ojc, string csvPath, List<string[]> csv, string password, File saveFile = null,
			Directory directory = null)
		{
			return Save(ojc, csvPath, csv, password, SaveStatus.Csv, saveFile, directory);
		}

		# endregion

		# region 读取未加密数据
		public static object LoadSingleData(this object ojc, string savePath, object defaultData, Type objectType,
			File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultData, objectType, null, SaveStatus.Single, saveFile, directory);
			return data;
		}

		public static T LoadSingleData<T>(this object ojc, string savePath, object defaultData,File saveFile = null, 
			Directory directory = null) where T : class
		{
			var data = ojc.LoadSingleData(savePath, defaultData, typeof(T), saveFile, directory);

			if (data == null) return null;

			return (T)data;
		}

		public static object[] LoadMultipleData(this object ojc, string savePath, object[] defaultData, Type objectType,
			File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultData, objectType, null, SaveStatus.Multiple, saveFile, directory);

			if (data == null) return null;

			return (object[])data;
		}

		public static T[] LoadMultipleData<T>(this object ojc, string savePath, object[] defaultData, File saveFile = null, 
			Directory directory = null) where T : class
		{
			var data = ojc.LoadMultipleData(savePath, defaultData, typeof(T), saveFile, directory);

			if (data == null) return null;

			return ojc.ChangeDataArrayType<T>(data);
		}
		# endregion

		# region 使用byte读取加密数据
		public static object LoadSingleData(this object ojc, string savePath, object defaultData, Type objectType,
			byte[] key, File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultData, objectType, key, SaveStatus.Single, saveFile, directory);
			return data;
		}

		public static T LoadSingleData<T>(this object ojc, string savePath, object defaultData, byte[] key,
			File saveFile = null, Directory directory = null) where T : class
		{
			var data = ojc.LoadSingleData(savePath, defaultData, typeof(T), key, saveFile, directory);

			if (data == null) return null;

			return (T)data;
		}

		public static object[] LoadMultipleData(this object ojc, string savePath, object[] defaultData, Type objectType,
			byte[] key, File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultData, objectType, key, SaveStatus.Multiple, saveFile, directory);

			if (data == null) return null;

			return (object[])data;
		}

		public static T[] LoadMultipleData<T>(this object ojc, string savePath, object[] defaultData, byte[] key,
			File saveFile = null, Directory directory = null) where T : class
		{
			var data = ojc.LoadMultipleData(savePath, defaultData, typeof(T), key, saveFile, directory);

			if (data == null) return null;

			return ojc.ChangeDataArrayType<T>(data);
		}
		# endregion

		# region 使用字符串密码读取数据
		public static object LoadSingleData(this object ojc, string savePath, object defaultData, Type objectType,
			string password, File saveFile = null, Directory directory = null)
		{
			var data = Load(ojc, savePath, defaultData, objectType, password, SaveStatus.Single, saveFile, directory);
			return data;
		}

		public static T LoadSingleData<T>(this object ojc, string savePath, object defaultData, string password,
			File saveFile = null, Directory directory = null) where T : class
		{
			var data = ojc.LoadSingleData(savePath, defaultData, typeof(T), password, saveFile, directory);

			if (data == null) return null;

			return (T)data;
		}

		public static object[] LoadMultipleData(this object ojc, string savePath, object[] defaultData, Type objectType,
			string password, File saveFile = null, Directory directory = null)
		{

			var data = Load(ojc, savePath, defaultData, objectType, password, SaveStatus.Multiple, saveFile, directory);

			if (data == null) return null;

			return (object[])data;
		}

		public static T[] LoadMultipleData<T>(this object ojc, string savePath, object[] defaultData, string password,
			File saveFile = null, Directory directory = null) where T : class
		{
			var data = ojc.LoadMultipleData(savePath, defaultData, typeof(T), password, saveFile, directory);

			if (data == null) return null;

			return ojc.ChangeDataArrayType<T>(data);
		}

		# endregion

		# region 不加密存储数据
		public static Error SaveSingleData(this object ojc, string savePath, object saveData, File saveFile = null,
			Directory directory = null)
		{
			return Save(ojc, savePath, saveData, null, SaveStatus.Single, saveFile, directory);
		}

		public static Error SaveMultipleData(this object ojc, string savePath, object[] saveData, File saveFile = null,
			Directory directory = null)
		{
		
			return Save(ojc, savePath, saveData, null, SaveStatus.Multiple, saveFile, directory);
		}

		# endregion

		# region 使用byte密码保存数据
		public static Error SaveSingleData(this object ojc, string savePath, object saveData, byte[] key,
			File saveFile = null, Directory directory = null)
		{
			return Save(ojc, savePath, saveData, key, SaveStatus.Single, saveFile, directory);
		}

		public static Error SaveMultipleData(this object ojc, string savePath, object[] saveData, byte[] key,
			File saveFile = null, Directory directory = null)
		{
			return Save(ojc, savePath, saveData, key, SaveStatus.Multiple, saveFile, directory);
		}

		# endregion

		# region 使用字符串密码保存数据
		public static Error SaveSingleData(this object ojc, string savePath, object saveData, string password,
			File saveFile = null, Directory directory = null)
		{
			return Save(ojc, savePath, saveData, password, SaveStatus.Single, saveFile, directory);
		}

		public static Error SaveMultipleData(this object ojc, string savePath, object[] saveData, string password,
			File saveFile = null, Directory directory = null)
		{
			return Save(ojc, savePath, saveData, password, SaveStatus.Multiple, saveFile, directory);
		}
		# endregion

		# region 获取存档文件的数据流
		public enum SaveStatus { Single, Multiple, Csv}
		/// <summary>
		/// 变化数组类型
		/// </summary>
		/// <param name="ojc"></param>
		/// <param name="loadData"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T[] ChangeDataArrayType<T>(this object ojc, object[] loadData)
		{
			T[] data = new T[loadData.Length];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = (T)loadData[i];
			}
			return data;
		}

		/// <summary>
		/// 验证正确的保存路径。
		/// </summary>
		/// <param name="ojc">扩展对象</param>
		/// <param name="savePath">保存</param>
		/// <param name="directory">目录</param>
		/// <param name="status">路径类型</param>
		/// <returns>可以使用的路径</returns>
		public static string GetSaveDataPath(object ojc, string savePath, Directory directory)
		{
			// 传入的目录程序为空则新建一个。
			var dir = directory == null ? new Directory() : directory;

			var dirPath = "";
			var filePath = "";
			ojc.GetDirectoryAndFile(savePath, out dirPath, out filePath);

			dirPath = ojc.CorrectDirectory(dir, dirPath);

			var path = dirPath + filePath;

			return path;
		}

		private static File GetFile(File saveFile)
		{
			var file = saveFile == null ? new File() : saveFile;
			return file;
		}

		private static List<string[]> LoadCsv(Error error, File saveFile)
		{
			List<string[]> config = new List<string[]>();
			if (error == Error.Ok)
			{
				while(!saveFile.EofReached())
				{
					var csv = saveFile.GetCsvLine();
					config.Add(csv);
				}
			}
			saveFile.Close();

			if (config.Count > 0)
			{
				if (config[config.Count - 1] != null)
				{
					if (config[config.Count - 1][0] == "")
					{
						config.RemoveAt(config.Count - 1);
					}
				}
			} 
			
			return config;
		}

		private static void SaveCsv(Error error, File saveFile, List<string[]> config)
		{
			// 将数据保存成csv样式
			if (error == Error.Ok)
			{
				foreach(var item in config)
				{
					saveFile.StoreCsvLine(item);
				}
			}
			saveFile.Close();
		}
		private static object Load(object ojc, string savePath, object defaultData, Type objectType, object password, SaveStatus saveStatus,
			File saveFile, Directory directory)
		{
			var file = GetFile(saveFile);

			var path = savePath;

			var data = defaultData; 

			Error error = Open(password, file, path, File.ModeFlags.Read);

			if (error == Error.Ok)
			{
				if (saveStatus == SaveStatus.Single)
				{
					data = LoadSingleData(error, file, objectType);
				}

				else if (saveStatus == SaveStatus.Multiple)
				{
					data = LoadMultipleData(error, file, objectType);
				}

				else if (saveStatus == SaveStatus.Csv)
				{
					data = LoadCsv(error, file);
				}
			}
			else
			{
				if (data == null) return null;
				Save(ojc, savePath, data, password, saveStatus, file, directory);
			}
  
			return data;
		}
		private static object LoadSingleData(Error error, File saveFile, Type objectType)
		{
			// 加载数据,返回对应的单个数据
			object data = null;
			if (error != Error.Ok) 
			{
				saveFile.Close();
				return data;
			}

			var josn = saveFile.GetLine();

			data = JsonConvert.DeserializeObject(josn, objectType);
			saveFile.Close();
			return data;
		}

		private static object[] LoadMultipleData(Error error, File saveFile, Type objectType)
		{
			// 加载数据,返回一组数据
			object[] data = null;

			if (error != Error.Ok) 
			{
				saveFile.Close();
				return data;
			}

			List<object> os = new List<object>();
			object d = null;
			while(!saveFile.EofReached())
			{
				var josn = saveFile.GetLine();
				d = JsonConvert.DeserializeObject(josn, objectType);
				os.Add(d);
			}
			data = os.ToArray();
			saveFile.Close();
			return data;
		}
		
		private static Error Save(object ojc, string savePath, object saveData, object password, SaveStatus saveStatus,
			File saveFile, Directory directory)
		{
			var file = GetFile(saveFile);

			var path = GetSaveDataPath(ojc, savePath, directory);

			Error error = Open(password, file, path, File.ModeFlags.Write);

			if (saveStatus == SaveStatus.Single)
			{
				SaveSingleData(error, file, saveData);
			}

			else if (saveStatus == SaveStatus.Multiple)
			{
				object[] o = (object[])saveData;
				SaveMultipleData(error, file, o);
			}

			else if (saveStatus == SaveStatus.Csv)
			{
				List<string[]> csv = (List<string[]>)saveData;
				SaveCsv(error, file, csv);
			}

			return error;
		}

		private static void SaveSingleData(Error error, File saveFile, object saveData)
		{
			// 存储的数据，每个都独立保存在一个文件里。
			if (error != Error.Ok) 
			{
				saveFile.Close();
				return;
			}
			var json = JsonConvert.SerializeObject(saveData);
			saveFile.StoreLine(json);
			saveFile.Close();
		}

		private static void SaveMultipleData(Error error, File saveFile, object[] saveData)
		{
			// 存储的数据，全部保存在一个文件里。
			if (error != Error.Ok) 
			{
				saveFile.Close();
				return;
			}
			foreach(var data in saveData)
			{
				var json = JsonConvert.SerializeObject(data);
				saveFile.StoreLine(json);
			}
			saveFile.Close();
		}
		private static Error Open(object password, File file, string path, File.ModeFlags flags)
		{
			Error error = Error.CantOpen;
			if (password == null)
			{
				error = OpenData(file, path, flags);
			}
			else if (password != null)
			{
				if (password.GetType() == typeof(string))
				{
					error = OpenData(file, path, flags, password.ToString());
				}
				if (password.GetType() == typeof(byte[]))
				{
					byte[] key = (byte[])password;
					error = OpenData(file, path, flags, key);
				}
			}
			return error;
		}
		private static Error OpenData(File saveFile, string savePath, File.ModeFlags flags)
		{
			// 打开不加密的文件
			Error error = saveFile.Open(savePath, flags);
			return error;
		}

		private static Error OpenData(File saveFile, string savePath, File.ModeFlags flags, byte[] key)
		{
			// 打开使用byte加密的文件
			Error error = saveFile.OpenEncrypted(savePath, flags, key);
			return error;
		} 

		private static Error OpenData(File saveFile, string savePath, File.ModeFlags flags, string password)
		{
			// 打开使用字符串密码加密的文件
			Error error = saveFile.OpenEncryptedWithPass(savePath, flags, password);
			return error;
		}
		# endregion

		# region 文件管理
		// /// <summary>
		// /// 获取路径前缀
		// /// </summary>
		// /// <remarks>
		// /// PathStatus.User = "user://" , PathStatus.Res = "res://" PathStatus.Custom = ""
		// /// </remarks>
		// /// <param name="ojc">扩展对象</param>
		// /// <param name="status">路径前缀类型</param>
		// /// <returns>路径前缀</returns>
		// public static string GetPathPrefix(this object ojc, PathStatus status)
		// {
		// 	var path = "";
		// 	switch (status)
		// 	{
		// 		case PathStatus.Custom:
		// 			path = "";
		// 			break;
		// 		case PathStatus.User:
		// 			path = "user://";
		// 			break;
		// 		case PathStatus.Res:
		// 			path = "res://";
		// 			break;
		// 		default:
		// 			path = "";
		// 			break;
		// 	}
		// 	return path;
		// }

		/// <summary>
		/// 检查目录是否存在
		/// </summary>
		/// <param name="ojc">扩展对象</param>
		/// <param name="directory">Godot 目录管理</param>
		/// <param name="dirPath">需要检测的目录</param>
		/// <param name="status">路径前缀类型</param>
		/// <returns></returns>
		public static bool HasDirectory(this object ojc, Directory directory, string dirPath)
		{
			var path = dirPath;
			var exists = directory.DirExists(path);
			return exists;
		}

		/// <summary>
		/// 检查文件是否存在
		/// </summary>
		/// <param name="ojc">扩展对象</param>
		/// <param name="directory">Godot 目录管理</param>
		/// <param name="dirPath">需要检测的目录</param>
		/// <param name="status">路径前缀类型</param>
		/// <returns></returns>
		public static bool HasFile(this object ojc, Directory directory, string filePath)
		{
			var path = filePath;
			var exists = directory.FileExists(path);
			return exists;
		}

		/// <summary>
		/// 获取系统中存在的路径
		/// </summary>
		/// <remarks>
		/// 如果已经存在则返回目录,否则新建该目录.
		/// </remarks>
		/// <param name="ojc">扩展对象</param>
		/// <param name="directory">Godot 目录管理</param>
		/// <param name="dirPath">需要检测的目录</param>
		/// <param name="status">路径前缀类型</param>
		/// <returns>系统存在的目录</returns>
		public static string CorrectDirectory(this object ojc, Directory directory, string dirPath)
		{
			var path = dirPath;
			if (!ojc.HasDirectory(directory, path)) 
			{
				Error error = directory.MakeDir(path);
				if (error != Error.Ok)
				{
					error = directory.MakeDirRecursive(path);
					if (error != Error.Ok)
					{
						GD.Print("建立目录失败！错误代码：" + error);
					}
				}
			}
			return path;
		}

		/// <summary>
		/// 删除目录或者文件
		/// </summary>
		/// <param name="ojc">扩展对象</param>
		/// <param name="directory">Godot 目录管理</param>
		/// <param name="dirPath">需要检测的目录</param>
		/// <param name="status">路径前缀类型</param>
		/// <returns></returns>
		public static Error RemoveDirectoryOrFile(this object ojc, Directory directory, string path)
		{
			Error error = Error.FileCantRead;
			if (ojc.HasDirectory(directory, path) || ojc.HasFile(directory, path))
			{
				error = directory.Remove(path);
			}
			return error;
		}

		/// <summary>
		/// 从路径中获取目录和文件名
		/// </summary>
		/// <param name="ojc">扩展对象</param>
		/// <param name="path">完整路径</param>
		/// <param name="directory">目录</param>
		/// <param name="file">文件</param>
		public static void GetDirectoryAndFile(this object ojc, string path, out string directory, out string file)
		{
			var paths = path.Split(new char[] { '/' });
			directory = "";
			file = "";
			if (paths != null)
			{
				if (paths.Length > 2)
				{
					directory = paths[0] + "/";
					for (int i = 1; i < paths.Length - 1; i++)
					{
						directory += paths[i] + "/";
					}
					file = paths[paths.Length - 1];
				}
				else
				{
					directory = paths[0];
					file = "";
				}
			}
		}
		# endregion
	}
}
