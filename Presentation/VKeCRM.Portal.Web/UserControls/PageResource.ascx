<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageResource.ascx.cs" Inherits="VKeCRM.Portal.Web.UserControls.PageResource" %>

<%--here are css files--%>



<%--<link rel="stylesheet" type="text/css" href="/Scripts/jquery/spinner/ui.spinner.css" />--%>
<link rel="stylesheet" type="text/css" href="/Scripts/jquery/dialog/ui.dialog.css" />
<link rel="stylesheet" type="text/css" href="/Scripts/jquery/datepicker/datepicker.css" />
<link rel="stylesheet" type="text/css" href="/Scripts/jquery/pagination/pagination.css" />

<link rel="stylesheet" type="text/css" href="/Themes/default/css/bootstrap-theme.css" />
<link rel="stylesheet" type="text/css" href="/Themes/default/css/bootstrap.css" />
<link rel="stylesheet" type="text/css" href="/Themes/default/css/bootstrap-datetimepicker.css" />
<link rel="stylesheet" type="text/css" href="/Themes/default/css/font-awesome.min.css" />
<link href="/Scripts/jquery/bootstrap-notify/css/bootstrap-notify.css" rel="stylesheet" />
<link href="/Scripts/jquery/tablesorter/css/theme.bootstrap.css" rel="stylesheet" />
<link href="/Scripts/jquery/tablesorter/css/theme.blue.css" rel="stylesheet" />
<link href="/Scripts/jquery/bootstrap-multiselect/css/bootstrap-multiselect.css" rel="stylesheet" />
<link href="/Scripts/plugins/jquery.select2/jquery.select2.css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/Themes/default/styles.css" media="all" />


<%--here are compressed js files, it should work for qa/production env--%>
<asp:Literal runat="server" ID="uxCompressedJs" Visible="false">
    <%--<script src="/scripts/VKeCRM-all/jquery-plugin-all.js" type="text/javascript"></script>--%>
</asp:Literal>
<% if (JsCompress)
   {
%>
<script src="<%: ResolveClientUrl("~/bundles/base?v=") + JsVersion %>"></script>
<%
   }
   else
   {
%>
<%:System.Web.Optimization.Scripts.Render("~/bundles/base") %>
<%
   } %>

<!--[if lte IE 9]>
    <%:System.Web.Optimization.Scripts.Render("~/bundles/hack") %>
<![endif]-->

<%--here are regular js files, it should work for dev env--%>
<asp:Literal runat="server" ID="uxRegularJs" Visible="false">
    
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lte IE 9]>
        <script src="/scripts/framework/bootstrap/html5shiv.min.js"></script>
        <script src="/scripts/framework/bootstrap/respond.min.js"></script>
    <![endif]-->

    <!-- JQuery Helper -->
    <script type="text/javascript" src="/scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="/scripts/jquery.domready.js"></script>
    <script type="text/javascript" src="/scripts/jquery/jquery.json.js"></script>
    <script type="text/javascript" src="/scripts/jquery/jquery.validate.js"></script>
    <script type="text/javascript" src="/Scripts/framework/avalon.js"></script>

    <script src="/Scripts/framework/vkecrm.common.js" type="text/javascript"></script>
    <script src="../Scripts/entity/entity.paging.js"></script>

    <!-- EXT -->
    <script type="text/javascript" src="/Scripts/ext/ext.js"></script>
    <script type="text/javascript" src="/Scripts/ext/ext-more.js"></script>
    <script type="text/javascript" src="/Scripts/ext/date.js"></script>
    <script type="text/javascript" src="/Scripts/ext/format.js"></script>
    <script type="text/javascript" src="/Scripts/ext/menuTop.js"></script>

        <!-- Framework Include here -->
    <script src="/Scripts/framework/framework.common.js" type="text/javascript"></script>
    <script src="/Scripts/framework/framework.ui.js" type="text/javascript"></script>
    <script src="/Scripts/framework/JSLINQ.js" type="text/javascript"></script>
    <script src="/Scripts/pages/global.js" type="text/javascript"></script>

    <!-- Bootstrap -->
    <script type="text/javascript" src="/scripts/framework/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript" src="/scripts/framework/bootstrap/moment.min.js"></script>
    <script type="text/javascript" src="/scripts/framework/bootstrap/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="/scripts/framework/bootstrap/bootstrap3-typeahead.min.js"></script>
    <script type="text/javascript" src="/scripts/framework/bootstrap/bootstrap3-typeaheadComplete.js"></script>
    <!-- jQuery and Plugin -->
    <script src="/Scripts/jquery/ui.core.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/spinner/ui.spinner.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/slider/ui.slider.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/dialog/jquery.blockUI.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/logger/ui.logger.js" type="text/javascript"></script>
    <script src="/scripts/jquery/datepicker/date.js"  type="text/javascript"></script>                                  
    <script src="/scripts/jquery/datepicker/jquery.datepicker.js"  type="text/javascript"></script> 
    <script src="/scripts/jquery/pagination/jquery.pagination.js"  type="text/javascript"></script>  
    <script src="/Scripts/jquery/jquery.form.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/tablesorter/js/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Scripts/jquery-jtemplates.js"></script>
    <script src="/Scripts/jquery/sortTemplate.js"></script>
    <script src="/Scripts/jquery/bootstrap-multiselect/js/bootstrap-multiselect.js"></script>
    <script src="/Scripts/jquery/bootstrap-notify/js/bootstrap-notify.js"></script>
    <!-- Ajax Helper -->
    <script type="text/javascript" src="/Scripts/AjaxHelper.js"></script>

    <!-- Access Right Helper -->
    <script type="text/javascript" src="/Scripts/AccessRightHelper.js"></script>


    <script src="/Scripts/jquery/ajaxLoading/jquery.ajaxLoading.js"></script>
    <script src="/Scripts/framework/VKeCRM.events.js"></script>
    

    <script src="/Scripts/richText/ueditor.config.js"></script>
    <script src="/Scripts/richText/ueditor.all.min.js"></script>
    <script src="/Scripts/richText/ueditor.parse.min.js"></script>
    <script src="/Scripts/cleditor/jquery.cleditor.css"></script>
    <script src="/Scripts/cleditor/jquery.cleditor.min.js"></script>
</asp:Literal>