using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public static class ControlExtension
	{

		/// <summary>
		/// It will get the value from the itself control, and then to set the paratmer of target control with reloading
		/// </summary>
		/// <param name="control"></param>
		/// <param name="eventType"></param>
		/// <param name="targetControl"></param>
		/// <param name="associatedParamterName"></param>
		public static void RegisterVKeCRMClientReloadWithParamterControlAction(this System.Web.UI.Control control, EventType eventType, ControlBase targetControl, string associatedParamterName)
		{
			//We must treate our VKeCRM control for special client id
			string sourceClientId = control is ControlBase ? ((ControlBase)control).GeneratedControlId : control.ClientID;

			if (control is ControlBase)
			{
				((ControlBase)control).AddClientControlBinding(sourceClientId,  targetControl.DataStoreID, associatedParamterName, eventType, true);
			}
			else
			{

				string function = string.Format(ClientHelper.FuncRegisterListener,
														sourceClientId, targetControl.DataStoreID,
														associatedParamterName, "true",
														eventType.ToString().ToLower(), sourceClientId);
				control.Page.ClientScript.RegisterStartupScript(control.GetType(),
															 control.ClientID + targetControl.ClientID + "InitFunc", function, true);
			}
			
		}

		/// <summary>
		/// It will get the value from the specific control, and then to set the paratmer of target control with reloading
		/// </summary>
		/// <param name="control"></param>
		/// <param name="eventType"></param>
		/// <param name="controlContainParamter"></param>
		/// <param name="targetControl"></param>
		/// <param name="associatedParamterName"></param>
		public static void RegisterVKeCRMClientReloadWithParamterControlAction(this System.Web.UI.Control control, EventType eventType, ControlBase targetControl, string associatedParamterName, Control controlContainParamter)
		{
			//We must treate our VKeCRM control for special client id
			string sourceClientId = control is ControlBase ? ((ControlBase)control).GeneratedControlId : control.ClientID;
			string containValueControlId = controlContainParamter is ControlBase ? ((ControlBase)controlContainParamter).GeneratedControlId : controlContainParamter.ClientID;


			if (control is ControlBase)
			{
				((ControlBase)control).AddClientControlBinding(sourceClientId,  targetControl.DataStoreID, associatedParamterName, containValueControlId, eventType, true);
			}
			else
			{

				string function = string.Format(ClientHelper.FuncRegisterListener,
														sourceClientId, targetControl.DataStoreID,
														associatedParamterName, "true",
														eventType.ToString().ToLower(), containValueControlId);
				control.Page.ClientScript.RegisterStartupScript(control.GetType(),
															 control.ClientID + targetControl.ClientID + "InitFunc", function, true);
			}
		}

		public static void RegisterVKeCRMClientReloadControlAction(this System.Web.UI.Control control, EventType eventType, ControlBase targetControl)
		{
			//We must treate our VKeCRM control for special client id
			string sourceClientId = control is ControlBase ? ((ControlBase)control).GeneratedControlId : control.ClientID;

			if (control is ControlBase)
			{
				((ControlBase)control).AddClientControlBinding(sourceClientId,  targetControl.DataStoreID, eventType);
			}
			else
			{
				targetControl.AddClientControlBinding(sourceClientId, targetControl.DataStoreID, eventType);
			}
		}

		public static void RegisterVKeCRMClientSetParamterAction(this System.Web.UI.Control control, EventType eventType, ControlBase targetControl, string associatedParamterName)
		{
			//We must treate our VKeCRM control for special client id
			string sourceClientId = control is ControlBase ? ((ControlBase)control).GeneratedControlId : control.ClientID;

			if (control is ControlBase)
			{
				((ControlBase)control).AddClientControlBinding(sourceClientId, targetControl.DataStoreID, associatedParamterName, eventType, true);
			}
			else
			{
				targetControl.AddClientControlBinding(sourceClientId, targetControl.DataStoreID, associatedParamterName, eventType, true);
			}
		}
	}
}
