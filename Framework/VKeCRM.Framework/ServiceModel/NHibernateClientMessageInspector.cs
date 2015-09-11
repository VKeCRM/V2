using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using Message=System.ServiceModel.Channels.Message;

namespace VKeCRM.Framework.ServiceModel
{
    public class NHibernateClientMessageInspector : IClientMessageInspector
    {
		/// <summary>
		/// the default setting of whether it should use the 2nd level cache.
		/// If no setting is specified in the config file, it defaults to false.
		/// </summary>
    	private static bool IsDefaultBypassNHibernateCache =
    		bool.Parse(ConfigurationManager.AppSettings["BypassNH2LevelCache"]??"false");

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        /// <summary>
        /// Add token message at header to using NHibernate cache
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
			//get the extension item that the client may have put in the channel
			var item = channel.Extensions.Find<NHibernate2ndLevelCacheBehaviorExtensionItem>();
        	bool clientWantsToBypass = (item != null && item.BypassNHibernate2ndLevelCache);

			if (clientWantsToBypass || IsDefaultBypassNHibernateCache)
			{
				MessageHeader<bool> mhg = new MessageHeader<bool>(true);
				MessageHeader untyped = mhg.GetUntypedHeader("token", "ns");
				request.Headers.Add(untyped);
			}

        	return null;
        }

        #endregion
    }
}
