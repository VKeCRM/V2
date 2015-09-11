using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using VKeCRM.Common.Logging;

namespace VKeCRM.Common.Trace
{
	public class TraceLogger
	{
		#region Constants

		private const string SessionIdKey = "SessionId";
		private const string TraceUniqueIdKey = "TraceUniqueId";
		private const string TraceLevelKey = "TraceLevel";
		private const string TraceTypeKey = "TraceType";
		private const string TraceNameKey = "TraceName";
		private const string TraceStatusKey = "TraceStatus";
		private const string DurationKey = "Duration";
		//private readonly Dictionary<Guid, TraceInfo> WcfActions = new Dictionary<Guid, TraceInfo>();

		#endregion

		#region Methods
		//public void TraceBeforeStartWcfAction(Guid messageId, TraceInfo traceInfo)
		//{
		//    traceInfo.TraceDateTime = DateTime.UtcNow;
		//    lock (WcfActions)
		//    {
		//        WcfActions.Add(messageId, traceInfo);
		//    }
		//    TraceLogger.Instance.TraceServiceStart(traceInfo);
		//}

		//public void TraceAfterWcfAction(Guid messageId)
		//{
		//    if (WcfActions.ContainsKey(messageId))
		//    {
		//        TraceInfo traceInfo = WcfActions[messageId];
		//        traceInfo.Duration = (long)(DateTime.UtcNow - traceInfo.TraceDateTime).TotalMilliseconds;
		//        TraceLogger.Instance.TraceServiceEnd(traceInfo);
		//        lock (WcfActions)
		//        {
		//            WcfActions.Remove(messageId);
		//        }
		//    }
		//}

		public void TraceServiceStart(TraceInfo traceInfo, bool isClient)
		{
			traceInfo.TraceStatus = TraceStatus.Start;
			TraceLogger.Instance.PrepareThreadContextProperties(traceInfo);
			if(isClient)
			{
				LoggerManager.TraceServiceClientStartLogger.Debug(traceInfo.Content);
			}
			else
			{
				LoggerManager.TraceServiceServerStartLogger.Debug(traceInfo.Content);
			}
			ThreadContext.Properties[TraceStatusKey] = TraceStatus.InProgress;
			ThreadContext.Properties[DurationKey] = 0;
		}

		public void TraceServiceEnd(TraceInfo traceInfo, bool isClient)
		{
			traceInfo.TraceStatus = TraceStatus.End;
			TraceLogger.Instance.PrepareThreadContextProperties(traceInfo);
			if (isClient)
			{
				LoggerManager.TraceServiceClientEndLogger.Debug(traceInfo.Content);
			}
			else
			{
				LoggerManager.TraceServiceServerEndLogger.Debug(traceInfo.Content);
			}
		}

		public void TracePageStart(TraceInfo traceInfo)
		{
			traceInfo.TraceType = TraceType.PageRequest;
			traceInfo.TraceStatus = TraceStatus.Start;
			TraceLogger.Instance.PrepareThreadContextProperties(traceInfo);
			LoggerManager.TracePageStartLogger.Debug(traceInfo.Content);
			ThreadContext.Properties[TraceStatusKey] = TraceStatus.InProgress;
			ThreadContext.Properties[DurationKey] = 0;
		}

		public void TracePageEnd(TraceInfo traceInfo)
		{
			traceInfo.TraceType = TraceType.PageRequest;
			traceInfo.TraceStatus = TraceStatus.End;
			TraceLogger.Instance.PrepareThreadContextProperties(traceInfo);
			LoggerManager.TracePageEndLogger.Debug(traceInfo.Content);
		}

		public void PrepareThreadContextProperties(TraceInfo traceInfo)
		{
			ThreadContext.Properties[SessionIdKey] = traceInfo.SessionId;
			ThreadContext.Properties[TraceUniqueIdKey] = traceInfo.TraceUniqueId;
			ThreadContext.Properties[TraceLevelKey] = traceInfo.TraceLevel;
			ThreadContext.Properties[TraceTypeKey] = traceInfo.TraceType;
			ThreadContext.Properties[TraceNameKey] = traceInfo.TraceName;
			ThreadContext.Properties[TraceStatusKey] = traceInfo.TraceStatus;
			ThreadContext.Properties[DurationKey] = traceInfo.Duration;
		}

		#endregion

		#region Singleton Implementation
		/// <summary>
		/// Gets an instance of the TraceLogger class.
		/// </summary>
		public static TraceLogger Instance
		{
			get
			{
				return Nested.Instance;
			}
		}
		#endregion

		#region Nested Classes
		/// <summary>
		/// Fully lazy initialization of the static member: triggered by the 
		/// first reference. Implementation has performance benefits, and is 
		/// fully lazy.
		/// </summary>
		/// <remarks>
		/// http://www.yoda.arachsys.com/csharp/singleton.html
		/// </remarks>
		private class Nested
		{
			/// <summary>
			/// read only instance of TraceLogger
			/// </summary>
			internal static readonly TraceLogger Instance = new TraceLogger();

			/// <summary>
			/// Initializes static members of the Nested class
			/// </summary>
			static Nested()
			{
				// Explicit static constructor to tell C# compiler to not mark type as beforefieldinit
			}
		}
		#endregion
	}
}
