//------------------------------------------------------------------------------
// This file contains custom code for the factory class.
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using VKeCRM.Portal.DataTransferObjects;
using MasterDomain = VKeCRM.Portal.DataAccess.DomainObjects.Master;
using MasterDto = VKeCRM.Portal.DataTransferObjects.Master;

namespace VKeCRM.Portal.Business.Factories
{
	internal partial class MasterFactory
	{
		/// <summary>
		/// Copies data from Domain object to Data transfer object
		/// </summary>
		/// <returns></returns>
        public void CopyToDto(MasterDomain domainObject, MasterDto dataTransferObject)
		{
			if (domainObject == null || dataTransferObject == null)
				return;
			
            dataTransferObject.Id = domainObject.Id;
            dataTransferObject.MasterName = domainObject.MasterName;
				
			#region Customized. DO NOT REGENERATE
			#endregion
			
			return;
		}
	


		
		
	}
}
