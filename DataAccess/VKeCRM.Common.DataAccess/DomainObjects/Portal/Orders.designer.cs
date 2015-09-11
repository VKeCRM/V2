using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Portal.DataAccess.DomainObjects
{
	/// <summary>
	/// Orders domain object contains a record from the dbo.Orders table
	/// </summary>
	public partial class Orders
	{
		#region Declarations
		private Guid _id;
		private string _orderNo;
		private string _orderName;
		#endregion

		#region Properties

		public virtual Guid Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public virtual string OrderNo
		{
			get { return _orderNo; }
			set { _orderNo = value; }
		}
		public virtual string OrderName
		{
			get { return _orderName; }
			set { _orderName = value; }
		}
		#endregion
	}
}
