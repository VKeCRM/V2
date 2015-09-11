using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace VKeCRM.Framework.Web.Helper
{
    public class ResourceHelper
    {
        public ResourceHelper()
        {
        }

		private static object _syncObject = new object();

        public static string GetGlobalResourceMessage(string resourceClass, string resourceKey)
        {
			lock (_syncObject)
			{
				string resourceMessage = "";
				try
				{
					object obj = HttpContext.GetGlobalResourceObject(resourceClass, resourceKey);
					resourceMessage = obj.ToString();
				}
				catch
				{
					resourceMessage = "Failed to get the resource file";
				}
				return resourceMessage;
			}
        }
    }
}
