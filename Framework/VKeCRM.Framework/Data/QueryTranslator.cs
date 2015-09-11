using System;
using NHibernate;
using NHibernate.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NHibernate.Linq.Expressions;
using NHibernate.Linq.Util;
using NHibernate.Linq.Visitors;
using LinqExpression = System.Linq.Expressions.Expression;

namespace VKeCRM.Framework.Data
{
	public class QueryTranslator : NHibernateExpressionVisitor
	{
		private readonly ISession session;
		private readonly string entityName;
		private ICriteria rootCriteria;
		private QueryOptions options;

		public QueryTranslator(ISession session)
		{
			this.session = session;
		}
		public QueryTranslator(ISession session, string entityName)
		{
			this.session = session;
			this.entityName = entityName;
		}

		public virtual ICriteria Translate(LinqExpression expression, QueryOptions queryOptions)
		{
			this.rootCriteria = null;
			this.options = queryOptions;

			Visit(expression); //ensure criteria

			return this.rootCriteria;	
		}

		protected override LinqExpression VisitQuerySource(QuerySourceExpression expr)
		{
			if (rootCriteria == null)
			{
				if(!string.IsNullOrEmpty(this.entityName))
					rootCriteria = session.CreateCriteria(entityName, expr.Alias);
				else
					rootCriteria = session.CreateCriteria(expr.ElementType, expr.Alias);

				System.Type t= options.GetType();
				System.Reflection.FieldInfo f = t.GetField("action", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
				var action = f.GetValue(options) as Action<ICriteria>;
				action(rootCriteria);
			
			}
			return expr;
		}
	}
}
