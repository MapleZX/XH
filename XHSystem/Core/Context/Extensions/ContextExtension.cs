using Godot;
using System;
using System.Reflection;
using XHSystem.Context.Services;
namespace XHSystem.Context.Extensions
{
    public static class ContextExtension
    {
        # region 获取上下文
        public static Node Root(this Node node)
        {
            var _root = node.GetNode("/root");
            return _root;
        }

        public static IContextObject Context(this IClientCollection client)
        {
            var clientType = client.GetType();
            MethodInfo method = clientType.GetMethod("ClientConfiguration");
            bool isServiceAttribute = Attribute.IsDefined(method, typeof(ServiceAttribute));
            if (isServiceAttribute)
            {
                ServiceAttribute serviceAttr = (ServiceAttribute)Attribute.GetCustomAttribute(method, typeof(ServiceAttribute));
                var contextPath = "/root/" + serviceAttr.path;
                var context = (client as Node).GetNode(contextPath);

                var contextType = context.GetType();
                var flags = (BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                FieldInfo[] info = contextType.GetFields(flags);
                foreach (var fo in info)
                {
                    if (fo.FieldType == typeof(IServiceCollection))
                    {
                        var service = fo.GetValue(context) as IServiceCollection;
                        method.Invoke(client, new object[] {service} );
                        service.ScopedEnd();
                        return context as IContextObject;
                    }
                }
            }
            return null;
        }

        public static T Singleton<T>(this Node node, string path) where T : class
        {
            string nodepath  = "/root/" + path;
            var singleton = node.GetNode(nodepath);
            return singleton as T;
        }
        
        # endregion
    }
}