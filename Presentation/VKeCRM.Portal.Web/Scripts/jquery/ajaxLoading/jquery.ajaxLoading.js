/// <reference path="../../_references.js" />
vkecrm.RegisterNameSpace('plugin.AjaxLoading');

function AjaxPage(p) {
    var self = this;
    self.key = p.key;
    self.Html = p.Html;
    self.JsName = p.JsName;
    self.LoadMethods = p.LoadMethods || [];
    self.ExitMethods = p.ExitMethods || [];
    self.MustRefresh = p.MustRefresh || false;
    self.InitFunctions = null;
    self.ExitFunctions = null;

    self.ExcuteInit = function () {
        if (!self.InitFunctions) {
            self.CreateMethod(self.LoadMethods, self.JsName, true);
        }
        var l = self.InitFunctions.length;
        for (var i = 0; i < l; i++) {
            plugin.AjaxLoading.excute(self.InitFunctions[i]);
        }

        if (plugin.AjaxLoading.loadComplete) {
            plugin.AjaxLoading.loadComplete(self.key);
        }
        plugin.AjaxLoading.ExcuteChangePageHandler();
    };

    self.ExcuteExit = function () {
        if (!self.ExitFunctions) {
            self.CreateMethod(self.ExitMethods, self.JsName, false);
        }
        var l = self.ExitFunctions.length;
        for (var i = 0; i < l; i++) {
            plugin.AjaxLoading.excute(self.ExitFunctions[i]);
        }
    };

    self.CreateMethod = function (ms, jn, isInit) {
        var methodStrs = [];
        var l = ms.length;
        for (var i = 0; i < l; i++) {
            if (jn && eval(jn)) {
                methodStrs.push(jn + '.' + ms[i]);
            } else if (ms[i] && eval(ms[i])) {
                methodStrs.push(ms[i]);
            }
        }
        if (isInit) {
            self.InitFunctions = [];
            for (var j = 0; j < l; j++) {
                var f = self.StrToFunc(methodStrs[j]);
                if (f) {
                    self.InitFunctions.push(f);
                }
            }
        } else {
            self.ExitFunctions = [];
            for (var k = 0; k < l; k++) {
                var m = self.StrToFunc(methodStrs[k]);
                if (m) {
                    self.ExitFunctions.push(m);
                }
            }
        }
    };

    self.StrToFunc = function (str) {
        try {
            if (str) {
                var func = eval(str);
                if (func && $.isFunction(func)) {
                    return func;
                }
            }
        } catch (e) {
            return null;
        } finally {
            if (plugin.AjaxLoading.waitingElement != null) {
                vkecrm.framework.ui.showWaiting(plugin.AjaxLoading.waitingElement, false);
            }
        }
        return null;
    };

    self.Js = function () {
        return eval(self.JsName);
    };
};

plugin.AjaxLoading = {
    prevKey: null,
    currentPage: null,
    pages: [],
    contentHolder: null,
    loadComplete: null,
    cannotLeave: false,
    whenChangePage: [],


    loadPage: function (key, info, h, cb) {
        var $this = plugin.AjaxLoading;


        if ($this.currentPage != null) {
            $this.prevKey = $this.currentPage.key;
            if (!$this.currentPage.MustRefresh) {

                if (!$this.findPage($this.prevKey)) {
                    $this.pages.push($this.currentPage);
                }
            }
            if ($this.currentPage.key == key && !$this.currentPage.MustRefresh) {
                $this.currentPage.ExcuteExit();
                if (h) {
                    h.append($this.contentHolder.html());
                    $this.currentPage.ExcuteInit();
                    if (cb) {
                        cb(h);
                    }
                }
                return;
            }
            $this.currentPage.ExcuteExit();
        }

        if ($this.cannotLeave) {
            $this.cannotLeave = false;
            return;
        }

        if (info) {
            $this.currentPage = new AjaxPage(info);
        } else {
            $this.currentPage = $this.findPage(key);
        }
        if (!$this.currentPage.key)
            $this.currentPage.key = key;
        if (h) {
            h.append($this.currentPage.Html);
            $this.currentPage.ExcuteInit();

        } else {
            $this.contentHolder.html($this.currentPage.Html);
            $this.contentHolder.animate({ scrollTop: '0px' }, 800);
            $this.currentPage.ExcuteInit();
            plugin.AjaxLoading.contentHolder.removeClass('hidden');
        }
        if (cb) {
            cb(h);
        }
    },

    findPage: function (key) {
        var l = plugin.AjaxLoading.pages.length;
        for (var i = 0; i < l; i++) {
            if (plugin.AjaxLoading.pages[i].key == key) {
                return plugin.AjaxLoading.pages[i];
            }
        }
        return null;
    },

    excute: function (f) {
        try {
            if ($.isFunction(f)) {
                f();
            }
        } catch (e) {

        }

    },

    Back: function () {
        var $this = plugin.AjaxLoading;

        if ($this.prevKey) {
            $this.Goto($this.prevKey);
        }
    },

    Goto: function (key, h, cb) {
        var ucPage = plugin.AjaxLoading.findPage(key);
        if (ucPage && !ucPage.MustRefresh) {
            plugin.AjaxLoading.loadPage(ucPage.key, undefined, h, cb);
            return false;
        }

        var studyId = page.global.GetQueryString('studyid');

        var data = { identity: key, param: studyId };

        $.ajax({
            url: '/RenderControl.ashx',
            data: data,
            type: 'get',
            dataType: 'json',
            beforeSend: function (data) {
                if (plugin.AjaxLoading.waitingElement != null) {
                    vkecrm.framework.ui.showWaiting(plugin.AjaxLoading.waitingElement, true);
                }
            },
            success: function (result) {
                if (!result) {
                    return;
                }

                if (plugin.AjaxLoading.waitingElement != null) {
                    vkecrm.framework.ui.showWaiting(plugin.AjaxLoading.waitingElement, false);
                }

                plugin.AjaxLoading.loadPage(key, result, h, cb);
            },
            error: function (xhr, t, e) {
                if (xhr.statusCode == 404) {
                    plugin.AjaxLoading.contentHolder.html(e);
                }
                if (plugin.AjaxLoading.waitingElement != null) {
                    vkecrm.framework.ui.showWaiting(plugin.AjaxLoading.waitingElement, false);
                }
            },
            complete: function () {
                if (plugin.AjaxLoading.waitingElement != null) {
                    vkecrm.framework.ui.showWaiting(plugin.AjaxLoading.waitingElement, false);
                }
                plugin.AjaxLoading.contentHolder.removeClass('hidden');
            }
        });
    },

    Click: function (el) {
        var a = $(el);

        var key = a.attr('key');

        plugin.AjaxLoading.Goto(key);

        return false;
    },

    AddChangePageHanlder: function (fn) {
        var $this = plugin.AjaxLoading;
        //if (!$this.whenChangePage.contains(fn)) {
        $this.whenChangePage.push(fn);
        //}
    },

    ExcuteChangePageHandler: function () {
        var $this = plugin.AjaxLoading;

        var l = $this.whenChangePage.length;
        for (var i = 0; i < l; i++) {
            $this.whenChangePage[i]($this.currentPage.key);
        }
    }
};

(function ($) {

    $.fn.AjaxLoading = function (options) {

        var $this = $(this);

        var $box = $('#' + options.target);
        plugin.AjaxLoading.contentHolder = $box;
        plugin.AjaxLoading.waitingElement = options.waitingElement;

        $this.attr('onclick', 'plugin.AjaxLoading.Click(this)');
        $this.attr('href', 'javascript:void(0)');
    };
})(jQuery);