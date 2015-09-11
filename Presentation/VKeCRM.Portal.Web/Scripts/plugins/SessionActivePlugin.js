(function () {
    var CookieHelper = {
        setCookie: function (name, value, seconds) {
            var d = new Date();
            d.setTime(d.getTime() + (seconds * 1000));
            var expires = "expires=" + d.toGMTString();
            document.cookie = name + "=" + value + ";" + expires + "; path=/;";
        },

        getCookie: function (cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ')
                    c = c.substring(1);
                if (c.indexOf(name) != -1)
                    return c.substring(name.length, c.length);
            }
            return "";
        }
    }

    var StartTimer = function () {
        CookieHelper.setCookie('ActiveSession', '1', 30);
        var t = setTimeout(function () { StartTimer(); }, 2000);
    }

    if (CookieHelper.getCookie('ActiveSession')) {
        StartTimer();
    }
})();


