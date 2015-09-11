//-----------------------------------------------------------------------
// <copyright file="NHibernateSessionInViewServiceBehavior.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.Collections.ObjectModel;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// Default service behavior that attaches our own CallContextInitializer, and MessageInspector.
	/// </summary>
	public class NHibernateSessionInViewServiceBehavior : Attribute, IServiceBehavior
	{
		/// <summary>
		/// Apply our own service behavior including CallContextInitializer, and MessageInspector.
		/// </summary>
		/// <param name="serviceDescription"></param>
		/// <param name="serviceHostBase"></param>
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
			{
				foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
				{
					DefaultMessageInspector defaultMessageInspector = new DefaultMessageInspector();
					defaultMessageInspector.ServiceName = serviceDescription.Name;
					endpointDispatcher.DispatchRuntime.MessageInspectors.Add(defaultMessageInspector);

					foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
					{
						dispatchOperation.CallContextInitializers.Add(new NHibernateOpenSessionInViewContext());
					}
				}
			}
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
						Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
			bindingParameters.Remove<ServiceCredentials>();
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{ }
	}
}
