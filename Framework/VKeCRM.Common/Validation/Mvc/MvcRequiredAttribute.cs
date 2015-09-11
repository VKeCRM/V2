using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcRequiredAttribute : MvcValidationAttribute
	{
		public string InitialValue
		{
			get;
			set;
		}

		public override bool IsValid(object value)
		{
			bool isValid = true;

			if (null == value)
			{
				isValid = false;
			}
			else
			{
				string str = value.ToString();

				isValid = (!string.IsNullOrEmpty(str)
					&& !string.Equals(str, InitialValue, StringComparison.CurrentCultureIgnoreCase));
			}

			return isValid;
		}

		public override MvcValidationType ValidationType
		{
			get { return MvcValidationType.MvcRequired; }
		}
	}
}
