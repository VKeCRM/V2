using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using VKeCRM.Common.Collections;
using VKeCRM.Portal.DataTransferObjects;
using OrdersDto = VKeCRM.Portal.DataTransferObjects.Orders;
using MasterDto = VKeCRM.Portal.DataTransferObjects.Master;

namespace VKeCRM.Portal.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPortalServices" in both code and config file together.
    [ServiceContract]
    public interface IPortalServices
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        VKList<Orders> GetAllOrders();

        [OperationContract]
        OrdersDto SaveOrder(OrdersDto dto);

        [OperationContract]
        MasterDto SaveMasterDetail(MasterDto dto);
    }
}
