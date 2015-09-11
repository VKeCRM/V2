//-----------------------------------------------------------------------
// <copyright file="DateValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Text.RegularExpressions;
using System;
using System.Globalization;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// To validate the email address
	/// </summary>
	public class DateValidator : ValidatorBase
	{
		#region Fields

		/// <summary>
		/// Date to validate
		/// </summary>
		private string _dateText = string.Empty;
		private string[] _dateFormats = new string[] { };

		public string DefaultErrorMessage
		{
			get;
			set;
		}

		public string MessageOfInValidDate
		{
			get;
			set;
		}

		public string MessageOfInValidYear
		{
			get;
			set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the DateValidator class
		/// </summary>
		public DateValidator()
		{

		}

		/// <summary>
		/// Initializes a new instance of the DateValidator class
		/// </summary>
		/// <param name="emailAddress">Date to validate</param>
		public DateValidator(string dateText, string[] dateFormats)
		{
			_dateText = dateText;
			_dateFormats = dateFormats;
		}

		#endregion

		#region Methods

		/// <summary>
		/// To validate the date
		/// </summary>
		/// <returns>Returns a boolean value to indicate whether the date is validated</returns>
		public override bool Validate()
		{
			if (string.IsNullOrEmpty(_dateText))
			{
				return false;
			}

			if (_dateFormats == null || _dateFormats.Length == 0)
			{
				return false;
			}

			bool isMatch = false;

			foreach (string format in _dateFormats)
			{
				if (!string.IsNullOrEmpty(format))
				{
					try
					{
						DateTime time = DateTime.ParseExact(_dateText, format, new CultureInfo("en-US"), DateTimeStyles.None);

						// if the format is matched, break.
						if (time.Year < 1900)
						{
							_errorMessage = MessageOfInValidYear;
						}
						else
						{
							isMatch = true;
						}
						break;
					}
					catch
					{
						//do nothing.
						_errorMessage = DefaultErrorMessage;

						// TODO:A need to check whether the date is like 02/30/2009
						//_errorMessage = MessageOfInValidDate;
					}
				}
			}

			return isMatch;
		}

		/// <summary>
		/// Pre-validate the email address
		/// </summary>
		/// <returns>Returns a boolean value to indicate whether the date is pre-validated</returns>
		protected override bool PreValidate()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}