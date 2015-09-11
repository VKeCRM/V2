using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Web.UI.Controls.MvcValidation
{
	public class ControlPropertyCollection : List<ControlPropertyMapping>
	{
		public string ModuleType
		{
			get;
			set;
		}
	}
}
