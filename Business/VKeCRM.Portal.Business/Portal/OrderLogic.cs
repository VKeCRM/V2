using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VKeCRM.Common.Collections;
using VKeCRM.Common.DataAccess;

using NHibernate.Criterion;
using VKeCRM.Portal.Business.Factories;
using VKeCRM.Portal.DataAccess.DataAccessObjects;
using VKeCRM.Portal.DataAccess.DomainObjects;

using OrdersDomain = VKeCRM.Portal.DataAccess.DomainObjects.Orders;
using OrdersDto = VKeCRM.Portal.DataTransferObjects.Orders;

using VKeCRM.Portal.DataTransferObjects;
using Iesi.Collections.Generic;
using VKeCRM.Framework.Data;

using MasterDomain = VKeCRM.Portal.DataAccess.DomainObjects.Master;
using MasterDto = VKeCRM.Portal.DataTransferObjects.Master;

namespace VKeCRM.Portal.Business
{
    /// <summary>
    /// Business logic to handle Group ,Discussions and create group
    /// </summary>
    public class OrdersLogic
    {
        #region Constructor
        public OrdersLogic()
        {

        }
        #endregion

        #region DataAccess Object Retrieval Properties
        private OrdersDao _ordersdao = null;
        private OrdersDao OrdersDao
        {
            get { return _ordersdao ?? (_ordersdao = new OrdersDao()); }
        }

        private MasterDao _MasterDao = null;
        private MasterDao MasterDao
        {
            get { return _MasterDao ?? (_MasterDao = new MasterDao()); }
        }

        #endregion

        #region Method

        #region Search

        public VKList<OrdersDto> GetAllOrders()
        {
            var ordersDomains = OrdersDao.GetAllOrders();
            var orders = FactoryManager.OrdersFactory.CreateListOfDtos(ordersDomains);
            return orders;
        }

        public OrdersDto SaveOrder(OrdersDto dto)
        {
            var domain = FactoryManager.OrdersFactory.CreateNewDomainObject(dto);
            OrdersDao.Update(domain);
            dto.Id = domain.Id;
            return dto;
        }

        public MasterDto SaveMasterDetail(MasterDto dto)
        {
            var domain = FactoryManager.MasterFactory.CreateNewDomainObject(dto);
            MasterDao.Update(domain);
            dto.Id = domain.Id;
            return dto;
        }

        public OrdersDto GetOrderById(int id)
        {
            var parameters
               = new Dictionary<string, object>
                    {
                        { "orderId", id }
                    };

            //if the sp's parameters were null, just set the parameters null like below
            /*
              Dictionary<string, object> parameters
               = new Dictionary<string, object> {};
             */

            var data
               = StoredProcedureHelper.Execute<OrdersDto>(
                   "GetOrder", parameters, true);

            if (data.Items != null && data.Items.Count > 0)
                return data.Items[0];

            return null;

            //return OrdersDao.GetById(id);
        }

        #endregion

        #region Insert/update/delete operations

        #endregion

        #region Private methods

        #endregion



        #endregion
    }
}
