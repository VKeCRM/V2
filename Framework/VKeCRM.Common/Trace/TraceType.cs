using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.Trace
{
	public enum TraceType
	{
		PageRequest = 10,
		WcfActionClientCall = 20,
		WcfActionServerCall = 21,
		ServiceImpl = 30
	}
}
