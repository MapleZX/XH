using Godot;
using System;
using System.Reflection;

namespace XHSystem
{
	public static class ContextExtension
	{
		# region 扩展
		public static void Start(this Node node, Action _Start = null)
		{
			node.RegisterNode(); // 注册节点
			if (_Start != null) _Start();
			node.SignalConnect(); // 连接信号		
		}

		public static void Start(this IXH xh)
		{           
			var node = xh as Node;

			xh.fieldInfo = node.RegisterNode(); // 注册节点

			var context = node.Context(xh.fieldInfo); // 获取上下文

			var client = xh as IClientCollisionCollection;
			if (client != null)
			{
				client.ClientCollision(context);
			}
			
			var data = xh as IDataInitialization;
			if (data != null)
			{
				data.DataDic = data.InitializationData(xh.fieldInfo); //数据初始化
			}

			xh._Start();

			xh.methodInfos = node.SignalConnect(); // 连接信号

			node.AddXHEvent(xh.methodInfos); // 注册事件

			
		}

		public static void Exit(this IXH xh, Action _Exit = null)
		{
			var node = xh as Node;
			node.RemoveXHEvent(xh.methodInfos); // 移除事件
			if (_Exit != null)
				_Exit();
		}

		# endregion

		# region <-- 获取上下文 -->
		public static Node Root(this Node node)
		{
			var _root = node.GetNode("/root");
			return _root;
		}
	 
		public static IContextObject Context(this Node node, FieldInfo[] fieldInfo = null, string path = "ContextNode")
		{
			string contextPath = "/root/" + path;
			var singleton = node.GetNode(contextPath);
			var context = singleton as IContextObject;

			var nodeType = node.GetType();

			var binding = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			
			FieldInfo[] info = fieldInfo == null ? nodeType.GetFields(binding) : fieldInfo;

			foreach (var fo in info)
			{
				// var isServiceAttribute = Attribute.IsDefined(fo, typeof(ServiceAttribute));
				var attr = (ServiceAttribute)Attribute.GetCustomAttribute(fo, typeof(ServiceAttribute));
				if (attr != null)
				{
					// var attr = (ServiceAttribute)Attribute.GetCustomAttribute(fo, typeof(ServiceAttribute));
					var xHname = attr.name;
					var xHServiceType = fo.FieldType;
					var value = context.GetXHCollection().XHResolve(xHServiceType, xHname);
					fo.SetValue(node, value);
				}
			}

			return context;
		}

		public static T Singleton<T>(this Node node, string path) where T : class
		{
			string nodepath  = "/root/" + path;
			var singleton = node.GetNode(nodepath);
			return singleton as T;
		}
		
		public static Node GetContextNode(this IContextObject context)
		{
			var node = context as Node;
			return node;
		}
		# endregion
	}
}
