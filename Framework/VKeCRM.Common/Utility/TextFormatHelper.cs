using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Utility
{
	public static class TextFormatHelper
	{
		private const int HeadlineSummaryLength = 150;
		public static string EncodeHtmlText(string input)
		{
			// here must first decode,
			// some text already been Encode at community
			// HtmlEncode will encode "&lt;" to "&amp;lt;",
			// so need to declode first.
			var temp = System.Web.HttpUtility.HtmlDecode(input);

			return System.Web.HttpUtility.HtmlEncode(temp);
		}

		/// <summary>
		/// Get the summary of the given text
		/// </summary>
		/// <param name="text">
		///     Set the original text
		/// </param>
		/// <returns>
		///     Return the summary of the given text
		/// </returns>
		public static string GetSummaryText(string input)
		{
			input = System.Web.HttpUtility.HtmlDecode(input);
			System.Text.RegularExpressions.Regex htmlTag = new System.Text.RegularExpressions.Regex("\n", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
			input= htmlTag.Replace(input, string.Empty);

			htmlTag = new Regex(@"<(.[^>]*)>", RegexOptions.IgnoreCase);


			string stripHtml = htmlTag.Replace(input, string.Empty);

			if (HeadlineSummaryLength < 1 || stripHtml.Length <= HeadlineSummaryLength)
			{
				return stripHtml;
			}

			int index = HeadlineSummaryLength - 1;

			if (stripHtml[index] == ',' || stripHtml[index] == '.')
			{
				return stripHtml.Substring(0, HeadlineSummaryLength);
			}

			index = stripHtml.Substring(0, HeadlineSummaryLength).LastIndexOf(' ');
			if (index < 0)
				return stripHtml.Substring(0, HeadlineSummaryLength);
			else
				return stripHtml.Substring(0, index).TrimEnd();
		}
	}
}
