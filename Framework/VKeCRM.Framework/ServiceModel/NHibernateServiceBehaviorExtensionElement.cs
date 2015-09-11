//-----------------------------------------------------------------------
// <copyright file="NHibernateServiceBehaviorExtensionElement.cs" company="VKeCRM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace VKeCRM.Framework.ServiceModel
{
	/// <summary>
	/// Class that defines the extension element. We only need this if we want to apply our service behavior in the config file.
	/// </summary>
	public class NHibernateServiceBehaviorExtensionElement : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get
			{
				return typeof(NHibernateSessionInViewServiceBehavior);
			}
		}

		protected override object CreateBehavior()
		{
			return new NHibernateSessionInViewServiceBehavior();
		}
	}
}
