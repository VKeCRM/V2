using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;

namespace VKeCRM.Framework.Data
{
	public abstract class RepositoryDao<T, IdT>
	{
		private Type persistentType = typeof(T);
		#region ctor
		public RepositoryDao() 
		{ }
		#endregion

		#region Properties

		/// <summary>
		/// Exposes the ISession used within the DAO.
		/// </summary>
		private ISession NHibernateSession
		{
			get
			{
				return NHibernateSessionManager.Instance.GetSession();
			}
		}

		#endregion

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
	}
}
