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
	/// Ajax Upload
	/// </summary>
	[ToolboxData("<{0}:Upload runat=server></{0}:Upload>")]
	public class Upload : ControlBase, IInitMessager
	{
		#region Data properties
		private const string DATA_ACTION = "data_action";
		public string Action
		{
			get
			{
				return GetItemFromViewState(DATA_ACTION, string.Empty);
			}
			set
			{
				ViewState[DATA_ACTION] = value;
			}
		}

		private const string DATA_AUTOSUBMIT = "data_submitbutton";
		public string SubmitButton
		{
			get
			{
				return GetItemFromViewState(DATA_AUTOSUBMIT, string.Empty);
			}
			set
			{
				ViewState[DATA_AUTOSUBMIT] = value;
			}
		}
		private const string DATA_ONCOMPLETED = "data_oncompleted";
		public string OnCompleted
		{
			get
			{
				return GetItemFromViewState(DATA_ONCOMPLETED, string.Empty);
			}
			set
			{
				ViewState[DATA_ONCOMPLETED] = value;
			}
		}
		private const string DATA_EXTENSIONS = "data_extensionsd";
		public string AllowedFileExtensions
		{
			get
			{
				return GetItemFromViewState(DATA_EXTENSIONS, string.Empty);
			}
			set
			{
				ViewState[DATA_EXTENSIONS] = value;
			}
		}
		
		public string ControlCssClass
		{
			get;
			set;
		}

		public string InputStyle
		{
			get;
			set;
		}
		public string ButtonStyle
		{
			get;
			set;
		}

		private string _btnValue = "Browse";
		public string BtnValue
		{
			get { return _btnValue; }
			set { _btnValue = value; }
		}
		private const string _fileName = "_fileName";
		private const string _btnName = "_BtnName";
		public string InputControlName
		{
			get { return string.Format(@"{0}{1}", this.ClientID, _fileName); }			
		}
		public string ButtonControlName
		{
			get { return string.Format(@"{0}{1}", this.ClientID, _btnName); }			
		}

		public string DoCheckEvent
		{
			get;
			set;
		}
		#endregion

		#region Override Page methods

		protected override void RegisterOnLoadEventHanlder()
		{
			if (string.IsNullOrEmpty(Action)) throw new ArgumentNullException("Action");		

			base.RegisterOnLoadEventHanlder();

			ClientHelper.RegisterGeneratedControlEventHandler(this);
		}

		/// <summary>
		/// Add the listener to automatically get the combobox's key value when page loading.
		/// </summary>
		/// <param name="associatedParamterName"></param>
		/// <param name="control"></param>
		public void RegisterInitListener(string associatedParamterName, ControlBase control)
		{
			
		}
		public override string GeneratedControlId
		{
			get
			{
				return this.ClientID;
			}
		}


		protected override void CreateChildControls()
		{			
			Controls.Clear();			
			ClearChildViewState();			
			CreateCtrls();
		}
		private void CreateCtrls()
		{					
			System.Web.UI.HtmlControls.HtmlInputText input = new System.Web.UI.HtmlControls.HtmlInputText();
			input.Attributes["style"] = InputStyle;
			input.ID = this.ID + _fileName;
			System.Web.UI.HtmlControls.HtmlInputButton button = new System.Web.UI.HtmlControls.HtmlInputButton();
			button.Attributes["style"] = ButtonStyle;
			button.ID = this.ID + _btnName;
			button.Value = BtnValue;
			this.Controls.Add(input);
			this.Controls.Add(button);
		}	
		#endregion
	}
}
