//-----------------------------------------------------------------------
// <copyright file="Persister.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using VKeCRM.Common.Cryptography;

namespace VKeCRM.Common.Serialization
{
    /// <summary>
    /// Base class for persistence of String formats and binary formats
    /// </summary>
    public abstract class Persister
    {
        #region Private Fields

        /// <summary>
        /// Crypto Provider
        /// </summary>
        protected ICryptoProvider _provider;

        /// <summary>
        /// A boolean value to indicate whether to use encryption or not
        /// </summary>
        protected bool _useEncryption;

        #endregion // Private Fields

        #region Public Properties
        #endregion // Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Persister class
        /// </summary>
        public Persister() : this(null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Persister class
        /// </summary>
        /// <param name="provider">Crypto Provider</param>
        public Persister(ICryptoProvider provider) : this(provider, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Persister class
        /// </summary>
        /// <param name="provider">Crypto Provider</param>
        /// <param name="useEncryption">A boolean value to indicate whether to use encryption or not</param>
        public Persister(ICryptoProvider provider, bool useEncryption)
        {
            Provider = provider;
            UseEncryption = useEncryption;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use encryption or not
        /// </summary>
        public bool UseEncryption
        {
            get 
            { 
                return _useEncryption; 
            }

            set 
            { 
                _useEncryption = value; 
            }
        }

        /// <summary>
        /// Gets or sets the Crypto Provider
        /// </summary>
        public ICryptoProvider Provider
        {
            get 
            { 
                return _provider; 
            }

            set 
            {
               _provider = value; 
            }
        }

        #endregion // Constructors

        #region Serialization Methods

        /// <summary>
        /// Signature for abstract method to serialize
        /// </summary>
        /// <param name="source">Source to serialize</param>
        public abstract void Serialize(object source);

        /// <summary>
        /// Signature for abstract method to deserialize
        /// </summary>
        /// <returns>Returns the deserialized object</returns>
        public abstract object Deserialize();

        #endregion // Serialization Methods
    }
}