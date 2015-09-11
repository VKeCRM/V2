/* =============================================================
 * bootstrap3-typeahead.js v3.0.3
 * https://github.com/bassjobsen/Bootstrap-3-Typeahead
 * =============================================================
 * Original written by @mdo and @fat
 * =============================================================
 * Copyright 2014 Bass Jobsen @bassjobsen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ============================================================ */


!function ($) {

    "use strict";
    // jshint laxcomma: true


    /* TYPEAHEAD PUBLIC CLASS DEFINITION
     * ================================= */

    var Typeahead = function (element, options, spanid, id) {
        this.$span = $(spanid);
        this.Id = id;
        this.$element = $(element);
        this.options = $.extend({}, $.fn.typeahead2.defaults, options);
        this.matcher = this.options.matcher || this.matcher;
        this.sorter = this.options.sorter || this.sorter;
        this.select = this.options.select || this.select;
        this.select2 = this.options.select2 || this.select2;
        this.autoSelect = typeof this.options.autoSelect == 'boolean' ? this.options.autoSelect : true;
        this.highlighter = this.options.highlighter || this.highlighter;
        this.updater = this.options.updater || this.updater;
        this.source = this.options.source;
        this.delay = typeof this.options.delay == 'number' ? this.options.delay : 250;
        this.$menu = $(this.options.menu);
        this.$menu2 = $(this.options.menu2);
        this.shown = false;
        this.shown2 = false;
        this.shown3 = false;
        this.listen();
        this.showHintOnFocus = typeof this.options.showHintOnFocus == 'boolean' ? this.options.showHintOnFocus : false;
    };

    Typeahead.prototype = {

        constructor: Typeahead

    , select: function () {
        var val = this.$menu.find('.active').data('value');
        if (this.autoSelect || val) {
            this.$element
              .val(this.updater(val))
              .change();
        }
        if (this.options._select != null) {
            this.options._select(val);
        }
        return this.hide();
    }
    , select2: function () {

        var val2 = this.$menu2.find('.active').data('value');
        if (this.autoSelect || val2) {
            this.$element
              .val(this.updater(val2))
              .change();
        }

        return this.hide();
    }
    , updater: function (item) {
        return item;
    }

    , setSource: function (source) {
        this.source = source;
    }

    , show: function () {
        var pos = $.extend({}, this.$element.position(), {
            height: this.$element[0].offsetHeight
        }), scrollHeight;

        scrollHeight = typeof this.options.scrollHeight == 'function' ?
            this.options.scrollHeight.call() :
            this.options.scrollHeight;

        this.$menu
          .insertAfter(this.$element)
          .css({
              top: pos.top + pos.height + scrollHeight
          , left: pos.left
          })
          .show();

        this.shown = true;
        return this;
    }
    , show2: function () {
        var pos = $.extend({}, this.$element.position(), {
            height: this.$element[0].offsetHeight
        }), scrollHeight;

        scrollHeight = typeof this.options.scrollHeight == 'function' ?
            this.options.scrollHeight.call() :
            this.options.scrollHeight;

        var menu2 = this.$menu2
            .insertAfter(this.$element);
        menu2.show2();

        this.shown2 = true;
        return this;
    }

    , hide: function () {
        this.$menu.hide();
        // this.$menu2.hide();
        this.shown = false;
        // this.shown2 = false;
        return this;
    }

    , lookup: function (query) {
        var items;
        if (typeof (query) != 'undefined' && query !== null) {
            this.query = query;
        } else {
            this.query = this.$element.val() || '';
        }

        if (this.query.length < this.options.minLength) {
            return this.shown || this.shown2 ? this.hide() : this;
        }

        var worker = $.proxy(function () {
            items = $.isFunction(this.source) ? this.source(this.query, $.proxy(this.process, this)) : this.source;
            if (items) {
                this.process(items);
            }
        }, this)

        clearTimeout(this.lookupWorker)
        this.lookupWorker = setTimeout(worker, this.delay)
    }

    , process: function (items) {
        var that = this;

        items = $.grep(items, function (item) {
            return that.matcher(item);
        });

        items = this.sorter(items);

        if (!items.length) {
            return this.shown || this.shown2 ? this.hide() : this;
        }

        if (this.options.items == 'all') {
            return this.render(items).show();
        } else {
            return this.render(items.slice(0, this.options.items)).show();
        }
    }

    , matcher: function (item) {
        return ~item.toLowerCase().indexOf(this.query.toLowerCase());
    }

    , sorter: function (items) {
        var beginswith = []
          , caseSensitive = []
          , caseInsensitive = []
          , item;

        while ((item = items.shift())) {
            if (!item.toLowerCase().indexOf(this.query.toLowerCase())) beginswith.push(item);
            else if (~item.indexOf(this.query)) caseSensitive.push(item);
            else caseInsensitive.push(item);
        }

        return beginswith.concat(caseSensitive, caseInsensitive);
    }

    , highlighter: function (item) {
        var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');
        return item.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
            return '<strong>' + match + '</strong>';
        });
    }

    , render: function (items) {
        var that = this;

        items = $(items).map(function (i, item) {
            i = $(that.options.item).data('value', item);
            i.find('a').html(that.highlighter(item));
            return i[0];
        });

        if (this.autoSelect) {
            items.first().addClass('active');
        }
        this.$menu.html(items);

        return this;
    }

    , render2: function () {
        var that = this;
        var items2 = [];
        items2 = $(that.options.source).map(function (i, item) {
            i = $(that.options.item).data('value', item);
            i.find('a').html(item);
            return i[0];
        });
        this.$menu2.html(items2);

        return this;
    }

    , next: function (event) {
        var active = this.$menu.find('.active').removeClass('active')
          , next = active.next();

        if (!next.length) {
            next = $(this.$menu.find('li')[0]);
        }

        next.addClass('active');

        var active2 = this.$menu2.find('.active').removeClass('active')
          , next2 = active2.next();

        if (!next2.length) {
            next2 = $(this.$menu2.find('li')[0]);
        }

        next2.addClass('active');
    }

    , prev: function (event) {
        var active = this.$menu.find('.active').removeClass('active')
          , prev = active.prev();

        if (!prev.length) {
            prev = this.$menu.find('li').last();
        }

        prev.addClass('active');

        var active2 = this.$menu2.find('.active').removeClass('active')
          , prev2 = active2.prev();

        if (!prev2.length) {
            prev2 = this.$menu2.find('li').last();
        }

        prev2.addClass('active');
    }
    , ScrollDocumentClick: function (e) {
        var that = e.currentTarget;
    }
    , listen: function () {
        // $("#_spanId").on('click', $.proxy(this.spanClick, this));
        $("#" + this.Id).on('mousedown', $.proxy(this.ScrollClick, this));
       // $("#" + this.Id).parents("body").on('mousedown', $.proxy(this.ScrollDocumentClick, this));
        this.$span.on('click', $.proxy(this.spanClick, this));
        this.$element
          .on('focus', $.proxy(this.focus, this))
          .on('blur', $.proxy(this.blur, this))
          .on('keypress', $.proxy(this.keypress, this))
          .on('keyup', $.proxy(this.keyup, this));

        if (this.eventSupported('keydown')) {
            this.$element.on('keydown', $.proxy(this.keydown, this));
        }

        this.$menu
          .on('click', $.proxy(this.click, this))
          .on('mouseenter', 'li', $.proxy(this.mouseenter, this))

          .on('mouseleave', 'li', $.proxy(this.mouseleave, this));
        this.$menu2
         .on('click', $.proxy(this.click2, this))
         .on('mouseenter', 'li', $.proxy(this.mouseenter, this))
         .on('mouseleave', 'ul', $.proxy(this.mouseleave2, this));
    }
        , ScrollClick: function (e) {
          //  e.preventDefault();
            this.shown4 = true;
           // this.$element.focus();
            //    alert("me");
            //var menuElement = this.$element[0];
            //this.$element.parents("body").one("mousedown", function (event) {
            //    var that = event;
            //    var ss = menuElement;
            //    var result = $.contains(menuElement, event.target)
            //    event.target;
            //});
        }

    , destroy: function () {
        this.$element.data('typeahead', null);
        this.$element
          .off('focus')
          .off('blur')
          .off('keypress')
          .off('keyup');

        if (this.eventSupported('keydown')) {
            this.$element.off('keydown');
        }

        this.$menu.remove();
        this.$menu2.remove();
    }
    , eventSupported: function (eventName) {
        var isSupported = eventName in this.$element;
        if (!isSupported) {
            this.$element.setAttribute(eventName, 'return;');
            isSupported = typeof this.$element[eventName] === 'function';
        }
        return isSupported;
    }

    , move: function (e) {
        if (!this.shown) return;
        if (!this.shown2) return;
        switch (e.keyCode) {
            case 9: // tab
            case 13: // enter
            case 27: // escape
                e.preventDefault();
                break;

            case 38: // up arrow
                e.preventDefault();
                this.prev();
                break;

            case 40: // down arrow
                e.preventDefault();
                this.next();
                break;
        }

        e.stopPropagation();
    }

    , keydown: function (e) {

        this.suppressKeyPressRepeat = ~$.inArray(e.keyCode, [40, 38, 9, 13, 27]);
        if (!this.shown && e.keyCode == 40) {
            this.lookup("");

        } else {
            this.move(e);

        }
    }

    , keypress: function (e) {
        if (this.suppressKeyPressRepeat) return;
        this.move(e);
    }

    , keyup: function (e) {
        switch (e.keyCode) {
            case 40: // down arrow
            case 38: // up arrow
            case 16: // shift
            case 17: // ctrl
            case 18: // alt
                break;

            case 9: // tab
            case 13: // enter
                if (!this.shown) return;
                this.select();
                break;

            case 27: // escape
                if (!this.shown) return;
                this.hide();
                break;
            default:
                this.lookup();
        }

        e.stopPropagation();
        e.preventDefault();
        if (this.options._keyDown != null) {
            var val = this.$element.val();
            this.options._keyDown(val);
        }
    }

    , focus: function (e) {

        ////if (!this.focused) {
        //this.focused = true;
        //this.shown2 = true;
        //if (!this.focused) {
        //    this.lookup("");

        //    if (this.options.minLength === 0 && !this.$element.val() || this.options.showHintOnFocus) {
        //        this.lookup("");
        //    }
        //} else { e.preventDefault(); }
        //   this.mousedover=true;
        this.shown4 = false;
        if (!this.focused) {
            this.focused = true;
            this.lookup("");
            if (this.options.minLength === 0 && !this.$element.val() || this.options.showHintOnFocus) {
                this.lookup("");
            }
        }

    }

    , blur: function (e) {
        if (this.shown4) {
            this.shown4 = false;
            return;
        }
        this.focused = false;
        if (this.showBlur) { this.hide(); }
        // if (this.shown3 || this.mousedover) { this.hide(); }
        if (!this.mousedover && this.shown) {
            this.hide();
        }
        // if (!this.mousedover && this.shown2) this.hide();
    }

    , click: function (e) {
        this.shown4 = false;
        e.stopPropagation();
        e.preventDefault();
        this.select();
        this.$element.focus();
    }
    , click2: function (e) {
        e.stopPropagation();
        e.preventDefault();
        this.select2();
        this.$element.focus();
    }

    , mouseenter: function (e) {
        this.shown3 = false;
        this.mousedover = true;
        this.shown4 = false;
        this.$menu.find('.active').removeClass('active');
        this.$menu2.find('.active').removeClass('active');
        $(e.currentTarget).addClass('active');
        this.$element.focus();
    }

    , mouseleave: function (e) {
        this.mousedover = false;
        this.shown4 = false;
        if (this.shown3) {

            return;
            this.shown3 = false;
        }

        if (!this.focused && this.shown)
            this.hide();

    }
    , spanClick: function (e) {

        //this.lookup("");
        if (typeof (query) != 'undefined' && query !== null) {
            this.query = query;
        } else {
            this.query = '';
        }
        this.render(this.options.source)
        var pos = $.extend({}, this.$element.position(), {
            height: this.$element[0].offsetHeight
        }), scrollHeight;

        scrollHeight = typeof this.options.scrollHeight == 'function' ?
            this.options.scrollHeight.call() :
            this.options.scrollHeight;

        this.$menu
          .insertAfter(this.$element)
          .css({
              top: pos.top + pos.height + scrollHeight
          , left: pos.left
          });
        //.show();
        this.$element.focus();
        var isShow = this.$menu.is(':visible');
        if (!isShow) {
            this.$menu.show();

        } else {
            this.$menu.hide();
        }
        this.shown4 = false;
        this.shown2 = true;
        this.shown = true;
    }
    , mouseleave2: function (e) {
        this.mousedover = false;
        if (!this.focused && this.shown2) this.hide();
    }
    };


    /* TYPEAHEAD PLUGIN DEFINITION
     * =========================== */

    var old = $.fn.typeahead2;

    $.fn.typeahead2 = function (option) {
        $(this).addClass("input-group");
        var inputid = $(this).attr("id") + "_typeahead";
        var spanid = $(this).attr("id") + "_spanid";
        var input = '<input class="form-control" type="text" id="' + inputid + '"> <span class="input-group-addon" id="' + spanid + '"><span><i class="fa fa-sort-desc" style="font-size: 12px;"></i></span></span>';
        $(this).append(input);

        var arg = arguments;

        return this.each(function () {
            var $this = $(this)
              , data = $this.data('typeahead')
              , options = typeof option == 'object' && option;
            if (!data) {
                $this.data('typeahead', (data = new Typeahead($("#" + inputid + ""), options, $("#" + spanid), $(this).attr("id"))));

                // data.render().show();
            }
            if (typeof option == 'string') {
                if (arg.length > 1) {
                    data[option].apply(data, Array.prototype.slice.call(arg, 1));
                } else {
                    data[option]();
                }
            }
        });
    };

    $.fn.typeahead2.defaults = {
        source: []
    , items: 8
    , menu: '<ul class="typeahead dropdown-menu"></ul>'
    , menu2: '<ul class="typeahead dropdown-menu typeahead-combox" style="top: 28px; left: 0px;"></ul>'
    , item: '<li><a href="#"></a></li>'
    , minLength: 1
    , scrollHeight: 0
    , autoSelect: true
    , _keyDown: null
    , _select: null
    };

    $.fn.typeahead2.Constructor = Typeahead;


    /* TYPEAHEAD NO CONFLICT
     * =================== */

    $.fn.typeahead2.noConflict = function () {
        $.fn.typeahead = old;
        return this;
    };


    /* TYPEAHEAD DATA-API
     * ================== */

    //$(document).on('focus.typeahead.data-api', '[data-provide="typeahead"]', function (e) {
    //    var $this = $(this);
    //    if ($this.data('typeahead')) return;
    //    $this.typeahead($this.data());
    //});

}(window.jQuery);
