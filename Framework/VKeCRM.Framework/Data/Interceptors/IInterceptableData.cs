//-----------------------------------------------------------------------
// <copyright file="IInterceptableData.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Data.Interceptors
{
    /// <summary>
    /// An interface to intercept data
    /// </summary>
	public interface IInterceptableData
	{
		/// <summary>
		/// A list of property names need to be intercepted. Sorry no type-safty here, so must be case-sensitive.
		/// </summary>
		/// <returns>Returns a list  of intercepted property names</returns>
		IList<string> GetInterceptedPropertyNames();
	}
}
