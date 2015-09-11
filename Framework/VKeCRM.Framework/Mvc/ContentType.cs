using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace VKeCRM.Framework.Mvc
{
	public enum ContentType
	{
		[Description("application/json")]
		Json,

		[Description("application/csv")]
		Csv,

		[Description("application/x-ofx")]
		Ofx,

		[Description("application/vnd.intu.qfx")]
		Qfx
	}
}
