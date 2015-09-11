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
    public sealed class XmlFilePersister : XmlPersister
    {
        #region Private Fields

        private string _fileName;

        #endregion // Private Fields

        #region Public Properties

        #endregion // Public Properties

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        public XmlFilePersister(Type graphType) : this(graphType, String.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="fileName"></param>
        public XmlFilePersister(Type graphType, string fileName) : this(graphType, null, false, fileName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="provider"></param>
        /// <param name="fileName"></param>
        public XmlFilePersister(Type graphType, ICryptoProvider provider, string fileName) : this(graphType, provider, true, fileName)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graphType"></param>
        /// <param name="provider"></param>
        /// <param name="useEncryption"></param>
        /// <param name="fileName"></param>
        public XmlFilePersister(Type graphType, ICryptoProvider provider, bool useEncryption, string fileName) : base(graphType, provider, useEncryption)
        {
            Filename = fileName;
        }

        public string Filename
        {
            get { return _fileName; }

            set { _fileName = value; }
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
                    bytes = ms.GetBuffer();
                }

                // Open file
                using (FileStream fs = File.Open(Filename, FileMode.Create))
                {
                    // Write encrypted stream
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public override object Deserialize()
        {
            object result;

            // Verify that the input file exists
            if (File.Exists(Filename))
            {
                // Open File
                using (FileStream fs = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (GraphType == null) throw new NullReferenceException(ErrorMessages.Persister_GraphTypeRequired);

                    // Create serializer
                    XmlSerializer s = new XmlSerializer(GraphType);

                    // Allocate buffer
                    byte[] bytes;

                    if (UseEncryption)
                    {
                        if (_provider == null) throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);

                        // Decrypt from file stream
                        bytes = _provider.Decrypt(fs);
                    }
                    else
                    {
                        // Allocate buffer
                        bytes = new byte[fs.Length];

                        // Read file contents
                        fs.Read(bytes, 0, (int) fs.Length);
                    }

                    // Create stream using encrypted buffer
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        // Deserialize the object graph from stream
                        result = s.Deserialize(ms);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException(string.Format(ErrorMessages.Persister_FileNotFound, Filename));
            }

            return result;
        }

        #endregion // Serialization Methods
    }
}