using Godot;
using System;
using _Deprecated_;
public class DecelerationBehavior : ICollisionBehavior
{
    public void _CollisionBehavior(object sender, EventArgs e)
    {
        // object[] senders = sender as object[];
        // if ((senders[1] as Node).Name == "Decelerate")
        // {
        //     var node = senders[0] as SimplePlayer;
        //     node.status = "Decelerate : ";
        //     node.speed = 0.5f;
        //     node.timer = 10;
        // }
    }

    public void _CollisionBehavior<TArgs>(object sender, TArgs e) where TArgs : EventArgs
    {
        
    }
}