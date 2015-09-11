using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace VKeCRM.Framework.Data
{
    /// <summary>
    ///  Contrl the inject sql for hql for HQL string and parameters
    /// </summary>
    public class HqlParameterHelper
    {
        #region params

        /// <summary>
        /// Contain the values
        /// </summary>
        private Dictionary<string, HqlParameter> _parameterValues;
        public Dictionary<string, HqlParameter> ParameterValues
        {
            get
            {
                if (_parameterValues == null)
                {
                    _parameterValues = new Dictionary<string, HqlParameter>();
                }
                return _parameterValues;
            }
        }

        /// <summary>
        /// Store hql string
        /// </summary>
        private string _hql;
        public string Hql
        {
            get
            {
                return _hql;
            }
        }

        /// <summary>
        /// Store hql string
        /// </summary>
        private string _countHql;
        public string CountHql
        {
            get
            {
                return _countHql;
            }
        }
        #endregion

        #region
        private HqlParameterHelper(string hql)
        {
            if (!ValidateHql(hql))
            {
                throw new Exception(string.Format("hql contain forbidden charactor : {0}", hql));
            }
            _hql = hql;
        }

        private HqlParameterHelper(string hql,string countHql)
        {
            if (!ValidateHql(hql))
            {
                throw new Exception(string.Format("hql contain forbidden charactor : {0}", hql));
            }
            _hql = hql;
            if (!ValidateHql(countHql))
            {
                throw new Exception(string.Format("hql contain forbidden charactor : {0}", countHql));
            }
            _countHql = countHql;
        }
        #endregion

        #region public methods
        public static HqlParameterHelper CreateInstance(string hql)
        {
            return new HqlParameterHelper(hql);
        }

        public static HqlParameterHelper CreateInstance(string hql,string countHql)
        {
            return new HqlParameterHelper(hql,countHql);
        }

        public HqlParameterHelper AppendSql(string sql)
        {
            if (ValidateHql(sql) == false)
            {
                throw new Exception(string.Format("hql contain forbidden charactor : {0}", sql));
            }
            _hql = string.Concat(_hql," ", sql);
            return this;
        }

        public HqlParameterHelper AppendCountSql(string sql)
        {
            if (ValidateHql(sql) == false)
            {
                throw new Exception(string.Format("hql contain forbidden charactor : {0}", sql));
            }
            _countHql = string.Concat(_countHql, " ", sql);
            return this;
        }

        public HqlParameterHelper SetInt32(string name, int val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name, SqlParamType.Int32,val));
            return this;
        }

        public HqlParameterHelper SetInt16(string name, short val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name,SqlParamType.Int16,val));
            return this;
        }

        public HqlParameterHelper SetString(string name, string val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name,SqlParamType.String,val));
            return this;
        }

        public HqlParameterHelper SetGuid(string name, Guid val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name, SqlParamType.Guid, val));
            return this;
        }

        public HqlParameterHelper SetDecimal(string name, decimal val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name, SqlParamType.Decimal, val));
            return this;
        }

        public HqlParameterHelper SetDouble(string name, double val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name,SqlParamType.Double,val));
            return this;
        }

        public HqlParameterHelper SetListInt32(string name, List<Int32> val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name, SqlParamType.ListInt32, val));
            return this;
        }

        public HqlParameterHelper SetListString(string name, List<string> val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name, SqlParamType.ListString, val));
            return this;
        }

        public HqlParameterHelper SetDateTime(string name, DateTime val)
        {
            ParameterValues.Add(name, HqlParameter.CreateInstance(name, SqlParamType.DateTime, val));
            return this;
        }
        #endregion

        #region private methods
        /// <summary>
        /// The hql could not contain invalid char eq : char(')
        /// </summary>
        /// <param name="hql">The hql string need to been checked</param>
        /// <returns></returns>
        public static bool ValidateHql(string hql)
        {
            string testString = hql;
            if (testString.Replace("''","").IndexOf("'") >= 0)
            {
                return false;
            }
            return true;
        }
        #endregion
    }

    public enum SqlParamType
    {
        Int16 = 0,
        Int32 = 1,
        String = 2,
        DateTime = 3,
        ListInt32 = 4,
        ListInt16 = 5,
        ListString = 7,
        Guid = 8,
        Double = 9,
        Decimal = 10
    }

    public class HqlParameter
    {
        private HqlParameter()
        {
        }

        private HqlParameter(string name, SqlParamType type, object value)
        {
            this.Name = name;
            this.Type = type;
            this.Value = value;
        }

        public static HqlParameter CreateInstance(string name, SqlParamType type, object value)
        {
            return new HqlParameter(name, type, value);
        }

        public string Name
        {
            get; set;
        }

        public SqlParamType Type
        {
            get;
            set;
        }

        public object Value
        {
            get; set;
        }
    }
}
