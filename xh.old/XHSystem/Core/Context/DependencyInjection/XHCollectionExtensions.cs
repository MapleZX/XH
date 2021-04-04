using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace XHSystem
{
	public static class XHCollectionExtensions
	{
		# region 添加元素
		public static IXHCollection XHRegister(this IXHCollection collection, Type serviceType, Type implementationType, string xHname = "", XHLifetime lifetime = XHLifetime.Transient)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			if (serviceType == null)
			{
				throw new ArgumentNullException(nameof(serviceType));
			}

			if (implementationType == null)
			{
				throw new ArgumentNullException(nameof(implementationType));
			}
			
			Add(collection, serviceType, implementationType, lifetime, xHname);
			return collection;
		}

		public static IXHCollection XHRegister<TService, TImplementation>(this IXHCollection collection, string xHname = "", XHLifetime lifetime = XHLifetime.Transient)
			where TService : class
			where TImplementation : class, TService
		{
			return collection.XHRegister(typeof(TService), typeof(TImplementation), xHname, lifetime);
		}

		public static IXHCollection XHRegisterSingleton(this IXHCollection collection, Type serviceType, Type implementationType, string xHname = "")
		{
			return collection.XHRegister(serviceType, implementationType, xHname, XHLifetime.Singleton);
		}

		public static IXHCollection XHRegisterSingleton<TService, TImplementation>(this IXHCollection collection, string xHname = "", XHLifetime lifetime = XHLifetime.Transient)
			where TService : class
			where TImplementation : class, TService
		{
			return collection.XHRegister(typeof(TService), typeof(TImplementation), xHname, XHLifetime.Singleton);
		}

		public static TService XHResolve<TService>(this IXHCollection collection, string xHname = "")
			where TService : class
		{
			return (TService)collection.XHResolve(typeof(TService), xHname);
		}

		private static IXHCollection Add(IXHCollection collection, Type serviceType, Type implementationType, XHLifetime lifetime, string xHname = "")
		{
			// if (collection == null)
			// {
			//     throw new ArgumentNullException(nameof(collection));
			// }
			var descriptor = new XHDescriptor(xHname, lifetime, serviceType, implementationType);
			// collection.ToList().RemoveAll(item => item.XHname == xHname && item.XHServiceType == serviceType);
			if (lifetime == XHLifetime.Singleton) 
			{
				// Instance = Expression.Lambda<Func<object>>(Expression.New(implementationType)).Compile();
				var Instance = Activator.CreateInstance(implementationType);
				descriptor = new XHDescriptor(xHname, lifetime, serviceType, implementationType, Instance);
			}
			collection.Add(descriptor);
			return collection;
		}
		# endregion

		# region 获取元素
		// private static Func<object> Instance = null;
		public static object XHResolve(this IXHCollection collection, Type serviceType, string xHname = "")
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			if (serviceType == null)
			{
				throw new ArgumentNullException(nameof(serviceType));
			}

			var value = collection.Cast<XHDescriptor>().LastOrDefault(item => (item.XHServiceType == serviceType && item.XHname == xHname));
			if (value == null) Godot.GD.Print("The entered value is empty : ", xHname, " Type : ", serviceType);
			// var values = (from item in collection where (item.XHServiceType == serviceType && item.XHname == xHname) select item).ToList();	

			//if (!values.Any()) GD.Print("The entered value is empty : ", xHname, " Type : ", serviceType);

			// var value = values[values.Count - 1];

			// if (values.Count > 1)
			// {     
			// 	for (int i = 0; i < values.Count - 1; i++)
			// 	{
			// 		collection.Remove(values[i]);
			// 	}
			// }

			if (value.XHImplementationInstance != null)
			{
				return value.XHImplementationInstance;
			}

			else
			{
				// XHCollectionExtensions.Instance = Expression.Lambda<Func<object>>(Expression.New(value.XHImplementationType)).Compile();
				// if (value.lifetime == XHLifetime.Singleton) value.XHImplementationInstance = XHCollectionExtensions.Instance();
				var Instance = Activator.CreateInstance(value.XHImplementationType);
				return Instance;
			}
		}
		# endregion
	}
}
