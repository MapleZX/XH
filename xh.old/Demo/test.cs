using Godot;
using System;

public class test : Node2D
{
    public float t;
    public override void _Ready()
    {
        t = OS.GetTicksMsec();
    }
}
