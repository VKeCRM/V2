vkecrm.RegisterNameSpace("vkecrm.framework");

var DropDownListDefaultSetting = {
    ElementId: null,
    DataSource: null,
    ValueField: "Code",
    TextField: "DisplayName",
    SelectValue: null
};

var RadioButtonListDefaultSetting = {
    ElementId: null,
    DataSource: null,
    NameField: "NameField",
    ValueField: "Code",
    TextField: "DisplayName",
    SelectValue: null,
    TabIndex: null
};

var CheckBoxListDefaultSetting = {
    ElementId: null,
    DataSource: null,
    NameField: "NameField",
    ValueField: "Code",
    TextField: "DisplayName",
    SelectValue: null,
    TabIndex: null
};

vkecrm.framework.ui = {
    FULL_FRAME_ID: 'AllContent',

    renderDropDownList: function (arg) {
        var setting = jQuery.extend({}, DropDownListDefaultSetting, arg);
        if (setting.SelectValue == null && setting.DataSource != null && setting.DataSource.length > 0) {
            setting.SelectValue = vkecrm.framework.common.getProperty(setting.DataSource[0], setting.ValueField);
        }
        var templatestring = '';
        for (var i = 0; i < setting.DataSource.length; i++) {
            var valueField = vkecrm.framework.common.getProperty(setting.DataSource[i], setting.ValueField);
            var textField = vkecrm.framework.common.getProperty(setting.DataSource[i], setting.TextField);
            if (valueField == setting.SelectValue) {
                templatestring = templatestring + '<option value="' + valueField + '" selected="selected">' + textField + '</option>';
            } else if (setting.DataSource[i].Disabled) {
                templatestring = templatestring + '<option value="' + valueField + '" disabled="disabled">' + textField + '</option>';
            } else {
                templatestring = templatestring + '<option value="' + valueField + '">' + textField + '</option>';
            }
        }

        return templatestring;
    },


    fillDropDownList: function (arg) {
        var setting = jQuery.extend({}, DropDownListDefaultSetting, arg);
        if (setting.ElementId != null) {
            jQuery('#' + setting.ElementId).html(vkecrm.framework.ui.renderDropDownList(setting));
        }
    },

    renderRadioButtonList: function (arg) {
        var setting = jQuery.extend({}, RadioButtonListDefaultSetting, arg);
        var templatestring = '';
        for (var i = 0; i < setting.DataSource.length; i++) {
            var valueField = vkecrm.framework.common.getProperty(setting.DataSource[i], setting.ValueField);
            var textField = vkecrm.framework.common.getProperty(setting.DataSource[i], setting.TextField);
            var tabIndex = setting.TabIndex;
            if (tabIndex != null) {
                tabIndex = parseInt(tabIndex) + i;
            } else {
                tabIndex = "";
            }
            var id = setting.NameField + i;
            if (setting.SelectValue != null) {
                setting.SelectValue = setting.SelectValue.Code;
            }

            if (valueField == setting.SelectValue) {
                templatestring = templatestring + '<input name="' + setting.NameField + '" type="radio"  value="' + valueField + '" id="' + id + '" tabindex="' + tabIndex + '" checked="checked"/>&nbsp;' + '<label for="' + id + '">' + textField + '</label>' + '<br/>';
            } else {
                templatestring = templatestring + '<input name="' + setting.NameField + '" type="radio"  value="' + valueField + '" id="' + id + '" tabindex="' + tabIndex + '"/>&nbsp;' + '<label for="' + id + '">' + textField + '</label>' + '<br/>';
            }
        }
        return templatestring;
    },

    renderCheckBoxList: function (arg) {
        var setting = jQuery.extend({}, CheckBoxListDefaultSetting, arg);
        var templatestring = '';
        for (var i = 0; i < setting.DataSource.length; i++) {
            var valueField = vkecrm.framework.common.getProperty(setting.DataSource[i], setting.ValueField);
            var textField = vkecrm.framework.common.getProperty(setting.DataSource[i], setting.TextField);
            var tabIndex = setting.TabIndex;
            if (tabIndex != null) {
                tabIndex = parseInt(tabIndex) + i;
            } else {
                tabIndex = "";
            }
            var id = setting.NameField + i;
            if (setting.SelectValue != null) {
                setting.SelectValue = setting.SelectValue.Code;
            }

            if (valueField == setting.SelectValue) {
                templatestring = templatestring + '<label class="check_lbl" for="' + id + '"><input name="' + setting.NameField + '" type="checkbox"  value="' + valueField + '" id="' + id + '" tabindex="' + tabIndex + '" checked="checked"/>&nbsp;' + textField + '</label>';
            } else {
                templatestring = templatestring + '<label class="check_lbl" for="' + id + '"><input name="' + setting.NameField + '" type="checkbox"  value="' + valueField + '" id="' + id + '" tabindex="' + tabIndex + '"/>&nbsp;' + textField + '</label>';
            }
        }
        return templatestring;
    },


    showWaiting: function (id, isShow) {
        var defaultOpts = {
            message: '<img class="waitingImg" alt=\'Waiting..\' src=\'/Themes/default/assets/icon_loading.gif\' />',
            css: { cursor: "default", border: '0px solid #aaa', opacity: 1, backgroundColor: 'transparent', left: '40%', top: '48%' },
            overlayCSS: { opacity: 0.2, backgroundColor: '#ccc' },
            showOverlay: true,
            centerY: true,
            centerX: true,
            fadeIn: 0
        };
        // show full waiting
        if (id == null) {
            if (isShow) {
                jQuery.blockUI(defaultOpts);
            } else {
                jQuery.unblockUI({ fadeOut: 0 });
            }
        } else {
            // show waiting on element
            var selector = '#' + id;
            if (isShow) {
                jQuery(selector).block(defaultOpts);
            } else {
                jQuery(selector).unblock();
            }
        }
    },

    ShowDialog: function (id, opts) {
        var defaultOpts = {
            message: $('#' + id),
            css: { cursor: "default", border: '0px solid #aaa', top: '25%' },
            hermesTheme: true,
            overlayCSS: { cursor: "default", opacity: 0.0 },
            fadeIn: 0,
            fadeOut: 0,
            focusInput: false
        };

        var setting = jQuery.extend({}, defaultOpts, opts);

        jQuery.blockUI(setting);
    },

    ShowWindowDialog: function (url, width, height, opts) {
        var iframeWrapper = '<iframe src="{0}" style="width:{1}px; height: {2}px;" frameborder="no" border="0" scrolling="no"></iframe>';
        var msg = String.format(iframeWrapper, url, width, height);
        var defaultOpts = {
            message: msg,
            css: { cursor: "default", border: '0px solid #aaa', top: '25%' },
            hermesTheme: true,
            overlayCSS: { cursor: "default", opacity: 0.0 },
            fadeIn: 0,
            fadeOut: 0
        };
        var setting = jQuery.extend({}, defaultOpts, opts);
        jQuery.blockUI(setting);
    },

    CloseDialog: function (opts) {
        var defaultOpts = {
            fadeIn: 0,
            fadeOut: 0
        };
        var setting = jQuery.extend({}, defaultOpts, opts);
        jQuery.unblockUI(setting);
    },

    AddCloseDialogEvent: function (func) {
        var closeBtn = jQuery('.blockPage .closeBtn');
        closeBtn.click(function () {
            // vkecrm.framework.ui.CloseDialog();
            func();
        });
    },

    InitializeTooltip: function (selector, opts) {
        var defaultOpts = {
            positions: 'top',
            fill: '#FFFFEA',
            strokeStyle: '#B7B7B7',
            spikeLength: 8,
            spikeGirth: 8,
            padding: 8,
            cornerRadius: 0,
            trigger: "click",
            clickAnywhereToClose: true,
            closeWhenOthersOpen: true,
            shadowshadow: false,
            hoverIntentOpts: {
                interval: 0,
                timeout: 0
            }
        };

        var setting = jQuery.extend({}, defaultOpts, opts);
        jQuery(selector).bt(setting);
    },

    ShowModal: function (id) {
        $("#modal_" + id).modal('toggle');

    },

    Dialog: avalon.define('dialog', function (vm) {
        var dialogOption = {
            title: 'Information',
            content: '',
            showOk: true,
            showCancel: false,
            confirm: function () { return false; },
            cancel: function () { return false; }
        };

        avalon.mix(vm, dialogOption);

        vm.ShowDialog = function (options) {
            $('.modal:visible').modal("hide");
            avalon.mix({}, options, dialogOption);
            avalon.mix(vm, options);
            vkecrm.framework.ui.ShowModal('warming');
        };

        vm.CloseDialog = function () {
            vkecrm.framework.ui.ShowModal('warming');
        };

        vm.ok = function () {
            vm.confirm(vkecrm.framework.ui.Dialog);
            vm.CloseDialog();
        };

        vm.close = function () {
            vm.CloseDialog();
            vm.cancel(vkecrm.framework.ui.Dialog);
        };
    }),

    AddMessage: function (options) {
        options.type = options.type || 'success';
        options.isHtml = options.isHtml || false;

        if (options.title) {
            //options.isHtml = true;
            options.message = '<b>' + options.title + '&nbsp;:&nbsp;</b>' + options.message;
        }

        var html = '<div class="alert alert-' + options.type
                    + ' alert-dismissible fade in" role="alert">'
                    + '<button type="button" class="close" data-dismiss="alert"><span aria-hidden="true" data-backdrop="static">×</span><span class="sr-only">Close</span></button>'
                    + options.message
                    + '</div>';

        if (options.el) {
            $('#' + options.el).html(html);
            return;
        }

        $('.top-center').notify({
            type: options.type,
            closable: true,
            fadeOut: { enabled: true, delay: 8000 },
            message: {
                html: options.isHtml,
                text: options.message
            }
        }).show();
    }
};
