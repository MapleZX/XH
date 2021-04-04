using Godot;
using System;
public class DamageEventArgs : EventArgs
{
    public DamageEventArgs(){}
    public DamageEventArgs(float damage, bool crit)
    {
        this.damage = damage;
        this.crit = crit;
    }
    public DamageEventArgs(float damage, Vector2 travel, float duration, float spread, bool crit)
    {
        this.damage = damage;
        this.travel = travel;
        this.duration = duration;
        this.spread = spread;
        this.crit = crit;
    }

    public DamageEventArgs(float damage, Vector2 travel, float duration, float spread, bool crit, Color color) : this(damage, travel, duration, spread, crit)
    {
        this.color = color;
    }

    public DamageEventArgs(float damage, Vector2 travel, float duration, float spread, bool crit, Color color, float multiple) : this(damage, travel, duration, spread, crit, color)
    {
        this.multiple = multiple;
    }

    public float damage { get; set; } 
    public Vector2 travel { get; set; } 
    public float duration { get; set; } 
    public float spread { get; set; } 
    public bool crit { get; set; }
    public Color color { get; set; }
    public float multiple  { get;set; }
}