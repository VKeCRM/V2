using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcRegularExpressionAttribute : MvcValidationAttribute
	{
		public string Pattern
		{
			get;
			set;
		}

		private Regex _regex = null;
		private Regex Regex
		{
			get
			{
				if (null == _regex)
				{
					_regex = new Regex(Pattern, RegexOptions.IgnoreCase);
				}

				return _regex;
			}
		}

		public override bool IsValid(object value)
		{
			bool isValid = true;

			if (null == value)
			{
				throw new NullReferenceException();
			}
			else
			{
				if (string.IsNullOrEmpty(Pattern))
				{
					throw new Exception("Pattern should not be empty.");
				}

				string sourse = value.ToString();

				isValid = Regex.IsMatch(sourse);
			}

			return isValid;
		}

		public override MvcValidationType ValidationType
		{
			get { return MvcValidationType.MvcRegularExpression; }
		}
	}
}
