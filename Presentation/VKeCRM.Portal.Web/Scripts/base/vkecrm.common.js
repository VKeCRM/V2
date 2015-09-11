// contains

Array.prototype.contains = function (item) {
    return RegExp(item).test(this);
};
var arr = [];
for (var i = 10; i < 15; i++) {
    arr.push(i);
}


String.prototype.startWith = function (str) {
    var reg = new RegExp("^" + str);
    return reg.test(this);
};

String.prototype.endWith = function (str) {
    var reg = new RegExp(str + "$");
    return reg.test(this);
};

// extend string
String.prototype.format = function () {
    var args = arguments;
    return this.replace(/\{(\d+)\}/g,
        function (m, i) {
            return args[i];
        });
};

//V2 static
String.format = function () {
    if (arguments.length == 0)
        return null;

    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
};

String.IsNullOrEmpty = function (val) {
    if (val == null || val == window.undefined) {
        return true;
    } else {
        // ToDo add trim		
        return jQuery.trim(val.toString()).length == 0;
    }
};

String.prototype.GetCenter = function (start, end) {
    var s = 0;
    if (start) {
        s = this.indexOf(start);
    }
    var e = this.indexOf(end);
    if (!e) return '';
    return this.substring(s, e);
};

Array.prototype.remove = function (val) {
    var index = this.indexOf(val);
    if (index > -1) {
        this.splice(index, 1);
    }
};

function Guid(g) {
    var arr = new Array();

    if (typeof (g) == "string") {
        InitByString(arr, g);
    }
    else {
        InitByOther(arr);
    }
    this.Equals = function (o) {
        if (o && o.IsGuid) {
            return this.ToString() == o.ToString();
        } else {
            return false;
        }
    };

    this.IsGuid = function () { };
    this.ToString = function (format) {
        if (typeof (format) == "string") {
            if (format == "N" || format == "D" || format == "B" || format == "P") {
                return ToStringWithFormat(arr, format);
            } else {
                return ToStringWithFormat(arr, "D");
            }
        } else {
            return ToStringWithFormat(arr, "D");
        }
    };

    function InitByString(arr, g) {
        g = g.replace(/\{|\(|\)|\}|-/g, "");
        g = g.toLowerCase();
        if (g.length != 32 || g.search(/[^0-9,a-f]/i) != -1) {
            InitByOther(arr);
        }
        else {
            for (var i = 0; i < g.length; i++) {
                arr.push(g[i]);
            }
        }
    }

    function InitByOther(arr) {
        var i = 32;
        while (i--) {
            arr.push("0");
        }
    }
    function ToStringWithFormat(arr, format) {
        switch (format) {
            case "N":
                return arr.toString().replace(/,/g, "");
            case "D":
                var str = arr.slice(0, 8) + "-" + arr.slice(8, 12) + "-" + arr.slice(12, 16) + "-" + arr.slice(16, 20) + "-" + arr.slice(20, 32);
                str = str.replace(/,/g, "");
                return str;
            case "B":
                var str = ToStringWithFormat(arr, "D");
                str = "{" + str + "}";
                return str;
            case "P":
                var str = ToStringWithFormat(arr, "D");
                str = "(" + str + ")";
                return str;
            default:
                return new Guid();
        }
    }
}

avalon.prototype.bindModel = function (id, vmodel) {
    var element = document.getElementById(id);
    if (element) {
        avalon.scan(element, vmodel);
    }
};

Guid.Empty = new Guid();

Guid.NewGuid = function () {
    var g = "";
    var i = 32;
    while (i--) {
        g += Math.floor(Math.random() * 16.0).toString(16);
    }
    return new Guid(g);
};

//for arrary to distinct
Array.prototype.distinct = function () {
    var self = this;
    var _a = this.concat().sort();
    _a.sort(function (a, b) {
        if (a == b) {
            var n = self.indexOf(a);
            self.splice(n, 1);
        }
    });
    return self;
}; 

var vkecrm;
if (vkecrm && (typeof vkecrm != "object" || vkecrm.NAME)) {
    throw new Error("Namespace 'vkecrm' already exists");
} else {
    // Create our namespace
    vkecrm = {
        RegisterNameSpace: function () {
            var a = arguments, o = null, i, j, d, rt;
            for (i = 0; i < a.length; ++i) {
                d = a[i].split(".");
                rt = d[0];
                eval('if (typeof ' + rt + ' == "undefined"){' + rt + ' = {};} o = ' + rt + ';');
                for (j = 1; j < d.length; ++j) {
                    o[d[j]] = o[d[j]] || {};
                    o = o[d[j]];
                }
            }
        }

    };
}

/**
* Copies all the properties of config to obj.
* @param {Object} obj The receiver of the properties
* @param {Object} config The source of the properties
* @param {Object} defaults A different object that will also be applied for default values
* @return {Object} returns obj
* @member Ext apply
*/
vkecrm.apply = function (o, c, defaults) {
    // no "this" reference for friendly out of scope calls
    if (defaults) {
        vkecrm.apply(o, defaults);
    }
    if (o && c && typeof c == 'object') {
        for (var p in c) {
            o[p] = c[p];
        }
    }
    return o;
};
