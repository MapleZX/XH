using Godot;
using System;

namespace XHSystem
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SignalMethodAttribute : Attribute
    {
        public string signal;
        public string nodeName;
        public string target;
        public string binds;
        public uint flags;

        public SignalMethodAttribute(string signal, string nodeName = "", string target = "", string binds = "", uint flags = 0)
        {
            this.signal = signal;
            this.nodeName = nodeName;
            this.target = target;
            this.binds = binds;
            this.flags = flags;
        }
    }
}