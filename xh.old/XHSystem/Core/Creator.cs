using System;

namespace XHSystem
{
    public class Creator : ICreatorService
    {
        /// <summary>
        /// 全局服务注册
        /// </summary>
        /// <param name="service"></param>
        public void ServiceConfiguration(IXHCollection service)
        {
            // service.XHRegister<IXHEventHandler<DamageEventArgs>, XHEventHandler<DamageEventArgs>>("DamageControl", XHLifetime.Singleton);

            // service.XHRegister<IXHEventHandler<BulletEventArgs>, XHEventHandler<BulletEventArgs>>("Bullet", XHLifetime.Singleton);

            // Godot.GD.Print(service.Count);
        }

        /// <summary>
        /// 设定碰撞层名
        /// </summary>
        /// <param name="collision"></param>
        public void CollisionConfiguration(ICollisionCollection collision)
        {
            collision.AddCollisionLayer("player", CollisionLayerStatus.Layer4);
        }
    }
}