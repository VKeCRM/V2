using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VKeCRM.Common.Serialization
{
	public sealed class FixmlSerialize
	{
		#region Singleton Implementation

		private static FixmlSerialize _instance = null;

		static FixmlSerialize()
		{
			_instance = new FixmlSerialize();
		}

		private FixmlSerialize()
		{
		}

		public static FixmlSerialize Instance
		{
			get
			{
				return _instance;
			}
		}

		#endregion

		private static object _sync = new object();

		private Dictionary<Type, XmlSerializer> _serializers = new Dictionary<Type, XmlSerializer>();

		public string ToXml<T>(T value)
		{
			MemoryStream ms = new MemoryStream();
			FixmlTextWriter xmlWriter = new FixmlTextWriter(ms, Encoding.ASCII);
			try
			{
				XmlSerializer serializer = CreateSerializer<T>();
				serializer.Serialize(xmlWriter, value);
				return new ASCIIEncoding().GetString((xmlWriter.BaseStream as MemoryStream).ToArray());
			}
			finally
			{
				xmlWriter.Close();
			}
		}

		public T FromXml<T>(string xml)
		{
			XmlSerializer serializer = this.CreateSerializer<T>();
			StringReader stringReader = null;
			try
			{
				stringReader = new StringReader(xml);
				return (T)serializer.Deserialize(stringReader);
			}
			finally
			{
				if (stringReader != null)
				{
					stringReader.Close();
				}
			}
		}

		/// <summary>
		/// Creates the serializer, if it exists in the cache, get it from cache.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		private XmlSerializer CreateSerializer<T>()
		{
			Type typeOfT = typeof(T);

			lock (_sync)
			{
				if (!_serializers.ContainsKey(typeOfT))
				{
					XmlSerializer newSerializer = new XmlSerializer(typeOfT);
					_serializers.Add(typeOfT, newSerializer);
				}
				return _serializers[typeOfT];
			}
		}
	}
}
