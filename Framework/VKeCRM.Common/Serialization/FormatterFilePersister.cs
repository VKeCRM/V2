//-----------------------------------------------------------------------
// <copyright file="FormatterFilePersister.cs" company="VKeCRM">
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
    /// File formatter
    /// </summary>
    public sealed class FormatterFilePersister : FormatterPersister
    {
        #region Private Fields

        /// <summary>
        /// Name of file to format
        /// </summary>
        private string _filename;

        #endregion // Private Fields

        #region Public Properties
        #endregion // Public Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        public FormatterFilePersister() : this(PersistenceFormats.SOAP, null, false, String.Empty)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        /// <param name="format">Persistence formats</param>
        public FormatterFilePersister(PersistenceFormats format) : this(format, String.Empty)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        /// <param name="fileName">Name of file</param>
        public FormatterFilePersister(string fileName) : this(PersistenceFormats.SOAP, fileName)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        /// <param name="format">Persistence format</param>
        /// <param name="fileName">Name of file</param>
        public FormatterFilePersister(PersistenceFormats format, string fileName) : this(format, null, false, fileName)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="fileName">Name of file</param>
        public FormatterFilePersister(ICryptoProvider provider, string fileName) : this(PersistenceFormats.SOAP, provider, fileName)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        /// <param name="format">Persistence Format</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="fileName">name of file</param>
        public FormatterFilePersister(PersistenceFormats format, ICryptoProvider provider, string fileName) : this(format, provider, true, fileName)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterFilePersister class
        /// </summary>
        /// <param name="format">Persistence Format</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="useEncryption">A boolean value to indicate whether to use encryption or not</param>
        /// <param name="fileName">Name of file</param>
        public FormatterFilePersister(PersistenceFormats format, ICryptoProvider provider, bool useEncryption, string fileName) : base(format, provider, useEncryption)
        {
            Filename = fileName;
        }

        /// <summary>
        /// Gets or sets the file name
        /// </summary>
        public string Filename
        {
            get 
            { 
                return _filename; 
            }

            set 
            { 
                _filename = value; 
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

                // Open file
                using (FileStream fs = File.Open(Filename, FileMode.Create))
                {
                    // Write encrypted stream
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }

        /// <summary>
        /// Overridden method to deserialize
        /// </summary>
        /// <returns>Returns the deserialized object</returns>
        public override object Deserialize()
        {
            object result;

            // Verify that the input file exists
            if (File.Exists(Filename))
            {
                // Open File
                using (FileStream fs = File.Open(Filename, FileMode.Open, FileAccess.Read))
                {
                    // Create a formatter object based on command line arguments
                    IFormatter formatter = GetFormatter();

                    // Allocate buffer
                    byte[] bytes;

                    if (UseEncryption)
                    {
                        if (_provider == null)
                        {
                            throw new NullReferenceException(ErrorMessages.Persister_CryptoProviderRequiredForSerialization);
                        }

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
                        result = formatter.Deserialize(ms);
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