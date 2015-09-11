/// <reference path="../_references.js" />

vkecrm.RegisterNameSpace("entity");

entity.Study = function() {
    this.StudyID = "",
        this.ProtocolNo = "",
        this.ProtocolTitle = "",
        this.ApprovalDate = "",
        this.HistoricalStudyFK = "",
        this.VersionNo = 0,
        this.VersionDate = "",
        this.StudyNatureFK = "",
        this.StudyStatusFK = "",
        this.StudyExpiryDate = new Date(),

        this.IACUCRefNo = "",
        this.IsDeleted = false,
        this.CreateDate = new Date(),
        this.CreateByLoginFK = "",
        this.CreateByUserFK = "",
        this.UpdateDate = new Date(),
        this.UpdateByLoginFK = "",
        this.UpdateByUserFK = "";
};

entity.SearchStudyListParam = function() {
    //this.StudyStartDate = new Date(),
    //this.StudyEndDate = new Date(),
    //this.AllStudyDate = true,
    //this.ApplicationStatus = "All",
    //this.IrbApprovalStatus = "All",
    this.StudyStatusCode = "",
    //this.CirbApprovalStartDate = new Date(),
    //this.CirbApprovalEndDate = new Date(),
    //this.AllCirbApprovalStartDate = true,
    //this.FundApprovalStartDate = new Date(),
    //this.FundApprovalEndDate = new Date(),
    //this.AllFundApprovalDate = true;
    this.StudyRoleCode = "",
    this.CurrentUserID = "";
};

entity.searchInstitutionStudyListParam = function () {
    this.CIRBRefNo = "";
    this.InstitutionId = "";
    this.DepartmentId = "";
    this.StatusId = "";
    this.Expire = 0;
    this.BeginDate = "";
    this.EndDate = "";
    this.UserId = "";
};

entity.SearchAgendaListParam = function() {
    this.Subject = "";
    this.AgendaStatus = 0;
    this.MeetingStartDate = new Date();
    this.MeetingEndDate = new Date();
    this.SentStartDate = new Date();
    this.SentEndDate = new Date();
    this.boardID = "";
};

entity.Attachment = function() {
    this.FieldName = null;
    this.Files = new Array();
};

entity.Attachments = new Array();


