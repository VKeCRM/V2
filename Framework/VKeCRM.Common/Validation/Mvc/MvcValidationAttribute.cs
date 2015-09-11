using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.Validation.Mvc
{
	public abstract class MvcValidationAttribute : Attribute, IComparable
	{
		public string ErrorMessage
		{
			get;
			set;
		}

		public string IngoreTaret
		{
			get;
			set;
		}

		public string IngoreValue
		{
			get;
			set;
		}

		public int Order
		{
			get;
			set;
		}

		protected MvcValidationAttribute()
		{
		}

		protected MvcValidationAttribute(string errorMessage)
		{
			ErrorMessage = errorMessage;	
			
		}

		public abstract bool IsValid(object value);

		public abstract MvcValidationType ValidationType
		{
			get;
		}

		#region IComparable Members

		public int CompareTo(object obj)
		{
			if (this == obj)
			{
				return 0;
			}
			else
			{
				MvcValidationAttribute a = obj as MvcValidationAttribute;

				if (a.Order > this.Order)
				{
					return -1;
				}
				else if (a.Order == this.Order)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}
		}

		#endregion
	}
}
