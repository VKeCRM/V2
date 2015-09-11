//console.log



vkecrm.RegisterNameSpace("page");

String.prototype.TrimLeft = function (chars) {
    //debugger;
    var re = chars ? new RegExp("^[" + chars + "]+/", "g")
                   : new RegExp(/^\s+/);
    return this.replace(re, "");
}
String.prototype.TrimRight = function (chars) {
    var re = chars ? new RegExp("[" + chars + "]+$/", "g")
                   : new RegExp(/\s+$/);
    return this.replace(re, "");
}
String.prototype.Trim = function (chars) {
    return this.TrimLeft(chars).TrimRight(chars);
}
String.prototype.replaceAll = function (reallyDo, replaceWith, ignoreCase) {
    if (!RegExp.prototype.isPrototypeOf(reallyDo)) {
        return this.replace(new RegExp(reallyDo, (ignoreCase ? "gi" : "g")), replaceWith);
    } else {
        return this.replace(reallyDo, replaceWith);
    }
}

Date.prototype.Format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};

function isNumber(oNum) {
    if (!oNum) return false;
    var strP = /^\d+(\.\d+)?$/;
    if (!strP.test(oNum)) return false;
    try {
        if (parseFloat(oNum) != oNum) return false;
    }
    catch (ex) {
        return false;
    }
    return true;
}

page.global = {
    UserProfileID: null,
    UserGivenName: null,
    UserFamilyName: null,
    LoginAccountID: null,
    LoginID: null,
    Domain: null,
    skipMaintenanceCheck: false,

    LogOut: function () {
        window.location.href = "/LogOut.aspx";
    },

    ClearTopButton: function () {
        $('#divTop .controlbtn').remove();
    },

    AddTopButton: function (callFuncStr, iconClass, title) {
        var box = $('#divTop');
        var input = '<a href="javascript:void(0);" class="controlbtn" title="' + title + '" onclick="' + callFuncStr + '"><i class="fa ' + iconClass + ' fa-2x"></i>&nbsp;&nbsp;</a>';
        box.append(input);
    },

    GetQueryString: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    },

    FormatDateString: function (jsonDate, format) {
        format = format || 'dd-mmm-yyyy';
        var date = eval(jsonDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
        return date.format(format);
    },

    ShowDropDownList: function (id) {
        $("#" + id).find(".typeahead-combox").show();
    }
};