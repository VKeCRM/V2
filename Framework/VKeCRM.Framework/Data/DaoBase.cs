//-----------------------------------------------------------------------
// <copyright file="DaoBase.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Transform;
using VKeCRM.Common.Collections;
using VKeCRM.Framework.Business;
using System.Reflection;

namespace VKeCRM.Framework.Data
{
    /// <summary>
    /// Base class for data access objects
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    /// <typeparam name="IdT">Id of entity</typeparam>
	public abstract class DaoBase<T, IdT>
    {

		#region Declarations
		// the flag indicate for dirty read
    	private bool isDirtyRead;

        /// <summary>
        /// Type pf persistence
        /// </summary>
		private Type persistentType = typeof(T);

		//select count(elements(m.Friends)) from Member m where m.Id=1
		private const string countQueryTemplate = "select count(elements(entity.{0})) from {1} entity where entity.{2}=:id";
		//select friends from Member m join m.Friends friends where m.Id = :id
		private const string collectionQueryTemplate = "select collection from {0} root join root.{1} collection where root.{2} = :id";
		//select friends from Member m join m.Friends friends where m.Id = :id order by friends.Id asc
		private const string sortedCollectionQueryTemplate = "select collection from {0} root join root.{1} collection where root.{4} = :id order by collection.{2} {3}";
		//select count(*) from ..., 
		private const string countHqlQueryTemplate = "select count(*) from {0} ";

		//type name that doesn't include the namespace, mainly used to query NH meta data
		protected string entityTypeName;

		//type name that includes the namespace, but no assembly name
		protected string fullEntityTypeName;

		//the name of the ID property
		private string idPropertyName;

        public string EntityNameSpace
        {
            get
            {
                return fullEntityTypeName.Substring(0, fullEntityTypeName.LastIndexOf('.'));
            }
        }
        #endregion

		#region ctor
		public DaoBase() 
		{
			fullEntityTypeName = persistentType.ToString();
			entityTypeName = fullEntityTypeName.Substring(fullEntityTypeName.LastIndexOf('.') + 1);
			NHibernate.Metadata.IClassMetadata meta = NHibernateSession.SessionFactory.GetClassMetadata(persistentType);
			idPropertyName = meta.IdentifierPropertyName;
		}
    	#endregion

		#region Properties

		/// <summary>
		/// Exposes the ISession used within the DAO.
		/// </summary>
		protected ISession NHibernateSession
		{
			get
			{
                return NHibernateSessionManager.Instance.GetSession();
			}
		}

		#endregion

		#region DaoExtensions
		
		#endregion

        #region Basic Search

        /// <summary>
		/// Default version of GetById, not locking the record
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T GetById(IdT id)
		{
			return GetById(id, false);
		}

		/// <summary>
		/// Loads an instance of type T from the DB based on its ID.
		/// </summary>
        /// <param name="id">Id of entity to load</param>
        /// <param name="shouldLock">A boolean value to indicate whether to lock the entity or not</param>
        /// <returns>Returns the instance of type T</returns>
		public T GetById(IdT id, bool shouldLock)
		{
			T entity;

			if (shouldLock)
			{
				entity = (T)NHibernateSession.Load(persistentType, id, LockMode.Upgrade);
			}
			else
			{
				entity = (T)NHibernateSession.Load(persistentType, id);
			}

			return entity;
		}

        /// <summary>
        /// Loads an instance of type T from the DB based on its ID.
        /// </summary>
        /// <param name="id">Id of entity to load</param>
        /// <param name="shouldLock">A boolean value to indicate whether to lock the entity or not</param>
        /// <returns>Returns the instance of type T</returns>
        public S GetById<S>(IdT id, bool shouldLock) where S : T
        {
            S entity;

            if (shouldLock)
            {
                entity = (S)NHibernateSession.Load(typeof(S), id, LockMode.Upgrade);
            }
            else
            {
                entity = (S)NHibernateSession.Load(typeof(S), id);
            }

            return entity;
        }

		/// <summary>
		/// Loads an instance of T, will return NULL if the record does not exist otherwise will return the object
		/// </summary>
		/// <param name="id"></param>
		/// <param name="shouldLock"></param>
		/// <returns></returns>
		public T GetInstanceById(IdT id, bool shouldLock)
		{
			T entity;

			if (shouldLock)
			{
				entity = (T)NHibernateSession.Get(persistentType, id, LockMode.Upgrade);
			}
			else
			{
				entity = (T)NHibernateSession.Get(persistentType, id);
			}

			return entity;
		}

		/// <summary>
		/// lock object
		/// </summary>
		/// <param name="instance">loaded object</param>
		public void Lock(T instance)
		{
			NHibernateSession.Lock(instance, LockMode.Upgrade);
		}

		/// <summary>
		/// Loads every instance of the requested type with no filtering.
		/// </summary>
        //public List<T> GetAll()
        //{
        //    return GetByCriteria();
        //}

		/// <summary>
		/// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
		/// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
		/// </summary>
		//public List<T> GetByCriteria(params ICriterion[] criterion)
		//{
		//    Search<T> _searchHelper = new Search<T>();

		//    foreach (ICriterion criterium in criterion)
		//    {
		//        _searchHelper.AddCondition(criterium);
		//    }

		//    return _searchHelper.List() as List<T>;
		//}

        #endregion

        #region CRUD
        /// <summary>
		/// For entities that have assigned ID's, you must explicitly call Save to add a new one.
		/// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
		/// </summary>
		protected virtual T Save(T entity)
		{
			if (isDirtyRead)
			{
				throw new Exception("Save operation is forbidden on dirty read mode.");
			}

        	NHibernateSession.Save(entity);
            
			// Mark the session as dirty if we have UID operations
			NHibernateSessionManager.Instance.SetSessionDirty();
			return entity;
		}

		/// <summary>
		/// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
		/// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any 
		/// entity, even if its ID is assigned.
		/// </summary>
		protected virtual T SaveOrUpdate(T entity)
		{
			if (isDirtyRead)
			{
				throw new Exception("SaveOrUpdate operation is forbidden on dirty read mode.");
			}

			NHibernateSession.SaveOrUpdate(entity);
			// Mark the session as dirty if we have UID operations
			NHibernateSessionManager.Instance.SetSessionDirty();
			return entity;
		}

		public virtual void Delete(T entity)
		{
			if (isDirtyRead)
			{
				throw new Exception("Delete operation is forbidden on dirty read mode.");
			}

			NHibernateSession.Delete(entity);
			// Mark the session as dirty if we have UID operations
			NHibernateSessionManager.Instance.SetSessionDirty();
		}

        //// we do not permit delete by hql, cause this operation don't interact with NH's cache layer
        //protected virtual void Delete(string hql)
        //{
        //    NHibernateSession.Delete(hql);
        //}
		
		public virtual T Create(T entity)
		{
			if (isDirtyRead)
			{
				throw new Exception("Create operation is forbidden on dirty read mode.");
			}

			return Save(entity);
		}
		
		public virtual T Update(T entity)
		{
			if (isDirtyRead)
			{
				throw new Exception("Update operation is forbidden on dirty read mode.");
			}
			return SaveOrUpdate(entity);
        }
        #endregion

        #region TX Control
        /// <summary>
        /// Commits changes depending on whether there's an open transaction
		/// </summary>
		public void CommitChanges()
		{
            NHibernateSessionManager.Instance.CommitTransaction();
		}

		public void CommitChanges(bool closeSession)
		{
			CommitChanges();
			if (closeSession)
			{
				NHibernateSessionManager.Instance.CloseSession();
			}
		}

		/// <summary>
		/// Rollback the operation depending on whether there's an open transaction
		/// </summary>
		public void Rollback()
		{
            NHibernateSessionManager.Instance.RollbackTransaction();
		}

		public void Rollback(bool closeSession)
		{
			Rollback();
			if (closeSession)
			{
				NHibernateSessionManager.Instance.CloseSession();
			}
		}

		/// <summary>
		/// Evict the object that matches the reference that is being passed in
		/// </summary>
		public virtual void Evict(T entity)
		{
			NHibernateSessionManager.Instance.GetSession().Evict(entity);
		}
		
		/// <summary>
		/// Explicitly begin a transaction in the current session
		/// </summary>
		public virtual void BeginTransaction()
		{
			NHibernateSessionManager.Instance.BeginTransaction();
        }

        #endregion

        #region Custom Query
        /// <summary>
        /// ToDo; need deny char ' for shield inject sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected internal IQuery GetQuery(string sql)
		{
            return this.GetQuery(sql, new Dictionary<string, HqlParameter>());
		}

        protected internal IQuery GetQuery(string sql, Dictionary<string, HqlParameter> nameAndValues)
        {
            IQuery query = RetrieveQueryCacheSetting(NHibernateSession.CreateQuery(sql));
            if (nameAndValues != null && nameAndValues.Count != 0)
            {
                SetNamesAndValues(query, nameAndValues);
            }
            return query;
        }


        protected internal IQuery GetQuery(string sql, PagingOptions pagingOptions)
		{
			IQuery query = NHibernateSession.CreateQuery(sql);

			if (pagingOptions.PageSize > 0)
			{
				//0 based page index used to calculate the first record to return
				int pageIndex = pagingOptions.PageNumber <= 0 ? 1 : pagingOptions.PageNumber - 1;

                int firstResult = pageIndex * pagingOptions.PageSize + pagingOptions.Start;

				query.SetMaxResults(pagingOptions.PageSize)
					.SetFirstResult(firstResult);
			}

			return RetrieveQueryCacheSetting(query);
		}

        protected internal IQuery GetQuery(IQuery iquery, PagingOptions pagingOptions)
        {
            if (pagingOptions.PageSize > 0)
            {
                //0 based page index used to calculate the first record to return
                int pageIndex = pagingOptions.PageNumber <= 0 ? 1 : pagingOptions.PageNumber - 1;

                int firstResult = pageIndex * pagingOptions.PageSize + pagingOptions.Start;

                iquery.SetMaxResults(pagingOptions.PageSize)
                    .SetFirstResult(firstResult);
            }

            return RetrieveQueryCacheSetting(iquery);
        }

        protected ISQLQuery GetNativeQuery(string sql)
		{
			return (ISQLQuery)RetrieveQueryCacheSetting(NHibernateSession.CreateSQLQuery(sql));
		}

		protected internal ISQLQuery GetNamedQuery(string name)
		{
			return (ISQLQuery)RetrieveQueryCacheSetting(NHibernateSession.GetNamedQuery(name));
		}

	    protected IQueryable<T> AsQueryable()
	    {
		    return NHibernateSession.Linq<T>();
	    } 
		#endregion

        #region Collections
        /// <summary>
		/// Provide a generic way of getting paged collection of a property.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="id">ID for the parent entity</param>
		/// <param name="collectionName">Name of the collection property</param>
		/// <param name="pagingOptions">Paging option</param>
		/// <returns>A customrized list which may contain the number of total record count.</returns>
		protected VKList<U> GetPagedCollection<U>(IdT id, string collectionName, PagingOptions pagingOptions)
		{
			IMultiQuery multiQuery = NHibernateSession.CreateMultiQuery();
			IQuery q;
			if (string.IsNullOrEmpty(pagingOptions.SortBy))
			{
                q = NHibernateSession.CreateQuery(string.Format(collectionQueryTemplate, fullEntityTypeName, collectionName, idPropertyName));
			}
			else
			{
                q = NHibernateSession.CreateQuery(string.Format(sortedCollectionQueryTemplate, fullEntityTypeName, collectionName,
					pagingOptions.SortBy, pagingOptions.SortDescending ? "desc" : "asc", idPropertyName));
			}

            q.SetFirstResult((pagingOptions.PageNumber - 1) * pagingOptions.PageSize + pagingOptions.Start)
				.SetMaxResults(pagingOptions.PageSize);

			multiQuery.Add(q);

			if (pagingOptions.FetchTotalRecordCount)
			{
                multiQuery.Add(string.Format(countQueryTemplate, collectionName, fullEntityTypeName, idPropertyName));
			}
			
			//a little ugly here, assuming we only support int and guid as ID
			if (id.GetType().IsAssignableFrom(typeof(int)))
			{
				multiQuery.SetInt32("id", Convert.ToInt32(id));
			}
			else if (id.GetType().IsAssignableFrom(typeof(Guid)))
			{
				multiQuery.SetGuid("id", new Guid(id.ToString()));
			}
			else
			{
				throw new NotImplementedException("only ID of type int and guid is supported");
			}

			ArrayList result = multiQuery.List() as ArrayList;

			//convert the result into a typed array
			VKList<U> list = new VKList<U>((result[0] as ArrayList).ToArray(typeof(U)) as IList<U>);

			if (pagingOptions.FetchTotalRecordCount)
			{
				list.TotalRecordCount = Convert.ToInt32((result[1] as ArrayList)[0]);
			}

			return list;
        }

        /// <summary>
        /// Set hql string and values only from HqlParameterHelper object
        /// </summary>
        /// <typeparam name="U">Out entity type</typeparam>
        /// <param name="hqlHelper">HqlParameterHelper object</param>
        /// <param name="aliases">Alias To Bean Result Transformer</param>
        /// <param name="pagingOptions"></param>
        /// <returns></returns>
        protected VKList<U> GetPagedCollection<U>(HqlParameterHelper hqlHelper, string[] aliases, PagingOptions pagingOptions)
        {
            VKList<U> ret = new VKList<U>();
            IQuery query = GetQuery(hqlHelper.Hql, hqlHelper.ParameterValues);
            string countHql = string.Empty;

            // retrieve count(*) of total records
            if (pagingOptions.FetchTotalRecordCount)
            {
                if (hqlHelper.CountHql == null)
                {
                    string subHql = hqlHelper.Hql.Substring(hqlHelper.Hql.IndexOf("from", StringComparison.OrdinalIgnoreCase)+4);

                    // remove order by clause
                    int orderByIndex = subHql.IndexOf("order by", StringComparison.OrdinalIgnoreCase);
                    if (orderByIndex > 0)
                    {
                        subHql = subHql.Remove(orderByIndex);
                    }

                    // remove group by clause
                    int groupByIndex = subHql.IndexOf("group by", StringComparison.OrdinalIgnoreCase);
                    if (groupByIndex > 0)
                    {
                        subHql = subHql.Remove(groupByIndex);
                    }

                    countHql = string.Format(countHqlQueryTemplate, subHql);
                } else
                {
                    countHql = hqlHelper.CountHql;
                }

                IQuery totalCountQuery = GetQuery(countHql, hqlHelper.ParameterValues);
                ret.TotalRecordCount = Convert.ToInt32(totalCountQuery.UniqueResult());
            }

            if (pagingOptions.PageSize > 0)
            {
                //0 based page index used to calculate the first record to return
                int pageIndex = pagingOptions.PageNumber <= 0 ? 1 : pagingOptions.PageNumber - 1;

                int firstResult = pageIndex * pagingOptions.PageSize + pagingOptions.Start;

                query.SetMaxResults(pagingOptions.PageSize).SetFirstResult(firstResult);
            }

            Type transformerType = typeof(U);

            // TODO andy - uncomment the following code once NHibernate 2.0 supports the alias in select statement of hql
            //if (!transformerType.Equals(typeof(object)))
            //{
            //    query.SetResultTransformer(Transformers.AliasToBean(transformerType));
            //}

            if (pagingOptions.PageNumber > 0 && pagingOptions.PageSize > 0)
            {
                if (aliases != null && aliases.Length > 0)
                {
                    // we have to handle the aliases to bean by ourself for now
                    AliasToBeanResultTransformer resultTransformer = new AliasToBeanResultTransformer(transformerType);
                    IList<U> items = new List<U>();
                    IList rows = query.List();
                    foreach (object[] o in rows)
                    {
                        items.Add((U)resultTransformer.TransformTuple(o, aliases));
                    }
                    ret.Items = items;
                }
                else
                {
                    ret.Items = query.List<U>();
                }

            }
            else
            {
                ret.Items = new List<U>();
            }

            return ret;
        }

        protected VKList<U> ListAll<U>(IQuery query, string[] aliases) 
        {
            VKList<U> ret = new VKList<U>();
            if (aliases != null && aliases.Length > 0)
            {
                Type transformerType = typeof(U);
                // we have to handle the aliases to bean by ourself for now
                AliasToBeanResultTransformer resultTransformer = new AliasToBeanResultTransformer(transformerType);
                IList<U> items = new List<U>();
                IList rows = query.List();
                foreach (object[] o in rows)
                {
                    items.Add((U)resultTransformer.TransformTuple(o, aliases));
                }
                ret.Items = items;
            }
            else
            {
                ret.Items = query.List<U>();
            }
            ret.TotalRecordCount = ret.Items.Count;
            return ret;
        }

        protected U FirstOrDefault<U>(IQuery query, string[] aliases) 
        {
            query.SetMaxResults(1);
            VKList<U> result = this.ListAll<U>(query, aliases);
            if (result.Items.Count > 0)
            {
                return result.Items[0];
            }
            else 
            {
                return default(U);
            }
        }

        /// <summary>
        /// Set the values for IQuery
        /// </summary>
        /// <param name="query"></param>
        /// <param name="nameAndValues"></param>
        private void SetNamesAndValues(IQuery query, Dictionary<string, HqlParameter> nameAndValues)
        {
            foreach (HqlParameter v in nameAndValues.Values)
            {
                if (v.Type == SqlParamType.DateTime)
                {
                    query.SetDateTime(v.Name, (DateTime) v.Value);
                } else if (v.Type == SqlParamType.Decimal)
                {
                    query.SetDecimal(v.Name, (Decimal)v.Value); 
                } else if (v.Type == SqlParamType.Double)
                {
                    query.SetDouble(v.Name, (Double)v.Value); 
                }
                else if (v.Type == SqlParamType.Guid)
                {
                    query.SetGuid(v.Name, (Guid)v.Value); 
                } else if (v.Type == SqlParamType.Int16)
                {
                    query.SetInt16(v.Name, (short)v.Value); 
                } else if (v.Type == SqlParamType.Int32)
                {
                    query.SetInt32(v.Name, (int)v.Value); 
                } else if (v.Type == SqlParamType.ListInt16)
                {
                    query.SetParameterList(v.Name, (List<short>)v.Value); 
                }
                else if (v.Type == SqlParamType.ListInt32)
                {
                    query.SetParameterList(v.Name, (List<int>)v.Value); 
                }
                else if (v.Type == SqlParamType.ListString)
                {
                    query.SetParameterList(v.Name, (List<string>)v.Value); 
                }
                else if (v.Type == SqlParamType.String)
                {
                    query.SetString(v.Name, v.Value.ToString()); 
                } 
            }
        }

		/// <summary>
		/// Provide a generic way of getting paged collection for a hql query, and return unmapped result into a strong-typed object.
		/// </summary>
		/// <typeparam name="U">The type of the result will be transformed into.</typeparam>
		/// <param name="hql">The HQL string in which all parameters are filled in.</param>
		/// <param name="aliases">The aliases which maps the result by order..</param>
		/// <param name="pagingOptions">Paging option</param>
		/// <returns>
		/// A customrized list which may contain the number of total record count.
		/// </returns>
        protected VKList<U> GetPagedCollection<U>(string hql, string[] aliases, PagingOptions pagingOptions)
		{
			return GetPagedCollection<U>(hql, null, aliases, pagingOptions);
		}

		/// <summary>
		/// Provide a generic way of getting paged collection for a hql query, and return unmapped result into a strong-typed object.
		/// </summary>
		/// <typeparam name="U">The type of the result will be transformed into.</typeparam>
		/// <param name="hql">The HQL string in which all parameters are filled in.</param>
		/// <param name="countHql">User defined HQL to retrieve the count.</param>
		/// <param name="aliases">The aliases which maps the result by order..</param>
		/// <param name="pagingOptions">Paging option</param>
		/// <returns>
		/// A customrized list which may contain the number of total record count.
		/// </returns>
		protected VKList<U> GetPagedCollection<U>(string hql, string countHql, string[] aliases, PagingOptions pagingOptions)
		{
			VKList<U> ret = new VKList<U>();
			IQuery query = GetQuery(hql);

			// retrieve count(*) of total records
			if (pagingOptions.FetchTotalRecordCount)
			{
				if(countHql == null)
				{				
					string subHql = hql.Substring(hql.IndexOf("from", StringComparison.OrdinalIgnoreCase)+4);

					// remove order by clause
					int orderByIndex = subHql.IndexOf("order by", StringComparison.OrdinalIgnoreCase);
					if (orderByIndex > 0)
					{
						subHql = subHql.Remove(orderByIndex);
					}

					// remove group by clause
					int groupByIndex = subHql.IndexOf("group by", StringComparison.OrdinalIgnoreCase);
					if (groupByIndex > 0)
					{
						subHql = subHql.Remove(groupByIndex);
					}

					countHql = string.Format(countHqlQueryTemplate, subHql);
				}
				
				IQuery totalCountQuery = GetQuery(countHql);
				ret.TotalRecordCount = Convert.ToInt32(totalCountQuery.UniqueResult());
			}

			if (pagingOptions.PageSize > 0)
			{
				//0 based page index used to calculate the first record to return
				int pageIndex = pagingOptions.PageNumber <= 0 ? 1 : pagingOptions.PageNumber - 1;

                int firstResult = pageIndex * pagingOptions.PageSize + pagingOptions.Start;

				query.SetMaxResults(pagingOptions.PageSize).SetFirstResult(firstResult);
			}

			Type transformerType = typeof(U);

			// TODO andy - uncomment the following code once NHibernate 2.0 supports the alias in select statement of hql
			//if (!transformerType.Equals(typeof(object)))
			//{
			//    query.SetResultTransformer(Transformers.AliasToBean(transformerType));
			//}

			if (pagingOptions.PageNumber > 0 && pagingOptions.PageSize > 0)
			{
				if (aliases != null && aliases.Length>0)
				{
					// we have to handle the aliases to bean by ourself for now
					AliasToBeanResultTransformer resultTransformer = new AliasToBeanResultTransformer(transformerType);
					IList<U> items = new List<U>();
					IList rows = query.List();
					foreach (object[] o in rows)
					{
						items.Add((U) resultTransformer.TransformTuple(o, aliases));
					}
					ret.Items = items;
				}
				else
				{
					ret.Items = query.List<U>();
				}
				
			}
			else
			{
				ret.Items = new List<U>();
			}

			return ret;
		}

		/// <summary>
		/// Provide a generic way of getting paged collection for a hql query, and return unmapped result into a strong-typed object.
		/// </summary>
		/// <typeparam name="U">The type of the result will be transformed into.</typeparam>
		/// <param name="hql">The HQL string in which all parameters are filled in.</param>
		/// <param name="pagingOptions">Paging option</param>
		/// <returns>
		/// A customrized list which may contain the number of total record count.
		/// </returns>
		protected VKList<U> GetPagedCollection<U>(string hql, PagingOptions pagingOptions)
		{
			return GetPagedCollection<U>(hql, null, pagingOptions);
        }

        protected VKList<U> GetPagedCollectionByStoredProc<U>(string spName, IList<ParamInfo> paramInfos, PagingOptions pagingOptions, string[] aliases)
        {
            VKList<U> ret = new VKList<U>();
            IList<U> items = new List<U>();
            IList rows = ExecuteStoredProc(spName, paramInfos);

            Type transformerType = typeof(U);
            AliasToBeanResultTransformer resultTransformer = new AliasToBeanResultTransformer(transformerType);
            if (aliases != null && aliases.Length > 0)
            {
                foreach (object[] o in rows)               
                    items.Add((U)resultTransformer.TransformTuple(o, aliases));
            }
            else
            {
                items = rows.Cast<U>().ToList();
            }

            ret.Items = items;

            return ret;
        }
        private IList ExecuteStoredProc(string spName, IList<ParamInfo> paramInfos)
        {
            IList result = new ArrayList();

            IDbConnection conn = NHibernateSessionManager.Instance.GetDbConnection();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandTimeout =
                Convert.ToInt32(new Configuration().GetProperty(NHibernate.Cfg.Environment.CommandTimeout));
            cmd.CommandText = spName;
            cmd.CommandType = CommandType.StoredProcedure;
         
            if (paramInfos != null)
            {
                foreach (ParamInfo info in paramInfos)
                {
                    IDbDataParameter parameter = cmd.CreateParameter();
                    parameter.ParameterName = info.Name;
                    parameter.DbType = info.Type;
                    parameter.Value = info.Value;
                    if (info.IsOutput)
                    {
                        parameter.Direction = ParameterDirection.Output;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
           
            try
            {                
                IDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    int fieldCount = rs.FieldCount;
                    object[] values = new Object[fieldCount];
                    for (int i = 0; i < fieldCount; i++)
                        values[i] = rs.IsDBNull(i) ? null : rs.GetValue(i);
                    result.Add(values);
                }
            }
            finally
            {
                conn.Close();
            }           

            foreach (ParamInfo paramInfo in paramInfos)
            {
                if (paramInfo.IsOutput)
                    paramInfo.Value = (cmd.Parameters[paramInfo.Name] as IDbDataParameter).Value;
            }

            return result;
        }
           
             
        #endregion

        #region Private helper method
        /// <summary>
        ///Config the query to use cache, if the use_query_cache property is set to true in config. 
        /// </summary>
        /// <param name="query">The query want to be set</param>
        /// <returns>The query after config</returns>
        private IQuery RetrieveQueryCacheSetting(IQuery query)
        {
			// Comment the next lines, let sub class to set the caccheable by themselves.
			NHibernate.Cfg.Settings settings= (NHibernateSession.SessionFactory as NHibernate.Engine.ISessionFactoryImplementor).Settings;
		
			if (settings.IsQueryCacheEnabled
				&& settings.IsSecondLevelCacheEnabled)
            {
                query.SetCacheable(true).SetCacheRegion(CacheRegionNames.HqlRegionName);
            }

            return query;
        }

        #endregion

        #region Isolation and Locking      		

        public void Clear()
        {
            NHibernateSession.Clear();
        }

        protected IFilter EnableFilter(string filterName)
        {
            return NHibernateSession.EnableFilter(filterName);
        }		

    	#endregion

       

    }
    
}

