using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace VKeCRM.Common.Trace.ServiceInspector
{
	public class TraceServerMessageInspector : IDispatchMessageInspector
	{
		private TraceInfo CurrentTraceInfo;
		private Stopwatch ServiceStopWatch;

		public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
		{
			// add trace log for debug and performance tuning
			if ((request.Headers).MessageId.IsGuid)
			{
				ServiceStopWatch = Stopwatch.StartNew();
				Guid messageId;
				(request.Headers).MessageId.TryGetGuid(out messageId);

				// retrieve sessionid in message header, then remove it
				string sessionId = string.Empty;
				if (OperationContext.Current.IncomingMessageHeaders.FindHeader("sessionid", "ns") >= 0)
				{
					sessionId = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("sessionid", "ns");
					OperationContext.Current.IncomingMessageHeaders.RemoveAt(OperationContext.Current.IncomingMessageHeaders.FindHeader("sessionid", "ns"));
				}

				CurrentTraceInfo = new TraceInfo()
				{
					SessionId = sessionId,
					TraceType = TraceType.WcfActionServerCall,
					TraceName = request.Headers.Action,
					TraceUniqueId = messageId.ToString()
				};
				TraceLogger.Instance.TraceServiceStart(CurrentTraceInfo, false);
			}

			return null;
		}

		public void BeforeSendReply(ref Message reply, object correlationState)
		{
			if (ServiceStopWatch != null)
			{
				ServiceStopWatch.Stop();
				if (CurrentTraceInfo != null)
				{
					CurrentTraceInfo.Duration = ServiceStopWatch.ElapsedMilliseconds;
					TraceLogger.Instance.TraceServiceEnd(CurrentTraceInfo, false);
				}
			}
		}
	}
}
