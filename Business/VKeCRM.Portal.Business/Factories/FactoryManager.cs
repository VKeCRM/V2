//-----------------------------------------------------------------------
// <copyright file="Factory.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using VKeCRM.Common.Lookups;
using VKeCRM.Portal.DataTransferObjects;

namespace VKeCRM.Portal.Business.Factories
{
    /// <summary>
    /// Properties for all factories used in VKeCRM.
    /// </summary>
    internal class FactoryManager
    {
        private static object _syncObject = new object();
        private static OrdersFactory _ordersFactory = null;
        private static MasterFactory _MasterFactory = null;
        private static DetailFactory _detailFactory = null;
      

        
        static FactoryManager()
        {
        }


        private FactoryManager()
        {
        }

        public static OrdersFactory OrdersFactory
        {
            get
            {
                if (_ordersFactory == null)
                {
                    lock (_syncObject)
                    {
                        if (_ordersFactory == null)
                        {
                            _ordersFactory = new OrdersFactory();
                        }
                    }
                }

                return _ordersFactory;
            }
        } 
        public static MasterFactory MasterFactory
        {
            get
            {
                if (_MasterFactory == null)
                {
                    lock (_syncObject)
                    {
                        if (_MasterFactory == null)
                        {
                            _MasterFactory = new MasterFactory();
                        }
                    }
                }

                return _MasterFactory;
            }
        }

        public static DetailFactory DetailFactory
        {
            get
            {
                if (_detailFactory == null)
                {
                    lock (_syncObject)
                    {
                        if (_detailFactory == null)
                        {
                            _detailFactory = new DetailFactory();
                        }
                    }
                }

                return _detailFactory;
            }
        }

       

        #region Convert List of lookups to list of enums and vice versa
        public static IList<enumT> GetListOfEnums<enumT, lookupT>(IList<lookupT> lookupList)
            where lookupT : LookupBase<lookupT>
            where enumT : struct
        {
            IList<enumT> enumList = null;

            if (lookupList != null)
            {
                enumList = new List<enumT>(lookupList.Count);

                foreach (lookupT lookupItem in lookupList)
                {
                    enumT enumItem = (enumT)System.Enum.Parse(typeof(enumT), lookupItem.Code.ToString());
                    enumList.Add(enumItem);
                }
            }

            return enumList;
        }

        public static IList<lookupT> GetListOfLookups<lookupT, enumT>(IList<enumT> enumlist)
            where lookupT : LookupBase<lookupT>
            where enumT : struct
        {
            IList<lookupT> lookupList = null;

            if (enumlist != null)
            {
                lookupList = new List<lookupT>(enumlist.Count);
                MethodInfo getByCode = typeof(lookupT).GetMethod("GetByCode");

                foreach (enumT enumItem in enumlist)
                {
                    // lookupT lookupItem = (lookupT)getByCode.Invoke(null, new object[] { Convert.ToInt16(enumItem) });
                    lookupT lookupItem = GetLookup<lookupT, enumT>(enumItem);

                    lookupList.Add(lookupItem);
                }
            }

            return lookupList;
        }

        private delegate lookupT GetLookupFromEnum<lookupT>(short code);
        private static Dictionary<string, object> lookupDelegates = new Dictionary<string, object>(40);
        private static object _lockObject = new object();

        private static lookupT GetLookup<lookupT, enumT>(enumT enumValue)
            where lookupT : LookupBase<lookupT>
            where enumT : struct
        {
            string key = typeof(lookupT).FullName;
            object lookupDelegate = null;
            lookupT lookup = null;

            if (lookupDelegates.TryGetValue(key, out lookupDelegate))
            {
                lookup = ((GetLookupFromEnum<lookupT>)lookupDelegate)(Convert.ToInt16(enumValue));
            }
            else
            {
                Type[] args = { typeof(short) };
                DynamicMethod dMethod = new DynamicMethod("GetLookupFromE", typeof(lookupT), args, typeof(FactoryManager).Module);
                MethodInfo getByCode = typeof(lookupT).GetMethod("GetByCode");

                ILGenerator il = dMethod.GetILGenerator(2048);
                il.Emit(OpCodes.Ldarg_0);
                il.EmitCall(OpCodes.Call, getByCode, null);
                il.Emit(OpCodes.Ret);

                GetLookupFromEnum<lookupT> getLookup = (GetLookupFromEnum<lookupT>)dMethod.CreateDelegate(typeof(GetLookupFromEnum<lookupT>));

                lock (_lockObject)
                {
                    if (!lookupDelegates.ContainsKey(key))
                    {
                        lookupDelegates.Add(key, getLookup);
                    }
                }

                lookup = getLookup(Convert.ToInt16(enumValue));
            }

            return lookup;
        }
        #endregion
    }
}
