using System;
using System.Collections.Generic;
using System.Reflection;
namespace XH.Container
{
    public class ServiceCollection : IServiceCollection
    {
        public const string OBJECTNAME = "./base/object_name";
        public const string TRANSIENTS = "transients";
        public const string SCOPEDS = "scopeds";
        public const string SINGLETONS = "singletons";

        private readonly static Dictionary<Type, Dictionary<string, ServiceModule>> services = new Dictionary<Type, Dictionary<string, ServiceModule>>();

        private readonly static Dictionary<Type, Dictionary<string, object>> singleton_objects = new Dictionary<Type, Dictionary<string, object>>();

        private Dictionary<Type, Dictionary<string, object>> scoped_objects = new Dictionary<Type, Dictionary<string, object>>();

        private readonly static Dictionary<string, ServiceModule> transients = new Dictionary<string, ServiceModule>();
        private readonly static Dictionary<string, ServiceModule> scopeds = new Dictionary<string, ServiceModule>();
        private readonly static Dictionary<string, ServiceModule> singletons = new Dictionary<string, ServiceModule>();

        public void AddSingleton<ISingletonService, SingletonService>()
            where ISingletonService : class
            where SingletonService : class, new()
        {
            AddSingleton<ISingletonService, SingletonService>(OBJECTNAME);
        }

        public void AddScoped<IScopedService, ScopedService>()
            where IScopedService : class
            where ScopedService : class, new()
        {
            AddScoped<IScopedService, ScopedService>(OBJECTNAME);
        }

        public void AddTransient<ITransientService, TransientService>()
            where ITransientService : class
            where TransientService : class, new()
        {
            AddTransient<ITransientService, TransientService>(OBJECTNAME);
        }

        public void AddTransient<ITransientService, TransientService>(string object_name)
            where ITransientService : class
            where TransientService : class, new()
        {
            SetService<ITransientService, TransientService>(object_name, TRANSIENTS, transients);
        }
        public void AddScoped<IScopedService, ScopedService>(string object_name)
            where IScopedService : class
            where ScopedService : class, new()
        {
            SetService<IScopedService, ScopedService>(object_name, SCOPEDS, scopeds);
        }

        public void AddSingleton<ISingletonService, SingletonService>(string object_name)
            where ISingletonService : class
            where SingletonService : class, new()
        {
            SetService<ISingletonService, SingletonService>(object_name, SINGLETONS, singletons);
            Dictionary<string, object> module = new Dictionary<string, object>();
            module[object_name] = GetService(typeof(ISingletonService), object_name);
            singleton_objects[typeof(ISingletonService)] = module;
        }
        public IService GetService<IService>(string object_name) where IService : class
        {
            var value = default(IService);
            var type = typeof(IService);
            if (singleton_objects.ContainsKey(type))
            {
                var modules = singleton_objects[type];
                if (modules.ContainsKey(object_name))
                {
                    value = (IService)modules[object_name];
                    return value;
                }
            }
            if (HasScoped(type, object_name))
            {
                if (scoped_objects.ContainsKey(type))
                {
                    var objects = scoped_objects[type];
                    if (objects.ContainsKey(object_name))
                    {
                        value = (IService)objects[object_name];
                        return value;
                    }
                    value = (IService)GetService(typeof(IService), object_name);
                    objects[object_name] = value;
                    return value;
                }
                var modules = new Dictionary<string, object>();
                value = (IService)GetService(typeof(IService), object_name);
                modules[object_name] = value;
                scoped_objects[type] = modules;
                return value;
            }
            value = (IService)GetService(typeof(IService), object_name);
            return value;
        }
        public void ScopedEnd()
        {
            scoped_objects = new Dictionary<Type, Dictionary<string, object>>();
        }
        private bool HasScoped(Type contract, string object_name)
        {
            var modules = services[contract];
            ServiceModule module = modules[object_name];
            if (module.pattern == SCOPEDS && module.pattern.Equals(SCOPEDS))
                return true;
            return false;
        }
        private object GetService(Type contract, string object_name)
        {
            var modules = services[contract];
            ServiceModule module = modules[object_name];
            Type implementation = module.service;

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
        private void SetService<IService, Service>(string object_name, string pattern, Dictionary<string, ServiceModule> modules)
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