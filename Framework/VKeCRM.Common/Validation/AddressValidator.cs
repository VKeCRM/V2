//-----------------------------------------------------------------------
// <copyright file="AddressValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace VKeCRM.Common.Validation
{
	/// <summary>
	/// Validator class for checking address
	/// </summary>
	public class AddressValidator : ValidatorBase
    {
        #region Fields

        /// <summary>
        /// Read only field for states
        /// </summary>
		private static readonly StringDictionary states = null;

        /// <summary>
        /// Read only string for first line in address
        /// </summary>
        private readonly string _address1 = string.Empty;

        /// <summary>
        /// Read only string for second line in address
        /// </summary>
		private readonly string _address2 = string.Empty;

        /// <summary>
        /// Read only string for city
        /// </summary>
		private readonly string _city = string.Empty;

        /// <summary>
        /// Read only boolean value to indicate whether 2nd line in address is required or not
        /// </summary>
		private readonly bool _requireAddress2 = false;

        /// <summary>
        /// Read only boolean value to indicate whether Zip code requires to have 4 more digits
        /// </summary>
		private readonly bool _requireZipPlus4 = false;

        /// <summary>
        /// String for State
        /// </summary>
		private readonly string _state = string.Empty;

        /// <summary>
        /// Address Zipcode
        /// </summary>
		private readonly string _zipCode = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes static members of the AddressValidator class
        /// </summary>
		static AddressValidator()
		{
			states = new StringDictionary();

			states.Add("DC", "washington dc");
			states.Add("DE", "delaware");
			states.Add("PA", "pennsylvania");
			states.Add("NJ", "new jersey");
			states.Add("GA", "georgia");
			states.Add("CT", "connecticut");
			states.Add("MA", "massachusetts");
			states.Add("MD", "maryland");
			states.Add("SC", "south carolina");
			states.Add("NH", "new hamshire");
			states.Add("VA", "virginia");
			states.Add("NY", "new york");
			states.Add("NC", "north carolina");
			states.Add("RI", "rhode island");
			states.Add("VT", "vermont");
			states.Add("KY", "kentucky");
			states.Add("TN", "tennessee");
			states.Add("OH", "ohio");
			states.Add("LA", "louisiana");
			states.Add("IN", "indiana");
			states.Add("MS", "mississippi");
			states.Add("IL", "illinois");
			states.Add("AL", "alabama");
			states.Add("ME", "maine");
			states.Add("MO", "missouri");
			states.Add("AR", "arkansas");
			states.Add("MI", "michigan");
			states.Add("FL", "florida");
			states.Add("TX", "texas");
			states.Add("IA", "iowa");
			states.Add("WI", "wisconsin");
			states.Add("CA", "california");
			states.Add("MN", "minnesota");
			states.Add("OR", "oregon");
			states.Add("KS", "kansas");
			states.Add("WV", "west virginia");
			states.Add("NV", "nevada");
			states.Add("NE", "nebraska");
			states.Add("CO", "colorado");
			states.Add("ND", "north dakota");
			states.Add("SD", "south dakota");
			states.Add("MT", "montana");
			states.Add("WA", "washington");
			states.Add("ID", "idaho");
			states.Add("WY", "wyoming");
			states.Add("UT", "utah");
			states.Add("OK", "oklahoma");
			states.Add("NM", "new mexico");
			states.Add("AZ", "arizona");
			states.Add("AK", "alaska");
			states.Add("HI", "hawaii");
		}

        /// <summary>
        /// Initializes a new instance of the AddressValidator class
        /// </summary>
		public AddressValidator()
		{
		}

        /// <summary>
        /// Initializes a new instance of the AddressValidator class
        /// </summary>
        /// <param name="address1">1st line in address</param>
        /// <param name="address2">2nd line in address</param>
        /// <param name="city">City name in address</param>
        /// <param name="state">State in address</param>
        /// <param name="zipCode">Zip code in address</param>
		public AddressValidator(string address1, string address2, string city, string state, string zipCode)
		{
			this._address1 = address1;
			this._address2 = address2;
			this._city = city;
			this._state = state;
			this._zipCode = zipCode;
		}

        /// <summary>
        /// Initializes a new instance of the AddressValidator class
        /// </summary>
        /// <param name="address1">1st line in address</param>
        /// <param name="address2">2nd line in address</param>
        /// <param name="city">City name in address</param>
        /// <param name="state">State in address</param>
        /// <param name="zipCode">Zip code in address</param>
        /// <param name="requireAddress2">A boolean value to indicate whether 2nd line in address is required or not</param>
        /// <param name="requireZipPlus4">Read only boolean value to indicate whether Zip code requires to have 4 more digits</param>
		public AddressValidator(string address1, string address2, string city, string state, string zipCode, bool requireAddress2, bool requireZipPlus4)
		{
			this._address1 = address1;
			this._address2 = address2;
			this._city = city;
			this._state = state;
			this._zipCode = zipCode;
			this._requireAddress2 = requireAddress2;
			this._requireZipPlus4 = requireZipPlus4;
        }

        #endregion

        #region Methods

        /// <summary>
        /// To validate the address
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the address is validated</returns>
		public override bool Validate()
		{
			bool result = false;
			try
			{
                if (PreValidate())
                {
                    result = IsValidAddress();
                }
			}
			catch (System.Exception ex)
			{
				_errorMessage = ex.Message;
			}

			return result;
        }

        /// <summary>
        /// To pre-validate the address
        /// </summary>
        /// <returns>Returns a boolean value to indicate whether the address is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;
            _errorMessage = string.Empty;
            return result;
        }

        /// <summary>
        /// To check if the address is valid or not
        /// </summary>
        /// <returns>Returns a boolean value indicating whether address is valid or not</returns>
        private bool IsValidAddress()
        {
            bool result = true;

            _errorMessage = string.Empty;

            if (_address1 == null || _address1.Length == 0)
            {
                _errorMessage += "Address line 1 is required.\n";
            }

            if ((_address2 == null || _address2.Length == 0) && _requireAddress2)
            {
                _errorMessage += "Address line 2 is required.\n";
                result = false;
            }

            if (_city == null || _city.Length == 0)
            {
                _errorMessage += "City is required.\n";
                result = false;
            }

            if (_state == null || _state.Length == 0)
            {
                _errorMessage += "State is required.\n";
                result = false;
            }
            else
            {
                if (!states.ContainsKey(_state) && !states.ContainsValue(_state.ToLower()))
                {
                    _errorMessage += "A valid state is required.\n";
                    result = false;
                }
            }

            if (_zipCode == null || _zipCode.Length == 0)
            {
                _errorMessage += "Zip code is required.\n";
                result = false;
            }
            else
            {
                Regex re;
                if (_requireZipPlus4)
                {
                    re = new Regex(@"\d{9}");
                    if (!re.Match(_zipCode).Success)
                    {
                        _errorMessage += "Zip code and Plus 4 (9 digits, no dash) is required.\n";
                        result = false;
                    }
                }
                else
                {
                    re = new Regex(@"\d{5}");
                    if (!re.Match(_zipCode).Success)
                    {
                        _errorMessage += "Zip code (5 digits) is required.\n";
                        result = false;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}