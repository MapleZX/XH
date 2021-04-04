using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace XHSystem
{
    public static class CollisionExtension
    {
        public static IContextObject GetContext(this IClientCollisionCollection client)
        {
            MemberInfo member = client.GetType();
            bool isCollisionAttribute = Attribute.IsDefined(member, typeof(CollisionAttribute));

            if (isCollisionAttribute)
            {
                CollisionAttribute serviceAttr = (CollisionAttribute)Attribute.GetCustomAttribute(member, typeof(CollisionAttribute));
                var contextPath = "/root/" + serviceAttr.path;
                var node = client as Node;
                var context = node.GetNode(contextPath) as IContextObject;
                return context;
            }
            return null;
        }

        public static IContextObject ClientCollision(this IClientCollisionCollection client, IContextObject _context = null)
        {
            var context = _context != null ? _context : GetContext(client);

            var contextType = context.GetType();
            var binding = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            var filedName = "collision";
            FieldInfo collisionInfo = contextType.GetField(filedName, binding);

            if (collisionInfo.FieldType == typeof(ICollisionCollection))
            {
                List<CollisionLayerStatus> layers = new List<CollisionLayerStatus>();
                List<CollisionLayerStatus> masks = new List<CollisionLayerStatus>();

                var collision = (ICollisionCollection)collisionInfo.GetValue(context);
                client.CollisionLayerConfiguration(collision, layers);
                client.CollisionMaskConfiguration(collision, masks);

                var node = client as Node;
                var physics = node as PhysicsBody2D;
                physics.CollisionLayer = 0;
                physics.CollisionMask = 0;
                for (int i = 0; i < layers.Count; i++)
                {
                    int index = (int)layers[i];
                    physics.SetCollisionLayerBit(index, true);
                }
                for (int i = 0; i < masks.Count; i++)
                {
                    int index = (int)masks[i];
                    physics.SetCollisionMaskBit(index, true);
                }
            }
            return context;
        }
    }
}