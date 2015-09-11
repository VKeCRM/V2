namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Summary description for ReqValidator.
    /// </summary>
    public class RequiredValidator : ValidatorBase
    {
        private string _initialValue = string.Empty;
        private string _valueToCheck = string.Empty;


        public RequiredValidator()
        {
        }

        public RequiredValidator(string valueToCheck)
        {
            _valueToCheck = valueToCheck;
        }

        public RequiredValidator(string valueToCheck, string initialValue)
        {
            _valueToCheck = valueToCheck;
            _initialValue = initialValue;
        }

        public string ValueToCheck
        {
            get { return _valueToCheck; }
            set { _valueToCheck = value; }
        }

        public string InitialValue
        {
            get { return _initialValue; }
            set { _initialValue = value; }
        }

        protected bool CheckValue()
        {
            return !_valueToCheck.Equals(_initialValue, System.StringComparison.InvariantCultureIgnoreCase);
        }

        protected override bool PreValidate()
        {
            bool result = true;

            if ((_valueToCheck == null) || (_valueToCheck.Length == 0))
            {
                _errorMessage = "Value to check is required.";
                result = false;
            }

            if ((_initialValue == null))
            {
                _errorMessage = "Initial value is required.";
                result = false;
            }

            return result;
        }

        public override bool Validate()
        {
            bool result = false;
            try
            {
                if (PreValidate()) result = CheckValue();
            }
            catch (System.Exception ex)
            {
                _errorMessage = ex.Message;
            }

            return result;
        }
    }
}