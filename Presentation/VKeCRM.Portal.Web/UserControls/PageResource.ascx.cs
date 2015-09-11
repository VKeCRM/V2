using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VKeCRM.Portal.Web.UserControls
{
    public partial class PageResource : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			JsVersion = ConfigurationManager.AppSettings["jsVersion"];
			JsCompress = ConfigurationManager.AppSettings["jsCompress"] == "true";
        }

		public string JsVersion { get; private set; }
		public bool JsCompress { get; private set; }
    }
}