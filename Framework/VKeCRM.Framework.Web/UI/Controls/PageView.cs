using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

namespace VKeCRM.Framework.Web.UI.Controls
{
	[ToolboxData(@"<{0}:PageView Runat=""server"" Width=""100%"">PageView</{0}:PageView>")]
	[ParseChildren(false)]
	[PersistChildren(true)]
	public class PageView : WebControl
	{
		public PageView() : base(HtmlTextWriterTag.Div) { }

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			Attributes.Add("class", string.IsNullOrEmpty(Attributes["class"]) ? "rtvDIV" : string.Concat(Attributes["class"], " ", "rtvDIV"));
		}
	}
}
