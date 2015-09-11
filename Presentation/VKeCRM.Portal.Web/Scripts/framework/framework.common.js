vkecrm.RegisterNameSpace("vkecrm.framework");
vkecrm.framework.common = {
    //TODO: we need to get ETTimeZoneOffset from server
    ETTimeZoneOffset: 4,
    oneTimeParam: {},

    formatDate: function (object, format) {
        if (object == null) {
            return '';
        }
        else {
            if (format == null || format == window.undefined) {
                format = "m/d/Y h:ia";
            }
            return Ext.util.Format.date(object, format);
        }
    },

    getDateByMsDate: function (msDate) {
        if (msDate == null) {
            return null;
        }
        //Update code, because of the Date.add method conflict with ajaxtoolkit DatePicker control
        var deltaMillonSecond = new Date(0) - new Date('12/30/1899 00:00:00 GMT');
        return new Date(msDate * 24 * 3600 * 1000 - deltaMillonSecond); //'MMM yyyy'
    },

    formatSerializedDate: function (msDate) {
        if (msDate == null) {
            return null;
        }
        //Update code, because of the Date.add method conflict with ajaxtoolkit DatePicker control
        var deltaMillonSecond = new Date(0) - new Date('12/30/1899 00:00:00 GMT');
        var sourceDate = new Date(msDate * 24 * 3600 * 1000 - deltaMillonSecond);
        return '\/Date(' + sourceDate.getTime() + ')\/';
    },

    getDaysFromInitial: function (msDateStr) {
        if (msDateStr == null) {
            return 0;
        }
        // get date
        var date0 = Date.parseDate(msDateStr, "M$");
        // var local = date0.getTime() - (date0.getTimezoneOffset() * 60000);
        var date1 = new Date('12/30/1899 00:00:00 GMT');
        var iDays = parseInt((date0 - date1) / (24 * 3600 * 1000));
        return iDays;
    },

    parseMSDate: function (msDateStr) {
        if (null == msDateStr || "" == msDateStr)
            return null;

        msDateStr = msDateStr.replace("/", "").replace("/", "");

        var matches = msDateStr.match(/^Date\((\-?\d+)([\+|-]\d+)?/);

        if (matches == null || matches.length < 2) {
            return null;
        }

        var newDateString = "new Date(" + matches[1] + ")";
        var finalDate = eval(newDateString);

        return finalDate;
    },

    parseDOBString: function (dobString) {
        if (null == dobString || "" == dobString)
            return null;

        var dob = dobString.split('/');

        if (dob == null || dob.length != 3) {
            return null;
        }

        return dob;
    },

    formatString: function (object) {
        return (object == null) ? "" : object.toString();
    },

    formatDecimal: function (object) {
        if (object == null) {
            return 0;
        }
        return object.toFixed(2);
    },

    formatMoney: function (object, precision) {

        if (object == null) {
            return '$0.00';
        }
        else if (object >= 0) {
            return "$" + vkecrm.framework.common.formatNumber(object, precision);
        }
        else {
            return "-$" + vkecrm.framework.common.formatNumber(Math.abs(object), precision);
        }

    },

    formatPrice: function (object, precision) {
        if (precision == null || precision == window.undefined) {
            precision = 4;
        }

        var objStr = Number(Number(object).toFixed(precision)).toString();
        var index = objStr.lastIndexOf('.');
        if (index > -1) {
            var len = objStr.length - index - 1;
            precision = precision > len ? (len <= 2 ? 2 : len) : precision;
        } else {
            precision = 2;
        }

        return vkecrm.framework.common.formatMoney(object, precision)
    },

    formatNumber: function (object, precision) {
        if (object == null) {
            return 0;
        }
        else {
            var fmt = '0,000';
            if (precision == null || precision == window.undefined) {
                precision = 2;
            }
            if (precision > 0) {
                fmt = '0,000.' + '00000'.substring(0, precision);
            } else {
                fmt = '0,000';
            }

            if (object < 0) {
                return "-" + Ext.util.Format.number(Math.abs(object), fmt);
            } else {
                return Ext.util.Format.number(object, fmt);
            }
        }
    },

    formatDecimalPercent: function (object) {
        if (object == null) {
            return 0.00;
        }
        else if (object > 0) {
            return "+" + Ext.util.Format.number(object, '0.00') + "%";
        }
        else {
            return Ext.util.Format.number(object, '0.00') + "%";
        }
    },

    formatChangeMoney: function (object) {
        if (object == null) {
            return '$0.00';
        }
        else if (object >= 0) {
            return "+$" + vkecrm.framework.common.formatNumber(object, 2);
        }
        else {
            return "-$" + vkecrm.framework.common.formatNumber(Math.abs(object), 2);
        }
    },

    formatChangePercent: function (object) {
        if (object == null) {
            return 0.00;
        }

        return Ext.util.Format.number(Math.abs(object), '0.00') + "%";
    },

    formatUIDate: function (object, format) {
        if (object == null) {
            return '';
        }
        else {
            if (format == null || format == window.undefined) {
                format = "m/d/Y g:ia";
            }
            return Ext.util.Format.date(object, format);
        }
    },

    formatDateByOffSet: function (utcDate, offset, format) {
        if (utcDate == null || utcDate == window.undefined) {
            return '';
        }
        offset = offset || 0;
        var timeForDisplay = new Date(utcDate.getTime() - (3600000 * offset) + (utcDate.getTimezoneOffset() * 60000));
        return vkecrm.framework.common.formatUIDate(timeForDisplay, format);
    },

    formatEstDate: function (utcDate, format) {
        var offset = vkecrm.framework.common.ETTimeZoneOffset;
        var dateStr = utcDate.format("yyyy/MM/dd hh:mm:ss");
        dateStr += " GMT+0000";
        return vkecrm.framework.common.formatDateByOffSet(new Date(dateStr), offset, format);
    },

    sortJson: function (jsonObjects, sortBy, direction) {

        if (jsonObjects == null) {
            return;
        }

        var intDirection = "asc" == direction ? 1 : -1;
        var fn = function (r1, r2) {
            var v1, v2;
            var tmpArr = sortBy.split(".");
            v1 = r1[tmpArr[0]];
            v2 = r2[tmpArr[0]];
            for (i = 1; i < tmpArr.length; i++) {
                v1 = v1[tmpArr[i]];
                v2 = v2[tmpArr[i]];
            }
            var result = (v1 > v2) ? 1 : ((v1 < v2) ? -1 : 0);
            return intDirection * result;
        };

        jsonObjects.sort(fn);
    },

    getProperty: function (obj, property) {
        if (property != null && property != window.undefined && property != '') {
            return obj[property];
        } else {
            return obj;
        }
    },

    /**
	* common function for getting parameter value from query string
	* @method getQueryStringParameter
	* @param name : parameter name
	*/
    getQueryStringParameter: function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return results[1];
    },

    getItemFromCollection: function (fieldName, fieldValue, collection) {
        if (collection == null) {
            return null;
        }
        for (var i = 0; i < collection.length; i++) {
            if (fieldName == "") {
                if (collection[i] == fieldValue) {
                    return collection[i];
                }
            }
            else {
                if (collection[i][fieldName] == fieldValue) {
                    return collection[i];
                }
            }
        }
        return null;
    },

    getItemIndexFromCollection: function (fieldName, fieldValue, collection) {
        if (collection == null) {
            return -1;
        }
        for (var i = 0; i < collection.length; i++) {
            if (fieldName == "") {
                if (collection[i] == fieldValue) {
                    return i;
                }
            }
            else {
                if (collection[i][fieldName] == fieldValue) {
                    return i;
                }
            }
        }
        return -1;
    },

    clone: function (sourceObject) {
        if (typeof (sourceObject) != 'object') return sourceObject;
        if (sourceObject == null) return sourceObject;
        var NewObj = new Object();
        for (var i in sourceObject) NewObj[i] = vkecrm.framework.common.clone(sourceObject[i]);
        return NewObj;

    },

    getUTCDate: function (strDate) {
        if (strDate == null || strDate == "")
            return null;
        var date = new Date(strDate.replace("T", " "));
        if (isNaN(date))
            return null;
        return date;
    },

    getESTDate: function (utcDate) {
        // create Date object for current location
        // var localDate = new Date();

        // convert to msec / add local time zone offset / get UTC time in msec
        // var utc = localDate.getTime() + (localDate.getTimezoneOffset() * 60000);

        var utc = utcDate.getTime(); // + (utcDate.getTimezoneOffset() * 60000);
        // create new Date object with supplied offset (EST Offset)
        var offset = vkecrm.framework.common.ETTimeZoneOffset;
        var estTime = new Date(utc - (3600000 * offset));
        return estTime;
    },

    getESTDateFromLocal: function (localDate) {
        // create Date object for current location
        // var localDate = new Date();

        // convert to msec / add local time zone offset / get UTC time in msec
        var utc = localDate.getTime() + (localDate.getTimezoneOffset() * 60000);

        // create new Date object with supplied offset (EST Offset)
        var offset = vkecrm.framework.common.ETTimeZoneOffset;
        var estTime = new Date(utc - (3600000 * offset));
        return estTime;
    },

    /**
	* common function to bind the drop down list
	* @method bindDropDownList
	* @param {args} arguments object {dataSource: data, valueField: field name, textFiled: field name, selectValue: the initial value}
	*/
    bindDropDownList: function (dataSource, valueField, textFiled, selectValue) {
        if (dataSource == null) {
            return "";
        }
        else {
            return vkecrm.framework.ui.renderDropDownList({ DataSource: dataSource, ValueField: valueField, TextField: textFiled, SelectValue: selectValue });
        }
    },

    /**
	* common function to bind the radio button list
	* @method bindRadioButtonList
	* @param {args} arguments object {dataSource: data,nameField: property name, valueField: field name, textFiled: field name, selectValue: the initial value,TabIndex:tabIndex}
	*/
    bindRadioButtonList: function (dataSource, nameField, valueField, textFiled, selectValue, tabIndex) {
        if (dataSource == null) {
            return "";
        }
        else {
            return vkecrm.framework.ui.renderRadioButtonList({ DataSource: dataSource, NameField: nameField, ValueField: valueField, TextField: textFiled, SelectValue: selectValue, TabIndex: tabIndex });
        }
    },

    /**
	* common function to bind the radio button list
	* @method bindRadioButtonList
	* @param {args} arguments object {dataSource: data,nameField: property name, valueField: field name, textFiled: field name, selectValue: the initial value,TabIndex:tabIndex}
	*/
    bindCheckBoxList: function (dataSource, nameField, valueField, textFiled, selectValue, tabIndex) {
        if (dataSource == null) {
            return "";
        }
        else {
            return vkecrm.framework.ui.renderCheckBoxList({ DataSource: dataSource, NameField: nameField, ValueField: valueField, TextField: textFiled, SelectValue: selectValue, TabIndex: tabIndex });
        }
    },



    /**
	* set number style, red or green
	* @method getNumberCss
	*/
    getNumberCss: function (change) {
        if (change >= 0) {
            return "quote-up";
        } else {
            return "quote-down";
        }
    },

    redirectStep: function (step) {
        window.scrollTo(0, 0);
        window.location.hash = "#" + step.DisplayName;
    },

    formatDOBString: function (year, month, day) {
        if (!year || !month || !day) {
            return null;
        }

        return year + '/' + month + '/' + day;
    },

    date2JsonString: function (year, month, day) {
        if (!year || !month || !day)
            return null;

        var iMonth = (month + '').length == 1 ? ('0' + month) : month;
        var iDay = (day + '').length == 1 ? ('0' + day) : day;

        var date = iMonth + '/' + iDay + '/' + year;

        if (isValidDate(date, 'MM/dd/yyyy', {}))
            //return '\/Date(' + new Date(0).setFullYear(year, month - 1, day) + ')\/';
            return '\/Date(' + Date.UTC(year, month - 1, day) + ')\/';
        else
            return null;
    },

    validateDateOfBirth: function (sender, e) {
        var date = e.Value;
        var onlyValidateDateFormat = e.OnlyValidateDateFormat;

        var invalidDate = "Please enter a valid date of birth.";
        var lessThan18years = "You must be 18 years old to open an account.";

        if (null == date || "" == date) {
            e.ErrorMessage = invalidDate;
            return false;
        }
        else {
            try {
                var birth = this.parseMSDate(date);
                var dateFormat = 'MM/dd/yyyy';
                var birth2String = birth.format(dateFormat);
                if (!isValidDate(birth2String, dateFormat, {})) {
                    e.ErrorMessage = invalidDate;
                    return false;
                }
                else {
                    if (onlyValidateDateFormat == true)
                        return true;
                    else {
                        // validate 18 old.
                        var newYear = birth.getFullYear() + 18;
                        var nBirth = new Date(0).setFullYear(newYear, birth.getMonth(), birth.getDate());
                        // TODO: need to get datetime.now from server!
                        if (new Date() > nBirth)
                            return true;
                        else {
                            e.ErrorMessage = lessThan18years;
                            return false;
                        }
                    }
                }
            }
            catch (ex) {
                e.ErrorMessage = invalidDate;
                return false;
            }
        }
    },

    hasFlash: function () {
        return vkecrm.framework.common.flashVersion() != '0,0,0';
    },

    flashVersion: function () {
        // ie
        try {
            try {
                // avoid fp6 minor version lookup issues
                // see: http://blog.deconcept.com/2006/01/11/getvariable-setvariable-crash-internet-explorer-flash-6/
                var axo = new ActiveXObject('ShockwaveFlash.ShockwaveFlash.6');
                try { axo.AllowScriptAccess = 'always'; }
                catch (e) { return '6,0,0'; }
            } catch (e) { }
            return new ActiveXObject('ShockwaveFlash.ShockwaveFlash').GetVariable('$version').replace(/\D+/g, ',').match(/^,?(.+),?$/)[1];
            // other browsers
        } catch (e) {
            try {
                if (navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin) {
                    return (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]).description.replace(/\D+/g, ",").match(/^,?(.+),?$/)[1];
                }
            } catch (e) { }
        }
        return '0,0,0';
    },
    yearDashed: function (target) {
        $.each(target, function (index, value) {
            //if ($(this).val() != "1900" && $(this).val() != "-1" && (new RegExp("[0-9]{3}[0]$").test($(this).val()))) {
            //$(this).css("border-bottom", "dashed #333 1px");
            //}
            if ($(this).val() == "-2") {
                $(this).attr("disabled", "disabled");
            }
        });
    },
    maskSSN: function (controlId) {
        $("#" + controlId).mask("999-99-9999");
    },

    dataMasker: function (str, len1, len2, len3) {
        str = str ? str.trim() : '';
        var arr = new Array();
        for (var i = 0; i < str.length; i++) {
            arr.push(str.charAt(i));

            if ((i == len1 - 1 || i == len1 + len2 - 1) && i != str.length - 1)
                arr.push('-');
        }
        var dash = 2;
        if (!len3) {
            dash = 1;
        }
        var len = len1 + len2 + len3;
        for (var i = arr.length; i < len + dash; i++) {
            if (i == len1 || i == len1 + len2 + 1)
                arr.push('-');
            else
                arr.push('_');
        }

        return arr.join('');
    },
    unmaskingForSubmitData: function (str) {
        return str.replace(/-/g, "").replace(/_/g, "").trim();
    },
    showInlineMessageTextImage: function (id, errorMessage, imageUrl, ctrlBorderColor, messageClass) {
        imageUrl = typeof (imageUrl) == "undefined" ? "/Themes/Common/assets/icon_exclamation_sm.gif" : imageUrl;
        ctrlBorderColor = typeof (ctrlBorderColor) == "undefined" ? "#e94592" : ctrlBorderColor;
        messageClass = typeof (messageClass) == "undefined" ? "inline-error" : messageClass;
        var targetCtrl = document.getElementById(id);
        if (targetCtrl && targetCtrl.parentNode) {
            // check if already append error messages
            var spans = targetCtrl.parentNode.getElementsByTagName("div");
            if (spans.length > 0) {
                for (var i = 0; i < spans.length; i++) {
                    if (spans[i].getAttribute("type") == "inline") {
                        return;
                    }
                }
            }

            var images = targetCtrl.parentNode.getElementsByTagName("img");
            if (images.length > 0) {
                for (var i = 0; i < images.length; i++) {
                    if (images[i].getAttribute("type") == "inline") {
                        return;
                    }
                }
            }

            // set target control style
            targetCtrl.style.borderColor = ctrlBorderColor;

            // set error message show
            // 1. add error img
            var errorImg;
            var errorSpan;

            errorImg = document.createElement("img");
            errorImg.setAttribute("type", "inline");
            //errorImg.width=18;
            errorImg.src = imageUrl;
            errorImg.style.border = "none";
            errorImg.style.position = "relative";
            errorImg.style.top = "3px";
            targetCtrl.parentNode.appendChild(errorImg);

            // 2. add error message.
            errorSpan = document.createElement('span');
            errorSpan.setAttribute("type", "inline");
            errorSpan.style.paddingLeft = "5px";
            errorSpan.className = messageClass;
            errorSpan.innerHTML = errorMessage;

            targetCtrl.parentNode.appendChild(errorSpan);
        }
    },

    restore: function (e) {
        if (e.options[e.selectedIndex].disabled) {
            e.selectedIndex = window.select_current[e.id];
        }
    },

    emulate: function (e) {
        for (var i = 0, option; option = e.options[i]; i++) {
            if (option.disabled) {
                option.style.color = "graytext";
            }
            else {
                option.style.color = "menutext";
            }
        }
    },

    // set option disabled in IE7 and IE8
    selectOptionDisabledEmulate: function () {
        if (document.getElementById) {
            var s = document.getElementsByTagName("select");

            if (s.length > 0) {
                window.select_current = new Array();

                for (var i = 0, select; select = s[i]; i++) {
                    select.onfocus = function () { window.select_current[this.id] = this.selectedIndex; }
                    select.onchange = function () { vkecrm.framework.common.restore(this); }
                    vkecrm.framework.common.emulate(select);
                }
            }
        }
    },

    hasSpaces: function (val) {
        var patter = /\s{2,}/;
        return patter.test(val);
    },

    EncodeHtml: function (value) {
        if (value) {
            return jQuery('<div />').text(value).html();
        } else {
            return '';
        }
    },

    DecodeHtml: function (value) {
        if (value) {
            return $('<div />').html(value).text();
        } else {
            return '';
        }
    },

    OneTimeRunner: function (n, delay, fn) {
        var $this = vkecrm.framework.common;
        if ($this.oneTimeParam[n]) {
            window.clearTimeout($this.oneTimeParam[n]);
        }
        $this.oneTimeParam[n] = window.setTimeout(function () {
            fn();
            $this.oneTimeParam[n] = undefined;
        }, delay);
    }
};
$.fn.appendToWithIndex=function(to,index){
    if(! to instanceof jQuery){
        to=$(to);
    };
    if(index===0) {
        $(this).prependTo(to);
    }else{
        $(this).insertAfter(to.children().eq(index-1));
    }
};