using Godot;
using System;
using System.Collections.Generic;
using XHSystem;

public class CustomInputMap : HBoxContainer
{
    LineEdit actionLineEdit;
    Button gamepadButton;
    Button keyboardButton;
    SpinBox deadzoneSpinBox;
    Button addButton;
    public string action;
    public float deadzone = 0.5f;
    public int GamePadIndex;
    public string KeyboardScancode; 
    public InputEvent GamePad;
    public InputEvent Keyboard;
    private bool isGamepad = false;
    private bool isKeyboard = false;
    private string originalGamePad = "";
    private string originalKeyboard = "";
    public enum XBoxJoystickList 
    {
        //
        // 摘要:
        //     Invalid button or axis.
        InvalidOption = -1,
        //
        // 摘要:
        //     Xbox controller A button.
        XboxA = 0,
        //
        // 摘要:
        //     Xbox controller B button.
        XboxB = 1,
        //
        // 摘要:
        //     Xbox controller X button.
        XboxX = 2,
        //
        // 摘要:
        //     Xbox controller Y button.
        XboxY = 3,
        //
        // 摘要:
        //     Gamepad left Shoulder button.
        L = 4,
        //
        // 摘要:
        //     Gamepad right Shoulder button.
        R = 5,
        //
        // 摘要:
        //     Gamepad left trigger.
        L2 = 6,
        //
        // 摘要:
        //     Gamepad right trigger.
        R2 = 7,
        //
        // 摘要:
        //     Gamepad left stick click.
        L3 = 8,
        //
        // 摘要:
        //     Gamepad right stick click.
        R3 = 9,
        //
        // 摘要:
        //     Gamepad button Select.
        Select = 10,
        //
        // 摘要:
        //     Gamepad button Start.
        Start = 11,
        //
        // 摘要:
        //     Gamepad DPad up.
        DpadUp = 12,
        //
        // 摘要:
        //     Gamepad DPad down.
        DpadDown = 13,
        //
        // 摘要:
        //     Gamepad DPad left.
        DpadLeft = 14,
        //
        // 摘要:
        //     Gamepad DPad right.
        DpadRight = 15,
    }
   
    public override void _Ready()
    {
        // if (!Engine.EditorHint) return;
        actionLineEdit = GetNode<LineEdit>("action");
        gamepadButton = GetNode<Button>("gamepad");
        keyboardButton = GetNode<Button>("keyboard");
        deadzoneSpinBox = GetNode<SpinBox>("deadzone");
        addButton = GetNode<Button>("add");

        actionLineEdit.Connect("text_changed", this, nameof(on_text_changed));

        gamepadButton.Connect("pressed", this, nameof(on_text_changed_in_gamepad));
        keyboardButton.Connect("pressed", this, nameof(on_text_changed_in_keyboard));

        deadzoneSpinBox.Connect("value_changed", this, nameof(on_value_change));

        addButton.Connect("pressed", this, nameof(on_add_input_map));
    }

    public void SetValue(string _action, int gamepad_index, string KeyboardScancode, float _deadzone)
    {
        actionLineEdit.Text = _action;
        gamepadButton.Text = (XBoxJoystickList)gamepad_index + "";
        keyboardButton.Text = KeyboardScancode;
        deadzoneSpinBox.Value = _deadzone;

        var joy = new InputEventJoypadButton();
        joy.ButtonIndex = gamepad_index;
        GamePad = joy;

        var key = new InputEventKey();
        key.Scancode = (uint)OS.FindScancodeFromString(KeyboardScancode);
        Keyboard = key;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouse && mouse.Pressed)
        {
            if (isGamepad)
            {
                isGamepad = false;
                gamepadButton.Text = originalGamePad;
            }

            if (isKeyboard)
            {
                isKeyboard = false;
                keyboardButton.Text = originalKeyboard;
            }    
        }

        if (@event is InputEventJoypadButton joy && joy.Pressed && isGamepad)
        {
            isGamepad = false;
            gamepadButton.Text = (XBoxJoystickList)joy.ButtonIndex + "";
            originalGamePad = gamepadButton.Text;
            GamePad = joy;
            GamePadIndex = joy.ButtonIndex;
        }

        if (@event is InputEventKey key && key.Pressed && isKeyboard)
        {
            isKeyboard = false;
            keyboardButton.Text = OS.GetScancodeString(key.Scancode);
            originalKeyboard = keyboardButton.Text;
            Keyboard = key;
            KeyboardScancode = keyboardButton.Text;
        }
    }

    private void on_text_changed(string new_text)
    {
        action = new_text;
        // GD.Print(action);
    }

    private void on_text_changed_in_gamepad()
    {
        isGamepad = true;
        gamepadButton.Text = "-";
    }

    private void on_text_changed_in_keyboard()
    {
        isKeyboard = true;
        keyboardButton.Text = "-";
    }

    private void on_value_change(float value)
    {
        deadzone = value;
    }

    private void on_add_input_map()
    {
        if (GamePad == null && Keyboard == null && action == "") return;
        GD.Print("add");
        if (!InputMap.HasAction(action))
        {
            InputMap.AddAction(action, deadzone);
        }
        InputMap.ActionEraseEvents(action);
        InputMap.ActionAddEvent(action, GamePad);
        InputMap.ActionAddEvent(action, Keyboard);
    }
}
