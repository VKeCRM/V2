using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// Class for including NH Open-Session-In-View.
	/// </summary>
	[NHibernateSessionInViewServiceBehavior]
	public class NHibernateServiceBase : ServiceBase
	{
	}
}
