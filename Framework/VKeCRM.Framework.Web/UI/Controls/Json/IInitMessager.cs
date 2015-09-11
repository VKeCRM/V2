using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public interface IInitMessager
	{
		void RegisterInitListener(string associatedParamterName, ControlBase control);
	}
}
