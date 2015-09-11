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

	public class DataGridItemBase : WebControl
	{
		#region Common properties for all dataitem
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				String s = (String)ViewState["Text"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["Text"] = value;
			}
		}

		public virtual string HeaderText
		{
			get
			{
				string s = (string)ViewState["Header"];
				return s ?? String.Empty;
			}
			set
			{
				ViewState["Header"] = value;
			}
		}

		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Td;
			}
		}

		private bool _isRednderDefaultTDTag = true;
		public bool IsRenderDefaultTDTag
		{
			get
			{
				return _isRednderDefaultTDTag;
			}
			set
			{
				_isRednderDefaultTDTag = value;
			}
		}

		#endregion

		public string ToHtml()
		{
			System.IO.StringWriter writer = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(writer);
	
			this.Render(htmlTextWriter);
			string result = htmlTextWriter.InnerWriter.ToString();

			//diable the container tag "td", maybe user want to define the container by themself in aspx page..
			if (!IsRenderDefaultTDTag)
			{
				result = result.Remove(0, 4);
				result = result.Remove(result.Count() - 5, 5);
			}
			htmlTextWriter.Close();

			return result;
		}

	}
}
