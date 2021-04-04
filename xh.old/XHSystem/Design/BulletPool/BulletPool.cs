using Godot;
using System;
using System.Reflection;
using XHSystem;

using GodotArray = Godot.Collections.Array;

public class BulletPool : Node2D, IXH, IObjectPool<BulletEventArgs>
{
    // [Export] private int num = 200;
    // [Export] private PackedScene bulletPacked = GD.Load<PackedScene>("res://Design/BulletPool/Bullet.tscn");
    // private MethodInfo[] methodInfos;
    // public GodotArray Objects { get; private set; }

    // [Service("Bullet")] IXHEventHandler<BulletEventArgs> Bullet;
    // public void AddObjectToPool(int number)
    // {
    //     for (int i = 0; i < number; i++)
    //     {
    //         var bullet = bulletPacked.Instance();
    //         bullet.Name = "bullet" + "_" + i;
    //         ((CanvasItem)bullet).Hide();
    //         AddChild(bullet);
    //     }
    // }

    // [EventHandler("Bullet")]
    // public void ShowObjectFromPool(object sender, BulletEventArgs e)
    // {
    //     for (int i = 0; i < Objects.Count; i++)
    //     {
    //         var b = Objects[i] as Bullet;
    //         var os = sender as object[];
    //         if (!b.Visible)
    //         {
    //             b.Show();
    //             b.GlobalPosition = (Vector2)(os[0]);
    //             b.LookAt((Vector2)(os[1]));
    //             b.direction = (Vector2)(os[2]);
    //             return;
    //         }
    //     }
    // }

    // public override void _Ready()
    // {
    //     this.Context();
    //     methodInfos = this.AddXHEvent();
    //     AddObjectToPool(num);
    //     Objects = GetChildren();
    // }
    // public override void _ExitTree()
    // {
    //     methodInfos = this.AddXHEvent();
    // }
     # region 
    public FieldInfo[] fieldInfo { get; set; }
    public MethodInfo[] methodInfos { get; set; }
    public override void _Ready() => this.Start();
    public override void _ExitTree() => this.Exit();
    # endregion

    [Export] private int num = 200;
    [Export] private PackedScene bulletPacked = GD.Load<PackedScene>("res://XHSystem/Design/BulletPool/Bullet.tscn");
    
    [Service("Bullet")] IXHEventHandler<BulletEventArgs> Bullet;

    public GodotArray Objects { get; private set; }

    public void _Start()
    {
        AddObjectToPool(num);
        Objects = GetChildren();
    }
    public void _Exit()
    {
        // This is _ExitTree;
    }

    [EventHandler(nameof(Bullet))]
    public void ShowObjectFromPool(object sender, BulletEventArgs e)
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            var bullet = Objects[i] as Bullet;
            bullet.CollisionMask = 0;
            var body = sender as Node2D;
            if (!bullet.Visible)
            {
                for (int j = 0; j < e.mask.Length; j++)
                {
                    bullet.SetCollisionMaskBit(e.mask[j], true);
                }
                
                bullet.GlobalPosition = body.GlobalPosition;

                bullet.LookAt(e.facing);

                bullet.args = e;

                bullet.Show();

                return;
            }
        }
    }

    public void AddObjectToPool(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var bullet = bulletPacked.Instance();
            bullet.Name = "bullet" + "_" + i;
            ((CanvasItem)bullet).Hide();
            AddChild(bullet);
        }
    }
}
