using Godot;
using System;
using _Deprecated_;

public class SimpleMoveController : IMoveController
{
    public void Move(float delta, Node node)
    {
        // GD.Print("Move : ", node.Name);
    }

    public void Move(float delta, Node node, object attr_objcet)
    {
        // GD.Print("Move : ", node.Name);
        // GD.Print("Attr : ", attr_objcet.GetType());
    }

    public void Move(float delta, int status, Node node, object attr_objcet)
    {
        // GD.Print("Move : ", node.Name);
        // GD.Print("Attr : ", attr_objcet.GetType());
        // GD.Print("Status : ", status);
        if ((SimpleMotionStatus)status == SimpleMotionStatus.Move)
        {
            var behavior = node as IMotionBehavior;
            var attr = attr_objcet as SimpleMotionModel;

            var direction = GetDirection();
            var max_speed = attr.max_speed;
            var acceleration = attr.max_speed;
            var friction = attr.friction;

            if (direction != Vector2.Zero)
            {
                attr.velocity = attr.velocity.MoveToward(direction * max_speed, delta * acceleration);
            }
            else if(direction == Vector2.Zero)
            {
                attr.velocity = attr.velocity.MoveToward(Vector2.Zero, delta * friction); 
            }
            behavior.Move(delta, status, attr.velocity, direction);
        } 
    }

    private Vector2 GetDirection()
    {
        var direction = Vector2.Zero;
        direction.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        direction.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        // direction = VirtualGamepad.LeftOutput != Vector2.Zero ? VirtualGamepad.LeftOutput : direction;

        if ((Mathf.Abs(direction.x) < 1  && Mathf.Abs(direction.x) != 0)|| (Mathf.Abs(direction.y) < 1 && Mathf.Abs(direction.y) != 0))
        {
            // GD.Print("direction : ", direction);
            return direction;
        }
        // if (VirtualGamepad.isLeftWorking)
        // {
        //     return VirtualGamepad.LeftOutput;
        // }
        direction = direction.Normalized(); 
        // GD.Print(direction);
        return direction;
    }
}