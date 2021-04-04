using Godot;
using System.Reflection;
using XHSystem;

public class VirtualGamepad : Control, IXH
{
    # region 
    public FieldInfo[] fieldInfo { get; set; }
    public MethodInfo[] methodInfos { get; set; }
    public override void _Ready() => this.Start();
    public override void _ExitTree() => this.Exit();
    # endregion

    [Node("LeftJoystick")] Joystick leftJoy;
    [Node("RightJoystick")] Joystick rightJoy;

    [Service("VirtualJoystick")] IXHEventHandler handler;

    private Vector2 LeftOutput = Vector2.Zero;
    private  Vector2 RightOutput = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        LeftOutput = leftJoy.IsWorking ? leftJoy.output : Vector2.Zero;
        RightOutput = rightJoy.IsWorking ? rightJoy.output : Vector2.Zero;

        handler.Run(new object[] { LeftOutput, RightOutput });
    }

    private void on_disable_gamepad(int device, bool connected)
    {
        Visible = !connected;
    }

    public void _Start()
    {
        Input.Singleton.Connect("joy_connection_changed", this, nameof(on_disable_gamepad));
    }

    public void _Exit()
    {
        
    }
}
