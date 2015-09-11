/// <reference path="../_references.js" />

vkecrm.RegisterNameSpace('vkecrm.events');

vkecrm.events.FormStatus = {
    currentStatus: null,
    statusEntities: [],
    fns: [],

    InitStatus: function () {
        var $this = vkecrm.events.FormStatus;

        $this.fns = [];

        $this.statusEntities.push(new entity.FormStatusEntity('Draft', 'Draft', 'Draft'));
        $this.statusEntities.push(new entity.FormStatusEntity('New', 'Draft', 'Draft'));
        $this.statusEntities.push(new entity.FormStatusEntity('Amendment', 'Amendment', 'Amendment'));
        $this.statusEntities.push(new entity.FormStatusEntity('RDOChecking', 'RDO Checking', 'RDO Checking'));
        $this.statusEntities.push(new entity.FormStatusEntity('RDOChecked', 'RDO Checked', 'RDO Checked'));
        $this.statusEntities.push(new entity.FormStatusEntity('Finalised', 'Finalised', 'Finalised'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingPIDeclaration', 'Pending PI Declaration', 'Pending PI Declaration'));
        $this.statusEntities.push(new entity.FormStatusEntity('Endorsement', 'Endorsement', 'Endorsement'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingDREndorsement', 'Pending DR Endorsement', 'Pending DR Endorsement'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingIREndorsement', 'Pending IR Endorsement', 'Pending IR Endorsement'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingSitePIClarification', 'Endorsement, Pending Site-PI Clarification', 'Endorsement, Pending Site-PI Clarification'));
        $this.statusEntities.push(new entity.FormStatusEntity('Rejected', 'Endorsement, Rejected by DR/IR', 'Endorsement, Rejected by DR/IR'));
        $this.statusEntities.push(new entity.FormStatusEntity('RDOChecking', 'RDO Check (Prior to IRB Submission)', 'RDO Check (Prior to IRB Submission)'));
        $this.statusEntities.push(new entity.FormStatusEntity('RDOChecked', 'RDO Checked', 'RDO Checked'));
        $this.statusEntities.push(new entity.FormStatusEntity('Submitted', 'Submitted', 'Submitted'));
        $this.statusEntities.push(new entity.FormStatusEntity('PreliminaryReview', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('SecretariatReview', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingPIClarification', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('CIRBReview', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('ReadyForChairman Review', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('FullBoardReview', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('TabledForReview', 'Pending Review', 'Pending Review'));
        $this.statusEntities.push(new entity.FormStatusEntity('CIRBReviewComplete', 'CIRB Review Complete', 'CIRB Review Complete'));
        $this.statusEntities.push(new entity.FormStatusEntity('NotApproved', 'Not Approved', 'Not Approved'));
        $this.statusEntities.push(new entity.FormStatusEntity('Approved', 'Approved', 'Approved'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingOverallPIDeclaration', 'Pending Site-PI Declaration / Pending DR Endorsement / Pending IR Endorsement', 'Pending Site-PI Declaration / Pending DR Endorsement / Pending IR Endorsement'));
        $this.statusEntities.push(new entity.FormStatusEntity('PendingSitePIDeclaration', 'Pending Site-PI Declaration / Pending DR Endorsement / Pending IR Endorsement', 'Pending Site-PI Declaration / Pending DR Endorsement / Pending IR Endorsement'));
        $this.statusEntities.push(new entity.FormStatusEntity('Endorsed', 'Endorsed', 'Endorsed'));
        $this.statusEntities.push(new entity.FormStatusEntity('EndorsingPending', 'Endorsing Pending', 'Endorsing Pending'));
        $this.statusEntities.push(new entity.FormStatusEntity('Declared', 'PI Declared', 'PI Declared'));
    },

    AddHandler: function (fn) {
        var $this = vkecrm.events.FormStatus;
        $this.fns.push(fn);
    },

    SetStatus: function (status) {
        var $this = vkecrm.events.FormStatus;

        if (status) {
            $this.currentStatus = status;
        }
        var l = $this.fns.length;
        for (var i = 0; i < l; i++) {
            $this.fns[i]();
        }
    },

    GetStatus: function () {
        return vkecrm.events.FormStatus.currentStatus;
    },

    FindStatus: function (code) {
        var $this = vkecrm.events.FormStatus;
        var l = $this.statusEntities.length;
        for (var i = 0; i < l; i++) {
            if ($this.statusEntities[i].Code.toUpperCase() == code.toUpperCase()) {
                return $this.statusEntities[i];
            }
        }
        return null;
    }
};

vkecrm.events.AjaxStatus = {
    complatedFlag: 0,
    complateFunc: [],
    delayFlag: false,
    delay: null,


    Init: function () {
        var $this = vkecrm.events.AjaxStatus;

        $(document).ajaxStop($this.AllComplate);
    },

    RegisterEvent: function (fn) {
        var $this = vkecrm.events.AjaxStatus;

        $this.complateFunc.push(fn);
    },

    AjaxStart: function () {
        var $this = vkecrm.events.AjaxStatus;

        $this.complatedFlag++;
    },

    AjaxComplate: function () {
        var $this = vkecrm.events.AjaxStatus;

        if ($this.delay != null)
            window.clearInterval($this.delay);

        if ($this.delay == null)
            $this.delay = window.setInterval($this.AllComplate, 1000);
        return;
    },

    AllComplate: function () {
        var $this = vkecrm.events.AjaxStatus;
        if ($this.delay != null)
            window.clearInterval($this.delay);

        if ($this.complateFunc.length == 0) return;

        var fn = $this.complateFunc.shift();
        fn();

        $this.AllComplate();
    }
}