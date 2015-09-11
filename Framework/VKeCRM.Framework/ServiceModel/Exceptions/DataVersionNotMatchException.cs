using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VKeCRM.Framework.Business.Exceptions;
using VKeCRM.Framework.Business.Enums;

namespace VKeCRM.Framework.ServiceModel.Exceptions
{
	public class DataVersionNotMatchException : BusinessExceptionBase
	{
		public DataVersionNotMatchException(string errorMessage)
			: base(ExceptionErrorCode.DataVersionNotMatch, errorMessage)
		{

		}
	}
}
