using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// A container class for service client behavior
	/// </summary>
	public static class ServiceClientBehaviorExtensionMethods
	{
		public static void BypassNHibernate2ndLevelCache<T>(this ClientBase<T> clientBase) where T : class
		{

			//clear the previous extension item
			NHibernate2ndLevelCacheBehaviorExtensionItem item =
				clientBase.InnerChannel.Extensions.Find<NHibernate2ndLevelCacheBehaviorExtensionItem>();
			if (item != null)
			{
				clientBase.InnerChannel.Extensions.Remove(item);
			}

			//add new one
			clientBase.InnerChannel.Extensions.Add(
				new NHibernate2ndLevelCacheBehaviorExtensionItem(){BypassNHibernate2ndLevelCache=true}
				);

		}
	}
}
