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
    public sealed class XmlByteArrayPersister : XmlPersister
    {
        #region Private Fields

        private byte[] _buffer;

        #endregion // Private Fields

        #region Public Properties

        #endregion // Public Properties

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        public XmlByteArrayPersister(Type graphType) : this(graphType, new byte[] {})
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="buffer"></param>
        public XmlByteArrayPersister(Type graphType, byte[] buffer) : this(graphType, null, false, buffer)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="provider"></param>
        /// <param name="buffer"></param>
        public XmlByteArrayPersister(Type graphType, ICryptoProvider provider, byte[] buffer) : this(graphType, provider, true, buffer)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="provider"></param>
        /// <param name="useEncryption"></param>
        /// <param name="buffer"></param>
        public XmlByteArrayPersister(Type graphType, ICryptoProvider provider, bool useEncryption, byte[] buffer) : base(graphType, provider, useEncryption)
        {
            Buffer = buffer;
        }

        public byte[] Buffer
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

                Buffer = (Byte[]) bytes.Clone();
            }
        }

        public override object Deserialize()
        {
            object result;

            if (GraphType == null) throw new NullReferenceException(ErrorMessages.Persister_GraphTypeRequired);

            // Create serializer
            XmlSerializer s = new XmlSerializer(GraphType);

            // Allocate buffer
            byte[] bytes = (byte[]) Buffer.Clone();

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