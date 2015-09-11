using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	[ParseChildren(true, "Text")]
	[ToolboxData("<{0}:DataGridHyperLinkItem runat=server></{0}:DataGridHyperLinkItem>")]
	public class DataGridHyperLinkItem : DataGridItemBase
	{
		#region Properties for link control
		public string NavigateUrl
		{
			get
			{
				string s = (string)ViewState["NavigateUrl "];
				return s ?? String.Empty;
			}
			set
			{
				ViewState["NavigateUrl "] = value;
			}
		}

		public string HrefClass
		{
			get
			{
				string s = (string)ViewState["HrefClass"];
				return s ?? String.Empty;
			}
			set
			{
				ViewState["HrefClass"] = value;
			}
		}

		[DefaultValue("")]
		[Localizable(true)]
		public string DataField
		{
			get
			{
				String s = (String)ViewState["DataField"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["DataField"] = value;
			}
		}

		[DefaultValue("")]
		[Localizable(true)]
		public string DataText
		{
			get
			{
				String s = (String)ViewState["DataText"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["DataText"] = value;
			}
		}

		[DefaultValue("_self")]
		[Localizable(true)]
		public string Target
		{
			get
			{
				String s = (String)ViewState["Target"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["Target"] = value;
			}
		}
		#endregion

		protected override void RenderContents(HtmlTextWriter output)
		{
			System.Web.UI.HtmlControls.HtmlAnchor link = new HtmlAnchor();
			link.HRef = NavigateUrl;
			link.Target = Target;


			if (!string.IsNullOrEmpty(DataField))
			{
				link.InnerText = string.Concat("{", DataField, "}");
			}
			else if (!string.IsNullOrEmpty(DataText))
			{
				link.InnerText = DataText;
			}

			if (!string.IsNullOrEmpty(HrefClass))
			{
				link.Attributes.Add("class", HrefClass);
			}

			link.RenderControl(output);
		}

	}
}
