using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcTextFilterAttribute : MvcValidationAttribute
	{
		private bool _validateEmptyText = true;

		[Description("Max length of textarea"), Category("Behavior")]
		public int MaxLength
		{
			get;
			set;
		}

		[Description("Min length of textarea"), Category("Behavior")]
		public int MinLenth
		{
			get;
			set;
		}

		[Description("Message of max length"), Category("Behavior")]
		public string MessageOfMaxLength
		{
			get;
			set;
		}

		[Description("Message of min length"), Category("Behavior")]
		public string MessageOfMinLength
		{
			get;
			set;
		}

		public bool ValidateEmptyText
		{
			get { return _validateEmptyText; }
			set { _validateEmptyText = value; }
		}

		public override bool IsValid(object value)
		{
			// if ValidateEmptyText = true, will return false
			// otherwise return true;
			if (null == value)
				return !ValidateEmptyText;

			string textValue = value.ToString();

			if (!ValidateEmptyText && string.IsNullOrEmpty(textValue))
				return true;

			if (textValue.Length < MinLenth)
			{
				base.ErrorMessage = MessageOfMinLength;
				return false;
			}

			// fix when MaxLength is not set, default to 0.
			if (MaxLength != 0 && textValue.Length > MaxLength)
			{
				base.ErrorMessage = MessageOfMaxLength;
				return false;
			}

			return true;
		}

		public override MvcValidationType ValidationType
		{
			get { return MvcValidationType.MvcTextFilter; }
		}
	}
}
