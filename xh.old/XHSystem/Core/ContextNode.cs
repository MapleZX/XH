using Godot;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XHSystem
{
	/// <summary>
	/// 上下文
	/// 需要添加到自动加载(AutoLoad)中
	/// </summary>
	public class ContextNode : Node, IContextObject
	{
		private readonly string ProjectConfigurationPath = "res://XHSystem/Configs/Project/project.cfg";
    	private readonly string ProjectConfigurationPassword = "project-xh-password-dbedacfe-79bd-48d1-9f74-53e55f56b8e2";
		private readonly string[] ProjeConfigurationName = {"Service", "InputMap", "Collision", "GameSave"};

		private readonly IXHCollection service = new XHCollection();
		private readonly ICollisionCollection collision = new CollisionCollection(); 
		private readonly ICreatorService creator = new Creator();

		private string ServiceConfigurationPath { get; set; }
		private string ServiceConfigurationPassword { get; set; }
		public string InputMapConfigPath { get; private set; }
		public string InputMapDefaultConfigPath { get; private set; }
		public string CollisionConfigurationPath { get; private set; }
		public string CollisionConfigurationPassword { get; private set; }
		public string GameSavePath { get; private set;}
		public string GameSavePassword { get; private set;}

		public override void _Ready()
		{	
			// LoadConfig();
			// LoadConfiguration(ServiceConfigurationPath, ServiceConfigurationPassword);
			creator.ServiceConfiguration(service);
			creator.CollisionConfiguration(collision);
		}

		private List<string[]> LoadConfig()
		{
			var config = this.LoadCsv(ProjectConfigurationPath, null, ProjectConfigurationPassword, null, null);

			if (config == null) return null;

			string path = "";
			string password = "";

			string[] serviceConfig = getConfigArray(config, ProjeConfigurationName[0], ref path, ref password);
			ServiceConfigurationPath = path;
			ServiceConfigurationPassword = password;

			string[] InputMapConfig = getConfigArray(config, ProjeConfigurationName[1], ref path, ref password);
			InputMapDefaultConfigPath = path;
			// InputMapConfigPath = this.GetPathPrefix(PathStatus.User) + InputMapConfig[1];

			string[] CollisionConfig = getConfigArray(config, ProjeConfigurationName[2], ref path, ref password);
			CollisionConfigurationPath = path;
			CollisionConfigurationPassword = password;

			string[] GameSaveConfig = getConfigArray(config, ProjeConfigurationName[3], ref path, ref password);
			GameSavePath = path;
			GameSavePassword = password;

			return config;
		}
		private string[] getConfigArray(List<string[]> config, string configName, ref string path, ref string password)
		{
			for (int i = 0; i < config.Count; i++)
			{
				var c = config[i];
				if (c[0] == configName)
				{
					path = GetConfigPath(c[1], c[3]);
					password = c[2];	
					return c;
				}
			}
			return null;
		}
		private string GetConfigPath(string path, string prefix)
		{
			// string pre = this.GetPathPrefix((PathStatus)int.Parse(prefix));
			string configPath =  path;
			return configPath;
		}

		private void LoadConfiguration(string path, string password)
		{
			List<string[]> config = null;
			if (password == "")
			{
				config = this.LoadCsv(path, null, null, null);
			}
			else
			{
				config = this.LoadCsv(path, null, password, null, null);
			}

			if (config == null) return;

			for (int i = 1; i < config.Count; i++)
			{
				var ServiceType = Type.GetType(config[i][0]);
				var ImplementationType = Type.GetType(config[i][1]);
				var Name = config[i][2];
				var xHLifetime =  XHLifetime.Singleton;
				Enum.TryParse<XHLifetime>(config[i][3], out xHLifetime);
				service.XHRegister(ServiceType, ImplementationType, Name, xHLifetime);
			}
		}

		private void LoadInputMapConfiguration(string configPath, string defaultPath)
		{
			ConfigFile configFile = new ConfigFile();
			Directory directory = new Directory();
			Dictionary<string, Dictionary<string, object>> config = this.LoadConfig(configPath, defaultPath, configFile, directory);
			foreach(var item in config)
			{
				var action = item.Key;
				var value = item.Value;
				var deadzone = value.ContainsKey("deadzone") ? (float)value["deadzone"] : 0.5f;
				var gamepad = value.ContainsKey("GamePad") ? (int)value["GamePad"] : -1;
				var keyboard = value.ContainsKey("Keyboard") ? value["Keyboard"] as string : "";
				if (action != "" && gamepad != -1 && keyboard != "")
				{
					if (!InputMap.HasAction(action))
					{
						InputMap.AddAction(action);
					}
					var inputKey = new InputEventKey();
					inputKey.Scancode = (uint)OS.FindScancodeFromString(keyboard);
					InputMap.ActionAddEvent(action, inputKey);
					var inputjoy = new InputEventJoypadButton();
					inputjoy.ButtonIndex = gamepad;
					InputMap.ActionAddEvent(action, inputjoy);
					InputMap.ActionSetDeadzone(action, deadzone);
				}
			}
		}
		
        public IXHCollection GetXHCollection()
        {
            return service;
        }

        #region 通知
        // public override void _Notification(int what)
        // {
        //     if (MainLoop.NotificationWmMouseEnter == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "鼠标进入游戏窗口!");
        //     }

        //     if (MainLoop.NotificationWmMouseExit == what)
        //     {
        //         GD.Print(OS.GetName(), " : ","鼠标退出游戏窗口!");
        //     }

        //     if (MainLoop.NotificationWmFocusIn == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "获得游戏窗口焦点!");
        //     }

        //     if (MainLoop.NotificationWmFocusOut == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "取消游戏窗口焦点!");
        //     }

        //     if (MainLoop.NotificationWmQuitRequest == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "退出游戏!");
        //     }

        //     if (MainLoop.NotificationWmGoBackRequest == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "返回!");
        //     }

        //     if (MainLoop.NotificationWmUnfocusRequest == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "没有平台!");
        //     }

        //     if (MainLoop.NotificationCrash == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "奔溃了!!!");
        //     }

        //     if (MainLoop.NotificationAppResumed == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "恢复游戏!");
        //     }

        //     if (MainLoop.NotificationAppPaused == what)
        //     {
        //         GD.Print(OS.GetName(), " : ", "暂停游戏!");
        //     }
        // }
        #endregion
    }
}
