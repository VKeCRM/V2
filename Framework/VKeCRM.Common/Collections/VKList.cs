using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace VKeCRM.Common.Collections
{
	/// <summary>
	/// Implements a customerized List that contains the total number of records. This should be primarily used to return a paged collection.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	[DataContract]
	public class VKList<T>
	{
		/// <summary>
		/// Get or set 
		/// </summary>
		[DataMember]
		public int TotalRecordCount
		{
			get;
			set;
		}


		/// <summary>
		/// Wrap the underlining list.
		/// </summary>
		[DataMember]
		public IList<T> Items
		{
			get;
			set;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public VKList()
		{
			Items = new List<T>();
		}

		public VKList(IList<T> list)
		{
			Items = list;
		}

        //public override string ToString()
        //{
			
        //    try
        //    {
        //        StringBuilder builder = new StringBuilder();
        //        builder.Append("{");
        //        builder.AppendFormat("\"{0}\":{1},", "TotalRecordCount", TotalRecordCount);

        //        // we have to serialize it like this, otherwise System.ExecutionEngineException would happen
        //        if (this.Items != null && Items.Count>0 && Items[0] is IDataTransferObject)
        //        {
        //            var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(this.Items.GetType());
        //            byte[] bytes;
        //            using (var memoryStream = new MemoryStream())
        //            {
        //                serializer.WriteObject(memoryStream, this.Items);
        //                bytes = new byte[memoryStream.Length];
        //                memoryStream.Position = 0;
        //                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
        //                memoryStream.Close();
        //            }
        //            builder.AppendFormat("\"{0}\":{1}", "Items", Encoding.Default.GetString(bytes));
        //        }
        //        builder.Append("}");
        //        return builder.ToString();
        //    }
        //    catch
        //    {
        //        return base.ToString();
        //    }
        //}
	}

}
