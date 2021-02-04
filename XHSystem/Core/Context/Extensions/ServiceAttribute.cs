using Godot;
using System;
namespace XHSystem.Context.Extensions
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class ServiceAttribute : Attribute
    {
        public string path;

        public ServiceAttribute(string path = "ContextNode")
        {
            this.path = path;
        }
    }
}