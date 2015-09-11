using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace VKeCRM.Framework.Web.UI.Controls
{
	public class VKeCRMScriptManager : Control
	{
		private bool _loadJsResourse = true;
		public bool LoadJsResourse
		{
			get { return _loadJsResourse; }
			set { _loadJsResourse = value; }
		}

		protected override void OnInit(EventArgs e)
		{
			if (LoadJsResourse)
			{
				string resourceUrl = WebResourceManager.GetScriptResourceUrl("VKeCRMScriptManager.js");
				Page.ClientScript.RegisterClientScriptInclude("VKeCRMScriptManager", resourceUrl);
			}

			// create VKeCRMSys instance and etc.
			Page.ClientScript.RegisterStartupScript(this.GetType(), "VKeCRMSys.initialize", "VKeCRMSys.initialize();", true);

			base.OnInit(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			Page.ClientScript.RegisterStartupScript(this.GetType(), "VKeCRMSys.page_load", "VKeCRMSys.getInstance().page_load();", true);

			base.Render(writer);
		}
	}
}
