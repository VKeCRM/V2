using System;
using System.Collections.Generic;

namespace VKeCRM.Common.Validation.Mvc
{
	public enum MvcValidationType
	{
		MvcRequired = 1,
		MvcRegularExpression = 2,
		MvcDateFormat = 3,
		MvcCompare = 4,
		MvcTextFilter = 5,
		MvcCustomer = 6
	}
}
