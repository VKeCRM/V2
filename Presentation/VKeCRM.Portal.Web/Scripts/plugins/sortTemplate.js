/// <reference path="../../_references.js" />
/// <reference path="../../jquery-jtemplates.js" />

(function ($) {
    var defaultOptions = function (el, o) {
        this.theme = o.theme || 'blue';
        this.noSort = o.noSort || false;
        this.tableId = o.tableId || '';
        this.tmpl = o.tmpl || '';
        this.displayHeader = o.displayHeader || true;
        this.data = o.data || [];
        this.tmplStr = $(el).html();
        this.params = o.params || {};
        this.callDataFunc = o.callDataFunc;
        return this;
    };

    $.fn.templateTable = function (o, cb) {
        var options = defaultOptions(this, o);
        var $this = $(this);

        var themeClass = 'tablesorter-' + options.theme;
        var header = $(options.tmplStr).addClass('tablesorter').addClass(themeClass);
            
        if (options.displayHeader) {
            $this.html(header);
        }
        if ((options.data.Items && options.data.Items.length > 0) || (options.data && options.data.length > 0)) {
            $this.setTemplateElement(options.tmpl);
            if (options.params != null && options.params.length > 0) {

                for (var i = 0; i < options.params.length; i++) {
                    $this.setParam(options.params[i].Name, options.params[i].Value);
                }
            }
            $this.processTemplate(options.data);

            var $table = $this.find('table');
            if (options.tableId) {
                $table = $('#' + options.tableId);
            }
            $table.tablesorter({
                theme: options.theme
            });

            var $head = $table.find('thead tr');
            var $lastTh = $table.find('thead tr th:last-child');
            var lastText = $lastTh.text();
            if (lastText.toLowerCase().trim().startWith('action')) {
                $lastTh.remove();
                $head.append('<th class="noprint">' + lastText + '</th>');
                $table.find('tbody tr td:last-child').addClass('noprint').children().addClass('noprint');

            }
        }
            //--- Added to handle condition where there is no records to be displayed ---//
        else {
            $this.setTemplateElement(options.tmpl);
            if (options.params != null && options.params.length > 0) {

                for (var i = 0; i < options.params.length; i++) {
                    $this.setParam(options.params[i].Name, options.params[i].Value);
                }
            }
            $this.processTemplate(options.data);

            var $table = $this.find('table');
            if (options.tableId) {
                $table = $('#' + options.tableId);
            }
            if (options.noSort) {
                $table.tablesorter({
                    theme: options.theme
                });
            }
        }

        if (cb) {
            cb();
        }
        return $this;
    };

    $.fn.templatePagedTable = function (o) {
        var options = defaultOptions(this, o);
        var $this = $(this);

        var pageOp = new entity.PagingOptions();

        function render(op) {
            if (options.callDataFunc) {
                options.callDataFunc(op, function (result) {
                    if (result && result.TotalRecordCount > 0) {
                        o.data = result.Items;
                        $this.templateTable(o, function () {
                            var $table = $this.find('table');
                            var $page = $table.find('tfoot ul.pagination');
                            setPager({
                                totalCount: result.TotalRecordCount,
                                pageSize: op.PageSize,
                                currentPage: op.PageNumber
                            }, $page);
                        });
                    }
                });
            }
        }

        function setPager(opts, $page) {
            opts.clickPage = render;
            $page.pager(opts);
        }

        render(pageOp);
    };

    $.fn.pager = function (opts) {
        var $this = $(this);
        opts = jQuery.extend({
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            clickPage: function () { return false; }
        }, opts || {});

        var pageCount = Math.ceil(opts.totalCount / opts.pageSize);

        var pagenums = [];
        if (opts.currentPage <= 3 || pageCount <= 5) {
            var p = pageCount > 5 ? 5 : pageCount;
            for (var i = 1; i <= p; i++) {
                pagenums.push(i);
            }
        } else if (pageCount > opts.currentPage + 2) {
            for (var j = opts.currentPage - 2; j < opts.currentPage + 3; j++) {
                pagenums.push(j);
            }
        } else {
            for (var k = pageCount - 4; k <= pageCount; k++) {
                pagenums.push(k);
            }
        }
        if (pagenums.length == 0) pagenums.push(1);

        var content = createLink('first', 'First');
        content += createLink('previous', 'Previous');
        for (var l = 0; l < pagenums.length; l++) {
            content += createLink(pagenums[l], pagenums[l], pagenums[l] == opts.currentPage);
        }
        content += createLink('next', 'Next');
        content += createLink('last', 'Last');

        $this.html(content);

        $this.find('li a.pagenum').click(function () {
            var $pageli = $(this);
            var pagenum = $pageli.attr('pagenum');
            var current = 1;
            switch (pagenum) {
                case 'first':
                    current = 1; break;
                case 'previous':
                    current = opts.currentPage > 1 ? opts.currentPage - 1 : opts.currentPage; break;
                case 'next':
                    current = opts.currentPage < pageCount ? opts.currentPage + 1 : opts.currentPage; break;
                case 'last':
                    current = pageCount; break;
                default:
                    current = pagenum * 1;
                    break;
            }
            if (opts.currentPage == current) {
                return;
            }
            var pageOp = new entity.PagingOptions();
            pageOp.PageNumber = current;
            pageOp.PageSize = opts.pageSize;

            if (opts.clickPage) {
                opts.clickPage(pageOp);
            }
        });

        function createLink(page, text, iscurrent) {
            if (iscurrent) {
                return '<li><a class="pagenum current" href="javascript:void(0);" pagenum="' + page + '">' + text + '</a></li>';
            }
            return '<li><a class="pagenum" href="javascript:void(0);" pagenum="' + page + '">' + text + '</a></li>';
        }
    };
})(jQuery);