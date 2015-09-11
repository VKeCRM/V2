using System;
using System.Reflection;

namespace VKeCRM.Common.Utility
{
	public static class ObjectCopyExtensions
	{
		public static T Copy<T>(this T obj)
		{
			Object targetDeepCopyObj;
			Type targetType = obj.GetType();
			if (targetType.IsValueType)
			{
				targetDeepCopyObj = obj;
			}
			else
			{
				targetDeepCopyObj = Activator.CreateInstance(targetType);
				MemberInfo[] memberCollection = obj.GetType().GetMembers();

				foreach (MemberInfo member in memberCollection)
				{
					if (member.MemberType == MemberTypes.Field)
					{
						var field = (FieldInfo)member;
						Object fieldValue = field.GetValue(obj);
						if (fieldValue is ICloneable)
						{
							field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
						}
						else
						{
							field.SetValue(targetDeepCopyObj, Copy(fieldValue));
						}
					}
					else if (member.MemberType == MemberTypes.Property)
					{
						var myProperty = (PropertyInfo)member;
						MethodInfo info = myProperty.GetSetMethod(false);
						if (info != null)
						{
							object propertyValue = myProperty.GetValue(obj, null);
							if (propertyValue is ICloneable)
							{
								myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
							}
							else
							{
								myProperty.SetValue(targetDeepCopyObj, Copy(propertyValue), null);
							}
						}
					}
				}
			}
			return (T)targetDeepCopyObj;
		}
	}
}