using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	[ParseChildren(true, "Text")]
	[ToolboxData("<{0}:DataGridDataItem runat=server></{0}:DataGridDataItem>")]
	public class DataGridDataItem : DataGridItemBase
	{

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

		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(string.Concat("{", DataField, "}"));
		}

	}
}
