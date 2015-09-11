using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcDateFormatAttribute : MvcValidationAttribute
	{
		private const char DateFormatSeparator = ',';

		[Description("Date format to support. Separate with ','."), Category("Behavior")]
		public string DateFormat
		{
			get;
			set;
		}

		[Description("default Error message."), Category("Behavior")]
		public string CommonErrorMessage
		{
			get;
			set;
		}

		[Description("Error message for invalid date."), Category("Behavior")]
		public string MessageOfInValidDate
		{
			get;
			set;
		}

		[Description("Error message for invalid year."), Category("Behavior")]
		public string MessageOfInValidYear
		{
			get;
			set;
		}

		public override bool IsValid(object value)
		{
			if (null == value)
			{
				return false;
			}
			else
			{
				string dateText = value.ToString();

				string[] dateFormats = DateFormat.Split(DateFormatSeparator);

				bool isMatch = false;

				foreach (string format in dateFormats)
				{
					if (!string.IsNullOrEmpty(format))
					{
						try
						{
							DateTime time = DateTime.ParseExact(dateText, format, new CultureInfo("en-US"), DateTimeStyles.None);

							// if the format is matched, break.
							if (time.Year < 1900)
							{
								base.ErrorMessage = MessageOfInValidYear;
							}
							else
							{
								isMatch = true;
							}
							break;
						}
						catch
						{
							base.ErrorMessage = CommonErrorMessage;
						}
					}
				}

				return isMatch;
			}
		}

		public override MvcValidationType ValidationType
		{
			get { return MvcValidationType.MvcDateFormat; }
		}
	}
}
