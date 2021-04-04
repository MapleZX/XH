using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using XHSystem;

using GodotArray = Godot.Collections.Array;

[Tool]
public class InputMapItem : Control
{
	# region 
	public enum XBoxJoystickList 
	{
		None = 0,
		//
		// 摘要:
		//     Xbox controller A button.
		XboxA = 1,
		//
		// 摘要:
		//     Xbox controller B button.
		XboxB = 2,
		//
		// 摘要:
		//     Xbox controller X button.
		XboxX = 3,
		//
		// 摘要:
		//     Xbox controller Y button.
		XboxY = 4,
		//
		// 摘要:
		//     Gamepad left Shoulder button.
		L = 5,
		//
		// 摘要:
		//     Gamepad right Shoulder button.
		R = 6,
		//
		// 摘要:
		//     Gamepad left trigger.
		L2 = 7,
		//
		// 摘要:
		//     Gamepad right trigger.
		R2 = 8,
		//
		// 摘要:
		//     Gamepad left stick click.
		L3 = 9,
		//
		// 摘要:
		//     Gamepad right stick click.
		R3 = 10,
		//
		// 摘要:
		//     Gamepad button Select.
		Select = 11,
		//
		// 摘要:
		//     Gamepad button Start.
		Start = 12,
		//
		// 摘要:
		//     Gamepad DPad up.
		DpadUp = 13,
		//
		// 摘要:
		//     Gamepad DPad down.
		DpadDown = 14,
		//
		// 摘要:
		//     Gamepad DPad left.
		DpadLeft = 15,
		//
		// 摘要:
		//     Gamepad DPad right.
		DpadRight = 16,
		//
		// 摘要:
		//     Gamepad left stick horizontal axis.
		LeftStickLeft = -17,
		LeftStickRight = 17,
		//
		// 摘要:
		//     Gamepad left stick vertical axis.
		LeftStickUp = -18,
		LeftStickDown = 18,
		//
		// 摘要:
		//     Gamepad right stick horizontal axis.
		RightStickLeft = -19,
		RightStickRight = 19,
		//
		// 摘要:
		//     Gamepad right stick vertical axis.
		RightStickUp = -20,
		RightStickDown = 20,
	}
	# endregion

	[Node("HBoxContainer/Action")] LineEdit actionEdit;
	[Node("HBoxContainer/GamepadOption")] OptionButton gamepadOption;
	[Node("HBoxContainer/KeyboardButton")] TextureButton keyboardButton;
	[Node("HBoxContainer/KeyboardButton/Label")] Label keyboardLabel;
	[Node("HBoxContainer/DeadzoneSpinBox")] SpinBox deadzoneSpinBox;
	[Node("HBoxContainer/Remove")] TextureButton removeButton;

	// public GodotArray ChildArray = new GodotArray();

	public string actionText = "";
	public string gamepadText = "";
	public int gamepadIndex;
	public string keyboardText = "";
	public float deadzone = 0.5f;

	private string originalkeyboardText = "";
	private bool isSetKeyboard = false;
	private bool isSetAction = false;
	public override void _Ready() => this.Start(_Start);
	public void _Start()
	{
		// This is _Ready;
		originalkeyboardText = keyboardText;
		SetGamepadOtionValue();
		// gamepadOption.Text = XBoxJoystickList.None.ToString();
		SetGampadIndexValue(gamepadOption.Text, out gamepadIndex);
		// GD.Print(gamepadIndex);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouse && mouse.Pressed)
		{
			if (isSetKeyboard)
			{
				isSetKeyboard = false;
				keyboardText = originalkeyboardText;
				keyboardLabel.Text = keyboardText;
				keyboardButton.FocusMode = FocusModeEnum.All;
			}

			if (isSetAction && actionEdit.HasFocus())
			{
				actionEdit.Text = actionText;
				actionEdit.FocusMode = FocusModeEnum.None;
				actionEdit.FocusMode = FocusModeEnum.All;
			}
		}
		if (@event is InputEventKey key && key.Pressed && isSetKeyboard)
		{
			isSetKeyboard = false;
			keyboardText = OS.GetScancodeString(key.Scancode);
			originalkeyboardText = keyboardText;
			keyboardLabel.Text = keyboardText;
			keyboardButton.FocusMode = FocusModeEnum.All;
		}
	}
	private void SetGampadIndexValue(string optionText, out int index)
	{
		index = (int)Enum.Parse(typeof(XBoxJoystickList), optionText);
		if (index == 0) 
		{
			index = -1;
			return;
		}
		var coefficient = index / Mathf.Abs(index);
		index = (Mathf.Abs(index) - 1) * coefficient;
	}
	private int GetGamepadValue(int index)
	{
		var item = Mathf.Abs(index) + 1;
		if (index <= 0)
		{
			return item;
		}
		GD.Print(item);
		var coefficient = index / Mathf.Abs(index);
		item = item * coefficient;
		return item;
	}
	private void SetGamepadOtionValue()
	{
		gamepadOption.Clear();
		var values = Enum.GetValues(typeof(XBoxJoystickList));
		foreach (var value in values)
		{
			gamepadOption.AddItem(value.ToString());
		}
	}

	public void SetItemValue(string action, Dictionary<string, object> config)
	{
		actionText = action;

		actionEdit.Text = actionText;

		gamepadIndex = config.ContainsKey("GamePad") ? (int)config["GamePad"] : -1;

		keyboardText = config.ContainsKey("Keyboard") ? config["Keyboard"] as string : "";

		deadzone = config.ContainsKey("deadzone") ? (float)config["deadzone"]  : 0.5f;

		gamepadText = ((XBoxJoystickList)GetGamepadValue(gamepadIndex)).ToString();

		gamepadOption.Text = gamepadText;
		keyboardLabel.Text = keyboardText;
		deadzoneSpinBox.Value = deadzone;

		originalkeyboardText = keyboardText;
	}
	[SignalMethod("mouse_entered")]
	private void On_Mouse_Entered_This()
	{
		isSetAction = false;
	}
	[SignalMethod("mouse_exited")]
	private void On_Mouse_Exited_This()
	{
		isSetAction = true;
	}
	[SignalMethod("text_entered", "actionEdit")]
	private void On_Text_Entered_Action(string new_text)
	{
		actionText = new_text;
	}
	[SignalMethod("focus_exited", "actionEdit")]
	private void On_Action_Text_Exited()
	{
		actionText = actionEdit.Text;
	}

	[SignalMethod("item_selected", "gamepadOption")]
	private void On_Gamepad_Item_Selected(int index)
	{
		var item = gamepadOption.GetItemText(index);
		SetGampadIndexValue(gamepadOption.Text, out gamepadIndex);
		// GD.Print(gamepadIndex);
	}

	[SignalMethod("pressed", "keyboardButton")]
	private void On_Text_Changed_In_Keyboard()
	{
		isSetKeyboard = true;
		// keyboardText = "-";
		keyboardLabel.Text = "-";
		keyboardButton.FocusMode = FocusModeEnum.None;
	}

	[SignalMethod("value_changed", "deadzoneSpinBox")]
	private void On_Value_Change(float value)
	{
		deadzone = value;
	}

	[SignalMethod("pressed", "removeButton")]
	private void On_Click_Remove()
	{
		QueueFree();
	}
}
