using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// Extension item that a service client uses to indicate whether to use or bypass
	/// NHibernate 2nd level cache
	/// </summary>
	public class NHibernate2ndLevelCacheBehaviorExtensionItem : IExtension<IContextChannel>
	{
		public bool BypassNHibernate2ndLevelCache { get; set; }

		#region IExtension<IContextChannel> Members

		public void Attach(IContextChannel owner)
		{
			//this is called when the item is added into the Extensions collection.
			//throw new NotImplementedException();
		}

		public void Detach(IContextChannel owner)
		{
			//this is called when theitem is removed from the Extensions collection.
			//throw new NotImplementedException();
		}

		#endregion
	}
}
