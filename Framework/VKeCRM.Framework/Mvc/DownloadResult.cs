using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;

namespace VKeCRM.Framework.Mvc
{
	public class DownloadResult : ActionResult
	{
		public ContentType ContentType
		{
			get;
			set;
		}
		public string Content
		{
			get;
			set;
		}

		public DownloadResult(string content, ContentType type)
		{
			Content = content;
			ContentType = type;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			string contentType =
				((DescriptionAttribute) (ContentType.GetType().GetMember(ContentType.ToString())[0].GetCustomAttributes(false)[0])).
					Description;

			context.HttpContext.Response.AddHeader("Content-Disposition",
				   string.Format("{0}; filename=\"{1}.{2}\"",
				   context.HttpContext.Request.Browser.MajorVersion == 6 ? "attachment" : "inline",
				   Guid.NewGuid(), ContentType.ToString()));
			context.HttpContext.Response.ContentType = contentType;
			context.HttpContext.Response.Write(Content);
		}
	}
}
