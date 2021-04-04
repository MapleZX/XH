using Godot;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace XHSystem
{
	public static class NodeExtension
	{
		/// <summary>
		/// 快捷获取节点
		/// </summary>
		/// <param name="node"></param>
		public static FieldInfo[] RegisterNode(this Node node, FieldInfo[] fieldInfo = null)
		{
			Type nodeType = node.GetType();
			var binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			FieldInfo[] infos = fieldInfo == null ? nodeType.GetFields(binding) : fieldInfo;
			
			var attrType = typeof(NodeAttribute);

			Dictionary<string, FieldInfo> child = new Dictionary<string, FieldInfo>();
			Dictionary<string, NodePath> nodepath = new Dictionary<string, NodePath>();

			foreach (var fo in infos)
			{
				bool isNodeAttribute = Attribute.IsDefined(fo, attrType);
				if (isNodeAttribute)
				{
					NodeAttribute attr = (NodeAttribute)Attribute.GetCustomAttribute(fo, attrType);
					if (fo.FieldType == typeof(NodePath) && attr.nodePath == "")
					{
						nodepath[fo.Name] = fo.GetValue(node) as NodePath;
					}

					if (attr.hint == NodePropertyHint.NodePath && attr.nodePath != "")
					{
						child[attr.nodePath] = fo;
					}
					else if (attr.nodePath != "")
					{
						fo.SetValue(node, node.GetNode(attr.nodePath));
					}

				}
			}

			foreach (var path in nodepath)
			{
				if (path.Value != null && child.ContainsKey(path.Key))
				{
					child[path.Key].SetValue(node, node.GetNode(path.Value));
				}
				else
				{
					GD.Print("NodePath is null : ", path.Key);
				}
			}
			return infos;
		}
		/// <summary>
		/// 快捷连接信号
		/// </summary>
		/// <param name="node"></param>
		public static MethodInfo[] SignalConnect(this Node node, MethodInfo[] methodInfos = null)
		{
			Type nodeType = node.GetType();
			var binding = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
			MethodInfo[] methods = methodInfos == null ? nodeType.GetMethods(binding) : methodInfos;

			var methodType = typeof(SignalMethodAttribute);

			foreach (var method in methods)
			{
				bool isSignalAttribute = Attribute.IsDefined(method, methodType);
				if (isSignalAttribute)
				{
					SignalMethodAttribute attr = (SignalMethodAttribute)Attribute.GetCustomAttribute(method, methodType);
					var signal = attr.signal;
					var nodeName = attr.nodeName;
					var current = nodeName == "" ? node : GetNodeInType<Node>(node, nodeName, binding);
					var target = attr.target == "" || attr.target == "this" ? node : GetNodeInType<Node>(node, attr.target, binding);
					var flags = attr.flags;
					var binds = attr.binds == "" ? null : GetNodeInType<Godot.Collections.Array>(node, attr.binds, binding);

					if (current != null)
					{
						current.Connect(signal, target, method.Name, binds, flags);
					}
					else
					{
						var className = node.GetType().Name;
						var objectName = attr.nodeName;
						// GD.Print("节点:", objectName, "不存在!");
						GD.Print("Connection signal failed!");
						GD.Print("Not found ", objectName, " node in ", className);
					} 
				}
			}
			return methods;
		}

		private static T GetNodeInType<T>(Node node, string target, BindingFlags binding) where T : class
		{
			var type = node.GetType();
			var field = type.GetField(target, binding);
			var new_node = field.GetValue(node) as T;
			return new_node;
		}

		public static AnimationNodeStateMachinePlayback RegisterAnimationtateMachine(this AnimationTree tree, string parameters = "parameters/playback")
		{
			var back = tree != null ? (AnimationNodeStateMachinePlayback)tree.Get(parameters) : null;
			return back;
		}
	}
}
