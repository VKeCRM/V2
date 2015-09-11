using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcRestrictAttribute : Attribute
	{
		private bool _isPreventPaste = false;

		public bool IsPreventPaste
		{
			get { return _isPreventPaste; }
			set { _isPreventPaste = value; }
		}

		public string AllowExpression
		{
			get;
			set;
		}

		public string WaterMark
		{
			get;
			set;
		}
	}
}
