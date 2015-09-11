using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web;

namespace VKeCRM.Common.Trace.ServiceInspector
{
    public class TraceClientMessageInspector : IClientMessageInspector
	{
		#region Fields
		private TraceInfo CurrentTraceInfo;
		private Stopwatch ServiceStopWatch;
		#endregion

		#region IClientMessageInspector Members

		public void AfterReceiveReply(ref Message reply, object correlationState)
        {
			if (ServiceStopWatch != null)
			{
				ServiceStopWatch.Stop();
				if (CurrentTraceInfo != null)
				{
					CurrentTraceInfo.Duration = ServiceStopWatch.ElapsedMilliseconds;
					TraceLogger.Instance.TraceServiceEnd(CurrentTraceInfo, true);
				}
			}
        }

        /// <summary>
        /// Add token message at header to using NHibernate cache
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
			// add trace log for debug and performance tuning
            if (null != (request.Headers).MessageId && (request.Headers).MessageId.IsGuid)
            {
				ServiceStopWatch = Stopwatch.StartNew();
                Guid messageId;
                (request.Headers).MessageId.TryGetGuid(out messageId);

                CurrentTraceInfo = new TraceInfo()
                {
					SessionId = (HttpContext.Current != null && HttpContext.Current.Session != null) ? HttpContext.Current.Session.SessionID : "",
                    TraceType = TraceType.WcfActionClientCall,
                    TraceName = request.Headers.Action,
                    TraceUniqueId = messageId.ToString()
                };

				TraceLogger.Instance.TraceServiceStart(CurrentTraceInfo, true);

                // Add a message header with sessionid 
				MessageHeader<string> messageHeader = new MessageHeader<string>(CurrentTraceInfo.SessionId);
                MessageHeader untyped = messageHeader.GetUntypedHeader("sessionid", "ns");
                request.Headers.Add(untyped);
            }
            return null;
        }

        #endregion
    }
}
