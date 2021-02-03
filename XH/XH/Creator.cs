using Godot;
using System;
using System.Collections.Generic;
using XH.Container;
namespace XH
{
	public class Creator : ICreator
	{
		public void Register(IServiceCollection service)
		{
			service.AddTransient<ICreator, Creator>();
		}
	}
}
