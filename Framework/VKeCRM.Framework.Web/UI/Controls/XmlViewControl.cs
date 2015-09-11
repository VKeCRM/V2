using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace VKeCRM.Framework.Web.UI.Controls
{
	[DefaultProperty("Text"),
	 ToolboxData("<{0}:XmlViewControl runat=server></{0}:XmlViewControl>")]
	public class XmlViewControl : System.Web.UI.WebControls.WebControl
	{
		private string _innerXml;

		[Bindable(true),
		 Category("Appearance"),
		 DefaultValue("")]
		public string InnerXml
		{
			get
			{
				return _innerXml;
			}

			set
			{
				_innerXml = value;
			}
		}



		/// <summary>
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			output.RenderBeginTag(HtmlTextWriterTag.Div);

			if (!string.IsNullOrEmpty(InnerXml))
			{
				XslTransform transform = new XslTransform();

				XmlDocument doc = new XmlDocument();
				doc.LoadXml(GetStringResource("XmlViewXslt.xslt"));

				transform.Load(doc.CreateNavigator(), new System.Xml.XmlUrlResolver());

				StringReader x = new StringReader(InnerXml);
				XPathDocument xPathDoc = new XPathDocument(x);
				transform.Transform(xPathDoc, null, output, null);
			}

			output.RenderEndTag();
		}

		private static string GetStringResource(string resourceName)
		{
			System.Reflection.Assembly myAssembly;
			string internalResourceName;

			myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
			internalResourceName = string.Format("{0}.{1}", myAssembly.GetName().Name, resourceName);
			string[] resourceNames;

			try
			{
				resourceNames = myAssembly.GetManifestResourceNames();
				Array.Sort(resourceNames);

				foreach (string name in resourceNames)
				{
					if ((string.Compare(name, internalResourceName, true) == 0))
					{
						internalResourceName = name;
					}
				}
				System.IO.Stream resource = myAssembly.GetManifestResourceStream(internalResourceName);
				byte[] data = new Byte[resource.Length];
				resource.Read(data, 0, (int)resource.Length);
				return System.Text.ASCIIEncoding.ASCII.GetString(data);
			}
			catch
			{
				throw new Exception(string.Format("String Resource '{0}' was not found", resourceName));
			}
		}
	}
}

