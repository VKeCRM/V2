using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using VKeCRM.Common.Collections;
using VKeCRM.Framework.ServiceModel;
using VKeCRM.Framework.ServiceModel.Exceptions;
using VKeCRM.Portal.Business;
using VKeCRM.Portal.DataTransferObjects;
using OrdersDto = VKeCRM.Portal.DataTransferObjects.Orders;
using MasterDto = VKeCRM.Portal.DataTransferObjects.Master;

namespace VKeCRM.Portal.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PortalServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PortalServices.svc or PortalServices.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PortalServices : NHibernateServiceBase, IPortalServices
    {
        public PortalServices()
        {
        }
        public void DoWork()
        {
        }

        #region Logic Retrieval Properties

        private OrdersLogic _ordersLogic;

        private OrdersLogic OrdersLogic
        {
            get
            {
                if (_ordersLogic == null)
                {
                    _ordersLogic = new OrdersLogic();
                }
                return _ordersLogic;
            }
        }

        #endregion

        [ServiceException]
        public VKList<OrdersDto> GetAllOrders()
        {
            return OrdersLogic.GetAllOrders();
        }

        [ServiceException]
        [NHibernateTransaction]
        public OrdersDto SaveOrder(OrdersDto dto)
        {
            return OrdersLogic.SaveOrder(dto);
        }

        [ServiceException]
        [NHibernateTransaction]
        public MasterDto SaveMasterDetail(MasterDto dto)
        {
            return OrdersLogic.SaveMasterDetail(dto);
        }
    }
}
