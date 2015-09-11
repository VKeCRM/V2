using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using VKeCRM.Framework.Web.UI.Controls.Json;

namespace VKeCRM.Framework.Web.UI.Controls.Json
{
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:DataGrid runat=server></{0}:DataGrid>")]
	public partial class DataGrid : CollectionControlBase
	{
		#region Control properties Object for items and foot.
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public List<DataGridItemBase> Items
		{
			get
			{
				List<DataGridItemBase> items = ViewState["VKeCRMDataGridItems"] as List<DataGridItemBase>;
				return items;
			}
			set
			{
				ViewState["VKeCRMDataGridItems"] = value;
			}
		}

		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DataGridFooter Footer
		{
			get
			{
				DataGridFooter foot = ViewState["VKeCRMDataGridFoot"] as DataGridFooter;
				return foot;
			}
			set
			{
				ViewState["VKeCRMDataGridFoot"] = value;
			}
		}
		#endregion


		protected override void RegisterOnLoadEventHanlder()
		{
			base.RegisterOnLoadEventHanlder();
			ClientHelper.RegisterDataGirdOnLoadEventHanlder(this);
			
		}

		public override CollectionType CollectionType
		{
			get
			{
				return CollectionType.DataGrid;
			}
		}





	}


	#region Useful but comment codes
	//string registerOnLoadFunction = string.Format(@"{0}.on('load', function() {{ {1} }});", _dataStoreName, GetOnLoadFunctionBody());
	//        private string GetOnLoadFunctionBody()
	//        {
	//            string functionBodyTemplate =
	//                @"
	//					var jsonData = {0}.reader.jsonData;
	//		
	//					var template = new Ext.XTemplate('{1}');
	//					template.overwrite('{2}',{0}.reader.jsonData)";

	//            string functionBodyHtml = string.Format(functionBodyTemplate, _dataStoreName, GetTemplatedConfigurationJS(), this.ClientID);

	//            return functionBodyHtml;

	//        }

	//        private string GetTemplatedConfigurationJS()
	//        {
	//            //For head, here only show the column name.
	//            string headTemplate = @"<thead>
	//										<tr>
	//											{0}
	//										</tr>
	//									</thead>";

	//            //For foot, here show paging bar and user define content
	//            string footTempate = @"<tfoot>
	//										{1}
	//										<tr class=""GridPager_Default"">
	//										<td colspan=""{0}"">
	//											{2}
	//
	//											</td>
	//										</tr>
	//									</tfoot>";
	//            //to {{[values.PageNumber * values.PageSize]}} of {{[values.PageSize * values.TotalPages]}}.
	//            //For body, here show data.
	//            string bodyTemplate =@"<tbody>
	//											<tpl for=""{0}"">		
	//											<tr class=""{{[xindex % 2 === 0 ? ""GridRow_Portal"" : ""GridAltRow_Portal""]}}"">
	//													{1}
	//											</tr>
	//											</tpl>				
	//									</tbody>";

	//            string template =
	//                @"<table id=""{0}"" class=""{1}"">
	//									{2}
	//									{3}
	//									{4}
	//				 </table>";

	//            //Replace the "'" char with "\'", because it will be used as a break char in javascript
	//            Regex regex = new Regex("'", RegexOptions.None);



	//            string headHtml = string.Format(headTemplate,
	//                                            Items.Aggregate<VKeCRMDataGridItemBase, string>(string.Empty,
	//                                                                                       (seed, item) =>
	//                                                                                       string.Concat(seed,
	//                                                                                                     "<td>" + item.Header +
	//                                                                                                     "</td>")));
	//            headHtml = regex.Replace(headHtml, "\\'");

	//            string bodyHtml = string.Format(bodyTemplate,
	//                                            this.For,
	//                                            Items.Aggregate<VKeCRMDataGridItemBase, string>(string.Empty,
	//                                                                                       (seed, item) =>
	//                                                                                       string.Concat(seed,
	//                                                                                       item.ToHtml())));

	//            string userFootHtml = string.Empty;
	//            if (Foot != null)
	//            {
	//                userFootHtml = regex.Replace(this.Foot.ToHtml(), "\\'");
	//            }

	//            string invokeGenerateFunction = @"'+ generatepager({0}.reader.jsonData.PageSize,
	//												{0}.reader.jsonData.PageNumber, 
	//												{0}.reader.jsonData.PageSize*{0}.reader.jsonData.TotalPages,
	//												{0}.reader.jsonData.TotalPages,
	//												'{1}', 
	//												{2},
	//												'{3}',
	//												'{4}')  + '";
	//            invokeGenerateFunction = string.Format(invokeGenerateFunction, _dataStoreName, LeftPagerClass, PageCount,
	//                                                   _dataStoreName, RightPagerClass);
	//            string footHtml = string.Format(footTempate, this.Items.Count, userFootHtml, invokeGenerateFunction);




	//            //#Clear specific character!!
	//            //Remove "\t\n" or "\n".
	//            string templateString =
	//                string.Format(template, _tableID, this.CssClass, headHtml, bodyHtml, footHtml).Replace(System.Environment.NewLine, string.Empty).
	//                    Replace("\n", string.Empty);

	//            ////Replace the "'" char with "\'", because it will be used as a break char in javascript
	//            //Regex regex = new Regex("'", RegexOptions.None);
	//            //templateString = regex.Replace(templateString, "\\'");

	//            return templateString;
	//        }

	#endregion

}
