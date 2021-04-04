
namespace XHSystem
{
    public interface ICollisionCollection
    {
        void AddCollisionLayer(string name, CollisionLayerStatus layer);
        int GetCollisionLayerIndex(string name);
        CollisionLayerStatus GetCollisionLayer(string name);
    }
}