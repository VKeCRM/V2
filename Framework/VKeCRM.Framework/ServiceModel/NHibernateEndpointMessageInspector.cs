using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace VKeCRM.Framework.ServiceModel
{
	public class NHibernateEndpointMessageInspector : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get
			{
				return typeof(NHibernateClientEndpointBehavior);
			}
		}

		protected override object CreateBehavior()
		{
            return new NHibernateClientEndpointBehavior();
		}
	}
}
