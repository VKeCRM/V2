using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Diagnostics;
using System.Web.Mvc;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using VKeCRM.Framework.Mvc;


namespace VKeCRM.Framework.Web.UI.Controls.Json
{

	public class FirstLoadControlCombiner : ControlBase
	{
		public override string GeneratedControlId
		{
			get 
			{
				return this.ClientID;
			}
		}

		/// <summary>
		/// Add the control which you want to combine when first load.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="controllerType"></param>
		public void AddControl(ControlBase control, Type controllerType)
		{
			if ((!_controls.ContainsKey(control.ID)) && control.isAutoLoad)
			{
				
				control.isAutoLoad = false;
				if (_controls.Any(item => item.Value.Key.DataStoreID == control.DataStoreID ) == false)
				{// We add it only if the same datasotre doesn't exist.
					_controls.Add(control.ClientID,
					              new KeyValuePair<ControlBase, string>(control, controllerType.AssemblyQualifiedName));
				}
			}
		}

		/// <summary>
		/// Override the base method do disable script register which we delay to prerender event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			//HttpContext.Current.Response.Cookies.Add(new HttpCookie("VKeCRMToken", Guid.NewGuid().ToString()));		
			//CsrfToken.SetNewCsrfCookie(HttpContext.Current.Response.Cookies);
		}

		protected override void  OnPreRender(EventArgs e)
		{
			if (this._controls.Count != 0)
			{
				ClientHelper.RegisterBlockPage(this);

				RegisterDataStore();

				//Register customer's prerender event
				if (!string.IsNullOrEmpty(this.ClientPreRenderHanlderFuncName))
				{
					ClientHelper.RegisterPreRenderScript(this);
				}
				IsShowLoadingImage = false;
				RegisterBeforeLoadEventHanlder();
				RegisterOnLoadEventHanlder();
			}
		}

		#region Register script method
		private void RegisterDataStore()
		{
			DataStoreExt dataStoreExt = new DataStoreExt();

			foreach (var item in _controls)
			{
				string rootKey = string.Format("{0}${1}${2}", item.Key, item.Value.Key.Controller,
				                               item.Value.Key.ControllerGetMethod);

				dataStoreExt.BaseParams.Add(rootKey, string.Format("'{0}${1}'", item.Value.Value, item.Value.Key.DataStoreID));

				foreach (var subParamter in item.Value.Key.JsonDataStore.BaseParams)
				{
					dataStoreExt.BaseParams.Add(rootKey + "$" + subParamter.Key, subParamter.Value);
				}
			}

			dataStoreExt.ControlName = DataStoreName;
			dataStoreExt.Fields = new List<string>();
			dataStoreExt.HttpUrl = MvcUrlHelper.GetDynamicMethodsUrl(Controller);
			dataStoreExt.HttpMethod = "POST";

			dataStoreExt.DataStoreConfig.Add("autoLoad", "true");
			dataStoreExt.DataStoreConfig.Add("storeId", "'" + DataStoreID + "'");

			Page.ClientScript.RegisterStartupScript(Page.GetType(), ClientID + "JsonStore", dataStoreExt.ToString(), true);
		}

		protected override void RegisterOnLoadEventHanlder()
		{
			ClientHelper.RegisterControlCombinerLoadEventHanlder(this);
		}
		#endregion

		private Dictionary<string, KeyValuePair<ControlBase, string>> _controls = new Dictionary<string, KeyValuePair<ControlBase, string>>();

	}
}
