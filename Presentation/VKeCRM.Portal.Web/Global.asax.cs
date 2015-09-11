using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VKeCRM.Framework.Web.UI.Controls.Json;
using VKeCRM.Portal.Web.App_Start;

namespace VKeCRM.Portal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.Register(BundleTable.Bundles);
            MvcUrlHelper.DefaultMapRoutes();
        }
    }
}
