using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;
using VKeCRM.Common.Collections;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;

namespace VKeCRM.Framework.Data
{
    public class StoredProcedureHelper
    {
        public static VKList<TResult> Execute<TResult>(string spName, IDictionary<string, object> parameters, bool isAutoMapping)
        {
            return Execute<TResult>(spName, parameters, null, isAutoMapping);
        }

        public static VKList<TResult> Execute<TResult>(string spName, IDictionary<string, object> parameters, IDictionary<string, object[]> parameterLists, bool isAutoMapping)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            IQuery query = session.GetNamedQuery(spName);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    query = query.SetParameter(parameter.Key, parameter.Value);

                }
            }
            if (parameterLists != null && parameterLists.Count > 0)
            {
                foreach (var p in parameterLists)
                {
                    query = query.SetParameterList(p.Key, p.Value);
                }
            }

            var result = new VKList<TResult> { Items = new List<TResult>() };
            if (isAutoMapping)
            {
                query = query.SetResultTransformer(Transformers.AliasToBean(typeof(TResult)));
                System.Collections.IList executeResult = query.List();

                foreach (var item in executeResult)
                {
                    result.Items.Add((TResult)item);
                }
            }
            else
            {
                IList<TResult> executeResult = query.List<TResult>();

                result.Items = executeResult;
            }
          
            //IList<TResult> executeResult = query.List<TResult>();
            
            //result.Items = executeResult;
            return result;
        }

        public static VKList<TResult> Execute<TResult>(string spName, IDictionary<string, object> parameters)
        {
            return Execute<TResult>(spName, parameters, false);
        }

        public static int ExecuteUpdate(string spName, IDictionary<string, object> parameters)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            IQuery query = session.GetNamedQuery(spName);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    query = query.SetParameter(parameter.Key, parameter.Value);
                }
            }
            
            return query.ExecuteUpdate();
        }
     
        public static DataSet ExecuteRetrieve(string storedProcedureName, IDictionary<string, object> parameters)
        {
            ISession session = NHibernateSessionManager.Instance.GetSession();

            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                using (conn = session.Connection as SqlConnection)
                {
                    conn.Open();

                    cmd = new SqlCommand(storedProcedureName, conn);

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            cmd.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                        }
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }

            return ds;
        }
    }
}
