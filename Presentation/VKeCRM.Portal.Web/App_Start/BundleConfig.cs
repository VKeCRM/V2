using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace VKeCRM.Portal.Web.App_Start
{
	public class BundleConfig
	{
		public static void Register(BundleCollection bundles)
		{
			BundleTable.EnableOptimizations = ConfigurationManager.AppSettings["jsCompress"] == "true";

			bundles.Add(new StyleBundle("~/Script/jquery/css").Include(
				"~/Scripts/jquery/spinner/ui.spinner.css",
				"~/Scripts/jquery/dialog/ui.dialog.css",
				"~/Scripts/jquery/datepicker/datepicker.css",
				"~/Scripts/jquery/pagination/pagination.css",
				"~/Scripts/plugins/jquery.select2/jquery.select2.css"));

			bundles.Add(new ScriptBundle("~/bundles/hack").IncludeDirectory(
				"~/Scripts/ltie", "*.js"));

	        bundles.Add(new ScriptBundle("~/bundles/base").Include("~/Scripts/plugins/SessionActivePlugin.*")
	            .IncludeDirectory(
				"~/Scripts/base", "*.js").Include(
				"~/Scripts/plugins/jquery.json.*",
				"~/Scripts/plugins/jquery.form.*",
				"~/Scripts/plugins/jquery.validate.*",
				"~/Scripts/plugins/jquery.select2/jquery.select2.js",
				"~/Scripts/ext/ext.*",
				"~/Scripts/ext/ext-more.*",
				"~/Scripts/ext/date.*",
				"~/Scripts/ext/format.*",
				"~/Scripts/ext/menuTop.*",
				"~/Scripts/framework/framework.common.*",
				"~/Scripts/framework/framework.ui.*",
                "~/Scripts/framework/global.*",
                "~/Scripts/framework/JSLINQ.*",
				"~/Scripts/framework/bootstrap/bootstrap.*",
				"~/Scripts/framework/bootstrap/moment.*",
				"~/Scripts/framework/bootstrap/bootstrap-datetimepicker.*",
				"~/Scripts/framework/bootstrap/bootstrap3-typeahead.*",
				"~/Scripts/framework/bootstrap/bootstrap3-typeaheadComplete.*",
				"~/Scripts/plugins/ui.core.*",
				"~/Scripts/plugins/slider/ui.slider.*",
				"~/Scripts/plugins/dialog/jquery.blockUI.*",
				"~/Scripts/plugins/logger/ui.logger.*",
				"~/Scripts/plugins/datepicker/date.*",
				"~/Scripts/plugins/datepicker/jquery.datepicker.*",
				"~/Scripts/plugins/numeric/jquery.number.*",
				"~/Scripts/plugins/pagination/jquery.pagination.*",
				"~/Scripts/plugins/jquery.form.*",
				"~/Scripts/plugins/tablesorter/js/jquery.tablesorter.*",
				"~/Scripts/plugins/jquery-jtemplates.*",
				"~/Scripts/entity/entity.paging.*",
				"~/Scripts/plugins/sortTemplate.*",
				"~/Scripts/plugins/bootstrap-multiselect/js/bootstrap-multiselect.*",
				"~/Scripts/plugins/bootstrap-notify/js/bootstrap-notify.*",
				"~/Scripts/framework/AjaxHelper.*",
				"~/Scripts/framework/AccessRightHelper.*",
				"~/Scripts/plugins/ajaxLoading/jquery.ajaxLoading.*",
				"~/Scripts/framework/VKeCRM.events.*",
				"~/Scripts/plugins/signalr/jquery.signalR-*"
				));
		}
	}
}