using System;
using System.Web.Mvc;

namespace VKeCRM.Framework.Mvc
{
	interface IErrorHanlder
	{
		ActionResult HandleError(Exception ex);
	}
}
