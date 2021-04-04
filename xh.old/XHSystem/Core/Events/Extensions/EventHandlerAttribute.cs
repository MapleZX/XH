using Godot;
using System;

namespace XHSystem
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EventHandlerAttribute : Attribute
    {
        public string handler;

        public EventHandlerAttribute(string handler)
        {
            this.handler = handler;
        }
    }
}