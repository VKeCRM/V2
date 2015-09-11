using System;
using System.Collections.Generic;

namespace VKeCRM.Common.Collections
{
	public static class EnumerableExtensions
	{
		public static void Each<T>(this IEnumerable<T> items, Action<T> action)
		{
			
			foreach (var t in items)
			{
				action(t);
			}
		}
	}
}
