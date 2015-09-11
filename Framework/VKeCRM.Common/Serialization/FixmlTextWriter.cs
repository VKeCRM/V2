using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace VKeCRM.Common.Serialization
{
	/// <summary>
	/// This serializer will skip the the xml declaration (which somehow SVI doesn't like) and also the default xsi/xsd namespace
	/// </summary>
	public class FixmlTextWriter : XmlTextWriter
	{
		public FixmlTextWriter(TextWriter w)
			: base(w) { }

		public FixmlTextWriter(Stream w, Encoding encoding)
			: base(w, encoding) { }

		public FixmlTextWriter(string filename, Encoding encoding)
			: base(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None), encoding) { }

		bool _skip = false;

		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			//Omits XSD and XSI declarations.
			if (prefix == "xmlns" && (localName == "xsd" || localName == "xsi"))
			{
				_skip = true;
				return;
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}

		public override void WriteString(string text)
		{
			if (_skip) return;
			base.WriteString(text);
		}

		public override void WriteEndAttribute()
		{
			if (_skip)
			{
				//reset the flag, keep writing.
				_skip = false;
				return;
			}
			base.WriteEndAttribute();
		}

		//skip the xml declaration
		public override void WriteStartDocument() { }
	}
}
