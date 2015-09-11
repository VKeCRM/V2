using System;
using System.IO;
using System.Xml.Serialization;

using VKeCRM.Common.Cryptography;
using ErrorMessages = VKeCRM.Common.Messages.Messages;

namespace VKeCRM.Common.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class XmlStringPersister : XmlPersister
    {
        #region Private Fields

        private string _buffer;

        #endregion // Private Fields

        #region Public Properties

        #endregion // Public Properties

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="graphType"></param>
        public XmlStringPersister(Type graphType) : this(graphType, String.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="buffer"></param>
        public XmlStringPersister(Type graphType, string buffer) : this(graphType, null, false, buffer)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="provider"></param>
        /// <param name="buffer"></param>
        public XmlStringPersister(Type graphType, ICryptoProvider provider, string buffer) : this(graphType, provider, true, buffer)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="provider"></param>
        /// <param name="useEncryption"></param>
        /// <param name="buffer"></param>
        public XmlStringPersister(Type graphType, ICryptoProvider provider, bool useEncryption, string buffer) : base(graphType, provider, useEncryption)
        {
            Buffer = buffer;
        }

        public string Buffer
        {
            get { return _buffer; }

            set { _buffer = value; }
        }

        #endregion // Constructors

        #region Serialization Methods

        public override void Serialize(object source)
        {
            if (source == null) throw new NullReferenceException(ErrorMessages.Persister_ObjectRequired);

            if (GraphType == null) throw new NullReferenceException(ErrorMessages.Persister_GraphTypeRequired);

            // Create serializer
            XmlSerializer s = new XmlSerializer(GraphType);

            // Create Stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Serialize the object graph to stream
                s.Serialize(ms, source);

                // Trim buffer
                ms.Capacity = (int) ms.Length;

                byte[] bytes;

                if (UseEncryption)
                {
                    if (_provider == null) throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);

                    // Reposition Stream
                    ms.Seek(0, SeekOrigin.Begin);

                    // Encrypt stream Contents
                    bytes = _provider.Encrypt(ms);
                }
                else
                {
                    // Retrieve stream contents
                    bytes = ms.ToArray();
                }

                // Store Base64 encoded string
                Buffer = Convert.ToBase64String(bytes);
            }
        }

        public override object Deserialize()
        {
            object result = null;

            if (GraphType == null) throw new NullReferenceException(ErrorMessages.Persister_GraphTypeRequired);

            // Create serializer
            XmlSerializer s = new XmlSerializer(GraphType);

            // Decode Base64 encoded string
            byte[] bytes = Convert.FromBase64String(Buffer);

            if (UseEncryption)
            {
                if (_provider == null) throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);

                // Decrypt from file stream
                bytes = _provider.Decrypt(new MemoryStream(bytes));
            }

            // Create stream using encrypted buffer
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                // Deserialize the object graph from stream
                result = s.Deserialize(ms);
            }

            return result;
        }

        #endregion // Serialization Methods
    }
}