//-----------------------------------------------------------------------
// <copyright file="IFactory.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using VKeCRM.Common.Collections;

namespace VKeCRM.Framework.Business
{
	/// <summary>
	/// This interface should be implemented by Factory classes
	/// </summary>
	/// <typeparam name="Tdto">Type of Data trasfer object</typeparam>
	/// <typeparam name="Tdomain">Type of Domain object</typeparam>
    public abstract class FactoryBase<Tdto, Tdomain>
		where Tdto : class
        where Tdomain : class
	{
		/// <summary>
		/// Creates Data transfer object from the provided Domain object
		/// </summary>
		/// <param name="domainObject">Domain object</param>
		/// <returns>Data transfer object</returns>
        public abstract Tdto CreateDto(Tdomain domainObject);

        /// <summary>
        /// Copy properties of provided Data transfer object to provided Domain object
        /// </summary>
        /// <param name="dataTransferObject">Data transfer object - source object</param>
        /// <param name="domainObject">Domain object - destination</param>
        public abstract void CopyToDomainObject(Tdto dataTransferObject, Tdomain domainObject);

        /// <summary>
        /// Creates Domain object from the provided Data transfer object
        /// </summary>
        /// <param name="dataTransferObject">Data transfer object</param>
        /// <returns>Domain object</returns>
        //public virtual Tdomain CreateNewDomainObject(Tdto dataTransferObject)
        //{
        //    Tdomain domainObject = new Tdomain();
        //    CopyToDomainObject(dataTransferObject, domainObject);
        //    return domainObject;
        //}
        public abstract Tdomain CreateNewDomainObject(Tdto dataTransferObject);

        /// <summary>
        /// Creates list of Data transfer objects from the provided list of Domain objects
        /// </summary>
        /// <param name="domainObjectList">list of Domain objects</param>
        /// <returns>list of Data transfer objects</returns>
        public virtual VKList<Tdto> CreateListOfDtos(VKList<Tdomain> domainObjectList) 
        {
            VKList<Tdto> dataTransferObjects = new VKList<Tdto>();

            if (domainObjectList != null)
            {
                dataTransferObjects.TotalRecordCount = domainObjectList.TotalRecordCount;
                dataTransferObjects.Items = new List<Tdto>(domainObjectList.Items.Count);

                for (int i = 0; i < domainObjectList.Items.Count; i++)
                {
                    Tdto dataTransferObject = CreateDto(domainObjectList.Items[i]);

                    if (dataTransferObject != null)
                    {
                        dataTransferObjects.Items.Add(dataTransferObject);
                    }
                }
            }

            return dataTransferObjects;
        }

        /// <summary>
        /// Creates list of Data transfer objects from the provided list of Domain objects
        /// </summary>
        /// <param name="domainObjectList">list of Domain objects</param>
        /// <returns>list of Data transfer objects</returns>
        public virtual IList<Tdto> CreateListOfDtos(IList<Tdomain> domainObjectList)
        {
            IList<Tdto> dataTransferObjects = new List<Tdto>();

            if (domainObjectList != null)
            {
                for (int i = 0; i < domainObjectList.Count; i++)
                {
                    Tdto dataTransferObject = CreateDto(domainObjectList[i]);

                    if (dataTransferObject != null)
                    {
                        dataTransferObjects.Add(dataTransferObject);
                    }
                }
            }

            return dataTransferObjects;
        }

        /// <summary>
        /// Creates list of Domain objects from the provided list of Data transfer objects
        /// </summary>
        /// <param name="dataTransferObjectList">list of Data transfer objects</param>
        /// <returns>list of Domain objects</returns>
        public virtual VKList<Tdomain> CreateListOfNewDomainObjects(VKList<Tdto> dataTransferObjectList) 
        {
            VKList<Tdomain> domainObjects = new VKList<Tdomain>();

            if (dataTransferObjectList != null)
            {
                domainObjects.TotalRecordCount = dataTransferObjectList.TotalRecordCount;
                domainObjects.Items = new List<Tdomain>(dataTransferObjectList.Items.Count);

                for (int i = 0; i < dataTransferObjectList.Items.Count; i++)
                {
                    Tdomain domainObject = CreateNewDomainObject(dataTransferObjectList.Items[i]);

                    if (domainObject != null)
                    {
                        domainObjects.Items.Add(domainObject);
                    }
                }
            }

            return domainObjects;
        }

        /// <summary>
        /// Creates list of Domain objects from the provided list of Data transfer objects
        /// </summary>
        /// <param name="dataTransferObjectList">list of Data transfer objects</param>
        /// <returns>list of Domain objects</returns>
        public virtual IList<Tdomain> CreateListOfNewDomainObjects(IList<Tdto> dataTransferObjectList)
        {
            IList<Tdomain> domainObjects = new List<Tdomain>();

            if (dataTransferObjectList != null)
            {

                for (int i = 0; i < dataTransferObjectList.Count; i++)
                {
                    Tdomain domainObject = CreateNewDomainObject(dataTransferObjectList[i]);

                    if (domainObject != null)
                    {
                        domainObjects.Add(domainObject);
                    }
                }
            }

            return domainObjects;
        }
        /// <summary>
        /// Creates list of Domain objects from the provided list of Data transfer objects
        /// </summary>
        /// <param name="dataTransferObjectList">list of Data transfer objects</param>
        /// <param name="domainObjects">list of Data original objects, for hibernate it can leave its p value</param>
        /// <returns>list of Domain objects</returns>
        public virtual IList<Tdomain> CreateListOfNewDomainObjects(IList<Tdto> dataTransferObjectList, IList<Tdomain> domainObjects)
        {
            if (domainObjects != null)
            {
                domainObjects.Clear();
            }
            else
            {
                domainObjects = new List<Tdomain>();
            }

            if (dataTransferObjectList != null)
            {

                for (int i = 0; i < dataTransferObjectList.Count; i++)
                {
                    Tdomain domainObject = CreateNewDomainObject(dataTransferObjectList[i]);

                    if (domainObject != null)
                    {
                        domainObjects.Add(domainObject);
                    }
                }
            }

            return domainObjects;
        }

        /// <summary>
        /// Convert DTO list to VKList
        /// </summary>
        /// <param name="dataTransferObjectList">Special IList DTO list</param>
        /// <returns>VKList entity</returns>
        public VKList<Tdto> CopyToVKList(IList<Tdto> dataTransferObjectList)
        {
            VKList<Tdto> dataTransferObjectVKList = new VKList<Tdto>();
            dataTransferObjectVKList.Items = dataTransferObjectList;
            return dataTransferObjectVKList;
        }

        /// <summary>
        /// Convert Domain list to VKList
        /// </summary>
        /// <param name="dataTransferObjectList">Special IList Domain list</param>
        /// <returns>VKList entity</returns>
        public VKList<Tdomain> CopyToVKList(IList<Tdomain> domainObjectList) 
        {
            VKList<Tdomain> domainObjectVKList = new VKList<Tdomain>();
            domainObjectVKList.Items = domainObjectList;
            return domainObjectVKList;
        }
	}
}