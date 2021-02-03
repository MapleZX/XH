using Godot;
using System;
using XH;
using XH.Extensions;
public class Demo : Node2D
{
	IContextObject context;
	public override void _Ready()
	{
		context = this.Context<ContextNodeObject>();
		var test1 = context.GetService<ICreator>();
		var test2 = context.GetService<ICreator>();
		GD.Print(Name, " : ", test1.GetHashCode());
		GD.Print(Name, " : ", test2.GetHashCode());
		context.ScopedEnd();
	}
}
