using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using VKeCRM.Common.IO;

namespace VKeCRM.Common.Security.Encryption
{
    public static class SymmetricEncryption
    {
        #region Public Method
            public static String SymmetricEncrypt(this String data, String key)
            {
                //--- Initialize Key ---//
                SymmetricEncryptionKey symmetricKey = new SymmetricEncryptionKey(key);

                //--- Encrypt ---//
                Byte[] encryptedDataBytes = Encrypt(data.ToByteArray(Encoding.UTF8), symmetricKey);

                //--- Convert Encrypted String as Base64 String ---//
                return Convert.ToBase64String(encryptedDataBytes);
            }

            public static Byte[] SymmetricEncrypt(this Byte[] data, String key)
            {
                //--- Initialize Key ---//
                SymmetricEncryptionKey symmetricKey = new SymmetricEncryptionKey(key);

                //--- Encrypt ---//
                return Encrypt(data, symmetricKey);
            }

            public static String SymmetricDecrypt(this String data, String key)
            {
                //--- Initialize Key ---//
                SymmetricEncryptionKey symmetricKey = new SymmetricEncryptionKey(key);

                //--- Decrypt ---//
                Byte[] decryptedBytes = Decrypt(Convert.FromBase64String(data), symmetricKey);

                //--- Convert to String ---//
                return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
            }

            public static Byte[] SymmetricDecrypt(this Byte[] data, String key)
            {
                //--- Initialize Key ---//
                SymmetricEncryptionKey symmetricKey = new SymmetricEncryptionKey(key);

                //--- Decrypt ---//
                return Decrypt(data, symmetricKey);
            }

            public static string SimpleSymmetricEncrypt(this string data, string key)
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.IV = new byte[8];
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[0]);
                des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);

                MemoryStream ms = new MemoryStream(data.Length * 2);
                CryptoStream encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

                byte[] plainBytes = Encoding.UTF8.GetBytes(data);
                encStream.Write(plainBytes, 0, plainBytes.Length);
                encStream.FlushFinalBlock();

                byte[] encryptedBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(encryptedBytes, 0, (int)ms.Length);
                encStream.Close();

                return Convert.ToBase64String(encryptedBytes);
            }

            public static string SimpleSymmetricDecrypt(this string data, string key)
            {
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.IV = new byte[8];
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(key, new byte[0]);
                des.Key = pdb.CryptDeriveKey("RC2", "MD5", 128, new byte[8]);

                byte[] encryptedBytes = Convert.FromBase64String(data);
                MemoryStream ms = new MemoryStream(data.Length);
                CryptoStream decStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

                decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                decStream.FlushFinalBlock();

                byte[] plainBytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(plainBytes, 0, (int)ms.Length);
                decStream.Close();

                return Encoding.UTF8.GetString(plainBytes);
            }
        #endregion

        #region Private Method
            private static Byte[] Encrypt(Byte[] data, SymmetricEncryptionKey symmetricKey)
            {
                //--- Initialize Encryptor ---//
                RijndaelManaged symmetricEncryptor = new RijndaelManaged();
                symmetricEncryptor.Mode = CipherMode.CBC;

                //--- Create Encryptor ---//
                ICryptoTransform encryptor = symmetricEncryptor.CreateEncryptor(symmetricKey.Key, symmetricKey.InitVector);

                //--- Encrypt ---//
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();

                //--- Read Encrypted Bytes ---//
                Byte[] encryptedDataBytes = ms.ToArray();

                //--- Dispose Resource ---//
                ms.Close();
                cs.Close();
                ms.Dispose();
                cs.Dispose();

                return encryptedDataBytes;
            }

            private static Byte[] Decrypt(Byte[] data, SymmetricEncryptionKey symmetricKey)
            {
                //--- Initialize Decryptor ---//
                RijndaelManaged symmetricDecryptor = new RijndaelManaged();
                symmetricDecryptor.Mode = CipherMode.CBC;

                //--- Create Decryptor ---//
                ICryptoTransform decryptor = symmetricDecryptor.CreateDecryptor(symmetricKey.Key, symmetricKey.InitVector);

                //--- Decrypt ---//
                MemoryStream ms = new MemoryStream(data);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                Byte[] tmpDataBytes = new Byte[ms.Length];
                Int32 decryptCount = cs.Read(tmpDataBytes, 0, tmpDataBytes.Length);
                
                //--- Extract Decrypted Data ---//
                Byte[] decryptedDataBytes = new Byte[decryptCount];
                Array.Copy(tmpDataBytes, decryptedDataBytes, decryptCount);

                //--- Dispose Resource ---//
                ms.Close();
                cs.Close();
                ms.Dispose();
                cs.Dispose();

                return decryptedDataBytes;
            }
        #endregion
    }
}
