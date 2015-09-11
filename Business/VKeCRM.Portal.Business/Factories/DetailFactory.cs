//------------------------------------------------------------------------------
// This file contains custom code for the factory class.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using VKeCRM.Portal.DataTransferObjects;
using DetailDomain = VKeCRM.Portal.DataAccess.DomainObjects.Detail;
using DetailDto = VKeCRM.Portal.DataTransferObjects.Detail;

namespace VKeCRM.Portal.Business.Factories
{
	internal partial class DetailFactory
	{
		/// <summary>
		/// Copies data from Domain object to Data transfer object
		/// </summary>
		/// <returns></returns>
		public void CopyToDto(DetailDomain domainObject, DetailDto dataTransferObject)
		{
			if (domainObject == null || dataTransferObject == null)
				return;
			
            dataTransferObject.DetailName = domainObject.DetailName;
            dataTransferObject.Id = domainObject.Id;
            dataTransferObject.MasterID = domainObject.MasterID;
				
			#region Customized. DO NOT REGENERATE
            //TODO: Add implementation
            //FactoryManager.DetailFactory.CopyToDto(dataTransferObject.DetailName
			#endregion
			
			return;
		}
	


		
		
	}
}
