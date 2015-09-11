//-----------------------------------------------------------------------
// <copyright file="IFactory.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace VKeCRM.Framework.Business
{
	/// <summary>
	/// This interface should be implemented by Factory classes
	/// </summary>
	/// <typeparam name="Tdto">Type of Data trasfer object</typeparam>
	/// <typeparam name="Tdomain">Type of Domain object</typeparam>
	public interface IFactory<Tdto, Tdomain>
		where Tdto : class
		where Tdomain : class
	{
		/// <summary>
		/// Creates Data transfer object from the provided Domain object
		/// </summary>
		/// <param name="domainObject">Domain object</param>
		/// <returns>Data transfer object</returns>
		Tdto CreateDto(Tdomain domainObject);

		/// <summary>
		/// Creates Domain object from the provided Data transfer object
		/// </summary>
		/// <param name="dataTransferObject">Data transfer object</param>
		/// <returns>Domain object</returns>
		Tdomain CreateNewDomainObject(Tdto dataTransferObject);

        ///// <summary>
        ///// Creates list of Data transfer objects from the provided list of Domain objects
        ///// </summary>
        ///// <param name="domainObjectList">list of Domain objects</param>
        ///// <returns>list of Data transfer objects</returns>
        //List<Tdto> CreateListOfDtos(IList<Tdomain> domainObjectList);

        ///// <summary>
        ///// Creates list of Domain objects from the provided list of Data transfer objects
        ///// </summary>
        ///// <param name="dataTransferObjectList">list of Data transfer objects</param>
        ///// <returns>list of Domain objects</returns>
        //List<Tdomain> CreateListOfNewDomainObjects(IList<Tdto> dataTransferObjectList);

		/// <summary>
		/// Copy properties of provided Data transfer object to provided Domain object
		/// </summary>
		/// <param name="dataTransferObject">Data transfer object - source object</param>
		/// <param name="domainObject">Domain object - destination</param>
		void CopyToDomainObject(Tdto dataTransferObject, Tdomain domainObject);
	}
}