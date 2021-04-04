using Godot;
using System;
public class BulletEventArgs : EventArgs
{
    public BulletEventArgs(int[] mask, Vector2 facing, float acceleration, Vector2 direction, float speed, Vector2 velocity, float damage)
    {
        this.mask = mask;
        this.facing = facing;
        this.acceleration = acceleration;
        this.direction = direction;
        this.speed = speed;
        this.velocity = velocity;
        this.damage = damage;
    }

    public int[] mask { get; set; }
    public Vector2 facing { get; set; }
    public float acceleration { get; set; }
    public Vector2 direction { get; set; }
    public float speed { get; set; }
    public Vector2 velocity { get; set; }
    public float damage { get; set; }
}