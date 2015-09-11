using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Web.UI;

namespace VKeCRM.Framework.Web.UI.Controls
{
	[XmlRoot("TabStrip")]
	[DefaultProperty("Tabs")]
	[ToolboxData("<{0}:TabStrip runat=server></{0}:TabStrip>")]
	public class TabStrip : WebControl
	{
		public TabStrip() : base(HtmlTextWriterTag.Div) { Tabs = new List<Tab>(); }

		[Description("The index of the selected child tab")]
		[DefaultValue(0)]
		[Category("Behavior")]
		public int SelectedIndex { get; set; }

		[DefaultValue("")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<Tab> Tabs { get; private set; }

		[Description("Gets or sets the ID of the RadMultiPage control that will be controlled by this RadTabStrip.")]
		[DefaultValue("")]
		public string MultiPageID { get; set; }

		[Browsable(false)]
		public MultiPage MultiPage { get; private set; }

		[DefaultValue("")]
		public string OnClicntTabClicked { get; set; }

		private bool _loadJsResourse = true;
		public bool LoadJsResourse 
		{
			get { return _loadJsResourse; }
			set { _loadJsResourse = value; }
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			// set MultiPage
			Control ctrl = this.Parent.FindControl(MultiPageID);
			if (null == ctrl || !(ctrl is MultiPage))
				throw new Exception(string.Format("Cannot find MultiPage: \"{0}\"", MultiPageID));
			else
				MultiPage = (MultiPage)ctrl;

			// this setting must after page_load, for customer will set it in code
			MultiPage.SelectedIndex = SelectedIndex;

			// this setting must before OnPreRender
			// child will render before parent
			for (int i = 0; i < Tabs.Count; i++)
			{
				if (SelectedIndex == i)
					Tabs[i].AnchorCssClass = "rtsLink rtsSelected";
				else
					Tabs[i].AnchorCssClass = "rtsLink";

				if (!string.IsNullOrEmpty(OnClicntTabClicked))
					Tabs[i].ClientTabClick = string.Format("{0}('{1}', '{2}');", OnClicntTabClicked, i, Tabs[i].Text);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			WebControl firstNode = new WebControl(HtmlTextWriterTag.Div);
			firstNode.CssClass = "rtsLevel rtsLevel1";

			WebControl ul = new WebControl(HtmlTextWriterTag.Ul);
			ul.CssClass = "rtsUL";

			string liCssClass = "rtsLI";
			for (int i = 0; i < Tabs.Count; i++)
			{
				if (Tabs.Count == 1)
					Tabs[i].CssClass = liCssClass;
				else
				{
					if (i == 0)
						Tabs[i].CssClass = string.Concat(liCssClass, " ", "rtsFirst");
					else if (i == Tabs.Count - 1)
						Tabs[i].CssClass = string.Concat(liCssClass, " ", "rtsLast");
					else
						Tabs[i].CssClass = liCssClass;
				}

				ul.Controls.Add(Tabs[i]);
			}

			firstNode.Controls.Add(ul);
			Controls.Add(firstNode);

			RegidterClientScript();
		}

		private void RegidterClientScript()
		{
			if (LoadJsResourse)
			{
				string resourceUrl = WebResourceManager.GetScriptResourceUrl("TabStrip.js");
				Page.ClientScript.RegisterClientScriptInclude("VKeCRMTabStrip", resourceUrl);
			}

			string opts = string.Format("{{ 'currentTabIndex': {0} }}", SelectedIndex);

			string registerCommand = @"<script type='text/javascript'>
				if(typeof (VKeCRMSys) == 'undefined') {
					alert('MvcValidator needs VKeCRMScriptManager control, use it like: <VKeCRM:VKeCRMScriptManager runat=server></VKeCRM:VKeCRMScriptManager>');
				}
				else {
					VKeCRMSys.getInstance().add_tabStrip('" + ClientID + @"', '" + MultiPage.ClientID + @"', " + opts + @");
				}
			</script>";

			Page.ClientScript.RegisterStartupScript(this.GetType(), ClientID, registerCommand);
		}
	}
}
