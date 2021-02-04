using Godot;
using System;
using XHSystem.Context.Services;

namespace XHSystem.Context
{
    public class ContextNode : Node, IContextObject
    {
        private static readonly IServiceCollection service = new ServiceCollection();
        private static readonly ICreatorService creator = new Creator();
        public override void _Ready()
        {
            creator.ServiceConfiguration(service);
        }
    }
}