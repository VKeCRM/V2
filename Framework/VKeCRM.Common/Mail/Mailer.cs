//-----------------------------------------------------------------------
// <copyright file="Mailer.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using VKeCRM.Common.Mail;
using VKeCRM.Common.Exceptions;
using VKeCRM.Common.Serialization;

namespace VKeCRM.Common.Mail
{
	/// <summary>
	/// To handle all email related logic
	/// </summary>
	public class Mailer
	{
		private const string MailDataRoot = "MailDataRoot";

		#region  Private Fields

		/// <summary>
		/// SMTP Host for email
		/// </summary>
		protected string _smtpHost;

		/// <summary>
		/// Url of XSLT
		/// </summary>
		protected string _xsltUrl;

		/// <summary>
		/// Subject of email
		/// </summary>
		protected string _subject = string.Empty;

		/// <summary>
		/// Text body of email
		/// </summary>
		protected string _textBody = string.Empty;

		/// <summary>
		/// HTML body fo email
		/// </summary>
		protected string _htmlBody = string.Empty;

		/// <summary>
		/// CC email address
		/// </summary>
		protected string _cc = string.Empty;

		/// <summary>
		/// Name of email receiver
		/// </summary>
		protected string _emailName = string.Empty;

		/// <summary>
		/// Email address of sender
		/// </summary>
		protected string _from = string.Empty;

		/// <summary>
		/// Email address to send reply to
		/// </summary>
		protected string _replyTo = string.Empty;

		/// <summary>
		/// Relative URL for email
		/// </summary>
		protected string _relativeURL = string.Empty;

		/// <summary>
		/// Email address of receiver
		/// </summary>
		protected string _to = string.Empty;

		/// <summary>
		/// A dictionary object of XML elements
		/// </summary>
		protected Dictionary<string, object> _xmlElements = new Dictionary<string, object>();

		#endregion  // Private Fields

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Mailer class
		/// </summary>
		public Mailer()
		{
		}

		/// <summary>
		/// <para>
		/// Initializes a new instance of the Mailer class. Use this constuctor when transforming only. 
		/// The intention is to transform and save the results to the email queue service for later delivery.
		/// </para>
		/// </summary>
		/// <param name="xsltUrl">Url where the email templates are located. Should be a configuration setting.</param>
		/// <param name="emailName">The name of the email template.</param>
		/// <param name="relativeURL">The URL of the template relative to the xsltHost.</param>
		public Mailer(string xsltUrl, string emailName, string relativeURL)
		{
			_xsltUrl = xsltUrl;
			_emailName = emailName;
			_relativeURL = relativeURL;
		}

		/// <summary>
		/// <para>
		/// Initializes a new instance of the Mailer class. Use this constuctor when transforming only. 
		/// Use this constuctor when transforming and sending the email immediately. The intention is to 
		/// transform and immediately send. You may still save the results.
		/// </para>
		/// </summary>
		/// <param name="smtpHost">Host name or address of the SMTP server. Should be a configuration setting.</param>
		/// <param name="xsltUrl">Url where the email templates are located. Should be a configuration setting.</param>
		/// <param name="emailName">The name of the email template.</param>
		/// <param name="relativeURL">The URL of the template relative to the xsltHost.</param>
		public Mailer(string smtpHost, string xsltUrl, string emailName, string relativeURL)
		{
			_smtpHost = smtpHost;
			_xsltUrl = xsltUrl;
			_emailName = emailName;
			_relativeURL = relativeURL;
		}

		#endregion // Constructors

		#region Public Properties

		/// <summary>
		/// Gets or sets the SMTP Host
		/// </summary>
		public string SmtpHost
		{
			get
			{
				return _smtpHost;
			}

			set
			{
				if (value != null)
				{
					_smtpHost = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the XSLt Url
		/// </summary>
		public string XsltUrl
		{
			get
			{
				return _xsltUrl;
			}

			set
			{
				if (value != null)
				{
					_xsltUrl = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Email Name
		/// </summary>
		public string EmailName
		{
			get
			{
				return _emailName;
			}

			set
			{
				if (value != null)
				{
					_emailName = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the relative URL
		/// </summary>
		public string RelativeURL
		{
			get
			{
				return _relativeURL;
			}

			set
			{
				if (value != null)
				{
					_relativeURL = value;
				}
			}
		}
		#endregion // Public Properties

		#region Public Readonly Properties

		/// <summary>
		/// Gets the From address
		/// </summary>
		public string From
		{
			get
			{
				return _from;
			}
		}

		/// <summary>
		/// Gets the To address
		/// </summary>
		public string To
		{
			get
			{
				return _to;
			}
		}


		/// <summary>
		/// Gets the ReplyTo address
		/// </summary>
		public string ReplyTo
		{
			get
			{
				return _replyTo;
			}
		}

		/// <summary>
		/// Gets the CC address
		/// </summary>
		public string Cc
		{
			get
			{
				return _cc;
			}
		}

		/// <summary>
		/// Gets the Subject of the email
		/// </summary>
		public string Subject
		{
			get
			{
				return _subject;
			}
		}

		/// <summary>
		/// Gets the HTML body of the email
		/// </summary>
		public string HtmlBody
		{
			get
			{
				return _htmlBody;
			}
		}

		/// <summary>
		/// Gets the Text body of the email
		/// </summary>
		public string TextBody
		{
			get
			{
				return _textBody;
			}
		}

		#endregion // Public Readonly Properties

		/// <summary>
		/// Add XML elements
		/// </summary>
		/// <param name="name">Name of the element to add</param>
		/// <param name="value">Value of the element</param>
		/// <returns>Returns a boolean value indicating whether element was added successfully or not</returns>
		public bool AddElement(string name, object value)
		{
			bool result = false;

			try
			{
				if (_xmlElements.ContainsKey(name))
				{
					_xmlElements[name] = value;
				}
				else
				{
					_xmlElements.Add(name, value);
				}

				result = true;
			}
			catch
			{
				// TODO: figure out what to do here
			}

			return result;
		}

		#region Helper Methods

		/// <summary>
		/// This method loads email template and formats the dynamic content. When it returns, the 
		/// to, cc, from, subject, html body, and text body properties may be used to save the 
		/// transformed email.
		/// </summary>
		/// <returns>true if the process was successful.</returns>
		public bool TransformMail()
		{
			try
			{
				return ProcessTransformedXslt();
			}
			catch (System.Exception ex)
			{
				// Debug.WriteToEventLog((ex.InnerException!=null) ? ex.InnerException.ToString() : ex.ToString(), Debug.EventLogEntryType.Error);
				throw new EmailFailedException(ex.InnerException ?? ex);
			}
		}

		/// <summary>
		/// This method loads email template and formats the dynamic content. It also sends the email using the host provided.
		/// When it returns, the to, cc, from, subject, html body, and text body properties may be used to save the 
		/// transformed email.
		/// </summary>
		/// <returns>true if the process and send was successful.</returns>
		public bool TransformAndSend()
		{
			bool result = false;

			try
			{
				if (ProcessTransformedXslt())
				{
					SmtpClient client = new SmtpClient(_smtpHost);
					MailMessage msg = new MailMessage();

					msg.To.Add(_to);
					msg.CC.Add(_cc);
					msg.From = new MailAddress(_from);
					msg.Subject = _subject;

					// Nothing to send
					if (!(string.IsNullOrEmpty(_htmlBody) && string.IsNullOrEmpty(_textBody)))
					{
						if (!string.IsNullOrEmpty(_htmlBody))
						{
							msg.Body = _htmlBody;
							msg.IsBodyHtml = true;

							if (!string.IsNullOrEmpty(_textBody))
							{
								msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(_textBody, new ContentType(MediaTypeNames.Text.Plain)));
							}
						}
						else
						{
							msg.Body = _textBody;
							msg.IsBodyHtml = false;
						}

						client.Send(msg);

						result = true;
					}
				}
			}
			catch (EmailFailedException)
			{
				// Debug.WriteToEventLog(ef.ToString(), Debug.EventLogEntryType.Error);
				throw;
			}
			catch (System.Exception ex)
			{
				// Debug.WriteToEventLog((ex.InnerException!=null) ? ex.InnerException.ToString() : ex.ToString(), Debug.EventLogEntryType.Error);
				throw new EmailFailedException(ex.InnerException ?? ex);
			}

			return result;
		}

		/// <summary>
		/// To get the XSLT for Url
		/// </summary>
		/// <returns>Returns the XSLT for url</returns>
		protected string GetXsltUrl()
		{
			return _xsltUrl; // + (_xsltUrl.EndsWith("/") ? string.Empty : "/") + _relativeURL;
		}

		/// <summary>
		/// To load and transform XSLT
		/// </summary>
		/// <returns>Returns the transformed XSLT</returns>
		protected string LoadAndTransformXslt()
		{
			string result = string.Empty;

			try
			{
				XslCompiledTransform xsltTransform = new XslCompiledTransform();

				// get necessary infomations
				// 1. load xslt template
				XmlReader xsltTemplate = null;
				GetStylesheet(GetXsltUrl(), xsltTransform, out xsltTemplate);

				// 2. get info about "from", "to", "mail content" and etc.
				XPathDocument xPathDocument = null;
				string xmlData = GetXmlData();
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
			}
			catch (System.Exception ex)
			{
				throw new EmailFailedException(ex);
			}

			return result;
		}

		/// <summary>
		/// To process the transformed XSLT
		/// </summary>
		/// <returns>Returns a boolean value indicating whether the process was a success or not</returns>
		protected bool ProcessTransformedXslt()
		{
			bool result;
			try
			{
				string message = LoadAndTransformXslt();

				// Find Body
				int htmlBodyStart = message.IndexOf("<HTML", StringComparison.InvariantCultureIgnoreCase);
				int htmlBodyEnd = message.IndexOf("=====", StringComparison.InvariantCultureIgnoreCase);

				if (htmlBodyEnd == -1)
				{
					if (htmlBodyStart != -1)
					{
						_htmlBody = message.Substring(htmlBodyStart);
					}
					else
					{
						// markup
						_htmlBody = message;
					}

					_textBody = String.Empty;
				}
				else
				{
					_htmlBody = message.Substring(htmlBodyStart, (htmlBodyEnd - htmlBodyStart));
					_textBody = message.Substring(htmlBodyEnd + 6);
				}
				string[] lines = new string[] { };
				if (htmlBodyStart != -1)
				{
					lines = message.Substring(0, htmlBodyStart).Split('\n');
				}
				else
				{
					lines = message.Split('\n'); ;
				}

				for (int index = 0; index < lines.Length; index++)
				{
					char[] whiteSpace = { ' ' };
					string line = lines[index].Replace("\r", string.Empty).Replace("\t", string.Empty).TrimStart(whiteSpace);
					if (line.StartsWith("from:"))
					{
						_from = line.Substring(5).Trim();
					}
					else if (line.StartsWith("to:"))
					{
						_to = line.Substring(3).Trim();
					}
					else if (line.StartsWith("cc:"))
					{
						_cc = (line.Length > 3) ? line.Substring(3).Trim() : string.Empty;
					}
					else if (line.StartsWith("replyto:"))
					{
						_replyTo = (line.Length > 8) ? line.Substring(8).Trim() : string.Empty;
					}
					else if (line.StartsWith("subject:"))
					{
						_subject = (line.Length > 8) ? line.Substring(8).Trim() : string.Empty;
					}
				}

				result = true;
			}
			catch (EmailFailedException)
			{
				// Debug.WriteToEventLog(ef.ToString(), Debug.EventLogEntryType.Error);
				throw;
			}
			catch (System.Exception ex)
			{
				// Debug.WriteToEventLog((ex.InnerException!=null) ? ex.InnerException.ToString() : ex.ToString(), Debug.EventLogEntryType.Error);
				throw new EmailFailedException(ex.InnerException ?? ex);
			}

			return result;
		}

		/// <summary>
		/// To check if object is serializable
		/// </summary>
		/// <param name="o">Object to check for serialization</param>
		/// <returns>Returns a boolean value indicating whether the object is serializable</returns>
		private static bool IsSerializable(object o)
		{
			bool result = false;

			try
			{
				object[] attrs = o.GetType().GetCustomAttributes(typeof(SerializableAttribute), true);
				result = (attrs.Length == 1);
			}
			catch
			{
				// TODO: figure out what to do here      
			}

			return result;
		}

		/// <summary>
		/// To get the stylesheet
		/// </summary>
		/// <param name="xslt">Returns the stylesheet</param>
		/// <param name="url">URL to create a web request</param>
		private void GetStylesheet(string url, XslCompiledTransform xsltTransform, out XmlReader xmlReader)
		{
			string xsltText = VKeCRM.Common.Utility.XsltTransformHelper.GetXsltContentByPath(url);

			using (StringReader sReader = new StringReader(xsltText))
			{
				xmlReader = new XmlTextReader(sReader);
				xsltTransform.Load(xmlReader);
			}
		}

		/// <summary>
		/// XML string writer
		/// </summary>
		/// <returns>Returns an XML string</returns>
		private string GetXmlData()
		{
			StringWriter writer = new StringWriter();
			XmlTextWriter xmlWriter = new XmlTextWriter(writer);

			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement(MailDataRoot);

			foreach (string name in _xmlElements.Keys)
			{
				object item = _xmlElements[name];

				if (item != null)
				{
					if ((item is string) || (!IsSerializable(item)))
					{
						xmlWriter.WriteElementString(name, _xmlElements[name].ToString());
					}
					else
					{
						XmlStringPersister p = new XmlStringPersister(item.GetType(), string.Empty);
						p.Serialize(item);
						string xml = Encoding.UTF8.GetString(Convert.FromBase64String(p.Buffer));
						XmlDocument doc = new XmlDocument();
						doc.LoadXml(xml);

						xmlWriter.WriteStartElement(name);
						xmlWriter.WriteRaw(doc.ChildNodes[1].InnerXml);
						xmlWriter.WriteEndElement();
					}
				}
			}

			xmlWriter.WriteEndElement();
			xmlWriter.Close();
			string resultXMLString = writer.ToString();
			return resultXMLString;
		}

		#endregion // Helper Methods
	}
}