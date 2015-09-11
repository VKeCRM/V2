using System;
using System.Security.Cryptography;
using System.Text;
using VKeCRM.Common.IO;

namespace VKeCRM.Common.Security.Encryption
{
    public static class HashEncryption
    {
        #region Public Method
            public static Byte[] SHAHash(this Byte[] data)
            {
                return ComputeHash(data, new SHA1CryptoServiceProvider());
            }

            public static String SHAHashString(this String data)
            {
                return ComputeHash(data, new SHA1CryptoServiceProvider(), Encoding.UTF8);
            }

            public static String SHAHashString(this String data, Encoding encoder)
            {
                return ComputeHash(data, new SHA1CryptoServiceProvider(), encoder);
            }

            public static Byte[] MD5Hash(this Byte[] data)
            {
                return ComputeHash(data, MD5.Create());
            }

            public static String MD5HashString(this String data)
            {
                return ComputeHash(data, MD5.Create(), Encoding.ASCII);
            }

            public static String MD5HashString(this String data, Encoding encoder)
            {
                return ComputeHash(data, MD5.Create(), encoder);
            }
        #endregion

        #region Private Method
            private static Byte[] ComputeHash(Byte[] data, HashAlgorithm algorithm)
            {
                return algorithm.ComputeHash(data);
            }

            private static String ComputeHash(String data, HashAlgorithm algorithm, Encoding encoder)
            {
                return encoder.GetString(ComputeHash(data.ToByteArray(encoder), algorithm));
            }
        #endregion
    }
}
