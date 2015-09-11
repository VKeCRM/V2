using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VKeCRM.Framework.Web.UI.Controls.Json;
using System.Web.UI;
using System.Diagnostics;


namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	/// <summary>
	/// Ajax ComboBox
	/// </summary>
	[ToolboxData("<{0}:AjaxComboBox runat=server></{0}:VKeCRMDataGrid>")]
	public class ComboBox : ControlBase, IInitMessager
	{
		#region Data properties
		private const string DATA_KEY = "DataKey";
		public string DataKey
		{
			get
			{
				return GetItemFromViewState(DATA_KEY, string.Empty);
			}
			set
			{
				ViewState[DATA_KEY] = value;
			}
		}

		private const string DATA_VALUE = "DataValue";
		public string DataValue
		{
			get
			{
				return GetItemFromViewState(DATA_VALUE, string.Empty);
			}
			set
			{
				ViewState[DATA_VALUE] = value;
			}
		}
		#endregion

		#region Override Page methods

		protected override void RegisterOnLoadEventHanlder()
		{

			ClientHelper.RegisterComboBoxOnLoadEventHanlder(this);

			base.RegisterOnLoadEventHanlder();

			ClientHelper.RegisterGeneratedControlEventHandler(this);
		}

		public override string DataRoot
		{
			get
			{
				return GetItemFromViewState(FOR, "DataSource");
			}
			set
			{
				ViewState[FOR] = value;
			}
		}

		#endregion

		#region Helper methods and fields to generate js scripts for Listeners and event

		public string JsonControlCssClass
		{
			get;
			set;
		}

		public List<KeyValuePair<EventType, string>> generatedControlEventListener = new List<KeyValuePair<EventType, string>>();
		//public Dictionary<EventType, string> generatedControlEventListener = new Dictionary<EventType, string>();
		public void RegisterGeneratedControlEventListener(EventType eventType, string eventHandlerFunc)
		{
			generatedControlEventListener.Add(new KeyValuePair<EventType, string>(eventType, eventHandlerFunc));
		}

		/// <summary>
		/// Add the listener to automatically get the combobox's key value when page loading.
		/// </summary>
		/// <param name="associatedParamterName"></param>
		/// <param name="control"></param>
		public void RegisterInitListener(string associatedParamterName, ControlBase control)
		{
			string regisetOnLoadEventHanlder = string.Format(ClientHelper.FuncRegisterInitListener,
														this.GeneratedControlId, control.DataStoreName,
														associatedParamterName);

			_baseRegisterFunctionList += regisetOnLoadEventHanlder;
		}

		public override string GeneratedControlId
		{
			get
			{
				return string.Format(@"{0}_Select", this.ClientID);
			}
		}

		private string _baseRegisterFunctionList = string.Empty;
		#endregion
	}


}
