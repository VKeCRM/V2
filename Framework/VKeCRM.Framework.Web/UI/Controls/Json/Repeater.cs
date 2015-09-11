using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using VKeCRM.Framework.Web.UI.Controls.Json;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:Repeater runat=server></{0}:Repeater>")]
	public class Repeater : CollectionControlBase
	{
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DataGridCustomItem ItemTemplate
		{
			get
			{
				DataGridCustomItem itemTemplate = ViewState["VKeCRMRepeaterItemTemplate"] as DataGridCustomItem;
				return itemTemplate;
			}
			set
			{
				ViewState["VKeCRMRepeaterItemTemplate"] = value;
			}
		}


		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DataGridFooter Footer
		{
			get
			{
				DataGridFooter foot = ViewState["VKeCRMRepeaterFoot"] as DataGridFooter;
				return foot;
			}
			set
			{
				ViewState["VKeCRMRepeaterFoot"] = value;
			}
		}

		protected override void RegisterOnLoadEventHanlder()
		{
			ClientHelper.RegisterRepeaterOnLoadEventHanlder(this);
			base.RegisterOnLoadEventHanlder();
		}

		private bool _isDivContainer;
		public bool IsDivContainer
		{
			get { return _isDivContainer; }
			set 
			{ 
				_isDivContainer = value;
				ItemTemplate.IsRenderDefaultTDTag = false;
			}
		}

		private int _colSpan = 1;
		public int ColSpan
		{
			get { return _colSpan; }
			set
			{
				_colSpan = value;
			}
		}

		/// <summary>+
		/// This specify the specific collection type
		/// </summary>
		public override CollectionType CollectionType
		{
			get
			{
				return CollectionType.Repeater;
			}
		}
	}
}
