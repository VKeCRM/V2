using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Diagnostics;
using VKeCRM.Framework.Mvc;
using System.Text.RegularExpressions;
using System.ComponentModel;


namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public abstract class ControlBase : WebControl
	{


		#region Override page properties and methods
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		protected override void OnLoad(EventArgs e)
		{

			base.OnLoad(e);

			//HttpContext.Current.Response.Cookies.Add(new HttpCookie("VKeCRMToken", Guid.NewGuid().ToString()));
			//CsrfToken.SetNewCsrfCookie(HttpContext.Current.Response.Cookies);


			if (IsUseSelfDataStore)
			{
				//Register jsonstore script for loading store in pageload phase.
				RegisterJsonStoreScript();

				//Register customer's prerender event
				if (!string.IsNullOrEmpty(this.ClientPreRenderHanlderFuncName))
				{
					ClientHelper.RegisterPreRenderScript(this);
				}
			}

			RegisterBeforeLoadEventHanlder();
			RegisterOnLoadEventHanlder();

			if (!string.IsNullOrEmpty(this.ClientAfterRenderHanlderFuncName))
			{
				ClientHelper.RegisterAfterRenderEventScript(this);
			}


			if (isLoadDataStore)
			{
				string loadFunc =
					string.Format(@" {0}.reload();", this.DataStoreName);
				Page.ClientScript.RegisterStartupScript(Page.GetType(), ClientID + "JsonStore_StartLoad",
													loadFunc, true);
			}
			



			

		}

		#endregion

		#region DataStore config and related method, properties
		//String which are used to be a key for accessing ViewState.
		private const string MVC_CONTROLLER = "MVCController";
		private const string MVC_METHOD = "MVCMethod";
		private const string HTTP_METHOD = "HttpMethod";
		private const string REQUEST_PARAMTER = "RequestParamters";
		private const string AUTO_LOAD = "AutoLoad";
		protected const string FOR = "For";
		
		private readonly object _myLock = new object();
		protected DataStoreExt _jsonDataStore = new DataStoreExt();

		private bool _isUseSelfDataStore = true;

		public bool IsUseSelfDataStore
		{ 
			get
			{
				return _isUseSelfDataStore;
			}
		}

		public DataStoreExt JsonDataStore
		{
			get
			{
				return _jsonDataStore;
			}
		}

		public virtual bool isAutoLoad
		{
			get
			{
				return GetItemFromViewState(AUTO_LOAD, true);
			}
			set
			{
				ViewState[AUTO_LOAD] = value;
			}
		}

		public virtual string DataRoot
		{
			get
			{
				return GetItemFromViewState(FOR, ".");
			}
			set
			{
				ViewState[FOR] = value;
			}
		}

		private string _dataStoreName;
		public string DataStoreName
		{
			get
			{

				return string.IsNullOrEmpty(_dataStoreName) ? ClientHelper.GetClientDataStoreObjName(ClientID) : _dataStoreName;
			}
			set
			{
				_dataStoreName = value;
			}
		}

		private string _dataStoreID;
		public string DataStoreID
		{
			get
			{
				return string.IsNullOrEmpty(_dataStoreID) ? ClientHelper.GetClientDataStoreID(ClientID) : _dataStoreID;
			}
			set
			{
				_dataStoreID = value;
			}
		}

		public string HttpMethod
		{
			get
			{//Default value is post
				return _jsonDataStore.HttpMethod;
			}
			set
			{
				if (value.ToLower() != "get" && value.ToLower() != "post")
				{
					throw new ArgumentException("HttpMethod string must be get or post");
				}
				_jsonDataStore.HttpMethod = value;
			}
		}

		/// <summary>
		/// Use to store the paramter values used to go with dataurl.
		/// </summary>
		private Dictionary<string, string> RequestParamtersCollection
		{
			get
			{
				return _jsonDataStore.BaseParams;
			}
		}

	
		#endregion

		#region MVC properties

		/// <summary>
		/// For ASP.NET mvc 1.0 
		/// </summary>
		public string Controller
		{
			get
			{
				return GetItemFromViewState(MVC_CONTROLLER, string.Empty);
			}
			set
			{
				ViewState[MVC_CONTROLLER] = value;
			}
		}

		/// <summary>
		/// For ASP.NET mvc 1.0 
		/// </summary>
		public string ControllerGetMethod
		{
			get
			{
				return GetItemFromViewState(MVC_METHOD, string.Empty);
			}
			set
			{
				ViewState[MVC_METHOD] = value;
			}
		}
		#endregion
		
		#region Helper methods for handling datastore
		public bool isLoadDataStore = false;
		protected string GetJasonStoreConfigurationJS()
		{
			//Check the paramters to make sure that the method of controller have the same paramter.
			//Which can make our developers to discover the error as soon as possible.
			//ValideMethodParamters();

			//#Init dataStoreConfig
			_jsonDataStore.ControlName = DataStoreName;
			_jsonDataStore.BaseParams = this.RequestParamtersCollection;
			_jsonDataStore.Fields = new List<string>();

			_jsonDataStore.DataStoreConfig.Add("autoLoad", "false");
			if (string.IsNullOrEmpty(Controller) || string.IsNullOrEmpty(ControllerGetMethod))
			{//User didn't set the VKeCRM url, maybe will use the external url when running.
				_jsonDataStore.HttpUrl = string.Empty;
				isLoadDataStore = false;
				//_jsonDataStore.DataStoreConfig.Add("autoLoad", "false");
			}
			else
			{
				_jsonDataStore.HttpUrl = MvcUrlHelper.GetMVCUrl(Controller, ControllerGetMethod);
				isLoadDataStore = isAutoLoad;
				//_jsonDataStore.DataStoreConfig.Add("autoLoad", isAutoLoad.ToString().ToLower());
			}
			
			_jsonDataStore.HttpMethod = HttpMethod;
			_jsonDataStore.DataStoreConfig.Add("storeId", "'"+DataStoreID+"'");

			return _jsonDataStore.ToString();
		}

		private void ValideMethodParamters()
		{
			if (string.IsNullOrEmpty(this.Controller))
			{//We expect a controller for data binding
				throw new ArgumentException("Controller can't be null");
			}

			MethodInfo methodInfo = Assembly.GetCallingAssembly().GetType(this.Controller).GetMethod(this.ControllerGetMethod);
			if (methodInfo == null)
			{//We expect a valid method from controller for data binding
				throw new ArgumentException(
					string.Format("Can't find method named '{0}' from '{1}' class",
								  this.ControllerGetMethod,
								  this.Controller.GetType().Name));
			}

			ParameterInfo[] methodParamters = methodInfo.GetParameters();
			foreach (var paramter in RequestParamtersCollection)
			{
				if (!methodParamters.ToList().Exists(item => item.Name == paramter.Key))
				{
					throw new ArgumentException(
						string.Format("The paramter name:{0} for request doesn't equal to any method paramter in '{1}'.",
									  paramter.Key, methodInfo.Name));
				}
			}
		}

		protected void RegisterJsonStoreScript()
		{
			//Register script for jsonstore from JsonDataStore type
			Page.ClientScript.RegisterStartupScript(Page.GetType(), ClientID + "JsonStore", GetJasonStoreConfigurationJS(), true);
		}

		public void UseSameDataStoreAs(ControlBase controlBase)
		{
			_isUseSelfDataStore = false;
			DataStoreID = controlBase.DataStoreID;
			DataStoreName = controlBase.DataStoreName;
			_jsonDataStore = controlBase.JsonDataStore;

			//This is not use to init datastore now, we just assign them to controller to help use coding.
			Controller = controlBase.Controller;
			ControllerGetMethod = controlBase.ControllerGetMethod;
		
		}

		//If the paramter is string type, we should add "'" for javascript.
		public void AddParamter(string key, string value)
		{
			Regex regex = new Regex("'", RegexOptions.None);
			value = regex.Replace(value, "\\'");

			RequestParamtersCollection.Add(key, string.Format("'{0}'", value));
		}
		public void AddParamter(string key, int value)
		{
			RequestParamtersCollection.Add(key, value.ToString());
		}
		#endregion

		#region full url as the datasource
		private const string DATA_URL = "DataUrl";
		/// <summary>
		/// If we use mvc, and this doesn't work
		/// </summary>
		public string DataUrl
		{
			get
			{
				return GetItemFromViewState(DATA_URL, string.Empty);
			}
			set
			{
				ViewState[DATA_URL] = value;
			}
		}

		#endregion

		#region HelperMethod for get/set properties
		protected int GetItemFromViewState(string key, int defaultValue)
		{
			int result = defaultValue;

			if (ViewState[key] != null)
			{
				try
				{
					result = (int)ViewState[key];
				}
				catch
				{

				}
			}
			return result;
		}

		protected bool GetItemFromViewState(string key, bool defaultValue)
		{
			bool result = defaultValue;

			if (ViewState[key] != null)
			{
				try
				{
					result = (bool)ViewState[key];
				}
				catch
				{

				}
			}

			return result;
		}

		protected string GetItemFromViewState(string key, string defaultValue)
		{
			string result = defaultValue;

			if (ViewState[key] != null)
			{
				try
				{
					result = (string)ViewState[key];
				}
				catch
				{

				}
			}

			return result;
		}
		#endregion

        #region Client Listener and event.

		/// <summary>
		/// The function accepte the Error(int) and ErrorMessage(string) 
		/// </summary>
		public string ClientErrorHandlerFuncName = "null";
		public string ClientErrorHandlerStyleClass = "message-error";
		public ErrorDisplayType ClientErrorHandlerStrategy = ErrorDisplayType.Override;

		private string _clientErrorHanlderTargetClientID;
		public string ClientErrorHandlerTargetClientID
		{
			get
			{
				if (string.IsNullOrEmpty(_clientErrorHanlderTargetClientID))
				{//Default value is itself
					_clientErrorHanlderTargetClientID = this.ClientID;
				}
				return _clientErrorHanlderTargetClientID;
			}
			set
			{
				_clientErrorHanlderTargetClientID = value;
			}
		}
	

		/// <summary>
		/// The function accepte the response json object.
		/// </summary>
		public string ClientPreRenderHanlderFuncName
		{ 
			get; set;
		}

		/// <summary>
		/// The function accepte the response json object.
		/// </summary>
		public string ClientAfterRenderHanlderFuncName
		{ 
			get; set;

		}

		protected virtual void RegisterBeforeLoadEventHanlder()
		{
			RegisterShowLoadingImageScript();
			RegisterCSRFClientDefense();
		}

		protected virtual void RegisterOnLoadEventHanlder()
		{
			RegisterBaseFunctionListeners();
		}
		

		/// <summary>
		/// Register base control scripts for datastore load
		/// We have to call this manually in the child class.
		/// beclase somethimes this scripts depend on control that the template has rendered.
		/// So we must let the child first.
		/// But i will fix this in future.
		/// Hack! 
		/// </summary>
		protected void RegisterBaseFunctionListeners()
		{
			ClientHelper.RegisterBaseOnLoadEventHanlder(this, _registerFunctionList);
		}

		/// <summary>
		/// This will be safely invoked by our control extension.
		/// So we don't adivse you to use this method direclty.
		/// </summary>
		/// <param name="sourceControlId">The source control id.</param>
		/// <param name="targetDataStoreID">The target data store ID.</param>
		/// <param name="associatedParamterName">Name of the associated paramter.</param>
		/// <param name="eventType">Type of the event.</param>
		/// <param name="isAutoReloadTarget">if set to <c>true</c> [is auto reload target].</param>
		public void AddClientControlBinding(string sourceControlId, string targetDataStoreID, 
												string associatedParamterName, EventType eventType, bool isAutoReloadTarget)
		{
			//Validate the event frist , to make sure our sepcfici control support that event type.
			//ValidateEvent(eventType);

			string regisetOnChangeEventHanlder = string.Format(ClientHelper.FuncRegisterListener,
														sourceControlId, targetDataStoreID,
														associatedParamterName, isAutoReloadTarget.ToString().ToLower(),
														eventType.ToString().ToLower(), sourceControlId);

			_registerFunctionList += regisetOnChangeEventHanlder;
		}


		/// <summary>
		/// This will be safely invoked by our control extension.
		/// So we don't adivse you to use this method direclty.
		/// </summary>
		/// <param name="sourceControlId">The source control id.</param>
		/// <param name="targetDataStoreID">The target data store ID.</param>
		/// <param name="associatedParamterName">Name of the associated paramter.</param>
		/// <param name="containValueControlId">The contain value control id.</param>
		/// <param name="eventType">Type of the event.</param>
		/// <param name="isAutoReloadTarget">if set to <c>true</c> [is auto reload target].</param>
		public void AddClientControlBinding(string sourceControlId, string targetDataStoreID,
												string associatedParamterName, string containValueControlId, EventType eventType, bool isAutoReloadTarget)
		{
			//Validate the event frist , to make sure our sepcfici control support that event type.
			//ValidateEvent(eventType);

			string regisetOnChangeEventHanlder = string.Format(ClientHelper.FuncRegisterListener,
														sourceControlId, targetDataStoreID,
														associatedParamterName, isAutoReloadTarget.ToString().ToLower(),
														eventType.ToString().ToLower(), containValueControlId);

			_registerFunctionList += regisetOnChangeEventHanlder;
		}


		/// <summary>
		/// Adds the client control binding.
		/// Just reload the control directly with none paramter modifications.
		/// </summary>
		/// <param name="sourceControlId">The source control id.</param>
		/// <param name="targetDataStoreID">The target data store ID.</param>
		/// <param name="eventType">Type of the event.</param>
		public void AddClientControlBinding(string sourceControlId, string targetDataStoreID, EventType eventType)
		{
			//Validate the event frist , to make sure our sepcfici control support that event type.
			//ValidateEvent(eventType);

			string regisetOnChangeEventHanlder = string.Format(ClientHelper.FuncRegisterOnlyReloadlistener,
														sourceControlId, targetDataStoreID,
														eventType.ToString());

			_registerFunctionList += regisetOnChangeEventHanlder;
		}

		public void AddParamter(string paramter, IInitMessager initListener)
		{
			//If we use other controls to provide a init value, it will reload datagrid.
			this.isAutoLoad = false;

			initListener.RegisterInitListener(paramter, this);

		}

		public abstract string GeneratedControlId
		{ 
			get;
		}


		private string _registerFunctionList = string.Empty;

		private string _jsVariableForFirstLoadFlag
		{
			get
			{
				return string.Format(@"{0}_dataStore_isFirstLoad", ClientID);
			}
		}

		#endregion

		#region Codes for showing loading image

		private int _timeOut = 30000;
		public int TimeOut
		{
			get
			{
				return _timeOut;
			}
			set
			{
				_timeOut = value;
			}
		}

		private const string IS_SHOW_LOADING_IMG = "IsShowLoadingImage";
		public bool IsShowLoadingImage
		{
			get
			{
				return GetItemFromViewState(IS_SHOW_LOADING_IMG, true);
			}
			set
			{
				ViewState[IS_SHOW_LOADING_IMG] = value;
			}
		}

		private const string SHOW_LOADING_AT = "ShowLoadingAt";

		[Description("If this is set, the loading icon will be shown at this control, default to show on itself. Current it doen't support server controls.")]
		public string ShowLoadingAt
		{
			get
			{
				return GetItemFromViewState(SHOW_LOADING_AT, string.Empty);
			}
			set
			{
				ViewState[SHOW_LOADING_AT] = value;
			}
		}

		private void RegisterShowLoadingImageScript()
		{
			if (IsShowLoadingImage == true)
			{
				ClientHelper.RegisterShowLoadingImageScript(this);
			}
		}

		#endregion

		#region Codes for csrf token

		private const string ENABLE_CSRF_DEFENSE = "EnableCSRFDefense";
		public bool EnableCSRFDefense
		{
			get
			{
				return GetItemFromViewState(ENABLE_CSRF_DEFENSE, true);
			}
			set
			{
				ViewState[ENABLE_CSRF_DEFENSE] = value;
			}
		}

		private void RegisterCSRFClientDefense()
		{
			if (EnableCSRFDefense == true)
			{
				ClientHelper.RegisterCSRFClientDefense(this);
			}
		}
		#endregion

	}
}
