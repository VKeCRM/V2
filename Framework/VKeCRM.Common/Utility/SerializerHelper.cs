using System;
using System.Collections.Generic;

using System.Xml.Serialization;
using System.IO;

namespace VKeCRM.Common.Utility
{
	public static class SerializerHelper
	{
		/// <summary>
		/// Serializer an object to xml data.
		/// </summary>
		/// <param name="instance">object instance</param>
		/// <returns>xml string</returns>
		public static string Object2XmlString(object instance)
		{
			XmlSerializer serializer = new XmlSerializer(instance.GetType());

			string result = null;

			using (StringWriter writer = new StringWriter())
			{
				serializer.Serialize(writer, instance);
				result = writer.ToString();
			}

			return result;
		}

		/// <summary>
		/// Convert xml string to object
		/// </summary>
		/// <param name="str">xml string</param>
		/// <param name="type">Target type</param>
		/// <returns>intance of the object.</returns>
		public static object String2Object(string str, Type type)
		{
			XmlSerializer serializer = new XmlSerializer(type);

			object obj;

			using (StringReader reader = new StringReader(str))
			{
				obj = serializer.Deserialize(reader);
			}

			return obj;
		}
	}


}
