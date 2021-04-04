using Godot;
using System;

namespace XHSystem
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ServiceAttribute : Attribute
    {
        public string name;
        public ServiceAttribute(string name = "")
        {
            this.name = name;
        }
    }
}