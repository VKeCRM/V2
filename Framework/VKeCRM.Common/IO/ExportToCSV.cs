using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VKeCRM.Common.Collections;

namespace VKeCRM.Common.IO
{
    public static class ExportToCSV
    {
        public static string ExportDataToCSV<T>(IList<T> list)
        {
            string csv = "";
            string columns = "";
            string data = "";

            if (list != null || list.Count > 0)
            {
                System.Reflection.PropertyInfo[] myPropertyInfo =
                    typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    System.Reflection.PropertyInfo pi = myPropertyInfo[i];
                    columns += "\"" + pi.Name + "\"";
                    if (i + 1 < j) columns = columns + ",";
                }
                columns = columns + "\n";

                foreach (T t in list)
                {
                    if (t == null)
                    {
                        continue;
                    }

                    for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                    {
                        System.Reflection.PropertyInfo pi = myPropertyInfo[i];
                        data += "\"" + pi.GetValue(t, null) + "\"";
                        if (i + 1 < j) data = data + ",";
                    }
                    data = data + "\n";
                }

                csv = columns + data;
            }

            return csv;
        }

        /// <summary>
        /// Only export inline data without columns to csv
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>return data record string</returns>
        public static string ExportDataWithoutColumnsToCSV<T>(IList<T> list)
        {
            string data = string.Empty;

            if (list != null || list.Count > 0)
            {
                System.Reflection.PropertyInfo[] myPropertyInfo =
                            typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                foreach (T t in list)
                {
                    if (t == null)
                    {
                        continue;
                    }

                    for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                    {
                        System.Reflection.PropertyInfo pi = myPropertyInfo[i];
                        if (pi.PropertyType.ToString().Contains("System.Collections.Generic.List"))
                        {
                            string strRule = string.Empty;
                            List<String> listRule = (List<String>)pi.GetValue(t, null);
                            foreach (string str in listRule)
                            {
                                strRule += str + ",";
                            }
                            strRule = strRule.TrimEnd(',');
                            data += "\"" + strRule + "\"";
                        }
                        else
                        {
                            data += "\"" + pi.GetValue(t, null) + "\"";
                        }
                        if (i + 1 < j) data = data + ",";
                    }
                    data = data + "\n";
                }
            }
            return data;
        }

        /// <summary>
        /// Export columns to csv
        /// </summary>
        /// <param name="list">columns title</param>
        /// <returns>return all columns string</returns>
        public static string ExportColumnsToCSV(IList<string> list)
        {
            string columns = string.Empty;
            if (list != null || list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    columns += "\"" + list[i] + "\"";
                    if (i + 1 < list.Count) columns = columns + ",";
                }
                columns = columns + "\n";

            }
            return columns;
        }
    }
}
