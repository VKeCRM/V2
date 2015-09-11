vkecrm.RegisterNameSpace("framework");

//*********************Listener********************************/
framework.listener = function () { };
framework.listener = new framework.listener();

framework.EventListener = function () {
    this.listenername;
    this.listener;
    this.listenerKey;
    this.defaultArguments;
}

vkecrm.apply(framework.listener, {

    LISTENER_GOTO_NEXT: 'LISTENER_GOTO_NEXT',
    LISTENER_GOTO_PREVIOUS: 'LISTENER_GOTO_PREVIOUS',

    LISTENER_GOTO_SECTION_A: 'LISTENER_GOTO_SECTION_A',
    LISTENER_GOTO_SECTION_B: 'LISTENER_GOTO_SECTION_B',
    LISTENER_GOTO_SECTION_C: 'LISTENER_GOTO_SECTION_C',
    LISTENER_GOTO_SECTION_D: 'LISTENER_GOTO_SECTION_C',
    LISTENER_GOTO_SECTION_E: 'LISTENER_GOTO_SECTION_D',
    LISTENER_GOTO_SECTION_F: 'LISTENER_GOTO_SECTION_E',
    LISTENER_GOTO_SECTION_G: 'LISTENER_GOTO_SECTION_F',
    LISTENER_GOTO_SECTION_H: 'LISTENER_GOTO_SECTION_H',
    LISTENER_GOTO_SECTION_I: 'LISTENER_GOTO_SECTION_I',
    LISTENER_GOTO_SECTION_J: 'LISTENER_GOTO_SECTION_J',
    LISTENER_GOTO_SECTION_K: 'LISTENER_GOTO_SECTION_K',
    LISTENER_GOTO_SECTION_L: 'LISTENER_GOTO_SECTION_L',
    LISTENER_GOTO_SECTION_M: 'LISTENER_GOTO_SECTION_M',
    LISTENER_GOTO_SECTION_N: 'LISTENER_GOTO_SECTION_N',
    LISTENER_GOTO_SECTION_O: 'LISTENER_GOTO_SECTION_O',
    LISTENER_GOTO_SECTION_P: 'LISTENER_GOTO_SECTION_P',
    LISTENER_GOTO_SECTION_Q: 'LISTENER_GOTO_SECTION_Q',
    LISTENER_GOTO_SECTION_R: 'LISTENER_GOTO_SECTION_R',
    LISTENER_GOTO_SECTION_S: 'LISTENER_GOTO_SECTION_S',
    LISTENER_GOTO_SECTION_T: 'LISTENER_GOTO_SECTION_T',
    LISTENER_GOTO_SECTION_U: 'LISTENER_GOTO_SECTION_U',

    LISTENER_INIT_SECTION: 'LISTENER_INIT_SECTION',
    LISTENER_SET_BUTTONS: 'LISTENER_SET_BUTTONS',
    LISTENER_UPDATE_OR_VIEW_FORMS: 'LISTENER_UPDATE_OR_VIEW_FORMS',

    _listeners: [],
    AddListener: function (listenername, fn, fnKey, defaultArgs) {
        fnKey = ((fnKey == null || fnKey == window.undefined) ? null : fnKey);
        var lst = new framework.EventListener();
        lst.listenername = listenername;
        lst.listener = fn;
        lst.listenerKey = fnKey;
        lst.defaultArguments = defaultArgs;

        for (var i = 0; i < this._listeners.length; i++) {
            if (this._listeners[i].listenername == listenername
                && ((fnKey == null && this._listeners[i].listener == fn) || (fnKey != null && this._listeners[i].listenerKey == fnKey))
                ) {
                // listener already exists, no need to add it
                return;
            }
        }
        // if listener does not exist, add it to listeners array
        this._listeners.push(lst);
        // this._listeners[this._listeners.length] = lst;
    },

    FireListener: function (listenername, args) {
        for (var i = 0; i < this._listeners.length; i++) {
            if (this._listeners[i].listenername == listenername) {
                // find the listener, then we need call the registered function
                if (args == null || args == window.undefined) {
                    this._listeners[i].listener(this._listeners[i].defaultArguments);
                } else {
                    this._listeners[i].listener(args);
                }
            }
        }
    }

});
