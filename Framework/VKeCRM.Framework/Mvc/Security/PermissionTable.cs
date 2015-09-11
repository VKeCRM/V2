using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace VKeCRM.Framework.Mvc.Security
{
	[Serializable]
	public class PermissionTable
	{
		[XmlArray("prsns")]
		[XmlArrayItem("prsn")]
		public List<Permission> Permissions
		{
			get;
			set;
		}
	}
}
