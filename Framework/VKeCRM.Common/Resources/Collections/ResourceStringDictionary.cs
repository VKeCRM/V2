//-----------------------------------------------------------------------
// <copyright file="ResourceStringDictionary.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace VKeCRM.Common.Resources.Collections
{
    /// <summary>
    /// String Dictionary for resources
    /// </summary>
    public class ResourceStringDictionary : Dictionary<string, string>
    {
        #region Fields

        /// <summary>
        /// Resource variable
        /// </summary>
        protected Resource _resource;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ResourceStringDictionary class
        /// </summary>
        /// <param name="assembly">Name of assembly</param>
        /// <param name="baseName">Name of base class</param>
        public ResourceStringDictionary(Assembly assembly, string baseName) : this(assembly, baseName, Thread.CurrentThread.CurrentUICulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResourceStringDictionary class
        /// </summary>
        /// <param name="assembly">Name of assembly</param>
        /// <param name="baseName">Name of base class</param>
        /// <param name="culture">Name of resource culture</param>
        public ResourceStringDictionary(Assembly assembly, string baseName, CultureInfo culture)
        {
            _resource = new Resource(assembly, baseName, culture);
        }

        /// <summary>
        /// Initializes a new instance of the ResourceStringDictionary class
        /// </summary>
        /// <param name="resourceManager">Name of resource manager</param>
        public ResourceStringDictionary(ResourceManager resourceManager) : this(resourceManager, Thread.CurrentThread.CurrentUICulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ResourceStringDictionary class
        /// </summary>
        /// <param name="resourceManager">Name of resource manager</param>
        /// <param name="culture">Name of resource culture</param>
        public ResourceStringDictionary(ResourceManager resourceManager, CultureInfo culture)
        {
            _resource = new Resource(resourceManager, culture);
        }

        #endregion

        #region Methods

        /// <summary>
        /// To check if ResourceSringDictionary contains the key for the given name
        /// </summary>
        /// <param name="name">Name of key to check if it exists</param>
        /// <returns>Returns a boolean value to indicate whether the ResourceStringDictionary contains the key for the given name</returns>
        public bool Contains(string name)
        {
            return ContainsKey(name);
        }

        /// <summary>
        /// To add into an array
        /// </summary>
        /// <param name="names">Names to add into array</param>
        public void Add(string[] names)
        {
            for (int index = 0; index < names.Length; index++)
            {
                Add(names[index]);
            }
        }

        /// <summary>
        /// To add a name into the ResourceStringDictionary
        /// </summary>
        /// <param name="name">Name to add into ResourceStringDictionary</param>
        public void Add(string name)
        {
            Add(name, _resource.GetString(name, String.Empty));
        }

        /// <summary>
        /// To remove from array
        /// </summary>
        /// <param name="names">Names to remove from array</param>
        public void Remove(string[] names)
        {
            for (int index = 0; index < names.Length; index++)
            {
                Remove(names[index]);
            }
        }

        /// <summary>
        /// To check if given ResourceStringDictionary equals this ResourceStringDictionary
        /// </summary>
        /// <param name="s">String Dictionary to check </param>
        /// <returns>Returns a boolean to indicate whether the given ResourceStringDictionary equals this ResourceStringDictionary</returns>
        public bool Equals(ResourceStringDictionary s)
        {
            if (s == null)
            {
                return false;
            }

            if (s == this)
            {
                return true;
            }

            return string.Equals(s.ToString(), ToString());
        }

        /// <summary>
        /// To check if given object is a ResourceStringDictionary
        /// </summary>
        /// <param name="obj">Object to check</param>
        /// <returns>Returns a boolean to indicate whether the given object is a ResourceStringDictionary</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is ResourceStringDictionary)
            {
                return Equals((ResourceStringDictionary)obj);
            }

            return false;
        }

        /// <summary>
        /// Get get Hash code
        /// </summary>
        /// <returns>Returns the hash code</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// To convert to string
        /// </summary>
        /// <returns>Returns the converted string</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            foreach (string key in Keys)
            {
                if (result.Length > 0)
                {
                    result.Append(", ");
                }

                result.Append(string.Format("{0}=\"{1}\"", key, this[key]));
            }

            return String.Format("{0}: [ {1} ]", GetType().Name, result);
        }

        #endregion
    }
}