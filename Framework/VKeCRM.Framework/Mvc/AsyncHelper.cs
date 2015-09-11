using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKeCRM.Common.Utility;
using VKeCRM.Framework.Business;

namespace VKeCRM.Framework.Mvc
{
	public static class AsyncHelper
	{
		public static Task AsyncRun(Action action)
		{
			var task = new Task(action);
			task.Start();
			return task;
		}

		public static Task<T> AsyncRun<T>(T param, Action<T> action)
		{
			var task = new Task<T>(() => param);
			task.Start();
			task.Wait();
			task.ContinueWith(task1 => action.Invoke(task1.Result));
			return task;
		}
	}
}
