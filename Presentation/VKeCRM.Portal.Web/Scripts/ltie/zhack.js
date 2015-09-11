$(document).ready(function () {
    var onetime;
    $('[placeholder]').addClass('fake-placeholder');
    function oneTime(time, cb) {
        return window.setTimeout(cb, time);
    }

    displayPlaceHolder();

    $(document).ajaxComplete(function () {
        $('[placeholder]').addClass('fake-placeholder');
        if (onetime) {
            window.clearTimeout(onetime);
        }
        onetime = oneTime(300, function () {
            displayPlaceHolder();
        });
    });

    $(document).click(function () {
        if (onetime) {
            window.clearTimeout(onetime);
        }
        onetime = oneTime(300, function () {
            displayPlaceHolder();
            hiddenPlaceholder();
        });

    });

    function hiddenPlaceholder() {
        $('fake-placeholder-r').each(function (i, d) {
            var t = $(d);
            var p = t.parent().find('.wrap-placeholder');
            if (t.val() || t.is(':hidden')) {
                p.css('display', 'none');
            } else {
                p.css('display', 'block');
            }
        });
    };

    function displayPlaceHolder() {
        if ($('.fake-placeholder:visible').length == 0) return;
        if (page && page.applicationform && page.applicationform.cirbform) {
            if (page.applicationform.cirbform.DataStatus == 8) {
                return;
            }
        }
        $('.fake-placeholder:visible').each(function (i, d) {
            $(d).removeClass('fake-placeholder');
            placeHolder(d);
        });
    }

    function placeHolder(obj) {
        if (!obj.getAttribute('placeholder')) return;
        var hasContent = obj.nodeName.toLowerCase() == 'textarea' ? obj.innerText : obj.getAttribute('value');
        var supportPlaceholder = 'placeholder' in document.createElement('input');
        if (!supportPlaceholder) {
            var defaultValue = obj.getAttribute('placeholder');
            var placeHolderCont = document.createTextNode(defaultValue);
            var oWrapper = document.createElement('span');
            oWrapper.style.cssText = 'position:absolute; color:#ACA899; display:inline-block; overflow:hidden;';
            oWrapper.className = 'wrap-placeholder';
            oWrapper.style.fontFamily = getStyle(obj, 'fontFamily');
            oWrapper.style.fontSize = getStyle(obj, 'fontSize');
            oWrapper.style.marginLeft = parseInt(getStyle(obj, 'marginLeft')) ? parseInt(getStyle(obj, 'marginLeft')) + 3 + 'px' : 3 + 'px';
            oWrapper.style.marginTop = parseInt(getStyle(obj, 'marginTop')) ? getStyle(obj, 'marginTop') : 1 + 'px';
            oWrapper.style.paddingLeft = getStyle(obj, 'paddingLeft');
            oWrapper.style.width = obj.offsetWidth - parseInt(getStyle(obj, 'marginLeft')) + 'px';
            oWrapper.style.height = obj.offsetHeight + 'px';
            oWrapper.style.lineHeight = obj.nodeName.toLowerCase() == 'textarea' ? '' : obj.offsetHeight + 'px';
            if (hasContent) {
                oWrapper.style.display = 'none';
            }
            oWrapper.appendChild(placeHolderCont);
            $(obj).addClass('fake-placeholder-r');
            obj.parentNode.insertBefore(oWrapper, obj);
            obj.parentNode.style.position = 'relative';
            oWrapper.onclick = function () {
                obj.focus();
            };
            if (typeof (obj.oninput) == 'object') {
                obj.addEventListener("keyup", changeHandler, false);
                obj.addEventListener("change", changeHandler, false);
            } else {
                obj.onpropertychange = changeHandler;
            }
            function changeHandler() {
                oWrapper.style.display = obj.value != '' ? 'none' : 'inline-block';
            }
            function getStyle(obj, styleName) {
                var oStyle = null;
                if (obj.currentStyle) oStyle = obj.currentStyle[styleName];
                else if (window.getComputedStyle) oStyle = window.getComputedStyle(obj, null)[styleName];
                return oStyle;
            }
        }
    }
});

