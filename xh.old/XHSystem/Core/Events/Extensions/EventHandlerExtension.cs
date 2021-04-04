using System;
using System.Collections.Generic;
using System.Reflection;
namespace XHSystem
{
    public static class EventHandlerExtension
    {
        public static MethodInfo[] AddXHEvent(this object o, MethodInfo[] methodInfos = null)
        {
            Type objectType = o.GetType();
            var binding = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            // MethodInfo[] methods = objectType.GetMethods(binding);
            MethodInfo[] methods = methodInfos == null ? objectType.GetMethods(binding) : methodInfos;

            var methodType = typeof(EventHandlerAttribute);

            foreach (var method in methods)
            {
                bool isEventHandlerAttribute = Attribute.IsDefined(method, methodType);

                if (isEventHandlerAttribute)
                {
                    EventHandlerAttribute attr = (EventHandlerAttribute)Attribute.GetCustomAttribute(method, methodType);
                    var eventHandler = objectType.GetField(attr.handler, binding).GetValue(o);
                    EventInfo eventInfo = eventHandler.GetType().GetEvent("handler");
                    Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, o, method);
                    eventInfo.AddEventHandler(eventHandler, handler);
                }
            }
            return methods;
        }

        public static MethodInfo[] RemoveXHEvent(this object o, MethodInfo[] methodInfos = null)
        {
            Type objectType = o.GetType();
            var binding = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            // MethodInfo[] methods = objectType.GetMethods(binding);
            MethodInfo[] methods = methodInfos == null ? objectType.GetMethods(binding) : methodInfos;

            var methodType = typeof(EventHandlerAttribute);

            foreach (var method in methods)
            {
                bool isEventHandlerAttribute = Attribute.IsDefined(method, methodType);

                if (isEventHandlerAttribute)
                {
                    EventHandlerAttribute attr = (EventHandlerAttribute)Attribute.GetCustomAttribute(method, methodType);
                    var eventHandler = objectType.GetField(attr.handler, binding).GetValue(o);
                    EventInfo eventInfo = eventHandler.GetType().GetEvent("handler");
                    Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, o, method);
                    eventInfo.RemoveEventHandler(eventHandler, handler);
                }
            }
            return methods;
        }
    }
}