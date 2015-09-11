using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcCustomerAttribute : MvcValidationAttribute
	{
		public event ServerValidateEventHandler ServerValidate;

		private bool _validateEmptyText = true;
		public bool ValidateEmptyText
		{
			get { return _validateEmptyText; }
			set { _validateEmptyText = value; }
		}

		public string ClientValidationFunction
		{
			get;
			set;
		}

		public override bool IsValid(object value)
		{
			if (null != ServerValidate)
			{
				ServerValidateEventArgs args = new ServerValidateEventArgs(value.ToString(), true);

				ServerValidate(this, args);

				return args.IsValid;
			}
			else
			{
				return true;
			}
		}

		public override MvcValidationType ValidationType
		{
			get { return MvcValidationType.MvcCustomer; }
		}
	}
}
