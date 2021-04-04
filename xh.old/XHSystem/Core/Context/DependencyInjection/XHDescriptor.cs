using System;

namespace XHSystem
{
    public class XHDescriptor
    {
        public XHDescriptor(string xHname, XHLifetime lifetime, Type xHServiceType, Type xHImplementationType)
        {
            XHname = xHname;
            this.lifetime = lifetime;
            XHServiceType = xHServiceType;
            XHImplementationType = xHImplementationType;
        }

        public XHDescriptor(string xHname, XHLifetime lifetime, Type xHServiceType, Type xHImplementationType, object xHImplementationInstance) : this(xHname, lifetime, xHServiceType, xHImplementationType)
        {
            XHImplementationInstance = xHImplementationInstance;
        }

        public XHDescriptor(string xHname, XHLifetime lifetime, Type xHServiceType, Type xHImplementationType, object xHImplementationInstance, Func<Type, object> creator) : this(xHname, lifetime, xHServiceType, xHImplementationType, xHImplementationInstance)
        {
            Creator = creator;
        }

        public string XHname { get; } = "";
        public XHLifetime lifetime { get; }
        public Type XHServiceType { get; }
        public Type XHImplementationType { get; }
        public object XHImplementationInstance { get; }
        public Func<Type, object> Creator { get; }
    }
}