using Godot;
using System;
using _Deprecated_;

public class SimpleMouseController : IMoveController
{
    public void Move(float delta, Node node)
    {
        
    }

    public void Move(float delta, Node node, object attr_objcet)
    {
        
    }

    public void Move(float delta, int status, Node node, object attr_objcet)
    {
        if ((SimpleMotionStatus)status == SimpleMotionStatus.Move)
        {
            if (Input.IsActionPressed("ui_mouse"))
            {
                var behavior = node as IMotionBehavior;
                var attr = attr_objcet as SimpleMotionModel;
                var physcis = node as PhysicsBody2D;
                var target = physcis.GetGlobalMousePosition();

                attr.velocity = physcis.Position.DirectionTo(target) * attr.max_speed;

                if (physcis.Position.DistanceTo(target) > 5)
                {
                    behavior.Move(delta, status, attr.velocity, attr.velocity.Normalized());
                }    
            }
        }
        
    }
}