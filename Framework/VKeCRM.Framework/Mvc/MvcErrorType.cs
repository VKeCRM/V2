using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Mvc
{
	public enum MvcErrorType
	{
		None = 0,
		Normal = 1,

		AuthenticationFailed = 2,
		TokenValidationgFailed = 3,

        SessionKickOutException = 4,

		ValidationFailedAtClient = 998, // this error won't be used at server leval, just add to enum
		ValidationFailedAtServer = 999,
		//BusinessValidationFailed = 1000,

		BusinessLevalException= 1001,

		JsonDeserializeError = 1002,

		PermissionDenied = 1003,

		Undefined = 9999,
	}
}
