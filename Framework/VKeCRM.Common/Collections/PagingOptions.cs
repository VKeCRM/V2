using System;
using System.Runtime.Serialization;
using System.Text;

namespace VKeCRM.Common.Collections
{
	/// <summary>
	/// Paging option frontend passes in to get a paged collection.
	/// </summary>
	[DataContract]
	public class PagingOptions
	{
		/// <summary>
		/// 1-based page number
		/// </summary>
		[DataMember]
		public int PageNumber { get; set; }

		/// <summary>
		/// Size of the page
		/// </summary>
		[DataMember]
		public int PageSize { get; set; }

        /// <summary>
        /// Set the starting Record,default PagingOption with Start=0.
		/// </summary>
		[DataMember]
		public int Start { get; set; }

		/// <summary>
		/// Whether the query will return the total number of records
		/// </summary>
		[DataMember]
		public bool FetchTotalRecordCount { get; set; }

		/// <summary>
		/// Column name for sorting, assuming that the value passed from frontend will match the column on the domain object.
		/// </summary>
		[DataMember]
		public string SortBy { get; set; }

		/// <summary>
		/// Whether the collection should be sort descending
		/// </summary>
		[DataMember]
		public bool SortDescending { get; set; }

		/// <summary>
        /// A default PagingOption with FetchTotalRecordCount=false, PageNumber=1, and PageSize=10,Start=0.
		/// </summary>
		public static PagingOptions Default
		{
			get
			{
				PagingOptions option = new PagingOptions();
				option.FetchTotalRecordCount = false;
				option.PageNumber = 1;
				option.PageSize = 10;
			    option.Start = 0;
				return option;
			}
		}

        /// <summary>
        /// Another default PagingOption with FetchTotalRecordCount=true, PageNumber=1, and PageSize=10,Start=0.
        /// </summary>
        public static PagingOptions FetchRecordCount
        {
            get
            {
                PagingOptions option = new PagingOptions();
                option.FetchTotalRecordCount = true;
                option.PageNumber = 1;
                option.PageSize = 10;
                option.Start = 0;
                return option;
            }
        }

		/// <summary>
        /// Another default PagingOption with FetchTotalRecordCount=true, PageNumber=0,Start=0.
		/// Only a select count(*) sql will be issued.
		/// </summary>
		public static PagingOptions FetchRecordCountOnly
		{
			get
			{
				PagingOptions option = new PagingOptions();
				option.FetchTotalRecordCount = true;
				option.PageNumber = 0;
                option.Start = 0;
				return option;
			}
		}

        public static PagingOptions Empty
        {
            get
            {
                PagingOptions option = new PagingOptions();
                option.FetchTotalRecordCount = false;
                option.PageNumber = 0;
                option.Start = 0;
                return option;
            }
        }

		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("{");
			builder.AppendFormat("\"{0}\":{1},", "PageNumber", PageNumber);
			builder.AppendFormat("\"{0}\":{1},", "PageSize", PageSize);
			builder.AppendFormat("\"{0}\":{1},", "Start", Start);
			builder.AppendFormat("\"{0}\":{1},", "SortBy", SortBy);
			builder.AppendFormat("\"{0}\":{1},", "SortDescending", SortDescending);
			builder.AppendFormat("\"{0}\":{1}", "FetchTotalRecordCount", FetchTotalRecordCount);
			builder.Append("}");
			return builder.ToString();
		}
	}
}
