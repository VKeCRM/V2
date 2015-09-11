using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace VKeCRM.Framework.Data
{
    public static class QueryExtension
    {
        public static void SetInt32(this IQuery query, int position, int? val)
        {
            if (val.HasValue)
            {
                query.SetInt32(position, val.Value);
            }
            else
            {
                query.SetParameter(position, null, NHibernateUtil.Int32);
            }
        }
        public static void SetInt32(this IQuery query, string name, int? val)
        {
            if (val.HasValue)
            {
                query.SetInt32(name, val.Value);
            }
            else
            {
                query.SetParameter(name, null, NHibernateUtil.Int32);
            }
        }

        public static void SetDateTime(this IQuery query, int position, DateTime? val)
        {
            if (val.HasValue)
            {
                query.SetDateTime(position, val.Value);
            }
            else
            {
                query.SetParameter(position, null, NHibernateUtil.DateTime);
            }
        }
        public static void SetDateTime(this IQuery query, string name, DateTime? val)
        {
            if (val.HasValue)
            {
                query.SetDateTime(name, val.Value);
            }
            else
            {
                query.SetParameter(name, null, NHibernateUtil.DateTime);
            }
        }

        public static void SetGuid(this IQuery query, int position, Guid? val)
        {
            if (val.HasValue)
            {
                query.SetGuid(position, val.Value);
            }
            else
            {
                query.SetParameter(position, null, NHibernateUtil.Guid);
            }
        }
        public static void SetGuid(this IQuery query, string name, Guid? val)
        {
            if (val.HasValue)
            {
                query.SetGuid(name, val.Value);
            }
            else
            {
                query.SetParameter(name, null, NHibernateUtil.Guid);
            }
        }
    }
}
