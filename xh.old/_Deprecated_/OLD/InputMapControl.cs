using Godot;
using System;
using System.Collections.Generic;
using XHSystem;

namespace old
{
	public class InputMapControl : Control
	{
	// 	VBoxContainer vBoxContainer;
	// 	Button ActionButton;
	// 	Button SaveButton;
	// 	List<CustomInputMap> maps = new List<CustomInputMap>();
	// 	// Dictionary<string, Dictionary<string, object>> actionDic = new Dictionary<string, Dictionary<string, object>>();
	// 	PackedScene mapScene = GD.Load<PackedScene>("res://Tools/InputMap/CustomInputMap.tscn");

	// 	private int Index = 1;

	// 	public override void _Ready()
	// 	{
	// 		vBoxContainer = GetNode<VBoxContainer>("MarginContainer/VBoxContainer/ScrollContainer/VBoxContainer");

	// 		ActionButton = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/Action");
	// 		SaveButton = GetNode<Button>("MarginContainer/VBoxContainer/HBoxContainer/Save");

	// 		ActionButton.Connect("pressed", this, nameof(on_add_action_map));
	// 		SaveButton.Connect("pressed", this, nameof(on_save_action_config));

	// 		var configs = this.LoadConfig("Configs/InputMap", "map.fig", PathDetail.Res + "Configs/InputMap/map.fig", PathStatus.User);
	// 		foreach(var config in configs)
	// 		{
	// 			var map = (CustomInputMap) mapScene.Instance();
	// 			var fig = config.Value;
	// 			map.Name = config.Key;
	// 			Index++;
	// 			map.action = map.Name;
	// 			map.GamePadIndex = (int)fig["GamePad"];
	// 			map.KeyboardScancode = (string)fig["Keyboard"];
	// 			map.deadzone = (float)fig["deadzone"];
	// 			vBoxContainer.AddChild(map);		
	// 			map.SetValue(map.action, map.GamePadIndex, map.KeyboardScancode, map.deadzone);
	// 			maps.Add(map as CustomInputMap);
	// 		}

	// 		// var save = this.SaveData("save", "save.save", new object[] { 5, "test", "testtest" },  "23124");
	// 		// var load = this.LoadData<object[]>( "save", "save.save", new object[] { 5, "testtest" }, "23124");
	// 	}

	// 	private void on_add_action_map()
	// 	{
	// 		var map = mapScene.Instance();
	// 		map.Name = "Map_" + Index;
	// 		Index++;
	// 		vBoxContainer.AddChild(map);
	// 		maps.Add(map as CustomInputMap);
	// 	}

	// 	private void on_save_action_config()
	// 	{
	// 		// var map = new CustomInputMap();
	// 		var MapDic = new Dictionary<string, Dictionary<string, object>>();
	// 		foreach (var map in maps)
	// 		{
	// 			var actionDic = new Dictionary<string, object>();
	// 			actionDic["deadzone"] = map.deadzone;
	// 			actionDic["GamePad"] = map.GamePadIndex;
	// 			actionDic["Keyboard"] = map.KeyboardScancode;
	// 			MapDic[map.action] = actionDic;
	// 		}
	// 		this.SaveConfig("Configs/InputMap", "map.fig", MapDic, PathStatus.User);
	// 	}
	 }
}
