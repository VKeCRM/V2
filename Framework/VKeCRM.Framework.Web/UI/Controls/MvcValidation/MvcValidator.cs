using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.Security.Permissions;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI.HtmlControls;
using VKeCRM.Common.Validation.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using VKeCRM.Common.DataTransferObjects;

namespace VKeCRM.Framework.Web.UI.Controls.MvcValidation
{
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ToolboxData("<{0}:MvcValidator runat=\"server\"></{0}:MvcValidator>")]
	[DefaultProperty("CaptionText")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class MvcValidator : Label
	{
		private string _moduleType = null;
		private bool _checkAtClient = true;

		private ControlPropertyCollection _fieldValidators = null;

		public MvcValidator()
		{
			_fieldValidators = new ControlPropertyCollection();
		}

		[Description("Gets or sets module type of the form fields.")]
		[Category("Appearance")]
		[RefreshProperties(RefreshProperties.All)]
		public virtual string ModuleType
		{
			get { return _moduleType; }
			set { _moduleType = value; }
		}

		[Description("Gets or sets callback when success.")]
		public virtual string OnSuccess
		{
			get;
			set;
		}

		[Description("Gets or sets callback when failure.")]
		public virtual string OnError
		{
			get;
			set;
		}

		[Description("Gets or sets callback before do validation.")]
		public virtual string BeforeValidate
		{
			get;
			set;
		}

		[Description("Gets or sets the provider to get target control id.")]
		public virtual string IdProvider
		{
			get;
			set;
		}

		[Description("Gets or sets whether to check at client.")]
		public virtual bool CheckAtClient
		{
			get { return _checkAtClient; }
			set { _checkAtClient = value; }
		}

		[Description("Gets or sets CustomerValidate fn.")]
		public virtual string CustomerValidate
		{
			get;
			set;
		}

		public virtual string AllowExpressionSwitch
		{
			get;
			set;
		}

		public virtual string OriginalBorderColor
		{
			get;
			set;
		}

		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ControlPropertyCollection FieldValidators
		{
			get { return _fieldValidators; }
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			if (Visible && Enabled)
			{
				if (string.IsNullOrEmpty(ModuleType))
				{
					throw new ArgumentException("ModuleType is not set.");
				}

				RegidterFieldValidators();

				RegisterClientScript();
			}
		}

		private void RegidterFieldValidators()
		{
			Attributes.Add("CheckAtClient", CheckAtClient ? "1" : "0");

			if (!string.IsNullOrEmpty(BeforeValidate))
			{
				Attributes.Add("BeforeValidate", BeforeValidate);
			}

			if (!string.IsNullOrEmpty(OnSuccess))
			{
				Attributes.Add("OnSuccess", OnSuccess);
			}

			if (!string.IsNullOrEmpty(OnError))
			{
				Attributes.Add("OnError", OnError);
			}

			if (!string.IsNullOrEmpty(IdProvider))
			{
				Attributes.Add("IdProvider", IdProvider);
			}

			if (!string.IsNullOrEmpty(CustomerValidate))
			{
				Attributes.Add("CustomerValidate", CustomerValidate);
			}

			if (!string.IsNullOrEmpty(AllowExpressionSwitch))
			{
				Attributes.Add("AllowExpressionSwitch", AllowExpressionSwitch);
			}

			if (!string.IsNullOrEmpty(OriginalBorderColor))
			{
				Attributes.Add("OriginalBorderColor", OriginalBorderColor);
			}

			Type moduleType = Type.GetType(ModuleType, true, true);

			ReaderAtClient(moduleType, null);
		}

		private void ReaderAtClient(Type moduleType, string parent)
		{
			PropertyInfo[] allProperties = moduleType.GetProperties();
			for (int i = 0; i < allProperties.Length; i++)
			{
				PropertyInfo propertyInfo = allProperties[i];

				if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
				{
					Type[] types = propertyInfo.PropertyType.GetGenericArguments();
					Type tType = types.FirstOrDefault(p => p.GetInterface(typeof(IMvcModule).FullName) != null);
					if (null != tType && null == parent)
					{
						ReaderAtClient(tType, propertyInfo.Name);
					}
				}
				else
				{
					// this mapping can write or not
					ControlPropertyMapping controlPropertyMapping = FieldValidators.Where(p => string.Equals(p.PropertyName, propertyInfo.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

					object[] validationAttributes = propertyInfo.GetCustomAttributes(typeof(MvcValidationAttribute), true);
					Array.Sort(validationAttributes);
					foreach (object attribute in validationAttributes)
					{
						RegisterClientValidationInfo(attribute as MvcValidationAttribute, controlPropertyMapping, propertyInfo.Name, parent);
					}

					object[] restrictAttributes = propertyInfo.GetCustomAttributes(typeof(MvcRestrictAttribute), true);
					foreach (object attribute in restrictAttributes)
					{
						RegisterRestrict(attribute as MvcRestrictAttribute, controlPropertyMapping, propertyInfo.Name);
					}
				}
			}
		}

		private void RegisterRestrict(MvcRestrictAttribute attribute, ControlPropertyMapping controlPropertyMapping, string property)
		{
			HtmlInputHidden hidden = new HtmlInputHidden();

			if (controlPropertyMapping != null)
			{
				WebControl targetControl = FindControl(controlPropertyMapping.ControlToValidate) as WebControl;
				hidden.Attributes.Add("ControlToValidate", (targetControl == null) ? controlPropertyMapping.ControlToValidate : targetControl.ClientID);
			}

			hidden.Attributes.Add("ValidateType", ".");
			hidden.Attributes.Add("Property", property);
			hidden.Attributes.Add("IsPreventPaste", attribute.IsPreventPaste ? "1" : "0");
			hidden.Attributes.Add("AllowExpression", attribute.AllowExpression);
			hidden.Attributes.Add("WaterMark", attribute.WaterMark);

			Controls.Add(hidden);
		}

		private void RegisterClientValidationInfo(MvcValidationAttribute attribute, ControlPropertyMapping controlPropertyMapping, string property, string parent)
		{
			HtmlInputHidden hidden = new HtmlInputHidden();

			if (controlPropertyMapping != null)
			{
				WebControl targetControl = FindControl(controlPropertyMapping.ControlToValidate) as WebControl;
				hidden.Attributes.Add("ControlToValidate", (targetControl == null) ? controlPropertyMapping.ControlToValidate : targetControl.ClientID);
			}

			if (!string.IsNullOrEmpty(parent))
			{
				hidden.Attributes.Add("Parent", parent);
			}

			hidden.Attributes.Add("Property", property);
			hidden.Attributes.Add("IngoreTaret", attribute.IngoreTaret);
			hidden.Attributes.Add("IngoreValue", attribute.IngoreValue);
			hidden.Attributes.Add("ErrorMessage", attribute.ErrorMessage);
			hidden.Attributes.Add("ValidateType", attribute.ValidationType.ToString());

			switch (attribute.ValidationType)
			{
				case MvcValidationType.MvcRequired:
					hidden.Attributes.Add("InitialValue", (attribute as MvcRequiredAttribute).InitialValue);
					break;

				case MvcValidationType.MvcRegularExpression:
					hidden.Attributes.Add("Pattern", (attribute as MvcRegularExpressionAttribute).Pattern);
					break;

				case MvcValidationType.MvcDateFormat:
					hidden.Attributes.Add("DateFormat", (attribute as MvcDateFormatAttribute).DateFormat);
					hidden.Attributes.Add("CommonErrorMessage", (attribute as MvcDateFormatAttribute).CommonErrorMessage);
					hidden.Attributes.Add("MessageOfInValidDate", (attribute as MvcDateFormatAttribute).MessageOfInValidDate);
					hidden.Attributes.Add("MessageOfInValidYear", (attribute as MvcDateFormatAttribute).MessageOfInValidYear);
					break;

				case MvcValidationType.MvcCompare:
					hidden.Attributes.Add("CompareToValue", (attribute as MvcCompareAttribute).CompareToValue);
					hidden.Attributes.Add("DataType", (attribute as MvcCompareAttribute).DataType.ToString().ToLower());
					hidden.Attributes.Add("CompareOperator", (attribute as MvcCompareAttribute).CompareOperator.ToString());
					hidden.Attributes.Add("AlwaysTrueForNullValue", (attribute as MvcCompareAttribute).AlwaysTrueForNullValue ? "1" : "0");
					break;

				case MvcValidationType.MvcTextFilter:
					int minLength = (attribute as MvcTextFilterAttribute).MinLenth;
					int maxLength = (attribute as MvcTextFilterAttribute).MaxLength;

					if (maxLength != 0 && maxLength <= minLength)
						throw new Exception("maxLength should greater than minLength!");

					hidden.Attributes.Add("ValidateEmptyText", (attribute as MvcTextFilterAttribute).ValidateEmptyText ? "1" : "0");
					hidden.Attributes.Add("MaxLength", maxLength.ToString());
					hidden.Attributes.Add("MinLenth", minLength.ToString());
					hidden.Attributes.Add("MessageOfMaxLength", (attribute as MvcTextFilterAttribute).MessageOfMaxLength);
					hidden.Attributes.Add("MessageOfMinLength", (attribute as MvcTextFilterAttribute).MessageOfMinLength);
					break;

				case MvcValidationType.MvcCustomer:
					hidden.Attributes.Add("ValidateEmptyText", (attribute as MvcCustomerAttribute).ValidateEmptyText ? "1" : "0");
					hidden.Attributes.Add("ClientValidationFunction", (attribute as MvcCustomerAttribute).ClientValidationFunction);
					break;
			}

			Controls.Add(hidden);
		}

		private void RegisterClientScript()
		{
			string validation = WebResourceManager.GetScriptResourceUrl("Validation/Scripts/Validation.js");
			Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "Validation", validation);

			string mvcJsResource = WebResourceManager.GetScriptResourceUrl("MvcValidation/Scripts/MvcValidation.js");
			Page.ClientScript.RegisterClientScriptInclude(Page.GetType(), "mvcJsResource", mvcJsResource);

			string registerCommand = @"<script type='text/javascript'>
				if(typeof (VKeCRMSys) == 'undefined')
				{
					alert('MvcValidator needs VKeCRMScriptManager control, use it like: <VKeCRM:VKeCRMScriptManager runat=server></VKeCRM:VKeCRMScriptManager>');
				}
				else
				{
					VKeCRMSys.getInstance().add_pageValidator('" + ClientID + @"');
				}
			</script>";
			Page.ClientScript.RegisterStartupScript(this.GetType(), ClientID, registerCommand);
		}
	}
}