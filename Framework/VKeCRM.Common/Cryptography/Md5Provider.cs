using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace VKeCRM.Common.Cryptography
{
	public class Md5Provider
	{
		// This method will validate
		// the entire querystring to determine if
		// the session is valid.
		// True = session is valid, False = session is invalid.
		public static string GetKey(string portal)
		{
			return "BdkjxyfoEEMphx";
		}

		public static bool ValidateToken(string guid, string token, string alternativeId, string portal)
		{
			// This variable is a constant key in both of our code bases.
			string key = GetKey(portal);

			// This is a string to hold the derived token to compare 
			// to the passed-in token.
			string tokenCompare;

			// Capture utc date time for comparison.
			DateTime utc = DateTime.UtcNow;

			// Retrieve date time from alternativeId.
			DateTime idDate = DateTime.ParseExact(Convert.ToUInt64(alternativeId, 16).ToString(), "yyyyMMddHHmm", new CultureInfo("en-US"));

			// If alternativeId is not within 15 minutes from current 
			// utc time then fail validation.
			if (idDate > utc.AddMinutes(15) || idDate < utc.AddMinutes(-15))
			{
				return false;
			}

			// Build comparison for token compare.
			// Algorithm: Encrypt GUID with MD5 and add as a
			// string to the key and the passed-in alternativeId.
			// Then encrypt entire result as MD5. This should equal
			// the passed-in token.
			tokenCompare = Md5(Md5(guid) + key + alternativeId);

			// If the tokens do not match, then fail validation.
			if (!(token.Equals(tokenCompare)))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		// This Encrypts to md5 in addition
		// to converting the byte array output
		// to a hexidecimal string.
		public static string Md5(string toEncrypt)
		{
			byte[] encryptedByteBuffer;
			char[] encryptedCharBuffer;
			string encryptedString = "";

			// Get byte array from string
			MD5 md5 = MD5.Create();
			encryptedByteBuffer = md5.ComputeHash(Encoding.ASCII.GetBytes(toEncrypt));

			encryptedCharBuffer = new char[16];

			for (int i = 0; i < encryptedByteBuffer.Length; i++)
			{
				if ((int)encryptedByteBuffer[i] < 0)
				{
					encryptedCharBuffer[i] = (char)((int)encryptedByteBuffer[i] + 256);
				}
				else
				{
					encryptedCharBuffer[i] = (char)(encryptedByteBuffer[i]);
				}
			}

			for (int i = 0; i < encryptedCharBuffer.Length; i++)
			{

				if ((int)encryptedCharBuffer[i] < 16)
				{
					encryptedString += "0";
				}

				encryptedString += ((int)(encryptedCharBuffer[i])).ToString("x");
			}

			return encryptedString;
		}

		public static string GetAltID()
		{
			return UInt64.Parse(DateTime.UtcNow.ToString("yyyyMMddHHmm")).ToString("x");
		}

		public static string GetToken(string guid, string alternativeId, string portal)
		{
			string key = GetKey(portal);
			return Md5(Md5(guid) + key + alternativeId);
		}
	}
}
