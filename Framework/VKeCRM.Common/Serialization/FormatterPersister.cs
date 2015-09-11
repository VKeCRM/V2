//-----------------------------------------------------------------------
// <copyright file="FormatterPersister.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using VKeCRM.Common.Cryptography;

namespace VKeCRM.Common.Serialization
{
    /// <summary>
    /// Enumerator for types of Persistence formats
    /// </summary>
    public enum PersistenceFormats
    {
        /// <summary>
        /// Persistence format can be Binary
        /// </summary>
        Binary = 0x0000,

        /// <summary>
        /// Persistence format can be SOAP
        /// </summary>
        SOAP = 0x0001
    }

    /// <summary>
    /// An abstract class for persistence formats
    /// </summary>
    public abstract class FormatterPersister : Persister
    {
        #region Private Fields

        /// <summary>
        /// Type of format
        /// </summary>
        protected PersistenceFormats _format;

        #endregion // Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FormatterPersister class
        /// </summary>
        public FormatterPersister() : this(PersistenceFormats.SOAP, null, false)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        public FormatterPersister(PersistenceFormats format) : this(format, null, false)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterPersister class
        /// </summary>
        /// <param name="provider">Crypto Provider</param>
        public FormatterPersister(ICryptoProvider provider) : this(PersistenceFormats.SOAP, provider)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        /// <param name="provider">Crypto Provider</param>
        public FormatterPersister(PersistenceFormats format, ICryptoProvider provider) : this(format, provider, true)
        {
            //// Constructor for FormatterFilePersister
        }

        /// <summary>
        /// Initializes a new instance of the FormatterPersister class
        /// </summary>
        /// <param name="format">Type of format</param>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="useEncryption">A boolean value to indicate whether to use encryption or not</param>
        public FormatterPersister(PersistenceFormats format, ICryptoProvider provider, bool useEncryption) : base(provider, useEncryption)
        {
            Format = format;
        }

        /// <summary>
        /// Gets or sets the persistence formats
        /// </summary>
        public PersistenceFormats Format
        {
            get 
            { 
                return _format; 
            }

            set 
            { 
                _format = value; 
            }
        }

        #endregion // Constructors

        #region Miscellaneous Public Methods

        /// <summary>
        /// Get the formatter based on the persistence type
        /// </summary>
        /// <returns>Returns a formatter</returns>
        protected IFormatter GetFormatter()
        {
            IFormatter result = null;

            switch (Format)
            {
                case PersistenceFormats.Binary:
                    result = new BinaryFormatter();
                    break;
                case PersistenceFormats.SOAP:
                    result = new SoapFormatter();
                    break;
            }

            return result;
        }

        #endregion // Miscellaneous Public Methods
    }
}