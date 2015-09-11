using NHibernate;
using System.Linq.Expressions;
using NHibernate.Linq.Expressions;
using NHibernate.Linq.Visitors;
using NHibernate.SqlCommand;

namespace VKeCRM.Framework.Data
{
	public class VKeCRMSelectManyVisitor : NHibernateExpressionVisitor
	{
		private readonly ICriteria _rootCriteria;
		private readonly string _alias;
		private JoinType joinType = JoinType.LeftOuterJoin;

		public VKeCRMSelectManyVisitor(ICriteria criteria, string alias)
		{
			_rootCriteria = criteria;
			_alias = alias;
		}

		protected override Expression VisitCollectionAccess(CollectionAccessExpression expr)
		{
			MemberNameVisitor visitor = new MemberNameVisitor(_rootCriteria, false);
			visitor.Visit(expr.Expression);

			visitor.CurrentCriteria.CreateCriteria(expr.Name, _alias, joinType);

			return expr;
		}

		protected override Expression VisitMethodCall(MethodCallExpression expr)
		{
			if (expr.Arguments.Count > 1 )
			{
				var e = expr.Arguments[1] as ConstantExpression;
				if (e != null)
				{
					if (e.Value is JoinType)
						joinType = (JoinType)e.Value;
				}
			}		

			Visit(expr.Arguments[0]);

			return expr;
		}
	}
}
