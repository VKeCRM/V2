using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace VKeCRM.Common
{
    public static class EnumExtension
    {
        public static String GetDescription(this Enum value)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static T ParseToEnum<T>(this String value)
        {
            T enumValue = (T)Enum.Parse(typeof(T), value);
            return enumValue;
        }
    }
}
