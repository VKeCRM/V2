//-----------------------------------------------------------------------
// <copyright file="RsaCryptoProvider.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using VKeCRM.Common.Messages;

namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// Rsa Crypto Provider base class
    /// </summary>
    public abstract class RsaCryptoProvider : ICryptoProvider
    {
        #region Constants

        /// <summary>
        /// RSA Block size
        /// </summary>
        private const int RSABLOCKSIZE = 256;

        /// <summary>
        /// Size for splitting RSA block
        /// </summary>
        private const int SPLITBLOCKSIZE = 32;

        #region ICryptoProvider Members

        /// <summary>
        /// Encrypts a stream of data.
        /// </summary>
        /// <param name="stream">Stream to encrypt.</param>
        /// <returns>Array containing encrypted bytes from the stream.</returns>
        byte[] ICryptoProvider.Encrypt(Stream stream)
        {
            RSACryptoServiceProvider csp = Load();

            byte[][] byteBlockArray = SplitBytes(GetStreamBytes(stream), SPLITBLOCKSIZE);
            byte[] bytes = new byte[byteBlockArray.Length * RSABLOCKSIZE];
            int offset = 0;
            for (int j = 0; j < byteBlockArray.Length; j++) 
            {
                offset = CopyBytes(csp.Encrypt(byteBlockArray[j], true), bytes, offset);
            }

            return bytes;
        }

        /// <summary>
        /// Decrypts a stream of data.
        /// </summary>
        /// <param name="stream">Stream to encrypt.</param>
        /// <returns>Array containing encrypted bytes from the stream.</returns>
        byte[] ICryptoProvider.Decrypt(Stream stream)
        {
            StringBuilder result = new StringBuilder();
            byte[] streamBytes = GetStreamBytes(stream);

            if (streamBytes.Length > 0)
            {
                RSACryptoServiceProvider csp = Load();
                byte[][] byteBlockArray = SplitBytes(GetStreamBytes(stream), RSABLOCKSIZE);
                for (int j = 0; j < byteBlockArray.Length; j++) 
                {
                    result.Append(ASCIIEncoding.UTF8.GetString(csp.Decrypt(byteBlockArray[j], true)));
                }
            }

            return ASCIIEncoding.UTF8.GetBytes(result.ToString());
        }

        #endregion

        #endregion // Constants

        #region Open/Load Methods

        /// <summary>
        /// Loads named container from the store.
        /// </summary>
        /// <returns>A new RSACryptoServiceProvider instance initialized retrieved from the cryptography store.</returns>
        public abstract RSACryptoServiceProvider Load();

        /// <summary>
        /// Loads container from an Xml file.
        /// </summary>
        /// <returns>
        /// <para>
        /// A new RSACryptoServiceProvider instance initialized using the public key parameters 
        /// from the specified public key file name.
        /// </para>
        /// </returns>
        /// <remarks>Throws FileNotFound exception if the public key file cannot be found.</remarks>
        public abstract RSACryptoServiceProvider Open();

        /// <summary>
        /// Loads container from an Xml file.
        /// </summary>
        /// <param name="openPrivateKey">
        /// If true, the parameters are loaded from the specified private key file.
        /// </param>
        /// <returns>
        /// <para>
        /// A new RSACryptoServiceProvider instance initialized using the parameters 
        /// from the either the public or the public/private key file name.
        /// </para>
        /// </returns>
        /// <remarks>Throws FileNotFound exception if the public key file cannot be found.</remarks>
        public abstract RSACryptoServiceProvider Open(bool openPrivateKey);

        #endregion // Open/Load Methods

        #region Static Helper Methods

        /// <summary>
        /// To convert to RSA Key size
        /// </summary>
        /// <param name="keySize">Size of key</param>
        /// <returns>Returns a RSA key size</returns>
        internal static RsaKeySize ConvertToRSAKeySize(int keySize)
        {
            RsaKeySize result = RsaKeySize.Unknown;

            switch (keySize)
            {
                case 512:
                    result = RsaKeySize.Bits512;
                    break;
                case 1024:
                    result = RsaKeySize.Bits1024;
                    break;
                case 2048:
                    result = RsaKeySize.Bits2048;
                    break;
                case 4096:
                    result = RsaKeySize.Bits4096;
                    break;
                case 8192:
                    result = RsaKeySize.Bits8192;
                    break;
                case 16384:
                    result = RsaKeySize.Bits16384;
                    break;
            }

            return result;
        }

        /// <summary>
        /// To get stream bytes
        /// </summary>
        /// <param name="stream">Stream to convert to bytes</param>
        /// <returns>Returns a byte array</returns>
        protected static byte[] GetStreamBytes(Stream stream)
        {
            // Allocate the buffer
            byte[] result = new byte[stream.Length];

            // Reset stream position
            stream.Seek(0, SeekOrigin.Begin);

            // Read Steam Contents
            if (stream.Read(result, 0, result.Length) != result.Length)
            {
                throw new IOException(ErrorMessages.StreamReadError);
            }

            return result;
        }

        /// <summary>
        /// Split a source array to a double dimensional byte array
        /// </summary>
        /// <param name="source">Source array</param>
        /// <param name="blocksize">Size of blocks to split source into</param>
        /// <returns>Returns a double dimensional byte array</returns>
        protected static byte[][] SplitBytes(byte[] source, int blocksize)
        {
            int numberOfBlocks = (source.Length < blocksize) ? 1 : (source.Length / blocksize);
            byte[][] byteBlockArray = new byte[numberOfBlocks][];
            int offset = 0;
            for (int i = 1; i <= numberOfBlocks; i++)
            {
                if (i == numberOfBlocks)
                {
                    byteBlockArray[i - 1] = CloneBytes(source, offset, source.Length - offset);
                }
                else
                {
                    byteBlockArray[i - 1] = CloneBytes(source, offset, blocksize);
                    offset += blocksize;
                }
            }

            return byteBlockArray;
        }

        /// <summary>
        /// To copy bytes from source to target
        /// </summary>
        /// <param name="source">Source of bytes to copy from</param>
        /// <param name="target">Target for bytes to be copied into</param>
        /// <param name="offset">Off set of bytes to copy</param>
        /// <returns>Returns the number of bytes copied</returns>
        protected static int CopyBytes(byte[] source, byte[] target, int offset)
        {
            Array.Copy(source, 0, target, offset, source.Length);
            return offset + source.Length;
        }

        /// <summary>
        /// To clone bytes
        /// </summary>
        /// <param name="source">Source of bytes to clone</param>
        /// <param name="offset">Off set of bytes for cloning</param>
        /// <param name="size">Size of bytes to clone</param>
        /// <returns>Returns a byte array that is a clone of the source</returns>
        protected static byte[] CloneBytes(byte[] source, int offset, int size)
        {
            byte[] result = new byte[size];
            Array.Copy(source, offset, result, 0, size);
            return result;
        }

        #endregion // Static Helper Methods
    }
}