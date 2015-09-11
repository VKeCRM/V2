//-----------------------------------------------------------------------
// <copyright file="NavigationHelper.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace VKeCRM.Framework.Web.Helper
{
    /// <summary>
    /// Helper class
    /// </summary>
    public class NavigationHelper
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the NavigationHelper class
        /// </summary>
		static NavigationHelper() 
        {
        }

        /// <summary>
        /// Prevents a default instance of the NavigationHelper class from being created
        /// </summary>
        private NavigationHelper()
        {
        }
        #endregion

        #region Logic to clean up "unique" querystring

        /// <summary>
        /// To remove unique query string parameters from the given url
        /// </summary>
        /// <param name="url">URL to clean up</param>
        /// <returns>Returns the URL without the unique query string parameter</returns>
        public static string RemoveExtraUniqueParams(string url)
        {
            string stringToSearch = string.Empty;
            string stringUniqueNo = string.Empty;

            if (url.Contains("&unique"))
            {
                stringUniqueNo = GetUniqueNumber(url, "&unique").ToString();
                stringToSearch = "&unique=" + stringUniqueNo;

                if (url.Contains(stringToSearch))
                {
                    url = url.Replace(stringToSearch, string.Empty);
                }
            }

            if (url.Contains("?unique"))
            {
                stringUniqueNo = GetUniqueNumber(url, "?unique").ToString();
                stringToSearch = "?unique=" + stringUniqueNo;

                if (url.Contains(stringToSearch))
                {
                    url = url.Replace(stringToSearch, string.Empty);
                }
            }

            return url;
        }

        /// <summary>
        /// To get the unique number in the unique query string parameter of URL
        /// </summary>
        /// <param name="url">URL to get unique number</param>
        /// <param name="tag">To check for tag in the URL</param>
        /// <returns>Returns the unique number from the URL</returns>
        private static int GetUniqueNumber(string url, string tag)
        {
            int retVal = 0;
            if (url.ToLower().Contains(tag))
            {
                int idx = url.ToLower().IndexOf(tag);
                string tempString = url.Substring(idx + tag.Length + 1);
                string tempVal = string.Empty;
                int length = ((tempString.Length > 4) ? 4 : tempString.Length);
				for (int idx2 = 0; idx2 < length; idx2++)
                {
                    if (IsNumeric(tempString.Substring(idx2, 1)))
                    {
                        tempVal += tempString.Substring(idx2, 1);
                    }
                }

                if (!string.IsNullOrEmpty(tempVal))
                {
                    retVal = int.Parse(tempVal);
                }
            }

            return retVal;
        }

        /// <summary>
        /// To check if a given expression is numeric
        /// </summary>
        /// <param name="expression">Expression to check</param>
        /// <returns>Returns a boolean value that indicates whether the given expression is numeric or not</returns>
        private static bool IsNumeric(object expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.

            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.
            isNum = Double.TryParse(Convert.ToString(expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);

            return isNum;
        }

        #endregion
    }
}
