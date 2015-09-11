using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using AjaxControlToolkit;

[assembly: System.Web.UI.WebResource("VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("VKeCRM.Framework.Web.UI.Controls.ControlExtender.Calendar.css", "text/css", PerformSubstitution = true)]
namespace VKeCRM.Framework.Web.UI.Controls.ControlExtender
{
	[ClientScriptResource("VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender",
		"VKeCRM.Framework.Web.UI.Controls.ControlExtender.TransferCalendarExtender.js")]
	[ClientCssResource("VKeCRM.Framework.Web.UI.Controls.ControlExtender.Calendar.css")]
	[ToolboxData("<{0}:TransferCalendarExtender runat=server></{0}:TransferCalendarExtender>")]
	public class TransferCalendarExtender : AjaxControlToolkit.CalendarExtender
	{
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("aserverOffset")]
		public virtual Int32? AserverOffset
		{
			get { return GetPropertyValue("AserverOffset", (Int32?)null); }
			set { SetPropertyValue("AserverOffset", value); }
		}
		
		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("lowerBoundDate")]
		public virtual DateTime? LowerBoundDate
		{
			get { return GetPropertyValue("LowerBoundDate", (DateTime?)null); }
			set { SetPropertyValue("LowerBoundDate", value); }
		}

		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("upperBoundDate")]
		public virtual DateTime? UpperBoundDate
		{
			get { return GetPropertyValue("UpperBoundDate", (DateTime?)null); }
			set { SetPropertyValue("UpperBoundDate", value); }
		}

		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("customerTodayDateForVKeCRM")]
		public virtual DateTime? CustomerTodayDateForVKeCRM
		{
			get { return GetPropertyValue("CustomerTodayDateForVKeCRM", (DateTime?)null); }
			set { SetPropertyValue("CustomerTodayDateForVKeCRM", value); }
		}

		[DefaultValue(null)]
		[ExtenderControlProperty]
		[ClientPropertyName("apesonHolidays")]
		public virtual String ApesonHolidays
		{
			get { return GetPropertyValue("ApesonHolidays", (String)null); }
			set { SetPropertyValue("ApesonHolidays", value); }
		}
	}
}
