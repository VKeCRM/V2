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
	[ToolboxData(@"<{0}:MultiPage Runat=""server""><{0}:PageView runat=""server"" id=""PageView1"">PageView</{0}:PageView></{0}:MultiPage>")]
	[DefaultProperty("PageViews")]
	[PersistChildren(false)]
	[ParseChildren(typeof(PageView))]
	public class MultiPage : WebControl
	{
		public MultiPage() : base(HtmlTextWriterTag.Div) { PageViews = new List<PageView>(); }
		
		[Browsable(false)]
		public int SelectedIndex { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public List<PageView> PageViews { get; private set; }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			PageViews.Clear();

			// TODO: there should be a good way to add targets
			foreach (Control ctrl in this.Controls)
			{
				if (ctrl is PageView)
					PageViews.Add((PageView)ctrl);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			for (int i = 0; i < PageViews.Count; i++)
			{
				if (SelectedIndex == i)
					PageViews[i].Attributes.Add("style", "display: block");
				else
					PageViews[i].Attributes.Add("style", "display: none");
			}
		}
	}
}
