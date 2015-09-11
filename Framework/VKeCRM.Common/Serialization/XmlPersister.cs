using System;
using VKeCRM.Common.Cryptography;

namespace VKeCRM.Common.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class XmlPersister : Persister
    {
        #region Private Fields

        protected Type m_graphType;

        #endregion // Private Fields

        #region Public Properties

        #endregion // Public Properties

        #region Constructors

        public XmlPersister(Type graphType) : this(graphType, null, false)
        {
        }

        public XmlPersister(Type graphType, ICryptoProvider provider) : this(graphType, provider, true)
        {
        }

        public XmlPersister(Type graphType, ICryptoProvider provider, bool useEncryption) : base(provider, useEncryption)
        {
            GraphType = graphType;
        }

        public Type GraphType
        {
            get { return m_graphType; }

            set { m_graphType = value; }
        }

        #endregion // Constructors
    }
}