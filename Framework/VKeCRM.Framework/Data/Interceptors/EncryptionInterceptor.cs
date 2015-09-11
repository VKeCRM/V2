//-----------------------------------------------------------------------
// <copyright file="EncryptionInterceptor.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Type;

namespace VKeCRM.Framework.Data.Interceptors
{
    /// <summary>
    /// Encryption Interceptor
    /// </summary>
	public class EncryptionInterceptor : EmptyInterceptor
    {
        #region Methods

        /// <summary>
        /// To decrypt the states during load
        /// </summary>
        /// <param name="entity">Entity to load</param>
        /// <param name="id">Id of the object</param>
        /// <param name="state">States to decrypt</param>
        /// <param name="propertyNames">Names of properties</param>
        /// <param name="types">Nhibernate types</param>
        /// <returns>Returns a boolean value to indicate whether decryption was a success or not</returns>
		public bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
		{
			if (entity is IInterceptableData)
			{
				IList<string> encProperties = (entity as IInterceptableData).GetInterceptedPropertyNames();
				List<string> allNames = new List<string>(propertyNames);
				foreach (string name in encProperties)
				{
					int i = allNames.IndexOf(name);
					if (i >= 0)
					{
						state[i] = Decrypt(state[i].ToString());
					}
				}
			}

			return true;

			// throw new NotImplementedException();
		}

        /// <summary>
        /// To encrypt the states when saving an entity
        /// </summary>
        /// <param name="entity">Entity to save</param>
        /// <param name="id">Id of the object</param>
        /// <param name="state">States to encrypt</param>
        /// <param name="propertyNames">Names of properties</param>
        /// <param name="types">NHibernate types</param>
        /// <returns>Returns a boolean value to indicate whether encryption was a success or not</returns>
		public bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
		{
			if (entity is IInterceptableData)
			{
				IList<string> encProperties = (entity as IInterceptableData).GetInterceptedPropertyNames();
				List<string> allNames = new List<string>(propertyNames);
				foreach (string name in encProperties)
				{
					int i = allNames.IndexOf(name);
					if (i >= 0)
					{
						state[i] = Encrypt(state[i].ToString());
					}
				}
			}

			return true;

			// throw new NotImplementedException();
		}

        /// <summary>
        /// To encrypt an input
        /// </summary>
        /// <param name="input">input to encrypt</param>
        /// <returns>Returns the encrypted string</returns>
		private string Encrypt(string input)
		{
			// return input + "...";
			return input;
		}

        /// <summary>
        /// To decrypt an input
        /// </summary>
        /// <param name="input">Input to decrypt</param>
        /// <returns>Returns the decrypted string</returns>
		private string Decrypt(string input)
		{
			// return input.Substring(0, input.Length - 3);
			return input;
		}

        #endregion
	}
}
