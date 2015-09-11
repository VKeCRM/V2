using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VKeCRM.Common.Validation.Mvc;

namespace VKeCRM.Framework.Mvc.Exceptions
{
	[Serializable]
	public class PropertyModel
	{
		public PropertyModel(string propertyName, string errorMessage, MvcValidationType? mvcValidationType, string parent, int? listIndex)
		{
			PropertyName = propertyName;
			ErrorMessage = errorMessage;
			MvcValidationType = mvcValidationType.HasValue ? mvcValidationType.ToString() : null;
			Parent = parent;
			ListIndex = listIndex;
		}

		//pz: keep the old constructor for backward compatibility
		public PropertyModel(string propertyName, string errorMessage, MvcValidationType? mvcValidationType) 
			: this(propertyName, errorMessage, mvcValidationType, string.Empty, null){}

		public string Parent
		{
			get;
			set;
		}

		public int? ListIndex
		{
			get;
			set;
		}

		public string PropertyName
		{
			get;
			set;
		}

		public string ErrorMessage
		{
			get;
			set;
		}

		public string MvcValidationType
		{
			get;
			set;
		}
	}
}
