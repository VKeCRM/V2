using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;

namespace VKeCRM.Common.Collections
{
	[DataContract]
	public class WhereOptions
	{
		private List<object> _values;
		private StringBuilder _predicates;
		private int index = 0;
		public string predicates
		{
			get {
				return _predicates.ToString();
			}
		}
		public List<object> Values
		{
			get
			{
				return _values;
			}
		}
		public void Or(string predicate, object value)
		{
			if (index > 0)
				predicate = string.Concat(" || ", predicate);

			Add(predicate, value);
		}

		public void And(string predicate, object value)
		{
			if (index > 0)
				predicate = string.Concat(" && ", predicate);

			Add(predicate, value);
		}

		private void Add(string predicate, object value)
		{	
			_values.Add(value);
			predicate = predicate.Replace("#", index.ToString());
			_predicates.Append(predicate);
			index++;
		}

		public WhereOptions()
		{
			_predicates = new StringBuilder(50);
			_values = new List<object>();
		}
	}
}
