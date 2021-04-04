using System;
namespace XHSystem
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class CollisionAttribute : Attribute
    {
        public string path;
        public CollisionAttribute(string path = "ContextNode")
        {
            this.path = path;
        }
    }
}