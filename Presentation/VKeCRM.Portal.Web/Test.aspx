<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="VKeCRM.Portal.Web.Test" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn
        {
            display: inline-block;
            width: auto !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        vkecrm.RegisterNameSpace("page");
        page.Test = {
            DoSave: function () {
                var MvcUrl = GetMvcUrl("Orders", "SaveOrder");
                var orderNo = "TestOrderNo_1";
                var orderName = "TestOrderName_1";

                makeAjaxCall({
                    url: MvcUrl,
                    data: { orderNo: orderNo, orderName: orderName },
                    success: function (result) {
                        alert("Save Successful!");
                    },
                    error: function (err) {
                    },
                    waitingElement: "divMain"

                });
            },
            DoSaveMasterDetail: function () {
                var MvcUrl = GetMvcUrl("Orders", "SaveMasterDetail");

                makeAjaxCall({
                    url: MvcUrl,
                    data: {},
                    success: function (result) {
                        alert("Save Successful!");
                    },
                    error: function (err) {
                    }

                });
            },
            GetAllOrder: function () {
                var MvcUrl = GetMvcUrl("Orders", "GetAllOrders");
                makeAjaxCall({
                    url: MvcUrl,
                    data: {},
                    success: function (result) {
                        //alert("Get Successful!");
                    },
                    error: function (err) {
                    },
                    waitingElement: "divMain"

                });
            },
            GetOrderById: function (id) {
                var MvcUrl = GetMvcUrl("Orders", "GetOrderById");
                makeAjaxCall({
                    url: MvcUrl,
                    data: { id: id },
                    success: function (result) {
                        alert("Get Successful!");
                    },
                    error: function (err) {
                    }

                });
            },
            CallAjaxTest: function (id) {
                var MvcUrl = GetMvcUrl("Security", "AuthenticateUserByPassword");
                makeAjaxCall({
                    url: MvcUrl,
                    data: { password: 'medidnno@123' },
                    success: function (result) {
                        alert("Ajax Successful");
                    },
                    error: function (err) {
                        alert("Ajax Failed");
                    }

                });
            },
            CallAjaxTest2: function (id) {
                var MvcUrl = GetMvcUrl("Security", "GetUserAccessRight");
                makeAjaxCall({
                    url: MvcUrl,
                    data: { AccessRightEntity: ['Study/IRBApplication/Checklist/Save', 'Study/IRBApplication/Checklist/Save', 'Dashboard'] },
                    success: function (result) {
                        alert("Ajax Successful");
                    },
                    error: function (err) {
                        alert("Ajax Failed");
                    }

                });
            },
            CallAjaxTest3: function (id) {
                var MvcUrl = GetMvcUrl("Security", "GetStudyAccessRight");
                makeAjaxCall({
                    url: MvcUrl,
                    data: { studyTeamMemberID: '11F3C242-C5B8-471A-B0A4-0423AB1B58B6', AccessRightEntity: ['Study/IRBApplication/Checklist/Save', 'Dashboard'] },
                    success: function (result) {
                        alert("Ajax Successful");
                    },
                    error: function (err) {
                        alert("Ajax Failed");
                    }

                });
            },
            CallAjaxTest4: function (id) {
                var MvcUrl = GetMvcUrl("Security", "AuthenticateUserByPassword");
                makeAjaxCall({
                    url: MvcUrl,
                    data: { password: 'medinno@123' },
                    success: function (result) {
                        if (result.DataSource)
                            alert("Correct Password");
                        else
                            alert("Wrong Password");
                    },
                    error: function (err) {
                        alert("Ajax Failed");
                    }

                });
            },
            UploadCallBack: function (file) {
                if (!file) return;
                console.log(file);
                var url = GetMvcUrl('File', 'GetFile');
                url += '?id=';
                url += file.FormAttachmentID;
                var a = $("<a href='" + url + "' target='_blank'>PDF</a>").get(0);

                var e = document.createEvent('MouseEvents');

                e.initEvent('click', true, true);
                a.dispatchEvent(e);
            }
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divMain">
        
        <button type="button" onclick="javascript: page.Test.GetAllOrder();" class="btn btn-block btn-primary btn-lg">Get Test</button>
        <button type="button" onclick="javascript: page.Test.DoSave();" class="btn btn-block btn-primary btn-lg">Save Test</button>
        <%--
        
        <button type="button" onclick="javascript: page.Test.GetOrderById(1);" class="btn btn-block btn-primary btn-lg">Get By SP Test</button>
        <button type="button" onclick="javascript: page.Test.CallAjaxTest2(999);" class="btn btn-block btn-primary btn-lg">SecurityTest</button>
        <button type="button" onclick="javascript: page.Test.CallAjaxTest3(999);" class="btn btn-block btn-primary btn-lg">SecurityTest</button>
        <button type="button" onclick="javascript: page.Test.CallAjaxTest4(999);" class="btn btn-block btn-primary btn-lg">SecurityTest</button>
        <button type="button" onclick="javascript: page.Test.DoSaveMasterDetail();" class="btn btn-block btn-primary btn-lg">Save MasterDetail</button>--%>
        
        <p>111</p>
        <p>11</p>
        <p>11</p>
        <p>1111</p>
    </div>

</asp:Content>
