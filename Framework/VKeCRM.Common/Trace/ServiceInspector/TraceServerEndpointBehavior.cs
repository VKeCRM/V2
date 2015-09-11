﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace VKeCRM.Common.Trace.ServiceInspector
{
	public class TraceServerEndpointBehavior  : IEndpointBehavior
	{
		public void Validate(ServiceEndpoint endpoint)
		{
		}

		public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			TraceServerMessageInspector inspector = new TraceServerMessageInspector();
			endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
		}

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			throw new Exception("Behavior not supported on the consumer side");
		}
	}
}
