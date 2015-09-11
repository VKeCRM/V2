using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using VKeCRM.Common.Utility;
using System.Xml.Serialization;
using System.Threading;
using System.Security.Principal;

namespace VKeCRM.Framework.Mvc.Security
{
	/// <summary>
	/// Manager mvc-action/user-permission mapping.
	/// </summary>
	public class PermessionManager
	{
		#region private properties

		private const string RootEleName = "prsnTable";

		private static PermessionManager _instance = null;

		private bool _isLoaded = false;
		private PermissionTable _permissionTable = null;

		/// <summary>
		/// Dictionary to hold permissionId - permission mapping.
		/// </summary>
		private Dictionary<int, Permission> _idPermissionMapping = null;

		/// <summary>
		/// Dictionary to hold mvc-action - permission mapping.
		/// </summary>
		private Dictionary<string, Permission> _actionPermissionMapping = null;

		#endregion

		#region singleton

		static PermessionManager()
		{
			_instance = new PermessionManager();
		}

		private PermessionManager()
		{
			_idPermissionMapping = new Dictionary<int, Permission>();
			_actionPermissionMapping = new Dictionary<string, Permission>();
		}

		public static PermessionManager Instance
		{
			get { return _instance; }
		}

		#endregion

		#region public methods

		/// <summary>
		/// Read permission table and transform to permission table class instance.
		/// </summary>
		/// <param name="path"></param>
		public void Load(string path)
		{
			if (!_isLoaded)
			{
				if (string.IsNullOrEmpty(path))
					throw new ArgumentException("path");

				if (!File.Exists(path))
					throw new FileNotFoundException(path);

				using (StreamReader reader = new StreamReader(path))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(PermissionTable), new XmlRootAttribute(RootEleName));
					_permissionTable = (PermissionTable)serializer.Deserialize(reader);
				}

				_permissionTable.Permissions.ForEach(delegate(Permission item)
				{
					_idPermissionMapping[item.PermissionId] = item;

					// if the action (url) is empty, don't add.
					if (!string.IsNullOrEmpty(item.Action))
						_actionPermissionMapping[item.Action.ToLower()] = item;
				});

				_isLoaded = true;
			}
		}

		/// <summary>
		/// true if permission manager is ready, otherwise false.
		/// </summary>
		public bool Enabled
		{
			get { return _isLoaded; }
		}

		/// <summary>
		/// get one permission by permissionId.
		/// </summary>
		/// <param name="byCode">permission id</param>
		/// <returns>the permission, null if not found.</returns>
		public Permission Get(int byCode)
		{
			LoadChecking();

			lock (_idPermissionMapping)
			{
				if (_idPermissionMapping.ContainsKey(byCode))
					return _idPermissionMapping[byCode];
				else
					return null;
			}
		}

		/// <summary>
		/// get one permission by mvc-action.
		/// </summary>
		/// <param name="byAction">mvc action.</param>
		/// <returns>the permission, null if not found.</returns>
		public Permission Get(string byAction)
		{
			LoadChecking();

			lock (_actionPermissionMapping)
			{
				string key = byAction.ToLower();

				if (_actionPermissionMapping.ContainsKey(key))
					return _actionPermissionMapping[key];
				else
					return null;
			}
		}

		/// <summary>
		/// to organize all permissions defined in permission table to current user.
		/// </summary>
		/// <returns></returns>
		public Dictionary<int, bool> MapCurrentUserzPermissions()
		{
			LoadChecking();

			Dictionary<int, bool> mappedResult = new Dictionary<int, bool>();

			_permissionTable.Permissions.ForEach(delegate(Permission item)
			{
				mappedResult.Add(item.PermissionId, HasPermission(item, true));
			});

			return mappedResult;
		}

		/// <summary>
		/// to check if the user has the permission to call this action.
		/// </summary>
		/// <param name="action"></param>
		/// <param name="passIfNoPernissionFound"></param>
		/// <returns></returns>
		public bool HasPermission(string action, bool passIfNoPermissionFound)
		{
			return this.HasPermission(this.Get(action), passIfNoPermissionFound);
		}

		/// <summary>
		/// to check if the user has the permission to the given permission id.
		/// </summary>
		/// <param name="p"></param>
		/// <param name="passIfNoPernissionFound"></param>
		/// <returns></returns>
		public bool HasPermission(Permission p, bool passIfNoPermissionFound)
		{
			if (null == p)
			{
				return passIfNoPermissionFound;
			}
			else
			{
				IPrincipal principal = System.Web.HttpContext.Current.User;
				string[] roleArray = p.GetRoleArray();

				foreach (string role in roleArray)
				{
					if (principal.IsInRole(role))
						return true;
				}

				return false;
			}
		}

		#endregion

		#region private mehtods

		private void LoadChecking()
		{
			if (!_isLoaded)
				throw new Exception("Please call PermessionManager.load(string) first!!!");
		}

		#endregion
	}
}
