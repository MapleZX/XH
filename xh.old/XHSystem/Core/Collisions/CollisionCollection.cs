using System;
using System.Collections.Generic;

namespace XHSystem
{
    public class CollisionCollection : ICollisionCollection
    {
        private static Dictionary<string, int> collisionDic = new Dictionary<string, int>();
        public void AddCollisionLayer(string name, CollisionLayerStatus layer)
        {
            var index = (int)layer;
            if (index >= 0 && index <= 32)
            {
                collisionDic[name] = index;
            }
        }

        public int GetCollisionLayerIndex(string name)
        {
            var layer = 0;
            if (collisionDic.ContainsKey(name))
            {
                layer = collisionDic[name];
            }
            else
            {
                var values = Enum.GetValues(typeof(CollisionLayerStatus));
                foreach (var value in values)
                {
                    if (Enum.GetName(typeof(CollisionLayerStatus), value) == name)
                    {
                        layer = (int)value;
                    }
                }
            }           
            return layer;
        }

        public CollisionLayerStatus GetCollisionLayer(string name)
        {
            var layer = CollisionLayerStatus.None;
            if (collisionDic.ContainsKey(name))
            {
                layer = (CollisionLayerStatus)collisionDic[name];
            }
            else
            {   
                var values = Enum.GetValues(typeof(CollisionLayerStatus));
                foreach (var value in values)
                {
                    if (Enum.GetName(typeof(CollisionLayerStatus), value) == name)
                    {
                        layer = (CollisionLayerStatus)value;
                    }
                }
            }
            return layer;
        }
    }
}