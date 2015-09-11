//-----------------------------------------------------------------------
// <copyright file="Convert.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Utility
{
    /// <summary>
    /// Class to handle all conversions
    /// </summary>
    public class Convert
    {
        #region Constructors

        /// <summary>
        /// Prevents a default instance of the Convert class from being created.
        /// </summary>
        private Convert()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts an input string to a boolean value. Converts "Yes" to true and "No" to false.
        /// </summary>
        /// <param name="valueYesNo">String value of Yes or No</param>
        /// <param name="defaultValue">The default boolean value</param>
        /// <returns>Returns a boolean value for the input string</returns>
        public static bool ConvertStringToBool(string valueYesNo, bool defaultValue)
        {
            try
            {
                switch (valueYesNo.ToUpper())
                {
                    case "ON":
                    case "YES":
                    case "Y":
                    case "1":
                        {
                            return true;
                        }

                    case "OFF":
                    case "NO":
                    case "N":
                    case "0":
                        {
                            return false;
                        }

                    default:
                        {
                            return defaultValue;
                        }
                }
            }
            catch (System.Exception e)
            {
                throw new ConvertException("ConvertStringToBool", valueYesNo, e);
            }
        }

        /// <summary>
        /// Converts a boolean value to string. 
        /// </summary>
        /// <param name="valueTrueFalse">Boolean value of True or False</param>
        /// <param name="defaultValue">Default string value</param>
        /// <returns>Returns a string for the input boolean value</returns>
        public static string ConvertBoolToString(bool valueTrueFalse, string defaultValue)
        {
            try
            {
                return valueTrueFalse ? "Yes" : "No";
            }
            catch (System.Exception e)
            {
                throw new ConvertException("ConvertBoolToString", String.Empty + valueTrueFalse, e);
            }
        }

        /// <summary>
        /// To convert a lowercase string to TCI data type
        /// </summary>
        /// <param name="sqlDbTypeName">String name of SQL data type</param>
        /// <param name="defaultDataType">Default SQL data type</param>
        /// <returns>Returns a TCI data type</returns>
        public static SqlDbType ConvertStringToSqlDbType(string sqlDbTypeName, SqlDbType defaultDataType)
        {
            try
            {
                SqlDbType dataType;
                switch (sqlDbTypeName.ToLower())
                {
                    case "int":
                        dataType = SqlDbType.Int;
                        break;
                    case "varchar":
                        dataType = SqlDbType.VarChar;
                        break;
                    case "uniqueidentifier":
                        dataType = SqlDbType.UniqueIdentifier;
                        break;
                    case "datetime":
                        dataType = SqlDbType.DateTime;
                        break;
                    case "smalldatetime":
                        dataType = SqlDbType.SmallDateTime;
                        break;
                    case "bit":
                        dataType = SqlDbType.Bit;
                        break;
                    case "tinyint":
                        dataType = SqlDbType.TinyInt;
                        break;
                    case "smallint":
                        dataType = SqlDbType.SmallInt;
                        break;
                    case "money":
                        dataType = SqlDbType.Money;
                        break;
                    case "smallmoney":
                        dataType = SqlDbType.SmallMoney;
                        break;
                    case "char":
                        dataType = SqlDbType.Char;
                        break;
                    case "decimal":
                        dataType = SqlDbType.Decimal;
                        break;
                    default:
                        dataType = defaultDataType;
                        break;
                }

                return dataType;
            }
            catch (System.Exception e)
            {
                throw new ConvertException("ConvertStringToSqlDbType", sqlDbTypeName, e);
            }
        }

        /// <summary>
        /// To convert an object to decimal
        /// </summary>
        /// <param name="o">Inout object</param>
        /// <returns>Returns the decimal value for the object</returns>
        public static decimal GetAsDecimal(object o)
        {
            decimal result;

            if (o is string)
            {
                result = decimal.Parse((string)o, NumberStyles.Number);
            }
            else
            {
                result = System.Convert.ToDecimal(o);
            }

            return result;
        }

        /// <summary>
        /// To convert an object to decimal
        /// </summary>
        /// <param name="objectFieldValue">Input object value</param>
        /// <param name="decimalFieldValue">Decimal value</param>
        /// <returns>Returns a true or false, and a decimal representation of an object when true.</returns>
        public static bool GetObjectAsDecimal(object objectFieldValue, ref decimal decimalFieldValue)
        {
            try
            {
                // Attempt to parse value into decimal
                decimalFieldValue = GetAsDecimal(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to decimal
        /// </summary>
        /// <param name="stringFieldValue">Input string value</param>
        /// <param name="decimalFieldValue">Decimal field value</param>
        /// <returns>Returns a true or false, and a decimal representation of a string when true.</returns>
        public static bool GetStringAsDecimal(string stringFieldValue, ref decimal decimalFieldValue)
        {
            try
            {
                // Attempt to parse value into decimal
                decimalFieldValue = GetAsDecimal(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert object to currency
        /// </summary>
        /// <param name="o">Input object</param>
        /// <returns>Returns the currency value for the object</returns>
        public static decimal GetAsMoney(object o)
        {
            decimal result;

            if (o is string)
            {
                result = decimal.Parse((string)o, NumberStyles.Currency);
            }
            else
            {
                result = System.Convert.ToDecimal(o);
            }

            return result;
        }

        /// <summary>
        /// To convert object to currency
        /// </summary>
        /// <param name="objectFieldValue">Input object value</param>
        /// <param name="decimalFieldValue">Decimal field value</param>
        /// <returns>Returns a true or false, and a decimal representation of an object when true.</returns>
        public static bool GetObjectAsMoney(object objectFieldValue, ref decimal decimalFieldValue)
        {
            try
            {
                // Attempt to parse value into decimal
                decimalFieldValue = GetAsMoney(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to currency
        /// </summary>
        /// <param name="stringFieldValue">Input string value</param>
        /// <param name="decimalFieldValue">Decimal field value</param>
        /// <returns>Returns a true or false, and a decimal representation of a string when true.</returns>
        public static bool GetStringAsMoney(string stringFieldValue, ref decimal decimalFieldValue)
        {
            try
            {
                // Attempt to parse value into decimal
                decimalFieldValue = GetAsMoney(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert object to byte
        /// </summary>
        /// <param name="o">Input object</param>
        /// <returns>Returns a byte value for the object</returns>
        public static byte GetAsByte(object o)
        {
            byte result;

            if (o is string)
            {
                result = byte.Parse((string)o, NumberStyles.Integer | NumberStyles.AllowThousands);
            }
            else
            {
                result = System.Convert.ToByte(o);
            }

            return result;
        }

        /// <summary>
        /// To convert object to byte
        /// </summary>
        /// <param name="objectFieldValue">Input object value</param>
        /// <param name="byteFieldValue">Byte field value</param>
        /// <returns>Returns a true or false, and a byte representation of a object when true.</returns>
        public static bool GetObjectAsByte(object objectFieldValue, ref byte byteFieldValue)
        {
            try
            {
                // Attempt to parse value into byte
                byteFieldValue = GetAsByte(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to byte
        /// </summary>
        /// <param name="stringFieldValue">Input string value</param>
        /// <param name="byteFieldValue">Byte field value</param>
        /// <returns>Returns a true or false, and a byte representation of a string when true.</returns>
        public static bool GetStringAsByte(string stringFieldValue, ref byte byteFieldValue)
        {
            try
            {
                // Attempt to parse value into byte
                byteFieldValue = GetAsByte(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert object to integer
        /// </summary>
        /// <param name="o">Input object value</param>
        /// <returns>Returns integer for the input object value</returns>
        public static Int32 GetAsInteger(object o)
        {
            Int32 result;

            if (o is string)
            {
                result = Int32.Parse((string)o, NumberStyles.Integer | NumberStyles.AllowThousands);
            }
            else
            {
                result = System.Convert.ToInt32(o);
            }

            return result;
        }

        /// <summary>
        /// To convert object to integer
        /// </summary>
        /// <param name="objectFieldValue">Input object value</param>
        /// <param name="integerFieldValue">Integer field value</param>
        /// <returns>Returns a true or false, and a integer representation of an object when true.</returns>
        public static bool GetObjectAsInteger(object objectFieldValue, ref Int32 integerFieldValue)
        {
            try
            {
                // Attempt to parse value into integer
                integerFieldValue = GetAsInteger(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to integer
        /// </summary>
        /// <param name="stringFieldValue">Input string value</param>
        /// <param name="integerFieldValue">Integer field value</param>
        /// <returns>Returns a true or false, and a integer representation of a string when true.</returns>
        public static bool GetStringAsInteger(string stringFieldValue, ref Int32 integerFieldValue)
        {
            try
            {
                // Attempt to parse value into integer
                integerFieldValue = GetAsInteger(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert object to small integer
        /// </summary>
        /// <param name="o">Input object value</param>
        /// <returns>Returns a small integer for the input object</returns>
        public static Int16 GetAsSmallInteger(object o)
        {
            Int16 result;

            if (o is string)
            {
                result = Int16.Parse((string)o, NumberStyles.Integer | NumberStyles.AllowThousands);
            }
            else
            {
                result = System.Convert.ToInt16(o);
            }

            return result;
        }

        /// <summary>
        /// To convert object to small integer
        /// </summary>
        /// <param name="objectFieldValue">Inout object value</param>
        /// <param name="smallIntegerFieldValue">Small integer value</param>
        /// <returns>Returns a true or false, and a small integer representation of a object when true.</returns>
        public static bool GetObjectAsSmallInteger(object objectFieldValue, ref Int16 smallIntegerFieldValue)
        {
            try
            {
                // Attempt to parse value into small integer
                smallIntegerFieldValue = GetAsSmallInteger(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to small integer
        /// </summary>
        /// <param name="stringFieldValue">Input string value</param>
        /// <param name="smallIntegerFieldValue">Small integer value</param>
        /// <returns>Returns a true or false, and a small integer representation of a string when true.</returns>
        public static bool GetStringAsSmallInteger(string stringFieldValue, ref Int16 smallIntegerFieldValue)
        {
            try
            {
                // Attempt to parse value into small integer
                smallIntegerFieldValue = GetAsSmallInteger(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert object to big integer
        /// </summary>
        /// <param name="o">Input object</param>
        /// <returns>Returns a big integer for the object</returns>
        public static Int64 GetAsBigInteger(object o)
        {
            Int64 result;

            if (o is string)
            {
                result = Int64.Parse((string)o, NumberStyles.Integer | NumberStyles.AllowThousands);
            }
            else
            {
                result = System.Convert.ToInt64(o);
            }

            return result;
        }

        /// <summary>
        /// To convert object to big integer value
        /// </summary>
        /// <param name="objectFieldValue">Input object value</param>
        /// <param name="bigIntegerFieldValue">Big integer value</param>
        /// <returns>Returns a true or false, and a big integer representation of a object when true.</returns>
        public static bool GetObjectAsBigInteger(object objectFieldValue, ref Int64 bigIntegerFieldValue)
        {
            try
            {
                // Attempt to parse value into big integer
                bigIntegerFieldValue = GetAsBigInteger(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to Bug integer
        /// </summary>
        /// <param name="stringFieldValue">Input string value</param>
        /// <param name="bigIntegerFieldValue">Big integer value</param>
        /// <returns>Returns a true or false, and a big integer representation of a string when true.</returns>
        public static bool GetStringAsBigInteger(string stringFieldValue, ref Int64 bigIntegerFieldValue)
        {
            try
            {
                // Attempt to parse value into big integer
                bigIntegerFieldValue = GetAsBigInteger(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converts an object to double
        /// </summary>
        /// <param name="o">Inout object</param>
        /// <returns>Returns a double value for the object</returns>
        public static double GetAsDouble(object o)
        {
            double result;

            if (o is string)
            {
                result = double.Parse((string)o, NumberStyles.Float);
            }
            else
            {
                result = System.Convert.ToDouble(o);
            }

            return result;
        }

        /// <summary>
        /// To convert an object value to double
        /// </summary>
        /// <param name="objectFieldValue">Input object value</param>
        /// <param name="doubleFieldValue">Double value</param>
        /// <returns>Returns a true or false, and a double representation of a object when true.</returns>
        public static bool GetObjectAsDouble(object objectFieldValue, ref double doubleFieldValue)
        {
            try
            {
                // Attempt to parse value into double
                doubleFieldValue = GetAsDouble(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert string to double
        /// </summary>
        /// <param name="stringFieldValue">Inout string value</param>
        /// <param name="doubleFieldValue">Double value</param>
        /// <returns>Returns a true or false, and a double representation of a string when true.</returns>
        public static bool GetStringAsDouble(string stringFieldValue, ref double doubleFieldValue)
        {
            try
            {
                // Attempt to parse value into double
                doubleFieldValue = GetAsDouble(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To convert an object to date time
        /// </summary>
        /// <param name="o">Input object</param>
        /// <returns>Returns date time value for object</returns>
        public static DateTime GetAsDateTime(object o)
        {
            DateTime result;

            result = System.Convert.ToDateTime(o);

            return result;
        }

        /// <summary>
        /// To convert an object to datetime
        /// </summary>
        /// <param name="objectFieldValue">Object value</param>
        /// <param name="dateFieldValue">Datetime value</param>
        /// <returns>Returns a true or false, and a date representation of a object when true.</returns>
        public static bool GetObjectAsDateTime(object objectFieldValue, ref DateTime dateFieldValue)
        {
            try
            {
                // Attempt to parse value into datetime
                dateFieldValue = GetAsDateTime(objectFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Converts a string to date time
        /// </summary>
        /// <param name="stringFieldValue">string value</param>
        /// <param name="dateFieldValue">Date time value</param>
        /// <returns>Returns a true or false, and a date representation of a string when true.</returns>
        public static bool GetStringAsDateTime(string stringFieldValue, ref DateTime dateFieldValue)
        {
            try
            {
                // Attempt to parse value into datetime
                dateFieldValue = GetAsDateTime(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// To get string as boolean
        /// </summary>
        /// <param name="stringFieldValue">String value</param>
        /// <param name="booleanFieldValue">Boolean value</param>
        /// <returns>Returns a true or false, and a boolean representation of a string when true</returns>
        public static bool GetStringAsBoolean(string stringFieldValue, ref bool booleanFieldValue)
        {
            try
            {
                // Attempt to parse value into a boolean
                booleanFieldValue = bool.Parse(stringFieldValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Get string from UTC time
        /// </summary>
        /// <param name="utcTime">UTC time</param>
        /// <param name="format">to format</param>
        /// <param name="culture">customer culture</param>
        /// <returns>formatted string of utc time</returns>
        public static string GetDatetimeAsString(DateTime? utcTime, string format)
        {
            if (utcTime.HasValue)
            {
                DateTime datetime = utcTime.Value;
                CultureInfo cultureInfo = new CultureInfo("en-US");

				string datetimeString = string.Empty;

                if (string.IsNullOrEmpty(format))
                {
                   datetimeString= datetime.ToString(cultureInfo.DateTimeFormat);
                }
                else
                {
                    datetimeString= datetime.ToString(format, cultureInfo.DateTimeFormat);
                }

				if (datetimeString.EndsWith("AM"))
					return datetimeString.Substring(0, datetimeString.Length - 2) + "am";
				else if (datetimeString.EndsWith("PM"))
					return datetimeString.Substring(0, datetimeString.Length - 2) + "pm";
				else
					return datetimeString;
            }
            else
            {
                return string.Empty;
            }
        }


        public static string FormatTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }

            title = System.Text.RegularExpressions.Regex.Replace(title.ToLower(), @"[^0-9 a-z]", "");
            if (title.Length >= 50)
            {
                title = title.Substring(0, 50);
				int lastIndex = title.LastIndexOf(" ");
				if (lastIndex > -1)
				{
					title = title.Substring(0, lastIndex).Replace(" ", "-");
				}
            }
            else
            {
                title = title.Substring(0, title.Length);
                title = title.Replace(" ", "-");
            }
            return title;
        }

        #endregion

        /// <summary>
        /// Convert utc datetime to east time
        /// </summary>
        /// <param name="utcDateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertUTCToET(DateTime utcDateTime)
        {
            string timeZoneName = "Eastern Standard Time";

            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, est);
        }

        /// <summary>
        /// convert east time to utc.
        /// </summary>
        /// <param name="etDateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertETToUTC(DateTime etDateTime)
        {
            string timeZoneName = "Eastern Standard Time";

            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            return TimeZoneInfo.ConvertTimeToUtc(etDateTime, est);
        }
        #region Format Bank Account For trading
        /// <summary>
        /// Format bank account number according to the rule
        /// </summary>
        /// <param name="realAccount">bank account</param>
        /// <returns>return string object</returns>
        public static string GetFormatedBankAccount(string realAccount)
        {
            if (string.IsNullOrEmpty(realAccount))
            {
                return realAccount;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();

                char[] charArr = realAccount.ToCharArray();
                for (int i = 0; i < charArr.Length; i++)
                {
                    if (realAccount.Length - 1 >= i + 4)
                    {
                        stringBuilder.Append("*");
                    }
                    else
                    {
                        stringBuilder.Append(charArr[i]);
                    }
                }

                return stringBuilder.ToString();
            }
        }
        #endregion

		/// <summary>
		/// Format Relationship Account Name
		/// </summary>
		/// <param name="nickName">Nick Name</param>
		/// <param name="bankName">Bank Name</param>
		/// <param name="accountNumber">Account Number</param>
		/// <returns></returns>
		public static string GetFormatedDisplayName(string nickName, string substitute, string accountNumber, bool isEncryptAccount)
		{
			string encryptAccountNumber = isEncryptAccount ? GetFormatedBankAccount(accountNumber) : accountNumber;

			if (string.IsNullOrEmpty(nickName) && string.IsNullOrEmpty(substitute))
			{
				return encryptAccountNumber;
			}
			else
			{
                string prefix = nickName;

                if (string.IsNullOrEmpty(prefix))
                {
                    string accountTypeName = (new Regex(@"Penson\s+", RegexOptions.IgnoreCase)).Replace(substitute, string.Empty);
                    prefix = accountTypeName.Length > 16 ? accountTypeName.Substring(0, 16) : accountTypeName;
                }

				//string prefix = !string.IsNullOrEmpty(nickName) ? nickName : (substitute.Length > 16 ? substitute.Substring(0, 16) : substitute);
				return string.Format("{0} - {1}", prefix, encryptAccountNumber);
			}
		}

        /// <summary>
        ///     Get the displaying PensonNickname for the given PensonNickname, PensonAccountId, DisplayName of AccountType
        /// </summary>
        /// <param name="pensonNickname">
        ///     Set PensonNickname
        /// </param>
        /// <param name="pensonAccountId">
        ///     Set PensonAccountId
        /// </param>
        /// <param name="accountDisplayName">
        ///     Set the display name of Account Type
        /// </param>
        /// <returns>
        ///     Return the displaying PensonNickname for the given PensonNickname, PensonAccountId, DisplayName of AccountType
        /// </returns>
        public static string GetDisplayingPensonNickname(string pensonNickname, string pensonAccountId, string accountDisplayName)
        {
            return GetFormatedDisplayName(pensonNickname, accountDisplayName, pensonAccountId, false);
            //return string.Format("{0} - {1}",
            //    string.IsNullOrEmpty(pensonNickname) ? (new Regex(@"Penson\s+", RegexOptions.IgnoreCase)).Replace(accountDisplayName, string.Empty) : pensonNickname,
            //    pensonAccountId);
        }
    }
}