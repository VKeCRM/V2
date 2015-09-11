namespace VKeCRM.Common.Validation
{
    /// <summary>
    /// Summary description for ValidatorBase.
    /// </summary>
    public abstract class ValidatorBase
    {
        protected string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        protected abstract bool PreValidate();
        public abstract bool Validate();
    }
}