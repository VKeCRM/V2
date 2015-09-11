using System;
using NHibernate;
using System.Linq.Expressions;
using NHibernate.Engine;
using NHibernate.Linq.Util;
using NHibernate.Linq.Visitors;
using NHibernate.Linq;
using VKeCRM.Common.Collections;
namespace VKeCRM.Framework.Data
{
	public class VKeCRMQueryProvider: QueryProvider
	{
		private readonly ISession _session;
		private readonly string entityName;
		private ICriteria rootCriteria;		
		public object Result = null;

		public VKeCRMQueryProvider(ISession session, QueryOptions queryOptions)
		{
			if (session == null) throw new ArgumentNullException("session");
			_session = session;
			this.queryOptions = queryOptions;
		}

		public VKeCRMQueryProvider(ISession session, QueryOptions queryOptions, string entityName)
		{
			if (session == null) throw new ArgumentNullException("session");
			_session = session;
			this.entityName = entityName;
			this.queryOptions = queryOptions;
		}

		private static object ResultsFromCriteria(ICriteria criteria, Expression expression)
		{
			System.Type elementType = TypeSystem.GetElementType(expression.Type);

			return Activator.CreateInstance(typeof(CriteriaResultReader<>)
			  .MakeGenericType(elementType), criteria);
		}

		public ICriteria RootCriteria
		{
			get { return rootCriteria; }
		}
		public object TranslateExpression(Expression expression) {
			return TranslateExpression(expression, true);
		}
		public object TranslateExpression(Expression expression, bool isExecute)
		{
			expression = Evaluator.PartialEval(expression);
			expression = new BinaryBooleanReducer().Visit(expression);
			expression = new AssociationVisitor((ISessionFactoryImplementor)_session.SessionFactory).Visit(expression);
			expression = new InheritanceVisitor().Visit(expression);
			expression = CollectionAliasVisitor.AssignCollectionAccessAliases(expression);
			expression = new PropertyToMethodVisitor().Visit(expression);
			expression = new BinaryExpressionOrderer().Visit(expression);

			QueryTranslator translator = new QueryTranslator(_session, entityName);
			this.rootCriteria = null;
			rootCriteria = translator.Translate(expression, this.queryOptions);

			VKeCRMRootVisitor visitor = new VKeCRMRootVisitor(rootCriteria, _session, true);
			visitor.Visit(expression);
			if (!isExecute)
			{
				return rootCriteria;
			}
			return visitor.Results;			
		}			

		public override object Execute(Expression expression)
		{
			var results = TranslateExpression(expression);
			var criteria = results as ICriteria;

			if (criteria != null )
				return Result=ResultsFromCriteria(criteria, expression);

			return results;
		}
	}
}
