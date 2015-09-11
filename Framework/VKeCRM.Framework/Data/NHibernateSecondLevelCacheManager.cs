using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Util;
using NHibernate.Engine;
using Memcached.ClientLibrary;
using NHibernate.Caches.MemCache;

namespace VKeCRM.Framework.Data
{
    public class NHibernateSecondLevelCacheManager
    {
        #region Thread-safe, lazy Singleton
        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static NHibernateSecondLevelCacheManager Instance
        {
            get
            {
                return Nested.NHibernateSecondLevelCacheManager;
            }
        }

        /// <summary>
        /// Assists with ensuring thread-safe, lazy singleton
        /// </summary>
        private class Nested
        {
            static Nested() { }
            internal static readonly NHibernateSecondLevelCacheManager NHibernateSecondLevelCacheManager =
                new NHibernateSecondLevelCacheManager();
        }
        
        /// <summary>
        /// Declare the private constructor which prevent others construct its instance.
        /// </summary>
        private NHibernateSecondLevelCacheManager()
        {
           // factory = NHibernateSessionManager.Instance.GetSession().GetSessionImplementation().Factory;
          
            _provider = new MemCacheProvider();
            _provider.Start(new Dictionary<string, string>());

            _client = new MemcachedClient() { PoolName = "nhibernate" };
            
        }
        #endregion

        #region Private field
        private readonly ISessionFactoryImplementor factory;
        private readonly ICacheProvider _provider;
        private readonly MemcachedClient _client;
        #endregion


        #region CacheRegion Management methods. Seems not useful for the distributed envrionment.
        /// <summary>
        ///  Evict cache region from SecondLevelCache by name.
        /// </summary>
        /// <param name="cacheregion"></param>
        public void EvictCacheRegion(string cacheregion)
        {
            if (string.IsNullOrEmpty(cacheregion))
            {
                return;
            }

            factory.GetSecondLevelCacheRegion(cacheregion).Clear();
        }

        /// <summary>
        /// Evict cache region from SecondLevelCache by related persistent type.
        /// </summary>
        /// <param name="persistentClass"></param>
        public void EvictEntityRelatedCacheRegion(System.Type persistentClass)
        {
            
        }

        /// <summary>
        /// Evict the record by its key from CacheRegion
        /// </summary>
        /// <param name="persistentClass"></param>
        /// <param name="key"></param>
        public void EvictRecord(System.Type persistentClass, object key)
        {
        
        }

        /// <summary>
        /// Evict the query record from SecondLevelCache
        /// Note:All related Criteria and Query are using query cache.
        /// </summary>
        public void EvictQueryCache()
        {
            factory.QueryCache.Clear();
        }

        /// <summary>
        /// Get all cache region in SecondLevelCache
        /// </summary>
        /// <returns>The array of region's name</returns>
        public string[] GetAllCacheRegions()
        {
            return ArrayHelper.ToStringArray(factory.GetAllSecondLevelCacheRegions().Keys); 
        }


        /// <summary>
        /// Evicts the cache regions.
        /// </summary>
        /// <param name="regions">The regions.</param>
        public void EvictCacheRegions(string[] regions)
        {   
            if(regions == null)
            {
                return;
            }

            foreach (string region in regions)
            {
                EvictCacheRegion(region);
            }

        }

        /// <summary>
        /// Evicts all cache regions.
        /// </summary>
        public void EvictAllCacheRegions()
        {
            EvictCacheRegions(GetAllCacheRegions());
        }
        #endregion


        #region CacheServer Management methods


        /// <summary>
        /// Retrieves stats for all servers.
        /// 
        /// Returns a map keyed on the servername.
        /// The value is another map which contains stats
        /// with stat name as key and value as value.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetAllMemcacheServersStats()
        {
            return _client.Stats();
        }

        /// <summary>
        /// Retrieves stats for special servers.
        /// 
        /// Returns a map keyed on the servername.
        /// The value is another map which contains stats
        /// with stat name as key and value as value.
        /// </summary>
        /// <returns></returns>
        public Hashtable GetMemcacheServersStats(ArrayList servers)
        {
            return _client.Stats(servers);
        }

        /// <summary>
        /// Invalidates the entire cache for all memcache servers.
        /// </summary>
        /// <returns></returns>
        public bool FlushAllMemcacheServers()
        {
            return _client.FlushAll();
        }

        /// <summary>
        /// Invalidates the entire cache for special memcache servers.
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public bool FlushMemcacheServers(ArrayList servers)
        {
            return _client.FlushAll(servers);
        }

        /// <summary>
        /// Retrieves name for all servers.
        /// 
        /// Returns a map keyed on the servername.
        /// The value is another map which contains stats
        /// with stat name as key and value as value.
        /// </summary>
        /// <returns></returns>
        public string[] GetAllMemcacheServersNameList()
        {
            MemCacheConfig[] configs = ConfigurationManager.GetSection("memcache") as MemCacheConfig[];

            if (configs == null)
            {
                return null;
            }

            ArrayList myServers = new ArrayList();
            foreach (MemCacheConfig config in configs)
            {
                myServers.Add(string.Format("{0}:{1}", config.Host, config.Port));

            }
            return (string[])myServers.ToArray(typeof(string));

        }

        #endregion

    }
}
