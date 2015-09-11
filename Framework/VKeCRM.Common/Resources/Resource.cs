//-----------------------------------------------------------------------
// <copyright file="Resource.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace VKeCRM.Common.Resources
{
    /// <summary>
    /// Resources common to the entire solution
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// The default culture
        /// </summary>
        protected CultureInfo _defaultCulture;

        /// <summary>
        /// Resource Manager
        /// </summary>
        protected ResourceManager _resourceManager;

        /// <summary>
        /// Initializes a new instance of the Resource class
        /// </summary>
        /// <param name="baseName">Base name for resource</param>
        public Resource(string baseName) : this(null, baseName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Resource class
        /// </summary>
        /// <param name="assembly">Name of assembly</param>
        /// <param name="baseName">Base name for resource</param>
        public Resource(Assembly assembly, string baseName) : this(assembly, baseName, Thread.CurrentThread.CurrentUICulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Resource class
        /// </summary>
        /// <param name="assembly">Name of assembly</param>
        /// <param name="baseName">Base name for resource</param>
        /// <param name="defaultCulture">The default culture</param>
        public Resource(Assembly assembly, string baseName, CultureInfo defaultCulture)
        {
            if (assembly != null)
            {
                _resourceManager = new ResourceManager(baseName, assembly ?? GetType().Assembly);
            }

            _defaultCulture = defaultCulture;
        }

        /// <summary>
        /// Initializes a new instance of the Resource class
        /// </summary>
        /// <param name="resourceManager">Name of resource manager</param>
        public Resource(ResourceManager resourceManager) : this(resourceManager, Thread.CurrentThread.CurrentUICulture)
        {
            _resourceManager = resourceManager;
        }

        /// <summary>
        ///  Initializes a new instance of the Resource class
        /// </summary>
        /// <param name="resourceManager">Name of resource manager</param>
        /// <param name="defaultCulture">The default culture</param>
        public Resource(ResourceManager resourceManager, CultureInfo defaultCulture)
        {
            _resourceManager = resourceManager;
            _defaultCulture = defaultCulture;
        }

        /// <summary>
        /// Return the string for the given id
        /// </summary>
        /// <param name="id">Id to get resource string</param>
        /// <returns>Returns the resource string for the given Id</returns>
        public string GetString(string id)
        {
            return GetString(id, String.Empty);
        }

        /// <summary>
        /// Return the string for the given id and culture
        /// </summary>
        /// <param name="id">Id to get resource string</param>
        /// <param name="culture">Resource culture</param>
        /// <returns>Returns the resource string for the given Id and culture</returns>
        public string GetString(string id, CultureInfo culture)
        {
            return GetString(id, String.Empty, culture);
        }

        /// <summary>
        /// Return the string for the given id and default value
        /// </summary>
        /// <param name="id">Id to get resource string</param>
        /// <param name="defaultValue">Default value for resource</param>
        /// <returns>Returns the resource string for the given Id and default value</returns>
        public string GetString(string id, string defaultValue)
        {
            return GetString(id, defaultValue, _defaultCulture);
        }

        /// <summary>
        /// Return the string for the given id, culture and default value
        /// </summary>
        /// <param name="id">Id to get resource string</param>
        /// <param name="defaultValue">Default value for resource</param>
        /// <param name="culture">Resource culture</param>
        /// <returns>Returns the resource string for the given Id, culture and default value</returns>
        public string GetString(string id, string defaultValue, CultureInfo culture)
        {
            string result = null;

            try
            {
                result = _resourceManager.GetString(id, culture);
            }
            catch
            {
                // TODO: Catch the exception
            }

            if (result == null)
            {
                result = defaultValue;
            }

            return result;
        }
    }
}