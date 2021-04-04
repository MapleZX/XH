using Godot;
using System;
using _Deprecated_;
public class AccelerationBehavior : ICollisionBehavior
{
    public void _CollisionBehavior(object sender, EventArgs e)
    {
        // object[] senders = sender as object[];
        // if ((senders[1] as Node).Name == "Acceleration")
        // {
        //     var node = senders[0] as SimplePlayer;
        //     node.status = "Acceleration : ";
        //     node.speed = 5;
        //     node.timer = 10;
        // }
    }

    public void _CollisionBehavior<TArgs>(object sender, TArgs e) where TArgs : EventArgs
    {
        
    }
}