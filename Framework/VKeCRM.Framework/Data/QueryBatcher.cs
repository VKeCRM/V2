using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using NHibernate.Criterion;
using NHibernate;

namespace VKeCRM.Framework.Data
{
    /// <summary>
    /// Query batcher to execute multi-criteria and multi-query in one database call
    /// </summary>
    public class QueryBatcher
    {
        #region Fields
        private readonly Dictionary<string, int> criteriaResultPositions;
        private readonly List<ICriteria> criteriaList;
        private readonly List<IQuery> hqlQueryList;
        private readonly Dictionary<string, int> queryResultPositions;
        private readonly ISession session;

        private IList criteriaResults;
        private IList queryResults;
        private Dictionary<string, IList> allResults;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBatcher"/> class.
        /// </summary>
        public QueryBatcher()
        {
            session = NHibernateSessionManager.Instance.GetSession();
            criteriaList = new List<ICriteria>();
            hqlQueryList = new List<IQuery>();
            criteriaResultPositions = new Dictionary<string, int>();
            queryResultPositions = new Dictionary<string, int>();
            allResults = new Dictionary<string, IList>();
        }
        #endregion

        #region Add batch criterias or querys methods
        /// <summary>
        /// Adds the criteria.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="criteria">The criteria.</param>
        public void AddCriteria(string key, ICriteria criteria)
        {
            criteriaList.Add(criteria);
            criteriaResultPositions.Add(key, criteriaList.Count - 1);
        }

        /// <summary>
        /// Adds the detached criteria.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="detachedCriteria">The detached criteria.</param>
        public void AddCriteria(string key, DetachedCriteria detachedCriteria)
        {
            AddCriteria(key, detachedCriteria.GetExecutableCriteria(session));
        }

        /// <summary>
        /// Adds the HQL query.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="query">The query.</param>
        public void AddHqlQuery(string key, IQuery query)
        {
            hqlQueryList.Add(query);
            queryResultPositions.Add(key, hqlQueryList.Count - 1);
        }

        /// <summary>
        /// Adds the HQL query.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="query">The query.</param>
        public void AddHqlQuery(string key, string query)
        {
            AddHqlQuery(key, session.CreateQuery(query));
        }
        #endregion

        #region Get results methods
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The query result object</returns>
        public object GetResult(string key)
        {
            ExecuteQueriesIfNecessary();

            object result = GetResultFromList(key, criteriaResults, criteriaResultPositions);

            if (result == null)
            {
                result = GetResultFromList(key, queryResults, queryResultPositions);
            }

            return result;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <returns>Dictionary with query key and result pairs </returns>
        public Dictionary<string, IList> GetResult()
        {
            ExecuteQueriesIfNecessary();
            return allResults;
        }

        /// <summary>
        /// Gets the enumerable result.
        /// </summary>
        /// <typeparam name="T">The query result type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>A collection of the result type</returns>
        public IEnumerable<T> GetEnumerableResult<T>(string key)
        {
            var list = GetResult<IList>(key);
            return list.Cast<T>();
        }

        /// <summary>
        /// Gets the single result.
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The result type</returns>
        public T GetSingleResult<T>(string key)
        {
            var result = GetResult<IList>(key);
            return (T)result[0];
        }
        #endregion

        #region Internal methods
        /// <summary>
        /// Executes the queries if necessary.
        /// </summary>
        private void ExecuteQueriesIfNecessary()
        {
            ExecuteCriteriaIfNecessary();
            ExecuteHqlIfNecessary();
        }

        /// <summary>
        /// Executes the criteria if necessary.
        /// </summary>
        private void ExecuteCriteriaIfNecessary()
        {
            if (criteriaList.Count > 0 && criteriaResults == null)
            {
                if (criteriaList.Count == 1)
                {
                    criteriaResults = new ArrayList { criteriaList[0].List() };
                }
                else
                {
                    var multiCriteria = session.CreateMultiCriteria();
                    criteriaList.ForEach(c => multiCriteria.Add(c));
                    criteriaResults = multiCriteria.List();
                }

                foreach (string key in criteriaResultPositions.Keys)
                {
                    allResults.Add(key, (IList)criteriaResults[criteriaResultPositions[key]]);
                }
            }
        }

        /// <summary>
        /// Executes the HQL if necessary.
        /// </summary>
        private void ExecuteHqlIfNecessary()
        {
            if (hqlQueryList.Count > 0 && queryResults == null)
            {
                if (hqlQueryList.Count == 1)
                {
                    queryResults = new ArrayList { hqlQueryList[0].List() };
                }
                else
                {
                    var multiQuery = session.CreateMultiQuery();
                    hqlQueryList.ForEach(q => multiQuery.Add(q));
                    queryResults = multiQuery.List();
                }

                foreach (string key in queryResultPositions.Keys)
                {
                    allResults.Add(key, (IList)queryResults[queryResultPositions[key]]);
                }
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <typeparam name="T">The query result type</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The result type</returns>
        private T GetResult<T>(string key)
        {
            return (T)GetResult(key);
        }

        /// <summary>
        /// Gets the result from list.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="list">The list.</param>
        /// <param name="positions">The positions.</param>
        /// <returns>The result object</returns>
        private static object GetResultFromList(string key, IList list, IDictionary<string, int> positions)
        {
            if (positions.ContainsKey(key))
            {
                return list[positions[key]];
            }

            return null;
        }
        #endregion
    }
}
