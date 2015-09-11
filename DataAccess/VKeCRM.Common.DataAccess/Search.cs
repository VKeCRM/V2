using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Engine;
using NHibernate.Persister.Entity;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using VKeCRM.Common.Collections;
using VKeCRM.Framework.Data;


namespace VKeCRM.Common.DataAccess
{
	/// <summary>
	/// Implement a generic and friendly way to search Domain objects without having to deal with NH Criterion interface.
	/// </summary>
	/// <typeparam name="T">The type of the Domain Object that the search starts as the root.</typeparam>
	public class Search<T>
	{
		#region Fields

		private static Type persistentType = typeof(T);

		private static string RegionName;

		/// <summary>
		///Criteria used to perform the search 
		/// </summary>
		protected ICriteria criteriaSearch;

		/// <summary>
		/// Criteria used to perform retrieving the total recoud count. This is similar to the criteriaSerach except for the sorting and grouping criteria.
		/// </summary>
		protected ICriteria criteriaRecordCount;
		#endregion

		#region Static constructor
		static Search()
		{
			RegionName = GetRegionName();
		}
		#endregion

		#region ctor
		public Search()
		{
			Initialize();
		}

		private void Initialize()
		{
			// Get a session for Search Criteria
			ISession sessionSearch = NHibernateSessionManager.Instance.GetSession();

			criteriaSearch = sessionSearch.CreateCriteria(persistentType);
			criteriaRecordCount = sessionSearch.CreateCriteria(persistentType);

			NHibernate.Cfg.Settings settings = (sessionSearch.SessionFactory as NHibernate.Engine.ISessionFactoryImplementor).Settings;
			// Set a default value indicates whether enables the second-level cache for nhibernate, 
			// the default value is saved in hibernate mapping file.		
			SetCacheable(settings.IsQueryCacheEnabled
				&& settings.IsSecondLevelCacheEnabled);
		}

		#endregion


		#region Getting result
		/// <summary>
		/// Returns a strong-typed list of all the records.
		/// </summary>
		/// <returns></returns>
		public virtual VKList<T> List()
		{
			VKList<T> result = new VKList<T>();
			result.Items = criteriaSearch.List<T>();
			return result;
		}
        /// <summary>
        /// Returns a strong-typed list of all the records.
        /// </summary>
        /// <returns></returns>
        public virtual VKList<T> Query(IQuery query)
        {
            VKList<T> result = new VKList<T>();
            result.Items = query.List<T>();

            return result;
        }

		/// <summary>
		/// Return a strong-typed list of paged result.
		/// </summary>
		/// <param name="pagingOptions">The sorting portion of the options are not used.</param>
		/// <returns></returns>
		public virtual VKList<T> List(PagingOptions pagingOptions)
		{
			VKList<T> ret = new VKList<T>();
			if (pagingOptions.FetchTotalRecordCount)
			{
				ret.TotalRecordCount = GetRecordCount();
			}

			SetPagingOption(pagingOptions);

			if (pagingOptions.PageNumber > 0 && pagingOptions.PageSize > 0)
			{
				ret.Items = criteriaSearch.List<T>();
			}
			else
			{
				ret.Items = new List<T>();
			}

			return ret;
		}

		/// <summary>
		/// Return a list of unmapped objects. Normally you will need this when you specify a projection such as grouping.
		/// </summary>
		/// <param name="pagingOptions"></param>
		/// <returns>A VKList which contains a jagged array as the result set.</returns>
		public virtual VKList<object> ListUnmapped(PagingOptions pagingOptions)
		{
			return ListUnmapped<object>(pagingOptions);
		}

		/// <summary>
		/// Provides a way to return unmapped result into a strong-typed object.
		/// </summary>
		/// <typeparam name="U">The type of the result will be transformed into.</typeparam>
		/// <param name="pagingOptions"></param>
		/// <returns></returns>
		public virtual VKList<U> ListUnmapped<U>(PagingOptions pagingOptions)
		{

			VKList<U> ret = new VKList<U>();
			if (pagingOptions.FetchTotalRecordCount)
			{
				ret.TotalRecordCount = GetRecordCount();
			}

			SetPagingOption(pagingOptions);

			Type transformerType = typeof(U);

			if (!transformerType.Equals(typeof(object)))
			{
				criteriaSearch.SetResultTransformer(Transformers.AliasToBean(transformerType));
			}

			// changing cacheregion name and set it false
			NHibernate.Impl.CriteriaImpl impl = criteriaSearch as NHibernate.Impl.CriteriaImpl;
			if (impl.Cacheable)
			{
				criteriaSearch.SetCacheable(false);
			}

			if (pagingOptions.PageNumber > 0 && pagingOptions.PageSize > 0)
			{
				ret.Items = (criteriaSearch.List() as ArrayList).ToArray(transformerType) as IList<U>;
			}
			else
			{
				ret.Items = new ArrayList().ToArray(transformerType) as IList<U>;
			}

			return ret;
		}

		/// <summary>
		/// Get the record count. This will be tricky if you have specified a ProjectionList already. 
		/// I would strongly suggest NOT to rely on this if you do have a ProjectionList, instead, you should write another Search query to retrieve the record count.
		/// </summary>
		/// <returns></returns>
		protected virtual int GetRecordCount()
		{
			return Convert.ToInt32(criteriaRecordCount.SetProjection(Projections.RowCount()).UniqueResult());
		}

		protected virtual void SetPagingOption(PagingOptions pagingOptions)
		{
			if (pagingOptions.PageSize > 0)
			{
				//0 based page index used to calculate the first record to return
				int pageIndex = pagingOptions.PageNumber <= 0 ? 1 : pagingOptions.PageNumber - 1;

				int firstResult = pageIndex * pagingOptions.PageSize + pagingOptions.Start;

				criteriaSearch.SetMaxResults(pagingOptions.PageSize).SetFirstResult(firstResult);
			}
		}

		/// <summary>
		/// Return a single instance based on the searching criteria. Assuming the caller's criteria will result only one record.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.ArgumentException">If the criteria result multiple records</exception>
		public virtual T GetSingleInstance()
		{
			try
			{
				return criteriaSearch.UniqueResult<T>();
			}
			catch (NHibernate.NonUniqueResultException)
			{
				throw new ArgumentException("Criteria result multiple records.");
			}
		}

		public virtual U GetMaxValue<U>(string propertyName)
		{
			try
			{
				return criteriaSearch.SetProjection(Projections.Max(propertyName)).UniqueResult<U>();
			}
			catch (NHibernate.NonUniqueResultException)
			{
				throw new ArgumentException("Criteria result multiple records.");
			}
		}

		public virtual U GetMinValue<U>(string propertyName)
		{
			try
			{
				return criteriaSearch.SetProjection(Projections.Min(propertyName)).UniqueResult<U>();
			}
			catch (NHibernate.NonUniqueResultException)
			{
				throw new ArgumentException("Criteria result multiple records.");
			}
		}

		public virtual U GetSumValue<U>(string propertyName)
		{
			try
			{
				return criteriaSearch.SetProjection(Projections.Sum(propertyName)).UniqueResult<U>();
			}
			catch (NHibernate.NonUniqueResultException)
			{
				throw new ArgumentException("Criteria result multiple records.");
			}
		}
		#endregion


		#region Conditions

		/// <summary>
		/// Add a search condition.
		/// </summary>
		/// <param name="criterion"></param>
		/// <returns></returns>
		public virtual Search<T> AddCondition(ICriterion criterion)
		{
			criteriaSearch.Add(criterion);
			criteriaRecordCount.Add(criterion);
			return this;
		}

		/// <summary>
		/// Add an sorting order.
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public virtual Search<T> AddOrder(Order order)
		{
			criteriaSearch.AddOrder(order);
			return this;
		}

		/// <summary>
		/// Set a projection: grouping, count, avg...
		/// Be careful only the last call to this takes effect!!!
		/// </summary>
		/// <param name="projection"></param>
		/// <returns></returns>
		public virtual Search<T> SetProjection(IProjection projection)
		{
			criteriaSearch.SetProjection(projection);
			criteriaRecordCount.SetProjection(projection);
			return this;
		}


		/// <summary>
		/// Use this to create a join between the "root" and its associate entity.
		/// Using InnerJoin as default join  type.
		/// </summary>
		/// <param name="association"></param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public virtual Search<T> JoinAssociation(string association, string alias)
		{
			return JoinAssociation(association, alias, JoinType.InnerJoin);
		}

		/// <summary>
		/// Use this to create a join between the "root" and its associate entity.
		/// </summary>
		/// <param name="association"></param>
		/// <param name="alias"></param>
		/// <param name="joinType"></param>
		/// <returns></returns>
		public virtual Search<T> JoinAssociation(string association, string alias, JoinType joinType)
		{
			criteriaSearch.CreateAlias(association, alias, joinType);
			criteriaRecordCount.CreateAlias(association, alias, joinType);
			return this;
		}

		/// <summary>
		/// Similar to JoinAssociation, this creates a join between the current "root" and the associated entity and set
		/// the associated entity as the new "root". Also bear in mind, the different order of calling this and adding a 
		/// condition will yield different results. Use InnerJoin as default join type.
		/// </summary>
		/// <param name="association"></param>
		/// <param name="alias"></param>
		/// <returns></returns>
		public virtual Search<T> SetRootAssociation(string association, string alias)
		{
			return SetRootAssociation(association, alias, JoinType.InnerJoin);
		}


		/// <summary>
		/// Similar to JoinAssociation, this creates a join between the current "root" and the associated entity and set
		/// the associated entity as the new "root". Also bear in mind, the different order of calling this and adding a 
		/// condition will yield different results.
		/// </summary>
		/// <param name="association"></param>
		/// <param name="alias"></param>
		/// <param name="joinType"></param>
		/// <returns></returns>
		public virtual Search<T> SetRootAssociation(string association, string alias, JoinType joinType)
		{
			criteriaSearch.CreateCriteria(association, alias, joinType);
			criteriaRecordCount.CreateCriteria(association, alias, joinType);
			return this;
		}


		/// <summary>
		/// Specify an association fetching strategy. Currently only 1-2-n and 1-2-1 associations are supported by NH.
		/// </summary>
		/// <param name="associationPath">A dot separated property path.</param>
		/// <param name="fetchMode"></param>
		/// <returns></returns>
		public virtual Search<T> SetFetchMode(string associationPath, FetchMode fetchMode)
		{
			criteriaSearch.SetFetchMode(associationPath, fetchMode);
			return this;
		}
		#endregion


		#region Caching capability

		public virtual Search<T> SetCacheMode(CacheMode cacheMode)
		{
			criteriaSearch.SetCacheMode(cacheMode);
			criteriaRecordCount.SetCacheMode(cacheMode);
			return this;
		}
		public virtual Search<T> SetCacheable(bool cacheable)
		{
			return SetCacheable(cacheable, RegionName);
		}

		public virtual Search<T> SetRegionName(string cacheRegionName)
		{
			return SetCacheable(true, cacheRegionName);
		}

		protected virtual Search<T> SetCacheable(bool cacheable, string cacheRegion)
		{
			criteriaSearch.SetCacheable(cacheable);
			criteriaRecordCount.SetCacheable(cacheable);
			if (!string.IsNullOrEmpty(cacheRegion))
			{
				criteriaSearch.SetCacheRegion(cacheRegion);
				criteriaRecordCount.SetCacheRegion(cacheRegion);
			}

			return this;
		}

		private static string GetRegionName()
		{
			string roleName = typeof(T).FullName;

			string cacheRegionName = string.Empty;

			ISessionFactoryImplementor sfi = NHibernateSessionManager.Instance.GetSession().GetSessionImplementation().Factory;
			AbstractEntityPersister ep = sfi.GetClassMetadata(typeof(T)) as AbstractEntityPersister;

			if (ep != null && ep.HasCache)
			{
				cacheRegionName = ep.Cache.RegionName;
			}

			return cacheRegionName;
		}
		#endregion
	}
}
