using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using XHSystem;
[Tool]
public class InputMapControl : Control, ITool
{

	public string ConfigurationPath { get; private set; }
	public string ConfigurationPassword { get; private set; }
	Dictionary<string, Dictionary<string, object>> configs = new Dictionary<string, Dictionary<string, object>>();
	PackedScene itemScene = GD.Load<PackedScene>("res://XHSystem/Tools/InputMap/InputMapItem.tscn");

	private Tool tool;

	[Node("VSplitContainer/LoadConfig/path")] LineEdit pathEdit;

	[Node("VSplitContainer/add")] TextureButton addButton;
	[Node("VSplitContainer/refresh")] TextureButton refreshButton;
	[Node("VSplitContainer/save")] TextureButton saveButton;
	[Node("VSplitContainer/ScrollContainer/VBoxContainer")] VBoxContainer vBoxContainer;
	public override void _Ready() => this.Start(_Start);
	public void _Start()
	{
		// This is _Ready;
		tool = (Tool)GetParent().GetParent();
		// LoadConfiguration();
	}

	private void LoadConfiguration()
	{
		ConfigurationPath = pathEdit.Text;

		if (ConfigurationPath == "") 
		{
			GD.Print("未输入InputMap配置路径!");
			return;
		}

		configs = this.LoadConfig(ConfigurationPath, ConfigurationPath, null, null);

		if (configs == null)
		{
			configs = new Dictionary<string, Dictionary<string, object>>();
			this.SaveConfig(ConfigurationPath, configs, null, null);
			GD.Print("路径返回的数据为空！");
		}

		foreach (var config in configs)
		{
			var item = itemScene.Instance() as InputMapItem;
			vBoxContainer.AddChild(item);
			item.SetItemValue(config.Key, config.Value);
		}
	}

	[SignalMethod("get_project_item", "tool")]
	private void Get_Project_Item(List<string[]> config)
	{
		var items = config.Cast<string[]>().SingleOrDefault(x => x[0] == "InputMap");
		if (items.Length > 0)
		{
			pathEdit.Text = items[1];
			// passwordEdit.Text = items[2];
		}
		LoadConfiguration();
	}

	[SignalMethod("text_entered", "pathEdit")]
	private void On_Path_Text_Entered_Changed(string new_text)
	{	
		ProjectItemChanged(new_text);
	}
	[SignalMethod("focus_exited", "pathEdit")]
	private void On_Path_Text_Exited()
	{
		pathEdit.Text = ConfigurationPath;
	}
	private void ProjectItemChanged(string new_text)
	{
		var old_path = ConfigurationPath;
		ConfigurationPath = new_text; 
		if (tool != null)
		{
			tool.EmitSignal("project_item_changed", ConfigurationPath, ConfigurationPassword, "InputMap");
			Directory dir = new Directory();
			this.RemoveDirectoryOrFile(dir, old_path);
		}		
		On_Click_Save();		
	}

	[SignalMethod("pressed", "addButton")]
	private void On_Click_Add()
	{
		var item = itemScene.Instance();
		vBoxContainer.AddChild(item);
	}
	[SignalMethod("pressed", "refreshButton")]
	private void On_Click_Refresh()
	{      
		var children = vBoxContainer.GetChildren();
		for (int i = 0; i < children.Count; i++)
		{     
			((CanvasItem)children[i]).QueueFree();
		}

		pathEdit.Text = ConfigurationPath;

		LoadConfiguration();
	}
	[SignalMethod("pressed", "saveButton")]
	private void On_Click_Save()
	{
		ConfigurationPath = pathEdit.Text;
		if (ConfigurationPath == "") 
		{
			On_Click_Refresh(); 
			return;
		}

		var children = vBoxContainer.GetChildren();
		configs.Clear();

		foreach (var item in children)
		{
			var configuration = item as InputMapItem;        
			if (configuration != null)
			{
				if (configuration.actionText != "" && configuration.gamepadIndex != -1 && configuration.keyboardText != "")
				{			
					var config = new Dictionary<string, object>();
					config["GamePad"] = configuration.gamepadIndex;
					config["Keyboard"] = configuration.keyboardText;
					config["deadzone"] = configuration.deadzone;
					configs[configuration.actionText] = config;
				}
			}
		}
		// GD.Print(configs.Count);
		Error error = this.SaveConfig(ConfigurationPath, configs, null, null);
		// Error error = this.SaveServiceConfiguration(ConfigurationPath, config);
		On_Click_Refresh();
	}
}
