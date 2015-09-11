//-----------------------------------------------------------------------
// <copyright file="RewriterConfiguration.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Xml.Serialization;

namespace VKeCRM.Common.Configuration.UrlRewriter
{
    /// <summary>
    /// To rewrite configuration from web.config
    /// </summary>
	[Serializable]
	[XmlRoot("urlRewriter")]
	public class RewriterConfiguration
    {
        #region Fields
        /// <summary>
        /// Name of rewriter
        /// </summary>
        private const string UrlRewriterName = "urlRewriter";

        /// <summary>
        /// Collection of rules
        /// </summary>
        private RewriterRuleCollection _rules;

        #endregion

        #region Public Properties
        /// <summary>
		/// Gets or sets an <see cref="RewriterRuleCollection"/> instance that provides access to a set of <see cref="RewriterRule"/>s.
		/// </summary>
		[XmlArrayItem(ElementName = "rewriterRule", Type = typeof(RewriterRule))]
		[XmlArray(ElementName = "rules")]
		public RewriterRuleCollection Rules
		{
			get
			{
				return _rules;
			}

			set
			{
				_rules = value;
			}
		}
		#endregion

        #region Private Properties
        /// <summary>
        /// Gets a value indicating whether httpContent is null or not
        /// </summary>
        private static bool IsInWebContext
        {
            get
            {
                return HttpContext.Current != null;
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// <para>
        /// GetConfig() returns an instance of the <b>RewriterConfiguration</b> class with the values populated from
        /// the Web.config file (for the web context).  It uses XML deserialization to convert the XML structure in Web.config into
        /// a <b>RewriterConfiguration</b> instance.
        /// </para>
        /// </summary>
        /// <remarks>
        /// <para>
        /// Web.Config
        /// <configSections>
        ///		<section name="urlRewriter" type="VKeCRM.Common.Configuration.UrlRewriter.RewriterSectionHandler, VKeCRM.Common.Configuration" />
        ///	</configSections>
        /// <urlRewriter>
        ///		<rules>
        ///			<rewriterRule name="aaa" lookFor="/trading/Faq.aspx" sendTo="~/aboutus/Faq.aspx?tab=trading" />
        ///			...
        ///			<rewriterRule name="bbb" lookFor="/trading/Complex-Options.aspx" sendTo="~/aboutus/faq.aspx?tab=trading&amp;faq=7" />
        ///		</rules>
        /// </urlRewriter>
        /// </para>
        /// </remarks>
        /// <returns>A <see cref="RewriterConfiguration"/> instance.</returns>
        public static RewriterConfiguration GetConfig()
        {
            if (IsInWebContext)
            {
                // Web application
                if (HttpContext.Current.Cache[UrlRewriterName] == null)
                {
                    try
                    {
                        object urlRewriter = WebConfigurationManager.GetSection(UrlRewriterName);

                        if (urlRewriter != null)
                        {
                            HttpContext.Current.Cache.Insert(UrlRewriterName, urlRewriter);
                        }
                        else
                        {
                            throw new Exception(string.Format("'{0}' section in the Web.config file is not found.", UrlRewriterName));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("'{0}' section in the Web.config file is not found.", UrlRewriterName), ex);
                    }
                }

                return (RewriterConfiguration)HttpContext.Current.Cache[UrlRewriterName];
            }
            else
            {
                // Windows application
                try
                {
                    object urlRewriter = ConfigurationManager.GetSection(UrlRewriterName);

                    if (urlRewriter != null)
                    {
                        return (RewriterConfiguration)urlRewriter;
                    }
                    else
                    {
                        throw new Exception(string.Format("'{0}' section in the configuration file is not found.", UrlRewriterName));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("'{0}' section in the configuration file is not found.", UrlRewriterName), ex);
                }
            }
        }
        #endregion
	}
}
