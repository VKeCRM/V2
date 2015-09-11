using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;

namespace VKeCRM.Common.Trace.ServiceInspector
{
	public class TraceClientBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(TraceClientEndpointBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new TraceClientEndpointBehavior();
        }
    }
}
