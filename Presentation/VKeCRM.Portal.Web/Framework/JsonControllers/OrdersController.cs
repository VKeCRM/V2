using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VKeCRM.Portal.Web.Framework.Controllers;
using VKeCRM.Common.Collections;
using VKeCRM.Portal.Web.PortalServices;
using OrdersDto = VKeCRM.Portal.DataTransferObjects.Orders;
using VKeCRM.Framework.Mvc;



namespace VKeCRM.Portal.Web.Framework.JsonControllers
{
    public class OrdersController : PortalJsonControllerBase
    {
        public OrdersController()
        {
        }

        public JsonResult GetAllOrders()
        {
            var client = new PortalServicesClient();
            try
            {
                var list = client.GetAllOrders();
                return new JsonResult(list);
            }
            catch(Exception ex)
            {
                return new JsonResult(MvcErrorType.Normal, ex.Message);
            }
            finally
            {
                CleanUpServiceClient(client, client.InnerChannel);
            }
        }

        public JsonResult SaveOrder(string orderNo, string orderName)
        {
            var client = new PortalServicesClient();
            try
            {
                var dto = new OrdersDto();
                dto.OrderNo = orderNo;
                dto.OrderName = orderName;
                var list = client.SaveOrder(dto);
                return new JsonResult(list);
            }
            catch (Exception ex)
            {
                return new JsonResult(MvcErrorType.Normal, ex.Message);
            }
            finally
            {
                CleanUpServiceClient(client, client.InnerChannel);
            }
        }

    }
}