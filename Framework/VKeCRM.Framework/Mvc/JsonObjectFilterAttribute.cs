using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Web.Security;
using VKeCRM.Framework.Mvc.Exceptions;

namespace VKeCRM.Framework.Mvc
{

	public class JsonObjectFilterAttribute : ActionFilterAttribute
	{
		private Type RootType { get; set; }
		private string ParamterName { get; set; }

		public string ObjectKeyInRequest = "Object";

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			//We assume the action accept only one object need to deserailize from json string.
			RootType = filterContext.ActionDescriptor.GetParameters()[0].ParameterType;
			ParamterName = filterContext.ActionDescriptor.GetParameters()[0].ParameterName;

			string jsonString;

			//Get json string from request
			if(HttpContext.Current.Request.ServerVariables["Request_Method"].Equals("POST"))
			{
				jsonString = HttpContext.Current.Request.Form[ObjectKeyInRequest];
			}
			else
			{
				jsonString = HttpContext.Current.Request.QueryString[ObjectKeyInRequest];
			}

			if (string.IsNullOrEmpty(jsonString))
			{
				return;
			}

			using(var mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
			{
				try
				{
					//Get the object from json string
					var searilizer = new DataContractJsonSerializer(RootType);
					object convertedObj = searilizer.ReadObject(mStream);

					//Attach the object to action paramter
					filterContext.ActionParameters[ParamterName] = convertedObj;
				}
				catch(Exception ex)
				{
					throw new JsonDeserializeError("Json serializer error.", ex);
				}
			}
		}
	}
}
