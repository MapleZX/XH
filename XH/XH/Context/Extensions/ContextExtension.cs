using Godot;
using System;
namespace XH.Extensions
{
    public static class ContextExtension
    {
        public static Node Root(this Node node)
        {
            var _root = node.GetNode("/root");
            return _root;
        }
        public static T Context<T>(this Node node) where T : class, IContextObject, new()
        {
            var _creator = node.Singleton<T>();
            return _creator;
        }
        public static Node SingletonNode(this Node node)
        {
            var _creator = node.Singleton<Node>();
            return _creator;
        }
        public static T Singleton<T>(this Node node, string path = "/root/") where T : class
        {
            Type type = typeof(T);
            string nodepath  = path + type.Name;
            var _creator = node.GetNode(nodepath);
            return _creator as T;
        }
        public static T ContextNode<T>(this IContextObject context) where T : Node
        {
            var value = context as T;
            return value;
        }
    }
}