function GetMvcUrl(controller, method) {
    var mvcUrlFormat = "/Controllers/{0}/{1}/Api";

    return String.format(mvcUrlFormat, controller, method);
}


function makeAjaxCall(paramters) {
    var defaultOpts = {
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: "",
        data: {},
        dataType: 'text',
        waitingElement: null,
        success: null,
        error: null,
        before: null,
        complete: function () { return; },
        async: true,
        reqTick: new Date().getTime()
    };
    var setting = jQuery.extend({}, defaultOpts, paramters);

    return jQuery.ajax({
        type: setting.type,
        contentType: setting.contentType,
        url: setting.url,
        data: jQuery.toJSON(setting.data),
        dataType: setting.dataType,
        async: setting.async,
        dataFilter: function (data, type) {
            return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1').replace(/"\\\/(Date\([\d\+]+\))\\\/"/gi, "new $1");
        },
        beforeSend: function (data) {
            if (setting.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(setting.waitingElement, true);
            }

            if (setting.before != null) {
                setting.before();
            }
            //request tick
            setting.reqTick = new Date().getTime();
        },
        success: function (result, status, jqXhr) {
            if (setting.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(setting.waitingElement, false);
            }
            result = eval('(' + result + ')');

            result = result || {};
            result.reqTick = setting.reqTick;
            //result = jQuery.evalJSON(result);
            if (setting.success != null) {
                setting.success(result);
            }
        },
        error: function (result) {
            if (result.status == 401) {
                location.href = location.protocol + '//' + location.host + '/loginpage/login.aspx';
            }
            if (setting.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(setting.waitingElement, false);
            }

            if (setting.error != null) {
                setting.error(result);
            }
        },
        complete: setting.complete
    });
};

function makeAjaxCallAsync(paramters) {
    var defaultOpts = {
        type: "POST",
        contentType: "application/json;charset=utf-8",
        url: "",
        data: {},
        dataType: 'text',
        waitingElement: null,
        success: null,
        error: null,
        before: null,
        complete: function () { return; },
        async: true
    };
    var setting = jQuery.extend({}, defaultOpts, paramters);

    var deferred = Q.defer();

    function onprogress(evt) {
        deferred.notify(evt.loaded / evt.total);
    }

    var xhrProvider = function () {
        var xhr = jQuery.ajaxSettings.xhr();
        if (onprogress && xhr.upload) {
            xhr.upload.addEventListener('progress', onprogress, false);
        }
        return xhr;
    };

    jQuery.ajax({
        type: setting.type,
        contentType: setting.contentType,
        url: setting.url,
        xhr: xhrProvider,
        data: jQuery.toJSON(setting.data),
        dataType: setting.dataType,
        async: setting.async,
        dataFilter: function (data, type) {
            return data.replace(/"\\\/(Date\([0-9-]+\))\\\/"/gi, 'new $1').replace(/"\\\/(Date\([\d\+]+\))\\\/"/gi, "new $1");
        },
        beforeSend: function (data) {
            if (setting.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(setting.waitingElement, true);
            }

            if (setting.before != null) {
                setting.before();
            }
        },
        success: function (result) {
            if (setting.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(setting.waitingElement, false);
            }

            result = eval('(' + result + ')');
            deferred.resolve(result);
            //result = jQuery.evalJSON(result);
            if (setting.success != null) {
                setting.success(result);
            }

        },
        error: function (result) {
            deferred.reject(result);
            if (result.status == 401) {
                location.href = location.protocol + '//' + location.host + '/loginpage/login.aspx';
            }
            if (setting.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(setting.waitingElement, false);
            }

            if (setting.error != null) {
                setting.error(result);
            }
        },
        complete: setting.complete
    });

    return deferred.promise;
}