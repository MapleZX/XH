using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using XHSystem;

[Collision]
public class SimplePlayer : KinematicBody2D, IXH, IClientCollisionCollection, IDataInitialization
{
    public FieldInfo[] fieldInfo { get; set; }
    public MethodInfo[] methodInfos { get; set; }
    public Dictionary<string, object> DataDic { get; set; }

    [Export] public string SavePath { get; private set; }

    [Export] public string SavePassword { get; private set; }

    // [Export] public PathStatus SaveStatus { get; private set; }

    public override void _Ready() => this.Start();
    public override void _ExitTree() => this.Exit();

    public void _Start()
    {
        // This is _Ready;
        // GD.Print("Ready");
    }
    public void _Exit()
    {
        // This is _ExitTree;
        // GD.Print("ExitTree");
    }

    [Node("Pivot")] Position2D Pivot;
    [Node("Pivot/Facing")] Position2D Facing;
    Vector2 d = Vector2.Zero;
    [Service("Bullet")] IXHEventHandler<BulletEventArgs> Bullet;
    [Service("VirtualJoystick")] IXHEventHandler Joystick;
    Vector2 leftJoy = Vector2.Zero;
    Vector2 rigthJoy = Vector2.Zero;
    [Service("DamageControl")] private IXHEventHandler<DamageEventArgs> Damage;

    [EventHandler("Joystick")]
    private void JoystickController(object sender, EventArgs e)
    {
        var joyout = sender as object[];
        leftJoy = (Vector2)joyout[0];
        rigthJoy = (Vector2)joyout[1];
    }

    public override void _PhysicsProcess(float delta)
    {
        var direction = Vector2.Zero;
        direction.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        direction.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        
        if (leftJoy != Vector2.Zero)
            direction = leftJoy;
        
        direction = direction.Normalized(); 
        
        if (direction != Vector2.Zero)
        {
            d = direction;
            Pivot.Rotation = (d.Angle());
        }
            
        if (Input.IsActionJustPressed("c"))
        {
            int [] mask = { (int)CollisionLayerStatus.Layer1, (int)CollisionLayerStatus.Layer2, (int)CollisionLayerStatus.Layer3};
            BulletEventArgs args = new BulletEventArgs(mask, Facing.GlobalPosition, 50, d, 50, Vector2.Zero, 10);
            Bullet.Run(this, args); 

            DamageEventArgs e = new DamageEventArgs(5, false);
            Damage.Run(this, e);
            
            // GD.Print("this : ", GlobalPosition);
            // GD.Print("camera : ", camera.GlobalPosition);
        }

        MoveAndCollide(direction);
    }

    public void Initialization(Dictionary<string, object> objectDic)
    {
        
    }

    public void CollisionLayerConfiguration(ICollisionCollection collision, List<CollisionLayerStatus> layers)
    {
        layers.Add(collision.GetCollisionLayer("player"));
    }

    public void CollisionMaskConfiguration(ICollisionCollection collision, List<CollisionLayerStatus> masks)
    {
        
    }
}
