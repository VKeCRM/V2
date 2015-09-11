//------------------------------------------------------------------------------
// This file contains custom code for the factory class.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using VKeCRM.Portal.DataTransferObjects;
using OrdersDomain = VKeCRM.Portal.DataAccess.DomainObjects.Orders;
using OrdersDto = VKeCRM.Portal.DataTransferObjects.Orders;

namespace VKeCRM.Portal.Business.Factories
{
	internal partial class OrdersFactory
	{
		/// <summary>
		/// Copies data from Domain object to Data transfer object
		/// </summary>
		/// <returns></returns>
		public void CopyToDto(OrdersDomain domainObject, OrdersDto dataTransferObject)
		{
			if (domainObject == null || dataTransferObject == null)
				return;

			dataTransferObject.Id = domainObject.Id;
			dataTransferObject.OrderNo = domainObject.OrderNo;
			dataTransferObject.OrderName = domainObject.OrderName;

			#region Customized. DO NOT REGENERATE
			#endregion

			return;
		}

		/// <summary>
		/// Copies data from Data transfer object to Domain object
		/// </summary>
		/// <returns></returns>
		public override void CopyToDomainObject(OrdersDto dataTransferObject, OrdersDomain domainObject)
		{
			if (dataTransferObject == null || domainObject == null)
				return;

			domainObject.Id = dataTransferObject.Id;
			domainObject.OrderNo = dataTransferObject.OrderNo;
			domainObject.OrderName = dataTransferObject.OrderName;

			#region Customized. DO NOT REGENERATE
			#endregion

			return;
		}



	}
}
