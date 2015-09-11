using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Transform;
using VKeCRM.Common.Collections;

namespace VKeCRM.Framework.Data
{
	public abstract class LinqDaoBase<T, IdT> : DaoBase<T, IdT>
	{
		public LinqDaoBase()
		{
		}
		public INHibernateQueryable<T> DBContext()
		{
			return NHibernateSession.DBContext<T>();
		}
		public IOrderedQueryable<S> DBContext<S>()
		{
			return NHibernateSession.DBContext<S>();
		}
		public INHibernateQueryable<T> DBContextByEntityName()
		{
			return NHibernateSession.DBContext<T>(entityTypeName);
		}
		public INHibernateQueryable<S> DBContextByEntityName<S>()
		{
			return NHibernateSession.DBContext<S>(entityTypeName);
		}
	}
}
