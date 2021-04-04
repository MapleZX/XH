using System;

namespace _Deprecated_
{
    public interface ICollisionBehavior
    {
        void _CollisionBehavior(object sender, EventArgs e);
        void _CollisionBehavior<TArgs>(object sender, TArgs e) where TArgs : EventArgs;
    }
}