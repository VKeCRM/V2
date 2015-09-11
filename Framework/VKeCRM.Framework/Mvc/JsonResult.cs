using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

using VKeCRM.Common.Collections;
using Newtonsoft.Json.Converters;

namespace VKeCRM.Framework.Mvc
{
	/// <summary>
	/// The class used to be a result for Controller
	/// Because we have to include many other useful informations.
	/// </summary>
	public class JsonResult : ActionResult
	{
		#region For exception
		/// <summary>
		/// This is the int value of enum: MvcErrorType
		/// </summary>
		public int Error
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}
		#endregion

		public object DataSource
		{
			get;
			set;
		}

		private DateTime _timeStamp = DateTime.UtcNow;
		public DateTime TimeStamp
		{
			get
			{
				return _timeStamp;
			}
		}

		#region contructor
		public JsonResult(object dataSource)
		{
			DataSource = dataSource;
			Error = (int)MvcErrorType.None;

		}

		public JsonResult(MvcErrorType error, string errorMessage)
		{
			Error = (int) error;
			ErrorMessage = errorMessage;
		}

		public JsonResult()
		{
			Error = (int)MvcErrorType.None;
		}

		#endregion

		/// <summary>
		/// Execute as the jsonresult.
		/// </summary>
		/// <param name="context"></param>
		public override void ExecuteResult(ControllerContext context)
		{
			if(DataSource == null)
			{
				DataSource = new System.Collections.ArrayList();
			}

			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
			jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            //jsonSerializerSettings.DateFormatString = "s"; //eg.2005-11-05T14:06:25
			jsonSerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
		   
			context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.Write(JsonConvert.SerializeObject(this, Formatting.None, jsonSerializerSettings));
		}
	}
}
