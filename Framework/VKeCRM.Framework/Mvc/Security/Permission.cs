using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace VKeCRM.Framework.Mvc.Security
{
	[Serializable]
	public class Permission
	{
		[XmlElement("pId")]
		public int PermissionId
		{
			get;
			set;
		}

		[XmlElement("act")]
		public string Action
		{
			get;
			set;
		}

		[XmlElement("rols")]
		public string Roles
		{
			get;
			set;
		}

		[XmlElement("desc")]
		public string Description
		{
			get;
			set;
		}

		private string[] roleArray = null;
		public string[] GetRoleArray()
		{
			if (roleArray == null)
				roleArray = Roles.Split(',').Select(p => p.Trim().ToLower()).ToArray();

			return roleArray;
		}
	}
}
