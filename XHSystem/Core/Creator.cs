using Godot;
using System;
using XHSystem.Context.Services;
namespace XHSystem.Context
{
    public class Creator : ICreatorService
    {
        public void ServiceConfiguration(IServiceCollection service)
        {
            service.AddTransient<ICreatorService, Creator>();
        }
    }
}