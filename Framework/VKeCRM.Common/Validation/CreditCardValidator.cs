//-----------------------------------------------------------------------
// <copyright file="CreditCardValidator.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Validator class for Credit cards
    /// </summary>
    public class CreditCardValidator : ValidatorBase
    {
        #region CreditCardType enum

        /// <summary>
        /// Enumerator for types of credit cards
        /// </summary>
        public enum CreditCardType
        {
            /// <summary>
            /// Indicates that the credit card is invalid
            /// </summary>
            Invalid = -1,

            /// <summary>
            /// Indicates that it is a Visa card
            /// </summary>
            Visa = 1,

            /// <summary>
            /// Indicates that it is Mastercard
            /// </summary>
            MasterCard,

            /// <summary>
            /// Indicates that it is Discover card
            /// </summary>
            Discover,

            /// <summary>
            /// Indicates that it is an American Express card
            /// </summary>
            AmericanExpress
        }

        #endregion

        #region Private Fields
        /// <summary>
        /// Month when the card expires
        /// </summary>
        private readonly int _cardMonth = DateTime.Today.Month;

        /// <summary>
        /// Credit card number
        /// </summary>
        private readonly string _cardNo = string.Empty;

        /// <summary>
        /// Type of credit card
        /// </summary>
        private readonly CreditCardType _cardType = CreditCardType.Invalid;

        /// <summary>
        /// Year in which the card expires
        /// </summary>
        private readonly int _cardYear = DateTime.Today.Year;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new innstance of the CreditCardValidator class
        /// </summary>
        public CreditCardValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CreditCardValidator class
        /// </summary>
        /// <param name="cardNo">Card Number</param>
        /// <param name="cardType">Type of Card</param>
        /// <param name="cardMonth">Month when card expires</param>
        /// <param name="cardYear">Year when card expires</param>
        public CreditCardValidator(string cardNo, int cardType, int cardMonth, int cardYear)
        {
            this._cardNo = cardNo;
            this._cardType = (CreditCardType)Enum.Parse(typeof(CreditCardType), cardType.ToString());
            this._cardMonth = cardMonth;
            this._cardYear = cardYear;
        }

        /// <summary>
        /// Initializes a new instance of the CreditCardValidator class
        /// </summary>
        /// <param name="cardNo">Card Number</param>
        /// <param name="cardType">Type of Card</param>
        /// <param name="cardMonth">Month when card expires</param>
        /// <param name="cardYear">Year when card expires</param>
        public CreditCardValidator(string cardNo, string cardType, string cardMonth, string cardYear)
        {
            this._cardNo = cardNo;
            this._cardType = (CreditCardType)Enum.Parse(typeof(CreditCardType), cardType);
            this._cardMonth = Convert.ToInt32(cardMonth);
            this._cardYear = Convert.ToInt32(cardYear);
        }

        /// <summary>
        /// Initializes a new instance of the CreditCardValidator class
        /// </summary>
        /// <param name="cardNo">Card Number</param>
        /// <param name="cardType">Type of Card</param>
        /// <param name="cardMonth">Month when card expires</param>
        /// <param name="cardYear">Year when card expires</param>
        public CreditCardValidator(string cardNo, CreditCardType cardType, int cardMonth, int cardYear)
        {
            this._cardNo = cardNo;
            this._cardType = cardType;
            this._cardMonth = cardMonth;
            this._cardYear = cardYear;
        }

        #endregion

        /// <summary>
        /// To check if card is valid
        /// </summary>
        /// <returns>Returns a boolean value indicating if the card is valid</returns>
        private bool IsValidCreditCard()
        {
            _errorMessage = string.Empty;

            //most of these checks are self explanitory
            if (_cardNo.Length == 0)
            {
                _errorMessage = "A valid card number is required.";
                return false;
            }

            // make sure the number is all digits.. (by design)
            char[] digits = _cardNo.ToCharArray();
            for (int index = 0; index < digits.Length; ++index)
            {
                char c = digits[index];
                if (c < '0' || c > '9')
                {
                    _errorMessage = "A valid card number is required. Use digits only, no spaces or hyphens.";
                    return false;
                }
            }

            //perform card specific length and prefix tests
            int length = _cardNo.Length;
            int prefix;
            switch (_cardType)
            {
                case CreditCardType.AmericanExpress:
                    if (length != 15)
                    {
                        _errorMessage = "American Express Card number must be 15 digits long.";
                        return false;
                    }

                    prefix = System.Convert.ToInt32(_cardNo.Substring(0, 2));
                    if (prefix != 34 && prefix != 37)
                    {
                        _errorMessage = "American Express Card number must start with 34 or 37.";
                        return false;
                    }
                    break;

                case CreditCardType.Discover:
                    if (length != 16)
                    {
                        _errorMessage = "Discover Card number must be 16 digits long.";
                        return false;
                    }

                    prefix = System.Convert.ToInt32(_cardNo.Substring(0, 4));
                    if (prefix != 6011)
                    {
                        _errorMessage = "Discover Card number must start with 6011.";
                        return false;
                    }
                    break;

                case CreditCardType.MasterCard:
                    if (length != 16)
                    {
                        _errorMessage = "MasterCard number must be 16 digits long.";
                        return false;
                    }

                    prefix = System.Convert.ToInt32(_cardNo.Substring(0, 2));
                    if (prefix < 51 || prefix > 55)
                    {
                        _errorMessage = "MasterCard Card number must start with 51 or 55.";
                        return false;
                    }
                    break;

                case CreditCardType.Visa:
                    if (length != 16 && length != 13)
                    {
                        _errorMessage = "Visa Card number must be 13 or 16 digits long.";
                        return false;
                    }

                    prefix = System.Convert.ToInt32(_cardNo.Substring(0, 1));
                    if (prefix != 4)
                    {
                        _errorMessage = "Visa Card number must start with 4.";
                        return false;
                    }
                    break;
            }

            // run the check digit algorithm
            if (!Mod10())
            {
                _errorMessage = "Invalid credit card number.";
                return false;
            }

            // check if entered date is already expired.
            if (Expired())
            {
                _errorMessage = "Invalid expiration date.";
                return false;
            }

            // at this point card has not been proven to be invalid    
            return true;
        }

        /// <summary>
        /// Checks the number of digits in the credit card number
        /// </summary>
        /// <returns>Returns a boolean value indicating if card has valid number of digits as card number</returns>
        private bool Mod10()
        {
            int[] array = new int[_cardNo.Length];
            int index;
            int sum = 0;

            for (index = 0; index < _cardNo.Length; ++index)
            {
                array[index] = Int32.Parse(_cardNo.Substring(index, 1));
            }

            // you have to start from the right, and work back.
            for (index = array.Length - 2; index >= 0; index -= 2)
            {
                // every second digit starting with the right most (check digit)
                array[index] *= 2;

                // will be doubled, and summed with the skipped digits.
                // if the double digit is > 9, ADD those individual digits together
                if (array[index] > 9)
                {
                    array[index] -= 9;
                }
            }

            for (index = 0; index < array.Length; ++index)
            {
                // if the sum is divisible by 10 mod10 succeeds
                sum += array[index];
            }
            return (((sum%10) == 0) ? true : false);
        }

        /// <summary>
        /// Check whether card has expired
        /// </summary>
        /// <returns>Returns a boolean value indicating if card has expired</returns>
        public static bool Expired(int cardYear, int cardMonth)
        {
            bool result = true;

            int currentYear = DateTime.Today.Year;
            int currentMonth = DateTime.Today.Month;

            if (cardYear > currentYear)
            {
                result = false;
            }
            else if ((cardYear == currentYear) && (cardMonth >= currentMonth))
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Check whether card has expired
        /// </summary>
        /// <returns>Returns a boolean value indicating if card has expired</returns>
        private bool Expired()
        {
            return Expired(_cardYear, _cardMonth);
        }

        /// <summary>
        /// Pre-validate the credit card
        /// </summary>
        /// <returns>Returns a boolean value indicating if card is pre-validated</returns>
        protected override bool PreValidate()
        {
            bool result = true;

            // check datatypes
            if (_cardNo == null)
            {
                _errorMessage = "A credit card number is required.";
                result = false;
            }

            if (_cardType == CreditCardType.Invalid)
            {
                _errorMessage = "A valid credit card type must be selected.";
                result = false;
            }

            if ((_cardMonth < 1) || (_cardMonth > 12))
            {
                _errorMessage = "A valid expiration month is required.";
                result = false;
            }

            if (_cardYear < DateTime.Today.Year)
            {
                _errorMessage = "A valid expiration year is required.";
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Validate the credit card
        /// </summary>
        /// <returns>Returns a boolean value indicating if card is validated</returns>
        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate())
                {
                    result = IsValidCreditCard();
                }
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }
    }
}