using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public abstract class CollectionControlBase : ControlBase
	{

		#region Override base methods and properties
		protected override void OnLoad(EventArgs e)
		{
			//Add paging paramter for controller method. 
			if (this.PageSize != 0 && this.PageNumber != 0)
			{
				AddParamter("pageSize", this.PageSize);
				AddParamter("pageNum", this.PageNumber);
			}

			//Our ControlBase will initialize and register scripts for datastore.
			base.OnLoad(e);
		}

		public override string GeneratedControlId
		{
			get
			{
				return string.Format(@"{0}_Table", this.ClientID);
			}
		}

		#endregion




		public abstract CollectionType CollectionType
		{
			get;
		}


		#region Paging paramters;

		private const string PAGER = "Pager";
		private const string PAGE_SIZE = "PageSize";
		private const string PAGE_NUMBER = "PageNumber";

		public Pager Pager
		{
			get
			{
				if (ViewState[PAGER] == null)
				{
					ViewState[PAGER] = new Pager();
				}

				return (Pager)ViewState[PAGER];
			}
			set
			{
				ViewState[PAGER] = value;
			}
		}

		public int PageSize
		{
			get
			{
				return GetItemFromViewState(PAGE_SIZE, 0);
			}
			set
			{
				ViewState[PAGE_SIZE] = value;
			}
		}

		public int PageNumber
		{
			get
			{
				return GetItemFromViewState(PAGE_NUMBER, 0);
			}
			set
			{
				ViewState[PAGE_NUMBER] = value;
			}
		}
		#endregion

		#region javascript and html related properties
		private string _tableID
		{
			get
			{
				return string.Concat(ClientID, "_Table");
			}
		}

		private string _tablePagerID
		{
			get
			{
				return string.Concat(ClientID, "_Table_Pager");
			}
		}

		public override string DataRoot
		{
			get
			{
				return GetItemFromViewState(FOR, "DataSource");
			}
			set
			{
				ViewState[FOR] = value;
			}
		}

		#endregion

		#region Style properties
		public string TableClass
		{
			get
			{
				return GetItemFromViewState("TableClass", string.Empty);
			}
			set
			{
				ViewState["TableClass"] = value;
			}
		}

		public string RowCssClass
		{

			get
			{
				return GetItemFromViewState("RowOddClass", ClientHelper.CssRowClass);
			}
			set
			{
				ViewState["RowOddClass"] = value;
			}
		}


		public string AlternatingRowCssClass
		{

			get
			{
				return GetItemFromViewState("RowEvenClass", ClientHelper.CssAltRowClass);
			}
			set
			{
				ViewState["RowEvenClass"] = value;
			}
		}


		#endregion

		#region Empty Text when no records
		public bool IsShowEmptyText
		{

			get
			{
				return GetItemFromViewState("IsShowEmtpyTextWhenNoRecords", false);
			}
			set
			{
				ViewState["IsShowEmtpyTextWhenNoRecords"] = value;
			}
		}

		public string EmptyText
		{

			get
			{
				return GetItemFromViewState("EmptyText", "No Data Found.");
			}
			set
			{
				ViewState["EmptyText"] = value;
			}
		}
		#endregion
	}
}
