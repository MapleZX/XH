using Godot;
using System;

namespace _Deprecated_
{
    public interface IMoveController
    {
        void Move(float delta, Node node);
        void Move(float delta, Node node, object attr_objcet);
        void Move(float delta, int status, Node node, object attr_objcet);
    }
}