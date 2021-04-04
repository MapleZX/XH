using Godot;
using System;
public class SimpleMotionModel
{
    public SimpleMotionModel() {}

    public SimpleMotionModel(Vector2 velocity, Vector2 direction, float max_speed, float acceleration, float friction)
    {
        this.velocity = velocity;
        this.direction = direction;
        this.max_speed = max_speed;
        this.acceleration = acceleration;
        this.friction = friction;
    }

    public Vector2 velocity { get; set; }
    public Vector2 direction { get; set; }
    public float max_speed { get; set; } 
    public float acceleration { get; set; }
    public float friction { get; set; }            
}