using Godot;
using System;
using XHSystem.Context;
using XHSystem.Context.Services;
using XHSystem.Context.Extensions;

public class Demo : Node2D, IClientCollection
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // this.Context<ContextNode>();
        this.Context();
    }
    [Service]
    public void ClientConfiguration(IServiceCollection service)
    {
        var test = service.GetService<ICreatorService>();
        var test2 = service.GetService<ICreatorService>();
        GD.Print(test.GetHashCode());
        GD.Print(test2.GetHashCode());
    }
}
