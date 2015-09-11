//-----------------------------------------------------------------------
// <copyright file="NHibernateOpenSessionInViewContext.cs" company="VKeCRM">
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
using NHibernate;
using VKeCRM.Framework.Data;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// CallContextInitializer that implements implicit NHibernate transaction control.
	/// </summary>
	public class NHibernateOpenSessionInViewContext : ICallContextInitializer
	{
		/// <summary>
		/// Begin NH transaction and also records the method name being called.
		/// </summary>
		/// <param name="instanceContext"></param>
		/// <param name="channel"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		object ICallContextInitializer.BeforeInvoke(
			System.ServiceModel.InstanceContext instanceContext,
			System.ServiceModel.IClientChannel channel,
			System.ServiceModel.Channels.Message message)
		{

			if (instanceContext.GetServiceInstance() is ServiceBase)
			{
                try
                {
					//try to catch the client request whether we should bypass the 2nd level cache.
					if (OperationContext.Current.IncomingMessageHeaders.FindHeader("token", "ns") >= 0)
					{
                        _isChange = true;
                        bool? bypass2ndLevelCache = OperationContext.Current.IncomingMessageHeaders.GetHeader<bool>("token", "ns");
						if (bypass2ndLevelCache.HasValue && bypass2ndLevelCache.Value)
						{
                            NHibernateSessionManager.Instance.EnforceRefreshData();
						}
					}
                	
                }
				catch(Exception ex)
                {
					//just ignore and we'll use the default 2nd level cache setting
                }
			    //NHibernateSessionManager.Instance.IsTransactionRequired = true;
			}
			return null;
		}

		void ICallContextInitializer.AfterInvoke(object state)
		{
            if (_isChange)
            {
                RemoveClientNHibernateSecondLevelCacheSetting();
                _isChange = false;
            }
		}

        /// <summary>
        /// Mark the client nhibernate second level cache token
        /// </summary>
	    private bool _isChange = false;
        private void RemoveClientNHibernateSecondLevelCacheSetting()
        {
            try
            {
                if (OperationContext.Current.IncomingMessageHeaders.FindHeader("token", "ns") >= 0)
                {
                    OperationContext.Current.IncomingMessageHeaders.RemoveAt(
                        OperationContext.Current.IncomingMessageHeaders.FindHeader("token", "ns"));
                }
            }
            catch (Exception ex)
            {
                
            }
        }
	}

}
