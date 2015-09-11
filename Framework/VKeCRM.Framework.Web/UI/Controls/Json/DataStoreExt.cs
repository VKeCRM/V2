using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	public class DataStoreExt
	{
		public Dictionary<string, string> DataStoreConfig = new Dictionary<string, string>();

		#region Special configuration

		public string ControlName;

		public List<string> Fields = new List<string>();
		public Dictionary<string, string> BaseParams = new Dictionary<string, string>();

		public string HttpMethod = "POST";	
		//Url 
		public string HttpUrl;
		#endregion

		public override string ToString()
		{
			string @dataStore =
				@"var {0} = new Ext.data.JsonStore({{
									proxy: new Ext.data.HttpProxy({{
													method: '{5}',
													disableCaching:false,
													url: '{1}'
													}}),
										{2},
										baseParams : {3},
										fields : {4}
								}});";

//            @"$.getJSON('QueryJson.ashx'
//					  ,{'MemberId': getMemberId(), 'Type' : 3}
//					  ,function(person){		
//							});"

			string dataStoreConfig = GetDataStoreConfigString();
			string dataStoreBaseParams = GetBaseParamsString();
			string dataStoreFields = GetFieldsString();


			return string.Format(@dataStore, ControlName, HttpUrl, dataStoreConfig, dataStoreBaseParams, dataStoreFields, HttpMethod);
		}

		#region Help Methods to generate script
		//Paramters for jsonstore configurations.
		private string GetDataStoreConfigString()
		{
			string result = DataStoreConfig.Aggregate
				<KeyValuePair<string, string>, string>
				(string.Empty, (seed, item) => string.Format("{0}, {1} : {2}", seed, item.Key, item.Value));

			if (!string.IsNullOrEmpty(result))
			{
				//Remove the frist char: ','
				//Now the string format is : Config1: value1, Config2: value2, Config3: value3...
				result = result.Remove(0, 1);
			}

			return result;
		}

		//Paramters for query strings.
		private string GetBaseParamsString()
		{
			string result = BaseParams.Aggregate
				<KeyValuePair<string, string>, string>
				(string.Empty, (seed, item) => string.Format("{0}, {1} : {2}", seed, item.Key, item.Value));

			if (!string.IsNullOrEmpty(result))
			{
				//Remove the frist char: ','
				//Now the string format is : Config1: value1, Config2: value2, Config3: value3...
				result =  result.Remove(0, 1);

			}

			return string.Concat("{", result, "}");
		}

		//Paramter for fields in jason store. //not use now
		private string GetFieldsString()
		{
			string result = Fields.Aggregate
				<string, string>
				(string.Empty, (seed, item) => string.Format("{0}, '{1}'", seed, item));

			if (!string.IsNullOrEmpty(result))
			{
				//Remove the frist char: ','
				//Now the string format is : 'field1', 'field2', 'field3'...
				result = result.Remove(0, 1);
			}

			return string.Concat("[", result, "]");

		}
		#endregion
	}
}