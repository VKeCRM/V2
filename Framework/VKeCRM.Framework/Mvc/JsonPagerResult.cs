using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VKeCRM.Common.Collections;

namespace VKeCRM.Framework.Mvc
{
	public class JsonPagerResult : JsonResult
	{

		#region For pager information
		public int TotalPages
		{
			get;
			set;
		}

		public int PageSize
		{
			get;
			set;
		}

		public int PageNumber
		{
			get;
			set;
		}

		public int TotalRecordsCount
		{
			get; set;
		}
		#endregion

		public JsonPagerResult(object dataSource, PagingOptions pagingOptions, int totalRecordCount)
			: base(dataSource)
		{
            InitJsonPagerResult(dataSource, pagingOptions, totalRecordCount);
		}
        /// <summary>
        /// can be passed errorCode JsonPagerResult
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="dataSource"></param>
        /// <param name="pagingOptions"></param>
        /// <param name="totalRecordCount"></param>
        public JsonPagerResult(int errorCode, object dataSource, PagingOptions pagingOptions, int totalRecordCount)
            : base(dataSource)
        {
            Error = errorCode;
            InitJsonPagerResult(dataSource, pagingOptions, totalRecordCount);
        }
        /// <summary>
        /// can be passed error code message JsonPagerResult
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <param name="dataSource"></param>
        /// <param name="pagingOptions"></param>
        /// <param name="totalRecordCount"></param>
        public JsonPagerResult(int errorCode, string errorMessage, object dataSource, PagingOptions pagingOptions, int totalRecordCount)
            : base(dataSource)
        {
            Error = errorCode;
            ErrorMessage = errorMessage;
            InitJsonPagerResult(dataSource,pagingOptions, totalRecordCount);
        }
        /// <summary>
        /// Init JsonPagerResult
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="pagingOptions"></param>
        /// <param name="totalRecordCount"></param>
        private void InitJsonPagerResult(object dataSource, PagingOptions pagingOptions, int totalRecordCount)
        {
            DataSource = dataSource;
            PageSize = pagingOptions.PageSize;
            PageNumber = pagingOptions.PageNumber;
            TotalPages = (totalRecordCount / pagingOptions.PageSize);
            TotalRecordsCount = totalRecordCount;


            if (0 < (totalRecordCount % pagingOptions.PageSize))
            {
                TotalPages++;
            }
        }
	}
}
