using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VKeCRM.Framework.Data
{
    public class ParamInfo
    {
        public string Name;
        private object _value=null;
        public object Value
        {
            get
            {
                if (_value == null)
                    return DBNull.Value;
                else
                    return _value;
            }
            set
            {
                _value = value;
            }
        }        
        public DbType Type;       
        public bool IsOutput=false;

        public ParamInfo()
        { 
        }
    } 
}
