using Godot;
using System;
using System.Reflection;
using XHSystem;

using GodotArray = Godot.Collections.Array;

/// <summary>
/// 伤害池
/// </summary>
public class DamageControl : Control, IXH, IObjectPool<DamageEventArgs>
{
    # region 
    public FieldInfo[] fieldInfo { get; set; }
    public MethodInfo[] methodInfos { get; set; }
    public override void _Ready() => this.Start();
    public override void _ExitTree() => this.Exit();
    # endregion

    [Export] private int objectNumber = 100;
    [Export] private Vector2 travel = new Vector2(0, -50);
    [Export] private float duration = 0.75f;
    [Export] private float spread = Mathf.Pi / 2 ;
    [Export] private float multiple = 2;
    [Export] private Color critColor = new Color(1, 0, 0);

    [Export] private PackedScene damegeLabelScene = GD.Load<PackedScene>("res://XHSystem/Design/UISystems/Damage/Damage.tscn");

    public GodotArray Objects { get; private set; }

    [Service("DamageControl")] private IXHEventHandler<DamageEventArgs> handler;

    [EventHandler("handler")]
    public void ShowObjectFromPool(object sender, DamageEventArgs e)
    {
        var node = sender as Node2D;
        e.travel = e.travel == Vector2.Zero ? travel : e.travel;
        e.duration = e.duration == 0 ? duration : e.duration;
        e.spread = e.spread == 0 ? spread : e.spread;
        e.multiple = e.multiple == 0 ? multiple : e.multiple;
        e.color = e.color == new Color() ? critColor : e.color;

        for (int i = 0; i < Objects.Count; i++)
        {
            var label = Objects[i] as DamageLabel;
            if (!label.Visible)
            {  
                // label.RectGlobalPosition = node.GlobalPosition;
                label.RectGlobalPosition = node.GetGlobalTransformWithCanvas().origin;
                label.ShowDamageLabel(e);
                label.Show();
                return;
            }
        }
    }

    public void AddObjectToPool(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var damageLabel = damegeLabelScene.Instance();
            damageLabel.Name = "DamageLabel" + "_" + i;
            ((CanvasItem)damageLabel).Hide();
            AddChild(damageLabel);
        }
    }

    // public override void _Ready()
    // {
    //     this.Context();
    //     methodInfos = this.AddXHEvent();
    //     AddObjectToPool(objectNumber);

    // }

    // public override void _ExitTree()
    // {
    //     this.RemoveXHEvent(methodInfos);
    // }
    public void _Start()
    {
        AddObjectToPool(objectNumber);
        Objects = this.GetChildren();
    }

    public void _Exit()
    {

    }
}
