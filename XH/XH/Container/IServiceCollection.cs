using System;
namespace XH.Container
{
    public interface IServiceCollection
    {
        void AddSingleton<ISingletonService, SingletonService>() where ISingletonService : class where SingletonService : class, new();
        void AddSingleton<ISingletonService, SingletonService>(string object_name) where ISingletonService : class where SingletonService : class, new();
        void AddScoped<IScopedService, ScopedService>() where IScopedService : class where ScopedService : class, new();
        void AddScoped<IScopedService, ScopedService>(string object_name) where IScopedService : class where ScopedService : class, new();
        void AddTransient<ITransientService, TransientService>() where ITransientService : class where TransientService : class, new();
        void AddTransient<ITransientService, TransientService>(string object_name) where ITransientService : class where TransientService : class, new();
        IService GetService<IService>(string object_name) where IService : class;
        void ScopedEnd();
    }
}