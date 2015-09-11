using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace VKeCRM.Common.Mail
{
	public class SmtpHelper
	{
		public SmtpHelper(string smtpHost)
		{
			SmtpHost = smtpHost;
		}

		public string SmtpHost
		{
			get;
			set;
		}

		public void SendDirectly(string from, string to, string subject, string body, byte[] attachment, string attachmentContentType)
		{
			MailMessage mailMessage = new MailMessage(from, to, subject, body);

			using (MemoryStream stream = new MemoryStream(attachment))
			{
				Attachment attached = new Attachment(stream, new ContentType(attachmentContentType));
				mailMessage.Attachments.Add(attached);
				SendDirectly(mailMessage);
			}
		}

		public void SendDirectly(MailMessage mailMessage)
		{
			SmtpClient client = new SmtpClient(SmtpHost);
			client.ServicePoint.MaxIdleTime = 1000;
			client.Send(mailMessage);
		}
	}
}
