using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public class ClientHelper
	{
		public static readonly string FuncRegisterListener = @"registerlistener('{0}', '{1}', '{2}', {3}, '{4}', '{5}');";
		public static readonly string FuncRegisterPreRender = @"preRenderDataStore({0}, {1});";
		public static readonly string FuncRegisterAfterRender = @"afterRenderDataStore({0}, {1});";
		public static readonly string FuncRegisterOnlyReloadlistener = @"registerOnlyReloadlistener('{0}', '{1}', '{2});";
		public static readonly string FuncDataBeforeLoad = @"VKeCRMdatabeforeload('{0}', {1}, '{2}');";
		public static readonly string FuncRegisterInitListener = @"regitserinitlistener('{0}', {1}, '{2}');";
		public static readonly string FuncRegisterComboboxLoad = @"VKeCRMcomboboxload({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}', {9}, '{10}', '{11}');";
		public static readonly string FuncRegisterDataGridLoad = @"VKeCRMdataload({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', {12}, '{13}', '{14}', {15}, {16}, {17}, '{18}', {19}, '{20}', {21}, {22}, '{23}', '{24}');";
		public static readonly string FuncRegisterRepeaterLoad = @"VKeCRMdataload({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', {12}, '{13}', '{14}', {15}, {16}, {17}, '{18}', {19}, '{20}', {21}, {22}, '{23}', '{24}');";
		public static readonly string FuncRegisterControlsCombiner = @"controlscombinerload({0})";
		public static readonly string FuncCSRFHelper = @"VKeCRMcsrfhelper('{0}');";

		public static readonly string CssRowClass = @"GridRow_Portal";
		public static readonly string CssAltRowClass = @"GridAltRow_Portal";
		public static readonly string CssLeftPagerClass = @"PagerLeft_Default";
		public static readonly string CssRightPagerClass = @"PagerRight_Default";
		public static readonly string CssPagerClass = @"GridPager_Default";

		public static string GetClientDataStoreID(string controlClientId)
		{
			return string.Concat(controlClientId, "_JsonStoreID");
		} 
			

		public static string GetClientDataStoreObjName(string controlClientId)
		{
			return string.Concat(controlClientId, "_JsonStore");
		}


		public static void RegisterPreRenderScript(ControlBase control)
		{
			string invokePreRenderFunc = string.Format(ClientHelper.FuncRegisterPreRender, control.DataStoreName, control.ClientPreRenderHanlderFuncName);
			
			string registerPreRenderFunction = string.Format(@"{0}.on('load', function() {{ {1} }});", control.DataStoreName,
																invokePreRenderFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_PreRenderFunction",
													registerPreRenderFunction, true);
		}

		public static void RegisterAfterRenderEventScript(ControlBase control)
		{
			string invokeAfterRenderFunc = string.Format(ClientHelper.FuncRegisterAfterRender, control.DataStoreName, control.ClientAfterRenderHanlderFuncName);
			string registerAfterRenderFunction = string.Format(@" {0}.on('load', function() {{ {1} }});", control.DataStoreName,
																invokeAfterRenderFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_AfterRenderFunction",
													registerAfterRenderFunction, true)  ;
		}

		public static void RegisterShowLoadingImageScript(ControlBase control)
		{
			string invokeVKeCRMOnBeforeloadFunc = string.Format(ClientHelper.FuncDataBeforeLoad, control.ClientID, control.TimeOut, control.ShowLoadingAt);
			string registerOnBeforeLoadFunction = string.Format(@"{0}.on('beforeload', function() {{ {1} }});", control.DataStoreName,
																invokeVKeCRMOnBeforeloadFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_OnBeforeloadFunction_ShowWating",
													registerOnBeforeLoadFunction, true);
		}

		public static void RegisterCSRFClientDefense(ControlBase control)
		{
			string invokeVKeCRMOnBeforeloadFunc = string.Format(ClientHelper.FuncCSRFHelper, control.DataStoreID);
			string registerOnBeforeLoadFunction = string.Format(@" {0}.on('beforeload', function() {{ {1} }});", control.DataStoreName,
																invokeVKeCRMOnBeforeloadFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_OnBeforeloadFunction_CSRF",
													registerOnBeforeLoadFunction, true);
		}

		public static void RegisterBaseOnLoadEventHanlder(ControlBase control, string functionListNeedToResiter)
		{
			//Register the onload event handlers
			string registerOnLoadFunction = string.Format(@"{0}.on('load', function() {{ {1} }});", control.DataStoreName, functionListNeedToResiter);
			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_BaseOnloadFunction", registerOnLoadFunction, true);
		}

		public static void RegisterComboBoxOnLoadEventHanlder(ComboBox control)
		{
			string invokeVKeCRMOnloadFunc = string.Format(ClientHelper.FuncRegisterComboboxLoad,
					  control.DataStoreName, control.ClientID,
					  control.GeneratedControlId, control.JsonControlCssClass,
					  control.DataKey, control.DataValue,
					  control.DataRoot, control.ClientErrorHandlerFuncName,
					  control.ClientErrorHandlerTargetClientID, (int)control.ClientErrorHandlerStrategy,
					  control.ClientErrorHandlerStyleClass,
					  control.ShowLoadingAt
			);

			//Register the onload event handlers
			string registerOnLoadFunction = string.Format(@" {0}.on('load', function() {{ {1} }});", control.DataStoreName, invokeVKeCRMOnloadFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_OnloadFunction", registerOnLoadFunction, true);
		}
		
		public static void RegisterGeneratedControlEventHandler(ComboBox control)
		{
			string addListner = " Ext.EventManager.addListener(\"{0}\", \"{1}\", {2});";
			string registerFuncList = string.Empty;

			foreach (var eventListner in control.generatedControlEventListener)
			{
				registerFuncList += string.Format(addListner, control.GeneratedControlId, eventListner.Key.ToString().ToLower(),
				                                  eventListner.Value);
			}

			string registerEventRegisterFunction = string.Format(@" {0}.on('load', function() {{ {1} }});", control.DataStoreName,
																registerFuncList);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_GeneratedControlEventListner",
													registerEventRegisterFunction, true);
		}

		public static void RegisterRepeaterOnLoadEventHanlder(Repeater control)
		{
			Regex regex = new Regex("'", RegexOptions.None);

			string bodyHtml = control.ItemTemplate.ToHtml();
			bodyHtml = bodyHtml.Replace(System.Environment.NewLine, string.Empty).Replace("\n", string.Empty);
			bodyHtml = regex.Replace(bodyHtml, "\\'");

			string userFootHtml = string.Empty;
			if (control.Footer != null)
			{
				userFootHtml = control.Footer.ToHtml().Replace(System.Environment.NewLine, string.Empty).Replace("\n", string.Empty);
				userFootHtml = regex.Replace(userFootHtml, "\\'");
			}

			string invokeVKeCRMOnloadFunc = string.Format(ClientHelper.FuncRegisterRepeaterLoad,
												  control.DataStoreName, control.DataStoreName,
												  control.ClientID, "", control.ClientID,
												  control.TableClass, control.DataRoot,
												  control.RowCssClass, control.AlternatingRowCssClass,
												  bodyHtml, userFootHtml,
												  "GridPager_Default", control.ColSpan,
												  control.Pager.LeftPagerClass, control.Pager.RightPagerClass,
												  control.Pager.MaxPageCount, (int)control.CollectionType,
												  control.ClientErrorHandlerFuncName, control.ClientErrorHandlerTargetClientID,
												  (int)control.ClientErrorHandlerStrategy, control.ClientErrorHandlerStyleClass, control.IsDivContainer.ToString().ToLower(),
												  control.IsShowEmptyText.ToString().ToLower(), control.EmptyText, control.ShowLoadingAt);

			string registerOnLoadFunction = string.Format(@" {0}.on('load', function() {{ {1} }});", control.DataStoreName, invokeVKeCRMOnloadFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_OnloadFunction", registerOnLoadFunction, true);
		}

		public static void RegisterBlockPage(ControlBase control)
		{
			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_BlockPage", "blockpage();", true);
		}
		
		public static void RegisterControlCombinerLoadEventHanlder(ControlBase control)
		{
			string invokeVKeCRMOnloadFunc = string.Format(ClientHelper.FuncRegisterControlsCombiner, control.DataStoreName);

			//Register the onload event handlers
			string registerOnLoadFunction = string.Format(@"{0}.on('load', function(){{ {1} }}); ", control.DataStoreName, invokeVKeCRMOnloadFunc);

			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_OnloadFunction", registerOnLoadFunction, true);
		}

		public static void RegisterDataGirdOnLoadEventHanlder(DataGrid control)
		{
			Regex regex = new Regex("'", RegexOptions.None);

			string headHtml = control.Items.Aggregate<DataGridItemBase, string>(string.Empty,
			                                                                    (seed, item) =>
			                                                                    	{
			                                                                    		if (string.IsNullOrEmpty(item.CssClass))
			                                                                    		{
			                                                                    			return string.Concat(seed,
			                                                                    			                     "<th>" + item.HeaderText +
			                                                                    			                     "</th>");
			                                                                    			
			                                                                    		}
			                                                                    		else
			                                                                    		{
																							return string.Concat(seed,
																												 string.Format("<th class={0}>{1}</th>", item.CssClass, item.HeaderText));
			                                                                    		}

			                                                                    	});
																			
			headHtml = headHtml.Replace(System.Environment.NewLine, string.Empty).Replace("\n", string.Empty);
			headHtml = regex.Replace(headHtml, "\\'");

			string bodyHtml = control.Items.Aggregate<DataGridItemBase, string>(string.Empty,
																		 (seed, item) =>
																		 string.Concat(seed,
																					   item.ToHtml()));

			bodyHtml = bodyHtml.Replace(System.Environment.NewLine, string.Empty).Replace("\n", string.Empty);
			bodyHtml = regex.Replace(bodyHtml, "\\'");

			string userFootHtml = string.Empty;
			if (control.Footer != null)
			{
				control.Footer.ColSpan = control.Items.Count;
				userFootHtml = control.Footer.ToHtml().Replace(System.Environment.NewLine, string.Empty).Replace("\n", string.Empty);
				userFootHtml = regex.Replace(userFootHtml, "\\'");
			}


			string invokeVKeCRMOnloadFunc = string.Format(ClientHelper.FuncRegisterDataGridLoad,
												  control.DataStoreName, control.DataStoreName,
												  control.ClientID, headHtml, control.ClientID,
												  control.TableClass, control.DataRoot,
												  control.RowCssClass, control.AlternatingRowCssClass,
												  bodyHtml, userFootHtml,
												  "GridPager_Default", control.Items.Count,
												  control.Pager.LeftPagerClass, control.Pager.RightPagerClass,
												  control.Pager.MaxPageCount, (int)control.CollectionType,
												  control.ClientErrorHandlerFuncName, control.ClientErrorHandlerTargetClientID,
												   (int)control.ClientErrorHandlerStrategy, control.ClientErrorHandlerStyleClass,
												   "false", control.IsShowEmptyText.ToString().ToLower(), control.EmptyText, control.ShowLoadingAt);

			string registerOnLoadFunction = string.Format(@" {0}.on('load', function() {{ {1} }});", control.DataStoreName, invokeVKeCRMOnloadFunc);


			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_OnloadFunction", registerOnLoadFunction, true);
		}

		public static void RegisterGeneratedControlEventHandler(Upload control)
		{
			System.Web.UI.Page CurrentPage = (System.Web.UI.Page)System.Web.HttpContext.Current.CurrentHandler;
			System.Web.UI.HtmlControls.HtmlGenericControl ScriptControl = new System.Web.UI.HtmlControls.HtmlGenericControl("script");
			ScriptControl.Attributes.Add("type", "text/javascript");
			ScriptControl.Attributes.Add("src", "/Scripts/ajaxupload.js");
			CurrentPage.Header.Controls.Add(ScriptControl);

			StringBuilder sb = new StringBuilder(500);
			sb.Append("$(document).ready(function(){");
			sb.Append("var upload = new AjaxUpload('#"+control.GeneratedControlId+"',{");
			sb.Append("	action: '"+control.Action +"',");
			sb.Append("	name: 'VKeCRMFileUpload',");
			sb.Append("	autoSubmit: false,");
			sb.Append("	responseType: 'json',");
			sb.Append("	onChange: function(file, extension) {");
			sb.Append("	jQuery('#"+control.InputControlName+"').val($(this._input).val());},");
			sb.Append("	onSubmit: function(file, ext) {");
			if (!string.IsNullOrEmpty(control.AllowedFileExtensions)){
				sb.Append("	if (!(ext && /^(" + control.AllowedFileExtensions + ")$/.test(ext.toLowerCase()))){ var data= {\"Error\":1,\"ErrorMessage\":\"only (" + control.AllowedFileExtensions + ") are allowed;\",\"DataSource\":[]};");
				if (!string.IsNullOrEmpty(control.OnCompleted))
					sb.Append(control.OnCompleted + "(file,data);");
				sb.Append("	return false; }");
			}			
			sb.Append("	this.disable();	jQuery('#" + control.InputControlName + "').val('');this.disable();},");
			sb.Append("	onComplete: function(file, response) {");
			if(!string.IsNullOrEmpty(control.OnCompleted))
				sb.Append(control.OnCompleted+"(file,response);");
			sb.Append("	this.enable();}");
			sb.Append("});");
			sb.Append("$('#" + control.SubmitButton + "').click(");
			sb.Append("function(){ if(!" + control.DoCheckEvent + "(jQuery('#" + control.InputControlName + "').val())){ return false;}");
			sb.Append("upload.submit()});");
			sb.Append("});");
		
			control.Page.ClientScript.RegisterStartupScript(control.Page.GetType(), control.ClientID + "JsonStore_GeneratedControlEvent",
													sb.ToString(), true);
		}


	}
}
