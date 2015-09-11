using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	[Serializable]
	public class Pager
	{	
		//Default is 4
		public int MaxPageCount = 4;

		//CssClass with default value
		public string LeftPagerClass = "PagerLeft_Default";
		public string RightPagerClass = "PagerRight_Default";

	}
}
