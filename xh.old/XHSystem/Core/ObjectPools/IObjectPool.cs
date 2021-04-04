using Godot;
using System;
using XHSystem;

using GodotArray = Godot.Collections.Array;

namespace XHSystem
{
    public interface IObjectPool<TArgs> where TArgs : EventArgs
    {
        GodotArray Objects { get; }
        void ShowObjectFromPool(object sender, TArgs e);
        void AddObjectToPool(int number);
    }
}