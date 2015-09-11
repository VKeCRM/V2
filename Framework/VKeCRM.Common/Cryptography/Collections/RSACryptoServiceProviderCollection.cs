//-----------------------------------------------------------------------
// <copyright file="RSACryptoServiceProviderCollection.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Security.Cryptography;

namespace VKeCRM.Common.Cryptography.Collections
{
    /// <summary>
    /// <para>
    /// Represents a strongly typed list of RSACryptoServiceProvider objects that can 
    /// be accessed by index. Provides methods to search, sort, and manipulate lists.
    /// </para>
    /// </summary>
    public class RSACryptoServiceProviderCollection : List<RSACryptoServiceProvider>
    {
        /// <summary>
        /// Initializes a new instance of the RSACryptoServiceProviderCollection class 
        /// that is empty and has the default initial capacity. 
        /// </summary>
        public RSACryptoServiceProviderCollection()
        {
        }

        /// <summary>
        /// <para>
        /// Initializes a new instance of the RSACryptoServiceProviderCollection class 
        /// that is empty and has the specified initial capacity. 
        /// </para>
        /// </summary>
        /// <param name="capacity">Initial capacity of RSA crypto service provider collection</param>
        public RSACryptoServiceProviderCollection(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// <para>
        /// Initializes a new instance of the RSACryptoServiceProviderCollection class 
        /// that contains elements copied from the specified collection and has 
        /// sufficient capacity to accommodate the number of elements copied. 
        /// </para>
        /// </summary>
        /// <param name="collection">Collection of RSA crypto service providers</param>
        public RSACryptoServiceProviderCollection(IEnumerable<RSACryptoServiceProvider> collection) : base(collection)
        {
        }
    }
}