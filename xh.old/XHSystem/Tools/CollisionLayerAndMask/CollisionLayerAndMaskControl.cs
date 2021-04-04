using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using XHSystem;

using GodotArray = Godot.Collections.Array;
[Tool]
public class CollisionLayerAndMaskControl : Control, ITool
{
	PackedScene itemScene = GD.Load<PackedScene>("res://XHSystem/Tools/CollisionLayerAndMask/Layer_1.tscn");

	[Node("VBoxContainer/ScrollContainer/VBoxContainer")] VBoxContainer vBoxContainer;
	[Node("VBoxContainer/Load/path")] LineEdit pathEdit;
	[Node("VBoxContainer/Load/HSplitContainer/password")] LineEdit passwordEdit;
	[Node("VBoxContainer/Load/HSplitContainer/CheckBox")] CheckBox usePassword;
	public string ConfigurationPath { get; private set; }
	public string ConfigurationPassword { get; private set; }
	private Tool tool;
	private Dictionary<string, int> LayerDic = new Dictionary<string, int>();
	private GodotArray Children;
	public override void _Ready() => this.Start(_Start);
	public void _Start()
	{
		AddLayer(32);
		Children = vBoxContainer.GetChildren();
		tool = (Tool)GetParent().GetParent();
		// LoadData();
	}

	private void AddLayer(int num)
	{
		for (int i = 0; i < num; i++)
		{
			var item = itemScene.Instance() as Layer_1;
			vBoxContainer.AddChild(item);         
			item.SetItem(i, "");
			item.Connect("new_text_changed", this, nameof(On_Changed_Text));
		}
	}
	private void LoadData()
	{
		ConfigurationPath = pathEdit.Text;
		ConfigurationPassword = passwordEdit.Text;

		if (ConfigurationPath == "") 
		{
			GD.Print("未输入碰撞配置路径!");
			return;
		}

		if (ConfigurationPassword == "")
		{
			LayerDic = this.LoadSingleData<Dictionary<string, int>>(ConfigurationPath, LayerDic, null, null);
		}
		else
		{
			LayerDic = this.LoadSingleData<Dictionary<string, int>>(ConfigurationPath, LayerDic, ConfigurationPassword, null, null);
		}

		if (LayerDic == null)
		{
			LayerDic = new Dictionary<string, int>();
			GD.Print("路径返回的数据为空！");
		}

		for (int i = 0; i < Children.Count; i++)
		{
			var Child = Children[i] as Layer_1;
			foreach (var item in LayerDic)
			{
				if (item.Value == Child.layer)
				{
					Child.SetItem(item.Value, item.Key);
				}
			}
		}
	}
	private void On_Changed_Text()
	{
		ConfigurationPath = pathEdit.Text;
		ConfigurationPassword = passwordEdit.Text;
		if (ConfigurationPath == "") 
		{
			LoadData();
			return;
		}
		LayerDic.Clear();
		for(int i = 0; i < Children.Count; i++)
		{
			var Child = Children[i] as Layer_1;
			if (Child.layerName != "")
			{
				LayerDic[Child.layerName] = Child.layer;
			}
		}
		if (ConfigurationPassword == "")
		{
			Error error = this.SaveSingleData(ConfigurationPath, LayerDic, null, null);
		}
		else
		{
			Error error = this.SaveSingleData(ConfigurationPath, LayerDic, ConfigurationPassword, null, null);
		}
		LoadData();
	}
	private void ProjectItemChanged(string new_path, string new_password)
	{
		var old_path = ConfigurationPath;
		var old_password = ConfigurationPassword;
		ConfigurationPath = new_path; 
		ConfigurationPassword = new_password;
		if (tool != null)
		{
			tool.EmitSignal("project_item_changed", ConfigurationPath, ConfigurationPassword, "Collision");
			Directory dir = new Directory();
			this.RemoveDirectoryOrFile(dir, old_path);
		}			
		On_Changed_Text();
	}
    [SignalMethod("get_project_item", "tool")]
	private void Get_Project_Item(List<string[]> config)
	{
		var items = config.Cast<string[]>().SingleOrDefault(x => x[0] == "Collision");
		if (items.Length > 0)
		{
			pathEdit.Text = items[1];
			passwordEdit.Text = items[2];
		}
		LoadData();
	}
	[SignalMethod("pressed", "usePassword")]
	private void On_Use_Password()
	{
		passwordEdit.Secret = usePassword.Pressed;
	}
	[SignalMethod("text_entered", "pathEdit")]
	private void On_Path_Text_Entered_Changed(string new_text)
	{
		ProjectItemChanged(new_text, ConfigurationPassword);
	}
	[SignalMethod("focus_exited", "pathEdit")]
	private void On_Path_Text_Exited()
	{
		pathEdit.Text = ConfigurationPath;
	}
	[SignalMethod("text_entered", "passwordEdit")]
	private void On_Password_Text_Entered_Changed(string new_text)
	{
		ProjectItemChanged(ConfigurationPath, new_text);
	}
	[SignalMethod("focus_exited", "passwordEdit")]
	private void On_Password_Text_Exited()
	{
		passwordEdit.Text = ConfigurationPassword;
	}
}
