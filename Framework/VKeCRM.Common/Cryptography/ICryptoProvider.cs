//-----------------------------------------------------------------------
// <copyright file="ICryptoProvider.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.IO;

namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// Provides an interface encrypting and decrypting streams.
    /// </summary>
    public interface ICryptoProvider
    {
        /// <summary>
        /// Encrypts a stream of data.
        /// </summary>
        /// <param name="stream">Stream to encrypt.</param>
        /// <returns>Array containing encrypted bytes from the stream.</returns>
        byte[] Encrypt(Stream stream);

        /// <summary>
        /// Decrypts a stream of data.
        /// </summary>
        /// <param name="stream">Stream to encrypt.</param>
        /// <returns>Array containing encrypted bytes from the stream.</returns>
        byte[] Decrypt(Stream stream);
    }
}