/// <reference path="require.js" />
'use strict';

require.config({
    baseUrl: 'Scripts',
    paths: {
        jquery: 'jquery-1.8.2.min',
        domready: './jquery.domready',

        ext: './ext',

        framework: './framework',

        global: './pages/global',

        bootstrap: './framework/bootstrap',

        jqPlugin: './jquery',

        ajaxHelper: './AjaxHelper',

        accessRight: './AccessRightHelper',

        jtemplate: './jquery-jtemplates',

        entity: './entity',

        pages: './pages',
        cirbform: './pages/CIRBForm',
        cirbpage: './pages/cirbpage',
        startup: './pages/Startup'
    },
    shim: {
        jquery: {
            exports: 'jQuery'
        },
        bootstrap: { deps: ['jquery'] },
        domready: { deps: ['jquery'] },
        jqPlugin: { deps: ['jquery', 'jtemplate'] },
        ext: { deps: ['jquery'] },
        framework: { deps: ['jquery', 'ext', 'jtemplate'] },
        global: { deps: ['framework'] },
        ajaxHelper: { deps: ['jquery'] },
        jtemplate: { deps: ['jquery'] },
        startup: { deps: ['ext', 'global', 'ajaxHelper', 'framework'] }
    }
});