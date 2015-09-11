using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

using VKeCRM.Common.DataAccess;
using VKeCRM.Common.Collections;
using VKeCRM.Framework.Data;
using VKeCRM.Framework.Data.Interceptors;
using VKeCRM.Portal.DataAccess.DomainObjects;

namespace VKeCRM.Portal.DataAccess.DataAccessObjects
{
    /// <summary>
    ///		FormDao class to provider operations associated with Form domain
    /// </summary>
    public class OrdersDao : DaoBase<Orders, int>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FormDao&lt;T&gt;"/> class.
        /// </summary>
        public OrdersDao()
        {
        }
        #endregion

        #region Methods

        public VKList<Orders> GetAllOrders()
        {
            return new Search<Orders>().List();
        }

        

        #endregion
    }
}
