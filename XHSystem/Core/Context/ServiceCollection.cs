using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;
using XHSystem.Context.Services;
using XHSystem.Context.Modules;
namespace XHSystem.Context
{
    public class ServiceCollection : IServiceCollection
    {
        public int ServiceCount { get => services.Count; private set => value = services.Count; }

        public int ClientCount { get => singleton_objects.Count; private set => value = singleton_objects.Count; }
        
        public const string OBJECTNAME = "";
		// public const string TRANSIENTS = "transients";
		// public const string SCOPEDS = "scopeds";
		// public const string SINGLETONS = "singletons";

        private readonly static Dictionary<Type, Dictionary<string, ServiceModule>> services = new Dictionary<Type, Dictionary<string, ServiceModule>>();

		private readonly static Dictionary<Type, Dictionary<string, object>> singleton_objects = new Dictionary<Type, Dictionary<string, object>>();

		private Dictionary<Type, Dictionary<string, object>> scoped_objects = new Dictionary<Type, Dictionary<string, object>>();

		private readonly static Dictionary<string, ServiceModule> transients = new Dictionary<string, ServiceModule>();
		private readonly static Dictionary<string, ServiceModule> scopeds = new Dictionary<string, ServiceModule>();
		private readonly static Dictionary<string, ServiceModule> singletons = new Dictionary<string, ServiceModule>();

        public void AddScoped<TService, TClient>()
            where TService : class
            where TClient : class, new()
        {
            AddScoped<TService, TClient>(OBJECTNAME);
        }

        public void AddScoped<TService, TClient>(string object_name)
            where TService : class
            where TClient : class, new()
        {
            SetService<TService, TClient>(object_name, ServiceModule.PatternStatus.SCOPEDS, scopeds);
        }

        public void AddSingleton<TService, TClient>()
            where TService : class
            where TClient : class, new()
        {
            AddSingleton<TService, TClient>(OBJECTNAME);
        }

        public void AddSingleton<TService, TClient>(string object_name)
            where TService : class
            where TClient : class, new()
        {
            SetService<TService, TClient>(object_name, ServiceModule.PatternStatus.SINGLETONS, singletons);

            Dictionary<string, object> module = new Dictionary<string, object>();
			module[object_name] = GetService(typeof(TService), object_name);
			singleton_objects[typeof(TService)] = module;
        }

        public void AddTransient<TService, TClient>()
            where TService : class
            where TClient : class, new()
        {
            AddTransient<TService, TClient>(OBJECTNAME);
        }

        public void AddTransient<TService, TClient>(string object_name)
            where TService : class
            where TClient : class, new()
        {
            SetService<TService, TClient>(object_name, ServiceModule.PatternStatus.TRANSIENTS, transients);
        }

        public void Clear()
        {
            services.Clear();
            singleton_objects.Clear();
        }

        public TService GetService<TService>() where TService : class
        {
            var value = (TService)GetService<TService>(OBJECTNAME);
			return value;
        }

        public TService GetService<TService>(string object_name) where TService : class
        {
            var value = default(TService);
			var type = typeof(TService);
	
			if (singleton_objects.ContainsKey(type))
			{
				var modules = singleton_objects[type];
				if (modules.ContainsKey(object_name))
				{
					value = (TService)modules[object_name];
					return value;
				}
			}

			if (IsScoped(type, object_name))
			{
				if (scoped_objects.ContainsKey(type))
				{
					var objects = scoped_objects[type];
					if (objects.ContainsKey(object_name))
					{
						value = (TService)objects[object_name];
						return value;
					}
					value = (TService)GetService(type, object_name);
					objects[object_name] = value;
					return value;
				}
				var modules = new Dictionary<string, object>();
				value = (TService)GetService(type, object_name);
				modules[object_name] = value;
				scoped_objects[type] = modules;
				return value;
			}

			value = (TService)GetService(type, object_name);
			return value;
        }

        public void RemoveClient(Type ServiceType)
        {
            services.Remove(ServiceType);
        }

        public void RemoveService(Type ServiceType)
        {
            singleton_objects.Remove(ServiceType);
        }

        public void ScopedEnd()
        {
            scoped_objects = new Dictionary<Type, Dictionary<string, object>>();
        }

        private bool IsScoped(Type contract, string object_name)
		{
			var modules = services[contract];
			ServiceModule module = modules[object_name];
			if (module.pattern == ServiceModule.PatternStatus.SCOPEDS)
				return true;
			return false;
		}
        private object GetService(Type contract, string object_name)
		{
			var modules = services[contract];
			ServiceModule module = modules[object_name];
			Type implementation = module.client;

			ConstructorInfo constructor = implementation.GetConstructors()[0];
			ParameterInfo[] constructorParameters = constructor.GetParameters();
			if (constructorParameters.Length == 0) 
			{ 
				return Activator.CreateInstance(implementation);  
			}
				
			List<object> parameters = new List<object>(constructorParameters.Length);
			foreach (ParameterInfo parameterInfo in constructorParameters) 
			{  
				parameters.Add(GetService(parameterInfo.ParameterType, object_name));
			}

			return constructor.Invoke(parameters.ToArray());
		}
		private void SetService<IService, Service>(string object_name, ServiceModule.PatternStatus pattern, Dictionary<string, ServiceModule> modules)
			where IService : class
			where Service : class, new()
		{
			var i_service = typeof(IService);
			var service = typeof(Service);
			var module = new ServiceModule(object_name, pattern, i_service, service);
			modules[object_name] = module;
			services[i_service] = modules;
		}
    }
}