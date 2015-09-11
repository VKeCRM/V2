//-----------------------------------------------------------------------
// <copyright file="BinaryPersister.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using VKeCRM.Common.Cryptography;
using ErrorMessages = VKeCRM.Common.Messages.Messages;

namespace VKeCRM.Common.Serialization
{
    /// <summary>
    /// Binary Persister class for Serialization
    /// </summary>
    public sealed class BinaryPersister : XmlPersister
    {
        #region Private Fields

        /// <summary>
        /// Buffer string
        /// </summary>
        private string _buffer;

        #endregion

       #region Constructors

        /// <summary>
        /// Initializes a new instance of the BinaryPersister class
        /// </summary>
        /// <param name="graphType">Type of graph</param>
        public BinaryPersister(Type graphType) : this(graphType, String.Empty)
        {
            //// Constructor for Binary Persister
        }

        /// <summary>
        /// Initializes a new instance of the BinaryPersister class
        /// </summary>
        /// <param name="graphType">Type of graph</param>
        /// <param name="buffer">Buffer string</param>
        public BinaryPersister(Type graphType, string buffer) : this(graphType, null, false, buffer)
        {
            //// Constructor for Binary Persister
        }

        /// <summary>
        /// Initializes a new instance of the BinaryPersister class
        /// </summary>
        /// <param name="graphType">Type of graph</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="buffer">Buffer string</param>
        public BinaryPersister(Type graphType, ICryptoProvider provider, string buffer) : this(graphType, provider, true, buffer)
        {
            //// Constructor for Binary Persister
        }

        /// <summary>
        /// Initializes a new instance of the BinaryPersister class
        /// </summary>
        /// <param name="graphType">Type of graph</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="useEncryption">A boolean value to indicate whether to use encryption or not</param>
        /// <param name="buffer">Buffer string</param>
        public BinaryPersister(Type graphType, ICryptoProvider provider, bool useEncryption, string buffer)
            : base(graphType, provider, useEncryption)
        {
            Buffer = buffer;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the buffer string
        /// </summary>
        public string Buffer
        {
            get 
            { 
                return _buffer; 
            }

            set 
            { 
                _buffer = value; 
            }
        }

        #endregion

        #region Serialization Methods

        /// <summary>
        /// Overridden method to serialize
        /// </summary>
        /// <param name="source">source to serialize</param>
        public override void Serialize(object source)
        {
            if (source == null)
            {
                throw new NullReferenceException(ErrorMessages.Persister_ObjectRequired);
            }

            if (GraphType == null)
            {
                throw new NullReferenceException(ErrorMessages.Persister_GraphTypeRequired);
            }

            //// Create Stream
            using (MemoryStream ms = new MemoryStream())
            {
                //// Create the formatter
                BinaryFormatter formatter = new BinaryFormatter();

                //// Serialize the object graph to stream
                formatter.Serialize(ms, source);

                //// Trim buffer
                ms.Capacity = (int) ms.Length;

                byte[] bytes;

                if (UseEncryption)
                {
                    if (_provider == null)
                    {
                        throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);
                    }

                    //// Reposition Stream
                    ms.Seek(0, SeekOrigin.Begin);

                    //// Encrypt stream Contents
                    bytes = _provider.Encrypt(ms);
                }
                else
                {
                    //// Retrieve stream contents
                    bytes = ms.ToArray();
                }

                //// Store Base64 encoded string
                Buffer = Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Overridden method to deserialize
        /// </summary>
        /// <returns>Returns the deserialized object</returns>
        public override object Deserialize()
        {
            object result = null;

            if (GraphType == null)
            {
                throw new NullReferenceException(ErrorMessages.Persister_GraphTypeRequired);
            }

            //// Create the formatter
            BinaryFormatter formatter = new BinaryFormatter();

            //// Decode Base64 encoded string
            byte[] bytes = Convert.FromBase64String(Buffer);

            if (UseEncryption)
            {
                if (_provider == null)
                {
                    throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);
                }

                //// Decrypt from file stream
                bytes = _provider.Decrypt(new MemoryStream(bytes));
            }

            //// Create stream using encrypted buffer
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                //// Deserialize the object graph from stream
                result = formatter.Deserialize(ms);
            }

            return result;
        }

        #endregion 
    }
}