using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace VKeCRM.Common.Utility
{
	public static class WcfClientHelper
	{
		/// <summary>
		/// Clean up client service and underlying channel
		/// </summary>
		public static void CleanUpServiceClient(ICommunicationObject client, IClientChannel channel)
		{

			if (client != null)
			{
				if (client.State == System.ServiceModel.CommunicationState.Faulted)
				{
					client.Abort();
				}
				else
				{
					try
					{
						client.Close();
					}
					catch (CommunicationException ex)
					{
						client.Abort();
					}
					catch (TimeoutException ex)
					{
						client.Abort();
					}
					catch (Exception ex)
					{
						client.Abort();
						throw;
					}
				}

				channel.Close();
				channel.Dispose();
			}
		}
	}
}
