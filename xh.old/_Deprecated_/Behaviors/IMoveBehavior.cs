using Godot;

namespace _Deprecated_
{
    public interface IMoveBehavior
    {
        void Move(float delta, int status, Vector2 velocity, Vector2 direction);
    }
}