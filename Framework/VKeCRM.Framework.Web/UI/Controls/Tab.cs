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
	[DefaultProperty("Text")]
	[ToolboxItem(false)]
	[XmlRoot("Tab")]
	public class Tab : WebControl
	{
		public Tab() : base(HtmlTextWriterTag.Li) { }

		[Description("The text of the tab")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text { get; set; }

		[Description("Gets or sets the ID of the PageView in a RadMultiPage that will be switched when this Tab is pressed.")]
		[Bindable(true)]
		[Category("Setup")]
		[DefaultValue("")]
		public string PageViewID { get; set; }

		[Browsable(false)]
		internal protected string AnchorCssClass { get; set; }

		[Browsable(false)]
		internal protected string ClientTabClick { get; set; }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			System.Web.UI.HtmlControls.HtmlAnchor anchor = new System.Web.UI.HtmlControls.HtmlAnchor();
			anchor.InnerHtml = InnerHTML;
			anchor.HRef = "javascript:void(0);";
			anchor.Attributes.Add("class", AnchorCssClass);

			string clickEvent = !string.IsNullOrEmpty(Attributes["onclick"]) ? Attributes["onclick"] : ClientTabClick;
			if(!string.IsNullOrEmpty(clickEvent))
				anchor.Attributes.Add("onclick", clickEvent);

			Attributes.Remove("onclick");

			Controls.Add(anchor);
		}

		private string InnerHTML
		{
			get { return string.Format("<span class=\"rtsOut\"><span class=\"rtsIn\"><span class=\"rtsTxt\">{0}</span></span></span>", Text); }
		}
	}
}
