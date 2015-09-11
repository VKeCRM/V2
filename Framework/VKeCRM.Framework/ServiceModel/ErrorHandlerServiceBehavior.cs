using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// Default service behavior that attaches our own CallContextInitializer, ErrorHandler.
	/// </summary>
	public class ErrorHandlerServiceBehavior : Attribute, IServiceBehavior
	{
		#region IServiceBehavior Members

		void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
			bindingParameters.Remove<ServiceCredentials>();
		}

		/// <summary>
		/// Apply our own service behavior including CallContextInitializer, ErrorHandler.
		/// </summary>
		/// <param name="serviceDescription"></param>
		/// <param name="serviceHostBase"></param>
		void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
			{
				DefaultErrorHandler defaultErrorHandler = new DefaultErrorHandler {ServiceName = serviceDescription.Name};
				channelDispatcher.ErrorHandlers.Add(defaultErrorHandler);
			}
		}

		void IServiceBehavior.Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
		{
			
		}

		#endregion
	}
}
