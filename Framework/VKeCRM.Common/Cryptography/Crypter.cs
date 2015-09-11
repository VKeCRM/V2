using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;

namespace VKeCRM.Common.Cryptography
{
	public class Crypter
	{
		private const uint StartChar = 101;
		private const uint EncryptBlockSize = 8;
		private static string _password = string.Empty;

		public Crypter()
		{
			_password = "BdkjxyfoEEMphx";
		}

		public static string EnCrypt(string src, string passKey)
		{
			TripleDESCryptoServiceProvider alg = new TripleDESCryptoServiceProvider();
			alg.Mode = CipherMode.ECB;
			alg.Key = String2Byte(GetLegalKey(CheckPassword(passKey), alg));

			MemoryStream ms = new MemoryStream();
			// create an Encryptor from the Provider Service instance
			// and create Crypto Stream that transforms a stream using the encryption
			CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

			byte[] baSrc = String2Byte(src);
			cs.Write(baSrc, 0, baSrc.Length);		// write out encrypted content into MemoryStream
			cs.FlushFinalBlock();

			byte[] baOut = ms.GetBuffer();			// get the output
			return Byte2Hex(baOut, EncryptBlockSize + ((uint)src.Length / EncryptBlockSize) * EncryptBlockSize);	// convert array into String
		}

		public static string DeCrypt(string src, string passKey)
		{
			TripleDESCryptoServiceProvider alg = new TripleDESCryptoServiceProvider();
			alg.Mode = CipherMode.ECB;
			alg.Key = String2Byte(GetLegalKey(CheckPassword(passKey), alg));	// set the private key

			byte[] baSrc = Hex2Byte(src);
			MemoryStream ms = new MemoryStream(baSrc, 0, baSrc.Length);
			// create a Decryptor from the Provider Service instance
			// and create Crypto Stream that transforms a stream using the decryption
			CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Read);

			StreamReader sr = new StreamReader(cs);	// read out the result from the Crypto Stream
			return sr.ReadToEnd();
		}

		public static string Byte2Hex(byte[] ba, uint len)
		{
			string s = "";
			if (ba.Length < len)
			{
				len = (uint)ba.Length;
			}

			for (long i = 0; i < len; ++i)
			{
				s += Convert.ToChar((uint)Convert.ToChar(ba[i]) / 16 + StartChar);
				s += Convert.ToChar((uint)Convert.ToChar(ba[i]) % 16 + StartChar);
			}

			return s;
		}

		public static byte[] Hex2Byte(string str)
		{
			uint len = (uint)str.Length / 2;
			byte[] ba = new byte[len];
			char[] ca = str.ToCharArray();

			for (int i = 0; i < len; ++i)
			{
				ba[i] = Convert.ToByte(((uint)ca[i * 2] - StartChar) * 16 + (uint)ca[i * 2 + 1] - StartChar);
			}

			return ba;
		}

		private static string CheckPassword(string pswd)
		{
			return string.IsNullOrEmpty(pswd) ? _password : pswd;
		}

		private static string GetLegalKey(string pswd, TripleDESCryptoServiceProvider alg)
		{
			if (alg.LegalKeySizes.Length > 0)
			{
				uint lessSize = 0;
				uint moreSize = (uint)alg.LegalKeySizes[0].MinSize;
				uint maxSize = (uint)alg.LegalKeySizes[0].MaxSize;
				while (moreSize < pswd.Length * 8 && moreSize < maxSize)
				{
					lessSize = moreSize;
					moreSize += (uint)alg.LegalKeySizes[0].SkipSize;
				}
				return pswd.PadRight((int)(moreSize / 8), Convert.ToChar(0));
			}
			return pswd;
		}

		private static byte[] String2Byte(string S)
		{
			char[] cha = S.ToCharArray();
			byte[] ba = new byte[S.Length];
			for (int i = 0; i < S.Length; ++i)
			{
				ba[i] = Convert.ToByte(cha[i]);
			}

			return ba;
		}
	}
}
