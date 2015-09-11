using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace VKeCRM.Common.Trace.ServiceInspector
{
	public class TraceServerBehaviorExtensionElement : BehaviorExtensionElement
	{
		protected override object CreateBehavior()
		{
			return new TraceServerEndpointBehavior();
		}

		public override Type BehaviorType
		{
			get
			{
				return typeof(TraceServerEndpointBehavior);
			}
		}
	}
}
