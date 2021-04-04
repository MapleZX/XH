using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using XHSystem;
[Tool]
public class Layer_1 : HSplitContainer
{
    [Node("Layer")] LineEdit layerEdit;
    [Node("Name")] LineEdit NameEdit;
    public int layer = -1;
    public string layerName = "";
    private bool isSetValue = false;
    [Signal] public delegate void new_text_changed();
    public override void _Ready() => this.Start(_Start);
    public void _Start()
    {
        // This is _Ready;
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouse && mouse.Pressed)
		{
			if (isSetValue)
            {
                if (NameEdit.HasFocus())
                {
                    NameEdit.Text = layerName;
					NameEdit.FocusMode = FocusModeEnum.None;
					NameEdit.FocusMode = FocusModeEnum.All;
                }
            }
        }
    }
    public void SetItem(int layer, string layerName)
    {
        this.layer = layer;
        layerEdit.Text = "Layer_" + (layer + 1);
        NameEdit.Text = layerName;
    }

    [SignalMethod("text_entered", "NameEdit")]
    private void On_Text_Entered(string new_text)
    {
        layerName = new_text;
        NameEdit.FocusMode = FocusModeEnum.None;
        NameEdit.FocusMode = FocusModeEnum.All;
        EmitSignal(nameof(new_text_changed));
    }
    [SignalMethod("focus_exited", "NameEdit")]
	private void On_Path_Text_Exited()
	{
        layerName = NameEdit.Text;
        EmitSignal(nameof(new_text_changed));
	}
    [SignalMethod("mouse_entered")]
    private void On_Mouse_Entered()
    {
        isSetValue = false;
    }
    [SignalMethod("mouse_exited")]
    private void On_Mouse_Exited()
    {
        isSetValue = true;
    }
}
