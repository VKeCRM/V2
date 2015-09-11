//-----------------------------------------------------------------------
// <copyright file="IInterceptableDao.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace VKeCRM.Framework.Data.Interceptors
{
    /// <summary>
    /// Interface for Interceptable data access object
    /// </summary>
	public interface IInterceptableDao
	{
		/// <summary>
		/// Choose which interceptor to use.
		/// </summary>
		/// <returns>Returns the interceptor interface</returns>
		IInterceptor GetInterceptor();
	}
}
