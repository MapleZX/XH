using Godot;
using System;

namespace _Deprecated_
{
    public interface IServiceCollection
    {
        // int ServiceCount { get; }
        // int ClientCount { get; }
        void AddSingleton<TService, TClient>() where TService : class where TClient : class, new();
        void AddSingleton<TService, TClient>(string object_name) where TService : class where TClient : class, new();
        void AddScoped<TService, TClient>() where TService : class where TClient : class, new();
        void AddScoped<TService, TClient>(string object_name) where TService : class where TClient : class, new();
        void AddTransient<TService, TClient>() where TService : class where TClient : class, new();
        void AddTransient<TService, TClient>(string object_name) where TService : class where TClient : class, new();
        TService GetService<TService>() where TService : class;
        TService GetService<TService>(string object_name) where TService : class;
        void ScopedEnd();
        void RemoveService(Type ServiceType);
        void RemoveClient(Type ServiceType);
		void Clear();
    }
}