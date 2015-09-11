using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using VKeCRM.Framework.Data;
using VKeCRM.Common.Collections;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;
using System.Linq.Expressions;
using System.Collections;
using NHibernate;
using NHibernate.Linq.Util;
using VKeCRM.Framework.Data.Linq;
using NHibernate.SqlCommand;

namespace VKeCRM.Framework.Data
{
	public static class DaoExtensions
	{
		/// <summary>
		/// Creates a new <see cref="T:NHibernate.Linq.NHibernateQueryProvider"/> object used to evaluate an expression tree.
		/// </summary>
		/// <typeparam name="T">An NHibernate entity type.</typeparam>
		/// <param name="session">An initialized <see cref="T:NHibernate.ISession"/> object.</param>
		/// <returns>An <see cref="T:NHibernate.Linq.NHibernateQueryProvider"/> used to evaluate an expression tree.</returns>
		public static INHibernateQueryable<T> DBContext<T>(this ISession session)
		{
			QueryOptions options = new QueryOptions();
			return new Query<T>(new VKeCRMQueryProvider(session, options), options);
		}

		public static INHibernateQueryable<T> DBContext<T>(this ISession session, string entityName)
		{
			QueryOptions options = new QueryOptions();
			return new Query<T>(new VKeCRMQueryProvider(session, options, entityName), options);
		}


		public static VKList<T> VKList<T>(this IEnumerable<T> query, PagingOptions pagingOptions) where T :new()
		{
			
			VKList<T> ret = new VKList<T>();			
			IQueryable queryable = query.AsQueryable();
			VKeCRMQueryProvider provider = queryable.Provider as VKeCRMQueryProvider;

			var results = provider.TranslateExpression(queryable.Expression,false);

			ICriteria criteria = results as ICriteria;

			if (criteria == null)
			{ criteria = provider.RootCriteria; }		

			NHibernate.Impl.CriteriaImpl impl = criteria as NHibernate.Impl.CriteriaImpl;
			if (!impl.IterateOrderings().Any())
			{
				if (!string.IsNullOrEmpty(pagingOptions.SortBy))
				{
					NHibernate.Criterion.Order order;
					if (pagingOptions.SortDescending)
						order = NHibernate.Criterion.Order.Desc(pagingOptions.SortBy);
					else
						order = NHibernate.Criterion.Order.Asc(pagingOptions.SortBy);

					criteria.AddOrder(order);
				}
			}

			if (pagingOptions.FetchTotalRecordCount)
			{
				ICriteria criteriaRecordCount = CriteriaTransformer.TransformToRowCount(criteria);
				ret.TotalRecordCount = GetRecordCount(criteriaRecordCount);
			}			

			SetPagingOption(criteria, pagingOptions);

			if (pagingOptions.PageNumber > 0 && pagingOptions.PageSize > 0)
			{
				ret.Items = criteria.List<T>();
			}
			else
			{
				ret.Items = new List<T>();
			}

			return ret;
		}

		public static IEnumerable<TResult> Join<TResult>(this IEnumerable<TResult> source, JoinType joinType)
		{			
			return source;
		}
		public static IEnumerable<TResult> Join<TResult>(this IEnumerable<TResult> source)
		{
			return source;
		}		

		private static int GetRecordCount(ICriteria criteria)
		{
			return Convert.ToInt32(criteria.SetProjection(NHibernate.Criterion.Projections.RowCount()).UniqueResult());
		}

		private static void SetPagingOption(ICriteria criteria,PagingOptions pagingOptions)
		{
			if (pagingOptions.PageSize > 0)
			{
				//0 based page index used to calculate the first record to return
				int pageIndex = pagingOptions.PageNumber <= 0 ? 1 : pagingOptions.PageNumber - 1;
				int firstResult = pageIndex * pagingOptions.PageSize + pagingOptions.Start;
				criteria.SetMaxResults(pagingOptions.PageSize).SetFirstResult(firstResult);
			}
		}

		public static IQueryable<T> Where<T>(this IQueryable<T> source,WhereOptions  whereOptions)
		{

			return (IQueryable<T>)Where((IQueryable)source, whereOptions);
		}

		public static IQueryable Where(this IQueryable source, WhereOptions whereOptions)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (string.IsNullOrEmpty( whereOptions.predicates)) return source;

			LambdaExpression lambda = VKeCRM.Framework.Data.Linq.DynamicExpression.ParseLambda(source.ElementType, typeof(bool), whereOptions.predicates, whereOptions.Values.ToArray());
			return source.Provider.CreateQuery(
				Expression.Call(
					typeof(Queryable), "Where",
					new Type[] { source.ElementType },
					source.Expression, Expression.Quote(lambda)));
		}

	}

}
