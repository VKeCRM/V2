using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	[ParseChildren(false)]
	[PersistChildren(true)]
	[ToolboxData("<{0}:DataGridFooter runat=server></{0}:DataGridFooter>")]
	public class DataGridFooter : WebControl
	{
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}


		public int ColSpan
		{
			get
			{
				return ViewState["ColSpan"] == null ? 0 : int.Parse(ViewState["ColSpan"].ToString());
			}
			set
			{
				ViewState["ColSpan"] = value;
			}
		}

		public string FootClass
		{
			get
			{
				return ViewState["UserFootClass"] == null ? null : ViewState["UserFootClass"].ToString();
			}
			set
			{
				ViewState["UserFootClass"] = value;
			}
		}

		public string ToHtml()
		{
			System.IO.StringWriter writer = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(writer);

			this.Render(htmlTextWriter);
			string result = htmlTextWriter.InnerWriter.ToString();

			htmlTextWriter.Close();

			string userDefineFoot = @"<tr {2}>
											<td colspan=""{0}"">
												{1}
											</td>
										</tr>";


			return string.Format(userDefineFoot, ColSpan, result, FootClass == string.Empty ? string.Empty : string.Format( @"class=""{0}""", this.FootClass));
		}

	}
}
