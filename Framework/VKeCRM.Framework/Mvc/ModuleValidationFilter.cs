using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;

using VKeCRM.Common.Validation.Mvc;
using VKeCRM.Common.DataTransferObjects;
using VKeCRM.Framework.Mvc.Exceptions;
using System.Collections;

namespace VKeCRM.Framework.Mvc
{
	public class ModuleValidationFilter : ActionFilterAttribute
	{
		private const string KeyFormat = "{0}#{1}#{2}";

		private static object _sync = new object();
		private static Dictionary<string, MvcValidationAttribute[]> _validationAttributeTable = null;

		static ModuleValidationFilter()
		{
			_validationAttributeTable = new Dictionary<string, MvcValidationAttribute[]>();
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			// call base method
			base.OnActionExecuting(filterContext);

			// using System.Reflection to get method info
			string actionMethodName = filterContext.ActionDescriptor.ActionName;
			MethodInfo methodInfo = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.GetMethod(actionMethodName);

			// get this action's module type
			ParameterInfo[] parameters = methodInfo.GetParameters();
			ParameterInfo parameterInfo = parameters.FirstOrDefault(p => p.ParameterType.GetInterface(typeof(IMvcModule).FullName, true) != null);

			IMvcModule mvcModule = (null == parameterInfo) ? null : filterContext.ActionParameters[parameterInfo.Name] as IMvcModule;

			if (null != mvcModule)
			{
				List<PropertyModel> validationInfo = new List<PropertyModel>();

				DoValidation(mvcModule, null, null, ref validationInfo);

				// generate error message if has.
				if (validationInfo.Count > 0)
				{
					throw new MvcValidationException(validationInfo);
				}
			}
		}

		private void DoValidation(IMvcModule modelInstance, string parent, int? listIndex, ref List<PropertyModel> validationInfo)
		{
			// get runtime type.
			Type modelType = modelInstance.GetType();

			// get all properties of the object.
			PropertyInfo[] properties = modelType.GetProperties();

			foreach (PropertyInfo propertyInfo in properties)
			{
				object propertyValue = null;

				// todo: need to do filter those are not supported type

				// is GenericType like: List<T>, current we only support List<T> & IList<T>.
				if (propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetInterface(typeof(IList<>).FullName) != null)
				{
					// we only support onetime loop.
					if (!string.IsNullOrEmpty(parent))
						continue;

					// get list of GenericType, type of <T>.
					Type[] types = propertyInfo.PropertyType.GetGenericArguments();

					// check if the GenericType implements IMvcModule interface?
					Type tType = types.FirstOrDefault(p => p.GetInterface(typeof(IMvcModule).FullName) != null);
					if (tType != null)
					{
						// reflect value of the property
						propertyValue = propertyInfo.GetValue(modelInstance, null);

						// we only support list, all list implements interface IEnumerable.
						IEnumerable enumerable = propertyValue as IEnumerable;
						if (null != enumerable)
						{
							int i = 0;
							foreach (object instance in enumerable)
							{
								DoValidation(instance as IMvcModule, propertyInfo.Name, i, ref validationInfo);
								i++;
							}
						}
					}
				}
				else
				{
					MvcValidationAttribute[] attributes = GetValidationAttributes(parent, propertyInfo);

					if (attributes.Length != 0)
					{
						// reflect value of the property
						propertyValue = propertyInfo.GetValue(modelInstance, null);

						foreach (MvcValidationAttribute attribute in attributes)
						{
							// check if it is ingored
							if (!string.IsNullOrEmpty(attribute.IngoreTaret))
							{
								object theValue = GetValueByPropertyName(modelType, modelInstance, attribute.IngoreTaret);

								// ingored?
								if (theValue != null && string.Equals(theValue.ToString(), attribute.IngoreValue, StringComparison.CurrentCultureIgnoreCase))
									continue;
							}

							// pre-fill ValueToCompare before checking
							if (attribute.ValidationType == MvcValidationType.MvcCompare)
								(attribute as MvcCompareAttribute).ValueToCompare = GetValueByPropertyName(modelType, modelInstance, (attribute as MvcCompareAttribute).CompareToValue);

							// check IsValid?
							if (!attribute.IsValid(propertyValue))
							{
								validationInfo.Add(new PropertyModel(propertyInfo.Name, attribute.ErrorMessage, attribute.ValidationType, parent, listIndex));
								break;
							}
						}
					}
				}
			}
		}

		private object GetValueByPropertyName(Type type, object instance, string propertyName)
		{
			PropertyInfo info = type.GetProperty(propertyName);
			if(null == info)
			{
				throw new Exception(string.Format("Cannot find property {0} in class {1}", propertyName, type.FullName));
			}

			return info.GetValue(instance, null);
		}

		/// <summary>
		/// Get property ValidationAttributes, sort by sequence.
		/// </summary>
		/// <param name="propertyInfo">propertyInfo</param>
		/// <returns>array of validation attributes</returns>
		private MvcValidationAttribute[] GetValidationAttributes(string parent, PropertyInfo propertyInfo)
		{
			string key = string.Format(KeyFormat, parent, propertyInfo.DeclaringType.FullName, propertyInfo.Name);

			lock (_sync)
			{
				if (!_validationAttributeTable.ContainsKey(key))
				{
					object[] validationAttributes = propertyInfo.GetCustomAttributes(typeof(MvcValidationAttribute), true);
					List<MvcValidationAttribute> list = new List<MvcValidationAttribute>();

					foreach (object validation in validationAttributes)
					{
						list.Add(validation as MvcValidationAttribute);
					}

					MvcValidationAttribute[] arr = list.ToArray();
					Array.Sort(arr);

					_validationAttributeTable.Add(key, arr);
				}

				return _validationAttributeTable[key];
			}
		}
	}
}
