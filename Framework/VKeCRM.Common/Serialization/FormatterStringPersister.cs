//-----------------------------------------------------------------------
// <copyright file="FormatterStringPersister.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Runtime.Serialization;

using VKeCRM.Common.Cryptography;
using ErrorMessages = VKeCRM.Common.Messages.Messages;

namespace VKeCRM.Common.Serialization
{
    /// <summary>
    /// Persister class for String formatter
    /// </summary>
    public sealed class FormatterStringPersister : FormatterPersister
    {
        #region Private Fields

        /// <summary>
        /// Buffer string
        /// </summary>
        private string _buffer;

        #endregion // Private Fields

        #region Public Properties
        #endregion // Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        public FormatterStringPersister() : this(PersistenceFormats.SOAP, null, false, String.Empty)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        public FormatterStringPersister(PersistenceFormats format) : this(format, String.Empty)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        /// <param name="buffer">Buffer string</param>
        public FormatterStringPersister(string buffer) : this(PersistenceFormats.SOAP, buffer)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        /// <param name="buffer">Buffer string</param>
        public FormatterStringPersister(PersistenceFormats format, string buffer) : this(format, null, false, buffer)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="buffer">Buffer string</param>
        public FormatterStringPersister(ICryptoProvider provider, string buffer) : this(PersistenceFormats.SOAP, provider, buffer)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="buffer">Buffer string</param>
        public FormatterStringPersister(PersistenceFormats format, ICryptoProvider provider, string buffer) : this(format, provider, true, buffer)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterStringPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="useEncryption">A boolean value to indicate whether to use encryption or not</param>
        /// <param name="buffer">Buffer string</param>
        public FormatterStringPersister(PersistenceFormats format, ICryptoProvider provider, bool useEncryption, string buffer) : base(format, provider, useEncryption)
        {
            Buffer = buffer;
        }

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

        #endregion // Constructors

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

            // Create a formatter object based on command line arguments
            IFormatter formatter = GetFormatter();

            // Create Stream
            using (MemoryStream ms = new MemoryStream())
            {
                // Serialize the object graph to stream
                formatter.Serialize(ms, source);

                // Trim buffer
                ms.Capacity = (int) ms.Length;

                byte[] bytes;

                if (UseEncryption)
                {
                    if (_provider == null)
                    {
                        throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);
                    }

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

                // Store Base64 encoded string
                Buffer = Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// Overridden method to deserialize
        /// </summary>
        /// <returns>Returns the deserialized object</returns>
        public override object Deserialize()
        {
            object result;

            // Create a formatter object based on command line arguments
            IFormatter formatter = GetFormatter();

            // Decode Base64 encoded string
            byte[] bytes = Convert.FromBase64String(Buffer);

            if (UseEncryption)
            {
                if (_provider == null)
                {
                    throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);
                }

                // Decrypt from file stream
                bytes = _provider.Decrypt(new MemoryStream(bytes));
            }

            // Create stream using encrypted buffer
            using (MemoryStream mem = new MemoryStream(bytes))
            {
                // Deserialize the object graph from stream
                result = formatter.Deserialize(mem);
            }

            return result;
        }

        #endregion // Serialization Methods
    }
}