using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace VKeCRM.Common.Cryptography
{
	public static class PensonPasswordProvider
	{
		/// <summary>
		/// Gets the penson password for user.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns></returns>
		public static string GetPensonPassword(string userName)
		{
			if (string.IsNullOrEmpty(userName))
				throw new ArgumentException("UserName parameter is required for this method.");
			return GetHashedPassword(userName);
		}


		private static string GetHashedPassword(string userName)
		{
			return MD5(string.Concat("VKeCRM", ReverseStr(userName), "Security"));
		}

		/// <summary>
		/// MD5
		/// </summary>
		/// <param name="source">The source string.</param>
		/// <returns>hashed string</returns>
		private static string MD5(string source)
		{
			MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
			byte[] md5Source = System.Text.Encoding.UTF8.GetBytes(source);
			byte[] md5Out = md5Provider.ComputeHash(md5Source);
			return Convert.ToBase64String(md5Out);
		}

		/// <summary>
		/// Reverses the STR.
		/// </summary>
		/// <param name="originalStr">The original STR.</param>
		/// <returns></returns>
		private static string ReverseStr(string originalStr)
		{
			char[] arr = originalStr.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}
	}
}
