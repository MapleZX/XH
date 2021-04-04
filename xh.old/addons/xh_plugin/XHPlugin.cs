#if TOOLS
using Godot;
using System;

[Tool]
public class XHPlugin : EditorPlugin
{
    Control ToolControl;
    public override void _EnterTree()
    {
        LoadTool("res://XHSystem/Tools/Tool.tscn");
    }

    public override void _ExitTree()
    {
        RemoveConfiguration();
    }

    private void LoadTool(string ToolPath)
    {
        ToolControl = (Control)GD.Load<PackedScene>(ToolPath).Instance();
        AddControlToDock(DockSlot.RightUl, ToolControl);
    }
    private void RemoveConfiguration()
    {
        RemoveControlFromDocks(ToolControl);
        ToolControl.Free();
    }
}
#endif
