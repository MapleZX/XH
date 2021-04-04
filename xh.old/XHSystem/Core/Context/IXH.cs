using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace XHSystem
{
    public interface IXH
    {
        FieldInfo[] fieldInfo { get; set; }
        MethodInfo[] methodInfos { get; set; }
        void _Start();
        // void _Exit();
    }
}