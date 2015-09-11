using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VKeCRM.Portal.DataTransferObjects;
using VKeCRM.Portal.Web.PortalServices;
using Newtonsoft.Json;
using WebGrease.Css.Extensions;

namespace VKeCRM.Portal.Web.MasterPages
{
	public partial class Main : System.Web.UI.MasterPage
	{
		public string ServerId
		{
			get
			{
                return Server.MachineName;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SetUserInfo();
			}
		}

	    private void SetUserInfo()
	    {
	        const string js = @"
                page.global.UserID = '{0}';
            ";
	        Page.ClientScript.RegisterStartupScript(this.GetType(), "JsonPageSetUserInfo",
	            string.Format(js,
	                "MyTestID"),
	            true);
	    }
	}
}