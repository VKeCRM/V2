//-----------------------------------------------------------------------
// <copyright file="DesCryptoProvider.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VKeCRM.Common.Cryptography
{
    /// <summary>
    /// DES Crypto Provider
    /// </summary>
    public class DesCryptoProvider : ICryptoProvider
    {
        #region Private Fields

        /// <summary>
        /// Pass Phrase
        /// </summary>
        private string _passphrase = null;

        /// <summary>
        /// Symmetric IV
        /// </summary>
        private byte[] _symIV = new byte[8];

        /// <summary>
        /// Symmetric Key
        /// </summary>
        private byte[] _symKey = new byte[8];

        #endregion // Private Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DesCryptoProvider class
        /// </summary>
        public DesCryptoProvider()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DesCryptoProvider class
        /// </summary>
        /// <param name="passphrase">Pass phrase</param>
        public DesCryptoProvider(string passphrase)
        {
            Passphrase = passphrase;
        }

        #endregion // Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the pass phrase
        /// </summary>
        public string Passphrase
        {
            get
            {
                return _passphrase;
            }

            set
            {
                if (!string.Equals(_passphrase, value))
                {
                    _passphrase = value;
                    GenerateSymArrays();
                }
            }
        }

        /// <summary>
        /// Gets the Key
        /// </summary>
        private byte[] Key
        {
            get
            {
                byte[] result = null;

                if (!string.IsNullOrEmpty(_passphrase))
                {
                    result = ASCIIEncoding.UTF8.GetBytes(_passphrase);
                }

                return result;
            }
        }

        #endregion // Public Properties

        #region ICryptoProvider Members

        /// <summary>
        /// Encrypts a stream of data.
        /// </summary>
        /// <param name="stream">Stream to encrypt.</param>
        /// <returns>Array containing encrypted bytes from the stream.</returns>
        public byte[] Encrypt(Stream stream)
        {
            byte[] result;

            if (Key != null)
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                ICryptoTransform transform = provider.CreateEncryptor(_symKey, _symIV);
                CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Read);

                // Create stream
                using (MemoryStream ms = new MemoryStream())
                {
                    int totalBytesRead = 0;
                    int bytesRead;
                    byte[] buffer = new byte[4096];

                    // Process in 4k blocks
                    do
                    {
                        // Read next block
                        bytesRead = cs.Read(buffer, 0, 4096);

                        // Store block in memory stream
                        ms.Write(buffer, 0, bytesRead);

                        // Increment byte count
                        totalBytesRead += bytesRead;
                    } 
                    while (bytesRead > 0);

                    // Set capacity to actual size
                    ms.Capacity = totalBytesRead;

                    // Retieve buffer
                    result = ms.ToArray();
                }
            }
            else
            {
                result = new byte[0];
            }

            return result;
        }

        /// <summary>
        /// Decrypts a stream of data.
        /// </summary>
        /// <param name="stream">Stream to encrypt.</param>
        /// <returns>Array containing encrypted bytes from the stream.</returns>
        public byte[] Decrypt(Stream stream)
        {
            byte[] result;

            if (Key != null)
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                ICryptoTransform transform = provider.CreateDecryptor(_symKey, _symIV);
                CryptoStream cs = new CryptoStream(stream, transform, CryptoStreamMode.Read);

                // Create stream
                using (MemoryStream ms = new MemoryStream())
                {
                    int totalBytesRead = 0;
                    int bytesRead;
                    byte[] buffer = new byte[4096];

                    // Process in 4k blocks
                    do
                    {
                        // Read next block
                        bytesRead = cs.Read(buffer, 0, 4096);

                        // Store block in memory stream
                        ms.Write(buffer, 0, bytesRead);

                        // increment byte count
                        totalBytesRead += bytesRead;
                    } 
                    while (bytesRead > 0);

                    // Set capacity to actual size
                    ms.Capacity = totalBytesRead;

                    // Retieve buffer
                    result = ms.ToArray();
                }
            }
            else
            {
                result = new byte[0];
            }

            return result;
        }
        #endregion

		public string GetEncryptedString(byte[] buffer)
		{
			return BitConverter.ToString(buffer).Replace("-", "").ToLower();
		}

		public string GetDecryptedString(byte[] buffer)
		{
			return Encoding.ASCII.GetString(buffer);
		}

		public byte[] BitStr2Bytes(string bitStr)
		{
			// insert "-"
			int n = bitStr.Length / 2 - 1;
			for (int i = n; i > 0; i--)
			{
				bitStr = bitStr.Insert(i * 2, "-");
			}

			// convert string to byte[]
			string[] arrSplit = bitStr.Split('-');
			byte[] byteTemp = new byte[arrSplit.Length];
			for (int i = 0; i < byteTemp.Length; i++)
			{
				byteTemp[i] = byte.Parse(arrSplit[i], System.Globalization.NumberStyles.AllowHexSpecifier);
			}

			return byteTemp;
		}

        #region Key Generation

        /// <summary>
        /// To generate symmetric arrays
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1817:DoNotCallPropertiesThatCloneValuesInLoops", Justification = "To prevent clone values in loops")]
        private void GenerateSymArrays()
        {
            if (_passphrase != null)
            {
                // Do the hash operation 
                using (SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider())
                {
                    provider.ComputeHash(Key);

                    // Use the low 64-bits for the key value
                    // Generate key and initialization vector based on passcodce.
                    // In symmetric encryption,
                    // Both users must have the same key in order to 
                    // encrypt/decrypt
                    _symKey = new byte[8];
                    int index;
                    for (index = 0; index < _symKey.Length; index++)
                    {
                        _symKey[index] = provider.Hash[index];
                    }

                    _symIV = new byte[8];
                    for (index = 0; index < _symIV.Length; index++)
                    {
                        _symIV[index] = provider.Hash[index + 8];
                    }
                }
            }
            else
            {
                // Clear sym arrays
                Array.Clear(_symKey, 0, _symKey.Length);
                Array.Clear(_symIV, 0, _symIV.Length);
            }
        }

        #endregion // Key Generation
    }
}