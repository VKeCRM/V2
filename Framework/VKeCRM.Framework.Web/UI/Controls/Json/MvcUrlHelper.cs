using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
//using VKeCRM.Common.ApplicationSetting;
using System.Web.Mvc;
using System.Web.Routing;
using VKeCRM.Framework.Mvc;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public static class MvcUrlHelper
	{
		private static string URL_FORMAT = "Controllers/{Controller}/{Action}/Api";
		private static string URL_FORMAT_CONIG = "/Controllers/{0}/{1}/Api";
		private static string FULLURL_FORMAT = @"{0}Controllers/{1}/{2}/Api";
		private const string DYNAMIC_METHODS_URL = @"/Controllers/{0}/DynamicExecute/Api";

		public static void DefaultMapRoutes()
		{
			RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			RouteTable.Routes.IgnoreRoute("{*allaspx}", new { allaspx = @".*\.aspx(/.*)?" });
			RouteTable.Routes.IgnoreRoute("{*allashx}", new { allashx = @".*\.ashx(/.*)?" });
			RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
			AreaRegistration.RegisterAllAreas();
			//	MapVKeCRMUrls(routes, "VKeCRM.Portal.Web");
			RouteTable.Routes.MapRoute("VKeCRM", MvcUrlHelper.GetMVCFormatUrl(), "").RouteHandler = new AsyncMvcRouteHandler(); 
		}

		public static string GetDynamicMethodsUrl(string controller)
		{
			if (string.IsNullOrEmpty(controller))
			{
				throw new ArgumentOutOfRangeException("controller");
			}

			controller = System.Text.RegularExpressions.Regex.Replace(controller, "Controller", string.Empty);
			return string.Format(DYNAMIC_METHODS_URL, controller);
		}

		public static string GetMVCFormatUrl()
		{
			return URL_FORMAT;
		}

		public static string GetMVCUrl(string controller, string method)
		{
			if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(method))
			{
				throw new ArgumentOutOfRangeException("controller or method");
			}

			controller = System.Text.RegularExpressions.Regex.Replace(controller, "Controller", string.Empty);
			return string.Format(URL_FORMAT_CONIG, controller, method);
		}

        //public static string GetPortalFullMVCUrl(string controller, string method)
        //{
        //    if (!controller.EndsWith("Controller", true, null))
        //    {
        //        throw new ArgumentException("The name of controller must end with controller!");
        //    }

        //    controller = System.Text.RegularExpressions.Regex.Replace(controller, "Controller", string.Empty);
        //    return string.Format(FULLURL_FORMAT, ApplicationSettingsManager.Instance.GlobalApplicationSettings.PortalWebHostUrl, controller, method);
        //}

        //public static string GetTradingFullMVCUrl(string controller, string method)
        //{
        //    if (!controller.EndsWith("Controller", true, null))
        //    {
        //        throw new ArgumentException("The name of controller must end with controller!");
        //    }

        //    controller = System.Text.RegularExpressions.Regex.Replace(controller, "Controller", string.Empty);
        //    return string.Format(FULLURL_FORMAT, ApplicationSettingsManager.Instance.GlobalApplicationSettings.HermesWebHostUrl, controller, method);
        //}


	}
}