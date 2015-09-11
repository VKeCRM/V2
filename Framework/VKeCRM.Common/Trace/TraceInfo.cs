using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.Trace
{
	public class TraceInfo
	{
		public string SessionId
		{
			get;
			set;
		}

		public string TraceUniqueId
		{
			get;
			set;
		}

		public int TraceLevel
		{
			get
			{
				return (int) TraceType;
			}
		}

		public TraceType TraceType
		{
			get;
			set;
		}

		public string TraceName
		{
			get;
			set;
		}

		public TraceStatus TraceStatus
		{
			get;
			set;
		}

		public string Content
		{
			get;
			set;
		}

		public long Duration
		{
			get;
			set;
		}

		public DateTime TraceDateTime
		{
			get;
			set;
		}
	}
}
