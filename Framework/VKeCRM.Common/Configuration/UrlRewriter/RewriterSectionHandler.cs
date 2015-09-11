//-----------------------------------------------------------------------
// <copyright file="RewriterSectionHandler.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace VKeCRM.Common.Configuration.UrlRewriter
{
	/// <summary>
    /// <para>
	/// Deserializes the markup in Web.config into an instance of 
	/// the <see cref="RewriterConfiguration"/> class.
    /// </para>
	/// </summary>
	public class RewriterSectionHandler : IConfigurationSectionHandler
	{
		#region IConfigurationSectionHandler Members

        /// <summary>
        /// Create a deserializer
        /// </summary>
        /// <param name="parent">Parent object</param>
        /// <param name="configContext">Configuration context</param>
        /// <param name="section">Section to deserialize</param>
        /// <returns>Returns the deserialized section</returns>
		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			XmlSerializer ser = new XmlSerializer(typeof(RewriterConfiguration));

			// Return the Deserialized object from the Web.config XML
			return ser.Deserialize(new XmlNodeReader(section));
		}
		#endregion
	}
}
