using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using XHSystem;

public class Bullet : Area2D, IXH
{
    # region 
    public FieldInfo[] fieldInfo { get; set; }
    public MethodInfo[] methodInfos { get; set; }    
    public override void _Ready() => this.Start();
    public override void _ExitTree() => this.Exit();
    # endregion

    [Export] float timer_dis = 2;
    [Node("CollisionShape2D")] CollisionShape2D collision;
    [Node("Timer")] Timer timer;
    public BulletEventArgs args;

    public void _Start() {}

    public void _Exit() {}
    
    public override void _PhysicsProcess(float delta)
    {
        if (args == null) return;
        args.velocity = args.velocity.MoveToward(args.direction * args.speed, delta * args.acceleration);
        Position += args.velocity;
    }

    [SignalMethod("visibility_changed")]
    private void on_visibility_changed()
    {
        collision.SetDeferred("disabled", !Visible);
        if (Visible)
        {
            timer.Start(timer_dis);
        }
    }

    [SignalMethod("area_entered")]
    private void on_area_entered(Area2D area)
    {
        ShowBullet();
    }
    
    [SignalMethod("body_entered")]
    private void on_body_entered(Node node)
    {
        ShowBullet();
    }
    [SignalMethod("timeout", nameof(timer))]
    private void on_timer()
    {
        ShowBullet();
    }

    private void ShowBullet()
    {
        Visible = false;
        args = null;
    }
}
