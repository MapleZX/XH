using System.Collections.Generic;

namespace XHSystem
{
    [Collision]
    public interface IClientCollisionCollection
    {
        void CollisionLayerConfiguration(ICollisionCollection collision, List<CollisionLayerStatus> layers);
        void CollisionMaskConfiguration(ICollisionCollection collision, List<CollisionLayerStatus> masks);
    }
}