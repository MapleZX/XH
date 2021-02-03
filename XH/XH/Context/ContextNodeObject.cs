using Godot;
using System;
using XH.Container;
namespace XH
{
	public class ContextNodeObject : Node, IContextObject
	{
		private readonly static IServiceCollection service_collection = new ServiceCollection();
		private readonly static ICreator creator = new Creator();
		public override void _Ready()
		{
			creator.Register(service_collection);
		}
		public override void _Notification(int what)
		{
			
		}

		public IService GetService<IService>() where IService : class
		{
			var object_name = ServiceCollection.OBJECTNAME;
			var service = service_collection.GetService<IService>(object_name);
			return service;
		}

		public IService GetService<IService>(string object_name) where IService : class
		{
			var service = service_collection.GetService<IService>(object_name);
			return service;
		}

		public void ScopedEnd()
		{
			service_collection.ScopedEnd();
		}
	}
}

