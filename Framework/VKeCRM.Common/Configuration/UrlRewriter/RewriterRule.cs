//-----------------------------------------------------------------------
// <copyright file="RewriterRule.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Xml.Serialization;

namespace VKeCRM.Common.Configuration.UrlRewriter
{
	/// <summary>
    /// <para>
	/// Represents a rewriter rule.  A rewriter rule is composed of a string to search for and a string to replace
	/// the pattern with (if matched).
    /// </para>
	/// </summary>
	[Serializable]
	public class RewriterRule
    {
        #region Fields

        /// <summary>
        /// To string to look for
        /// </summary>
        private string _lookFor;

        /// <summary>
        /// The string to replace
        /// </summary>
		private string _sendTo;

        #endregion

        #region Public Properties
        /// <summary>
		/// Gets or sets the pattern to look for.
		/// </summary>
		/// <remarks>
        /// <para>
        /// <b>LookFor</b> is a regular expression pattern.  Therefore, you might need to escape
		/// characters in the pattern that are reserved characters in regular expression syntax (., ?, ^, $, etc.).
		/// <p />
		/// The pattern is searched for using the <b>System.Text.RegularExpression.Regex</b> class's <b>IsMatch()</b>
		/// method.  The pattern is case insensitive.
        /// </para>
        /// </remarks>
		[XmlAttribute("lookFor")]
		public string LookFor
		{
			get
			{
				return _lookFor;
			}

			set
			{
				_lookFor = value;
			}
		}

		/// <summary>
		/// Gets or sets the string to replace the pattern with, if found.
		/// </summary>
		/// <remarks>
        /// <para>
        /// The replacement string may use grouping symbols, like $1, $2, etc.  Specifically, the
		/// <b>System.Text.RegularExpression.Regex</b> class's <b>Replace()</b> method is used to replace
		/// the match in <see cref="LookFor"/> with the value in <b>SendTo</b>.
        /// </para>
        /// </remarks>
		[XmlAttribute("sendTo")]
		public string SendTo
		{
			get
			{
				return _sendTo;
			}

			set
			{
				_sendTo = value;
			}
		}
		#endregion
	}
}