using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Common.Validation.Mvc
{
	public class MvcCompareAttribute : MvcValidationAttribute
	{
		private DataType _dataType = DataType.String;
		public DataType DataType
		{
			get { return _dataType; }
			set { _dataType = value; }
		}

		private ValidationCompareOperator _compareOperator = ValidationCompareOperator.Equal;
		public ValidationCompareOperator CompareOperator
		{
			get { return _compareOperator; }
			set { _compareOperator = value; }
		}

		public object ValueToCompare
		{
			get;
			set;
		}

		public string CompareToValue
		{
			get;
			set;
		}

        public bool AlwaysTrueForNullValue
        {
            get;
            set;
        }

		public override bool IsValid(object value)
		{
            if (AlwaysTrueForNullValue && (ValueToCompare == null || value == null))
            {
                return true;
            }

			if (ValueToCompare == null || string.IsNullOrEmpty(ValueToCompare.ToString()))
			{
				return true;
			}

			if (value == null)
			{
				return false;
			}

			CompareValidatorBase compare = null;

			switch (DataType)
			{
				case DataType.String:
					{
						compare = new CompareStringValidator(true, CompareOperator, value, ValueToCompare);
						break;
					}

				case DataType.Integer:
					{
						compare = new CompareIntegerValidator(CompareOperator, value, ValueToCompare);
						break;
					}

				case DataType.Decimal:
					{
						compare = new CompareDecimalValidator(CompareOperator, value, ValueToCompare);
						break;
					}

				case DataType.Double:
					{
						compare = new CompareDoubleValidator(CompareOperator, value, ValueToCompare);
						break;
					}

				case DataType.DateTime:
					{
						compare = new CompareDateTimeValidator(CompareOperator, value, ValueToCompare);
						break;
					}
			}

			return compare.Validate();
		}

		public override MvcValidationType ValidationType
		{
			get { return MvcValidationType.MvcCompare; }
		}
	}

	public enum DataType
	{
		String = 0,

		Integer = 1,

		Decimal = 2,

		Double = 3,

		DateTime = 4
	}

	public enum CompareOperator
	{
		Equal = 0,

		NotEqual = 1,

		GreaterThan = 2,

		GreaterThanEqual = 3,

		LessThan = 4,

		LessThanEqual = 5,

		DataTypeCheck = 6,
	}
}
