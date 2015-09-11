//-----------------------------------------------------------------------
// <copyright file="RewriterRuleCollection.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections;

namespace VKeCRM.Common.Configuration.UrlRewriter
{
	/// <summary>
	/// The RewriterRuleCollection models a set of RewriterRules in the Web.config file.
	/// </summary>
	/// <remarks>
    /// <para>
	/// The RewriterRuleCollection is expressed in XML as:
	/// <code>
	/// &lt;rewriterRule lookFor="<i>pattern to search for</i>" sendTo="<i>string to redirect to</i>" /&gt;
	/// ...
	/// &lt;rewriterRule lookFor="<i>pattern to search for</i>" sendTo="<i>string to redirect to</i>" /&gt;
	/// </code>
    /// </para>
	/// </remarks>
	[Serializable]
	public class RewriterRuleCollection : CollectionBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a RewriterRule at a specified ordinal index.
        /// </summary>
        /// <param name="index">Ordinal index</param>
        /// <returns></returns>
		public RewriterRule this[int index]
		{
			get
			{
				return (RewriterRule)this.InnerList[index];
			}

			set
			{
				this.InnerList[index] = value;
			}
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new RewriterRule to the collection.
        /// </summary>
        /// <param name="rule">A RewriterRule instance.</param>
        public virtual void Add(RewriterRule rule)
        {
            this.InnerList.Add(rule);
        }

        #endregion
    }
}
