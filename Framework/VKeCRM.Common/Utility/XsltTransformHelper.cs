using System;
using System.Collections.Generic;

using System.Xml.Xsl;
using System.IO;
using System.Xml.XPath;
using System.Xml;
using System.Net;

namespace VKeCRM.Common.Utility
{
	public static class XsltTransformHelper
	{
		private const string HostSurrenal = "%HOST%";

		/// <summary>
		/// Gets xml and xslt string
		/// </summary>
		/// <param name="xmlData">xml data</param>
		/// <param name="templateUrl">templateUrl or path</param>
		/// <returns>Transformed string</returns>
		public static string Transform2String(string xmlData, string templateUrl)
		{
			string result = string.Empty;

			//1. 
			XslCompiledTransform xsltTransform = new XslCompiledTransform();
			XmlReader xsltTemplate = null;

			string xsltText = GetXsltContentByPath(templateUrl);
			using (StringReader sReader = new StringReader(xsltText))
			{
				xsltTemplate = new XmlTextReader(sReader);
				xsltTransform.Load(xsltTemplate);
			}

			// 2.
			XPathDocument xPathDocument = null;
			using (StringReader stringReader = new StringReader(xmlData))
			{
				xPathDocument = new XPathDocument(stringReader);
			}

			// 3. Transform.
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					try
					{
						// transform, and set the result to StringWriter.
						xsltTransform.Transform(xPathDocument, xmlTextWriter);

						// assign to result.
						result = stringWriter.ToString();
					}
					finally
					{
						if (null != xsltTemplate)
						{
							xsltTemplate.Close();
						}
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Gets xslt content by path or url.
		/// </summary>
		/// <param name="path">path or url</param>
		/// <returns>xslt content</returns>
		public static string GetXsltContentByPath(string path)
		{
			string xsltText = string.Empty;

			if (path.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
			{
				int index = path.LastIndexOf('/');
				string xsltUrlPrefix = (index == -1) ? string.Empty : path.Substring(0, index);

				WebRequest request = WebRequest.Create(path);
				request.Credentials = CredentialCache.DefaultCredentials;
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				try
				{
					using (StreamReader reader = new StreamReader(response.GetResponseStream()))
					{
						xsltText = reader.ReadToEnd();
						xsltText = xsltText.Replace(HostSurrenal, xsltUrlPrefix);
					}
				}
				finally
				{
					response.Close();
				}
			}
			else
			{
				using (StreamReader reader = new StreamReader(path))
				{
					int index = path.LastIndexOf('\\');
					string xsltUrlPrefix = (index == -1) ? string.Empty : path.Substring(0, index);

					xsltText = reader.ReadToEnd();
					xsltText = xsltText.Replace(HostSurrenal, xsltUrlPrefix);
				}
			}

			return xsltText;
		}
	}
}
