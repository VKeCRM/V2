vkecrm.RegisterNameSpace("entity");

entity.FormFlow = function () {
    this.CurrentStep = entity.enums.SectionStep.SectionA;
    this.NextStep = 0;
    this.StepTitle = "";
    this.StepDescription = "";
    this.StepButtonsStatus = 0;  //checked all
    this.StepButtonsEvent = [];  //{ButtonKey: 0, ButtonEvent: fn}

    this.GetButtonEvent = function (buttonKey) {
        for (var i = 0; i < this.StepButtonsEvent.length; i++) {
            if (this.StepButtonsEvent[i].ButtonKey == buttonKey) {
                return this.StepButtonsEvent[i].ButtonEvent;
            }
        }
        return null;
    };
};

entity.StudyTeamMember = function () {
    this.UserProfileFK = null;
    this.StudyRoleFK = null;
    this.InstitutionFK = null;
    this.DivisionFK = null;
    this.DepartmentFK = null;
    this.StudyRoleDisplay = null;
    this.CVFileFK = null;
    this.CITIFileFK = null;
    this.SGGCPFileFK = null;
    this.SC1 = null;
    this.SC1_1 = false;
    this.SC1_2 = false;
    this.SC1_3 = false;
    this.SC1_4 = false;
    this.SC1_5 = false;
    this.SC1_6 = false;
    this.FormFK = null;
    this.SC2_1 = '';
    this.SC3_1 = '';
    this.IsDeclaration = false;
    this.DeclarationDate = '1999/01/01';
    this.IsInformed = false;
    this.Seq = null;
    this.Designation = null;
};

entity.StudyStatusReport = function () {
    this.DataStatus = entity.enums.DataStatus.Create;
    this.Attachments = new Array();
    this.StudyStatusReportFormID = null;
    this.FormFK = null;
    this.Section2Tables = new Array();
    this.Section1Tables = new Array();
    this.ReportConflicts = new Array();
    this.SSR1 = null;
    this.SSR2 = null;
    this.SSR3 = null;
    this.SSR4 = null;
    this.SSR5_1 = null;
    this.SSR5_1_1 = null;
    this.SSR5_2 = null;
    this.SSR5_3 = null;
    this.SSR5_4 = null;
    this.SSR5_5 = null;
    this.SSR6_1_2 = null;
    this.SSR7_1 = null;
    this.SSR7_1_3_1 = null;
    this.SSR7_1_3_2 = null;
    this.SSR7_1_3_3 = null;
    this.SSR7_1_3_4 = null;
    this.SSR7_1_3_5 = null;
    this.SSR7_2_1 = null;
    this.SSR8_1 = null;
    this.SSR8_1_2_1 = null;
    this.SSR8_2 = null;
    this.SSR8_2_1_1 = null;
    this.SSR8_3 = null;
    this.SSR8_4 = null;
    this.SSR8_5 = null;
    this.SSR8_6 = null;
    this.SSR8_7 = null;
    this.SSR8_8 = null;
    this.SSR8_8_2 = null;
    this.SSR8_9 = null;
    this.SSR8_10 = null;
    this.SSR8_11 = null;
    this.SSR8_11_2 = null;
    this.SSR8_12 = null;
    this.SSR8_12_2 = null;
    this.SSR9_1 = null;
    this.SSR9_2 = null;
};

entity.StudyReactivationReport = function () {
    this.DataStatus = entity.enums.DataStatus.Create;
    this.Attachments = new Array();
    this.StudyReactivationReportFormID = null;
    this.FormFK = null;
    this.Section2Tables = new Array();
    this.Section1Tables = new Array();
    this.ReportConflicts = new Array();
    this.SRR1 = null;
    this.SRR2 = null;
    this.SRR3 = null;
    this.SRR4 = null;
    this.SRR5_1 = null;
    this.SRR5_2 = null;
    this.SRR5_2_1 = null;
    this.SRR5_3 = null;
    this.SRR5_4 = null;
    this.SRR5_5 = null;
    this.SRR5_5_1 = null;
    this.SRR6_1_2 = null;
    this.SRR7_1_1 = null;
    this.SRR7_1_2 = null;
    this.SRR7_1_3 = null;
    this.SRR7_1_3_1 = null;
    this.SRR7_1_3_2 = null;
    this.SRR7_1_3_3 = null;
    this.SRR7_1_3_4 = null;
    this.SRR7_1_3_5 = null;
    this.SRR7_2_1 = null;
    this.SRR8_1_1 = null;
    this.SRR8_1_2 = null;
    this.SRR8_1_2_1 = null;
    this.SRR8_2_1 = null;
    this.SRR8_2_1_1 = null;
    this.SRR8_2_2 = null;
    this.SRR8_2_3 = null;
    this.SRR8_3 = null;
    this.SRR8_4 = null;
    this.SRR8_5_1 = null;
    this.SRR8_5_2 = null;
    this.SRR8_6_1 = null;
    this.SRR8_6_2 = null;
    this.SRR8_6_2_1 = null;
    this.SRR8_7_1 = null;
    this.SRR8_7_2 = null;
    this.SRR8_8_1 = null;
    this.SRR8_8_2 = null;
    this.SRR8_8_2_1 = null;
    this.SRR8_9 = null;
    this.SRR8_10 = null;
    this.SRR8_11_1 = null;
    this.SRR8_11_2 = null;
    this.SRR8_11_2_1 = null;
    this.SRR8_12_1 = null;
    this.SRR8_12_2 = null;
    this.SRR8_12_2_1 = null;
    this.SRR9_1 = null;
    this.SRR9_2 = null;
    this.SRR9_3 = null;
    this.SRR9_4 = null;
    this.SRR9_5 = null;
    this.SRR9_6_1 = null;
    this.SRR9_9_6_2_1 = null;
    this.SRR9_6_2 = null;
    this.SRR9_6_2_1 = null;
    this.SRR9_6_3 = null;
    this.SRR9_3_1 = null;
    this.SRR9_6_3_1 = null;
    this.SRR10_1 = null;
    this.SRR10_2 = null;
};

entity.StudyClosureReport = function () {
    this.DataStatus = entity.enums.DataStatus.Create;
    this.Attachments = new Array();
    this.StudyReportFormID = null;
    this.FormFK = null;
    this.Section2Tables = new Array();
    this.Section1Tables = new Array();
    this.SCR1 = null;
    this.SCR2 = null;
    this.SCR3_1 = null;
    this.SCR3_2 = null;
    this.SCR3_3 = null;
    this.SCR4 = null;
    this.SCR5_1 = null;
    this.SCR5_1_1 = null;
    this.SCR5_2 = null;
    this.SCR5_2_1 = null;
    this.SCR5_2_2 = null;
    this.SCR5_3 = null;
    this.SCR5_3_1 = null;
    this.SCR5_3_2 = null;
    this.SCR6_1_1 = null;
    this.SCR7_1_1 = null;
    this.SCR7_1_2 = null;
    this.SCR7_1_3 = null;
    this.SCR7_1_3_1 = null;
    this.SCR7_1_3_2 = null;
    this.SCR7_1_3_3 = null;
    this.SCR7_1_3_4 = null;
    this.SCR7_1_3_5 = null;
    this.SCR7_2 = null;
    this.SCR8_1_1 = null;
    this.SCR8_1_2 = null;
    this.SCR8_1_2_1 = null;
    this.SCR8_2_1 = null;
    this.SCR8_2_1_1 = null;
    this.SCR8_2_2 = null;
    this.SCR8_2_3 = null;
    this.SCR8_3_1 = null;
    this.SCR8_4_1 = null;
    this.SCR8_5_1 = null;
    this.SCR8_5_2 = null;
    this.SCR8_5_2_1 = null;
    this.SCR8_6_1 = null;
    this.SCR8_6_2 = null;
    this.SCR8_7_1 = null;
    this.SCR8_7_2 = null;
    this.SCR8_7_2_1 = null;
    this.SCR8_8 = null;
    this.SCR8_9 = null;
    this.SCR8_10_1 = null;
    this.SCR8_10_2 = null;
    this.SCR8_10_2_1 = null;
    this.SCR8_11_1 = null;
    this.SCR8_11_2 = null;
    this.SCR8_11_2_1 = null;
    this.SCR9_1 = null;
    this.SCR9_2 = null;
};

entity.Section1Table = function () {
    this.StudyStatusReportFK = null;
    this.NameofInsititution = '';
    this.IfOther = '';
    this.ProposedRecruitmentTarget1 = '';
    this.NumofSubjectsEnrolled = '';
    this.NumofSubjectsActuallyRecruitment = '';
    this.NumofSubjectsWhoHaveCompletedStudy = '';
    this.NumofSubjectsWithdrawnFromStudy = '';
    this.SSRSection1TableID = null;
    this.Order = null;
}

entity.Section2Table = function () {
    this.StudyStatusReportFK = null;
    this.NameofInstitution = '';
    this.OthersInstitution = '';
    this.ProposedTarget = '';
    this.NumSampleRecordsStudied = '';
    this.SSRSection2TableID = null;
    this.Order = null;
}

entity.ReportConflict = function () {
    this.ReportConflictID = null;
    this.ReportFK = null;
    this.GivenName = '';
    this.FamilyName = '';
    this.StudyRoleDisplay = '';
    this.DepartmentName = '';
    this.InstitutionName = '';
    this.C = null;
    this.C1_1 = null;
    this.C1_2 = null;
    this.C1_3 = null;
    this.C1_4 = null;
    this.C1_5 = null;
    this.C1_6 = null;
    this.C2_1 = '';
    this.C3_1 = '';
}

entity.Amendment = function () {
    AmendmentID = null;
    StudyID = null;
    OriginalIRBFormID = null;
    IRBFormID = null;
    SectionKey = null;
    ChangeReason = null;
    OriginalHtml = null;
    CurrentHtml = null;
    SectionName = null;
    AmendmentDate = new Date();
};

entity.AmendmentCoverNote = function() {
    this.StudyID = "";
    this.IRBFormFK = "";
    this.AC1 = "";
    this.AC2 = "";
    this.AC3 = "";
    this.AC4 = "";
    this.AC5 = "";
    this.AC6 = "";
    this.AC7 = "";
    this.AC8_1 = false;
    this.AC8_2 = false;
    this.AC8_3 = false;
    this.AC9 = "";
};

entity.CirbForm = function () {
    this.CurrentStep = entity.enums.SectionStep.SectionA;
    this.IRBApplicationFormID = null;
    this.NextStep = 0;
    this.ProtocolNo = null;
    this.ProtocolTitle = null;
    this.Version = null;
    this.VersionDate = new Date();
    this.IRBApplicationForm_PA = [];
    this.FormAttachments = [];
    this.StudyTeamMember = [];
    this.DataStatus = entity.enums.DataStatus.Create;
    this.FormStatus = vkecrm.events.FormStatus;
    this.AmendmentCoverNote = null;


    // Section A
    this.SA1_1 = '';
    this.SA1_2 = '';
    this.SA1_3 = 1;
    this.SA1_4 = new Date();

    // Section B
    this.SB1_1 = null;
    this.SB1_2 = null;
    this.SB1_3 = "";
    this.SB2_1 = null;
    this.SB2_2 = null;
    this.SB3_Modified = null;
    this.SB3_1 = null;
    this.SB3_1_1 = null;
    this.SB3_2 = null;
    this.SB3_2_Y1 = null;
    this.SB3_2_Y1_1 = '';
    this.SB3_3 = null;
    this.SB3_3_Y1 = '';
    this.SB3_3_Y2 = '';

    // Section C
    this.SC1_1 = 0;
    this.SC1_2 = 0;
    this.SC1_3 = 0;
    this.SC1_4 = 0;
    this.SC1_5 = 0;
    this.SC1_6 = 0;
    this.SC2_1 = null;
    this.SC3_1 = null;

    // Section D
    this.SD1 = null;
    this.SD1_1 = null;
    this.SD1_2_1 = null;
    this.SD2 = null;
    this.SD2_Y1 = null;

    // SectionE
    this.SE1 = null;
    this.SE1_2_1_1 = null;
    this.SE1_2_1_2 = null;
    this.SE1_2_2_1 = null;
    this.SE1_2_2_2 = null;
    this.SE1_2_2_3 = null;
    this.SE1_2_2_4 = null;
    this.SE1_2_2_5 = null;
    this.SE1_2_3_1 = null;
    this.SE1_2_3_2 = null;
    this.SE1_2_4_1 = null;
    this.SE1_2_4_2 = null;
    this.SE1_2_4_3 = null;
    this.SE1_2_4_4 = null;
    this.SE1_2_4_5 = null;
    this.SE1_2_5 = false;
    this.SE1_2_5_Y1 = null;
    this.SE1_3_1_1 = null;
    this.SE1_3_1_2 = null;
    this.SE1_3_2_1 = null;
    this.SE1_3_3_1 = null;
    this.SE1_3_4_1 = null;
    this.SE1_3_5 = null;
    this.SE1_3_6 = true;
    this.SE1_3_7_1 = null;
    this.SE2_1 = null;
    this.SE2_2 = null;
    this.SE2_a = null;
    this.SE2_b = null;
    this.SE2_c = null;
    this.SE2_d = null;
    this.SE2_e = null;
    this.SE2_f = null;
    this.SE3 = null;
    this.SE4 = null;
    this.SE4_1 = '';
    this.SE4_2 = '';

    // Section F
    this.SF1_1 = null;
    this.SF2_1 = null;
    this.SF3_1 = null;
    this.SF4_1 = null;

    this.SF6_1 = null;
    this.SF7_1 = null;
    this.SF8_1 = null;
    this.SF9_1 = null;
    this.SF10_1 = null;
    this.SF11_1 = null;
    this.SF12_1 = null;
    this.SF13_1 = null;
    this.SF14_1 = null;
    this.SF15_1 = null;
    this.SF15_2 = null;
    this.SF15_3 = "";

    this.SF16_1 = null;
    this.SF17_1 = false;
    this.SH17_1_3_1 = null;

    // Section G
    this.SG1_1 = null;
    this.SG3_1 = null;
    this.SG4_1 = null;
    this.SG4_1_Y1 = null;
    this.SG4_1_Y2 = null;
    this.SG4_1_Y3 = null;
    this.SG4_1_Y4 = null;
    this.SG4_1_Y5 = null;
    this.SG4_1_Y6 = null;
    this.SG4_1_Y7 = null;

    // Section H
    this.SH1_1 = false;
    this.SH1_2 = false;
    this.SH1_3 = false;
    this.SH1_3_1 = null;
    this.SH1_4 = false;
    this.SH1_4_1 = null;
    this.SH2_1 = null;
    this.SH3_1 = null;
    this.SH4_1 = null;
    this.SH4_1_Y1 = false;
    this.SH4_1_Y1_1 = null;
    this.SH4_1_Y2 = false;
    this.SH4_1_Y2_1 = null;
    this.SH4_1_Y3 = false;
    this.SH4_1_Y3_1 = null;
    this.SH4_1_Y4 = false;
    this.SH4_1_Y4_1 = null;
    this.SH4_1_Y5 = false;
    this.SH4_1_Y_1 = null;
    this.SH4_1_Y6 = false;
    this.SH4_1_Y6_1 = null;
    this.SH4_1_Y7 = false;
    this.SH4_1_Y7_1 = null;
    this.SH5_1 = null;
    this.SH5_1_Y1 = null;
    this.SH6_1 = null;
    this.SH6_2 = null;
    this.SH6_3 = null;
    this.SH6_4 = null;
    this.SH7_1 = null;
    this.SH7_1_Y1 = null;

    // Section I
    this.SI1_1 = null;

    this.SI2_1 = null;
    this.SI2_1_Y1 = null;

    // Section J
    this.SJ1_1 = null;
    this.SJ2_1 = null;
    this.SJ2_1_N1 = null;
    this.SJ2_2 = null;
    this.SJ2_2_N1 = null;
    this.SJ2_3 = null;
    this.SJ2_3_1 = null;
    this.SJ2_4 = null;
    this.SJ2_4_1 = null;
    this.SJ2_5 = null;
    this.SJ2_5_1 = null;
    this.SJ2_5_3 = '';

    // Section K
    this.SK1_1 = null;
    this.SK2_1 = null;
    this.SK3_1 = '';
    this.SK3_2 = '';
    this.SK4_1 = null;
    this.SK4_1_Y1 = null;
    this.SK5_1 = null;
    this.SK5_1_Y1 = null;
    this.SK6_1 = null;
    this.SK6_1_Y1 = null;
    this.SK7_1 = null;
    this.SK7_1_Y1 = false;
    this.SK7_1_Y2 = false;
    this.SK7_1_Y3 = false;
    this.SK7_1_Y4 = false;
    this.SK7_1_Y5 = false;
    this.SK7_1_Y5_1 = '';
    this.SK7_2 = null;
    this.SK7_3 = null;
    this.SK8_1 = false;
    this.SK8_2 = false;
    this.SK8_3 = false;
    this.SK8_4 = false;

    //Section L
    this.SL1_1 = false;
    this.SL1_2 = false;
    this.SL1_3 = false;
    this.SL2_1 = null;
    this.SL3_1 = null;
    this.SL4_1 = null;
    this.SL5_1 = false;
    this.SL5_1_1 = false;
    this.SL5_1_2 = false;
    this.SL5_1_3 = false;
    this.SL5_2 = false;
    this.SL5_3 = false;
    this.SL6_1 = null;

    //Section M
    this.SM1_1 = null;
    this.SM2_1 = null;
    this.SM3_1 = null;
    this.SM4_1 = null;
    this.SM5_1 = null;
    this.SM5_1_1 = false;
    this.SM5_1_2 = false;

    //Section N
    this.SN1_1 = null;
    this.SN2_1 = null;
    this.SN3_1 = null;
    this.SN4_1 = null;
    this.SN5_1 = null;

    //Section O
    this.SO1 = null;
    this.SO1_Y1 = null;
    this.SO2 = null;
    this.SO2_Y1 = null;
    this.SO3 = null;
    this.SO3_N1 = null;
    this.SO4 = null;
    this.SO4_N1 = null;
    this.SO5 = null;
    this.SO5_Y1 = null;
    this.SO6 = null;
    this.SO6_N1 = null;
    this.SO7 = null;
    this.SO7_N1 = null;
    this.SO8 = null;
    this.SO8_N1 = null;
    this.SO9 = null;
    this.SO9_Y1 = null;
    this.SO10 = null;
    this.SO10_N1 = null;

    // Section P
    this.SP1 = null;
    this.SP2 = null;
    this.SP3_1 = null;
    this.SP3_2 = null;
    this.SP3_3 = null;
    this.SP5 = null;
    this.SP5_Y1 = null;
    this.SP6 = null;
    this.SP7 = null;
    this.SP8 = null;
    this.SP8_Y1 = null;
    this.SP9 = null;
    this.SP9_N = null;
    this.SP9_N1_1 = '';
    this.SP9_N1_2 = '';
    this.SP9_N1_3 = null;
    this.SP9_N1_4 = null;
    this.SP9_N2_1 = '';
    this.SP9_N2_2 = '';
    this.SP10 = null;
    this.SP10_Y1_1 = false;
    this.SP10_Y1_2 = false;
    this.SP10_Y1_3 = false;
    this.SP10_Y1_4 = false;
    this.SP10_Y1_4_1 = '';
    this.SP10_Y2 = null;
    this.SP10_Y3 = '';
    this.SP11 = null;
    this.SP11_Y1 = null;
    this.SP12 = null;
    this.SP12_Y1 = null;

    // Section Q
    this.SQ1 = null;
    this.SQ2 = null;
    this.SQ3 = null;
    this.SQ4 = null;
    this.SQ5 = null;
    this.SQ4_Y1 = null;
    this.SQ5_Y1 = null;

    // Section R
    this.SR1 = null;
    this.SR1_N1 = null;
    this.SR1_N2 = null;
    this.SR1_N3 = null;
    this.SR1_N4 = null;
    this.SR1_N4_Y1 = null;
    this.SR1_N5 = null;
    this.SR2 = null;
    this.SR2_Y1 = null;
    this.SR2_Y2 = null;
    this.SR2_Y3 = null;
    this.SR2_Y4 = null;
    this.SR2_Y5 = null;

    // Section S
    this.SS1 = null;
    this.SS1_Y1 = null;
    this.SS1_Y2 = null;
    this.SS1_Y3 = null;
    this.SS1_Y4 = null;
    this.SS1_Y5 = null;
    this.SS1_Y6_1 = false;
    this.SS1_Y6_2 = false;
    this.SS1_Y6_3 = false;
    this.SS1_Y6_4 = false;
    this.SS1_Y6_4_1 = null;
    this.SS1_Y7 = null;
    this.SS1_Y7_Y1 = null;
    this.SS1_Y7_Y1_1 = null;
    this.SS1_Y8 = null;
    this.SS1_Y8_1 = null;
    this.SS1_Y8_2 = null;
    this.SS1_Y8_3 = null;
    this.SS1_Y8_4 = null;
    this.SS1_Y8_4_1 = null;

    // Section T
    this.ST1_1 = null;
    this.ST2_1 = null;
    this.ST3_1 = null;
    this.ST4_1 = null;
    this.ST5_1 = null;

    this.IsDeleted = null;
    this.Type = 0;

    // Category
    this.Category1 = false;
    this.Category2 = false;
    this.Category3 = false;
    this.Category4 = false;
    this.Category5 = false;
    this.Category6 = false;
    this.Category7 = false;
    this.Category8 = false;
    this.Category9 = false;
    this.Category10 = false;
    this.CategorySub = null;
    this.CategorySub4 = null;
};

entity.CirbFormKeyWord = function () {
    var keyWords = [];
    keyWords["SA1"] = "sa1";

    keyWords["SB1"] = "SB1";
};

entity.IRBApplicationForm_PA = function () {
    this.PAID = null;
    this.IRBFormFK = null;
    this.UserProfileFK = null;
    this.InstitutionFK = null;
    this.DepartmentFK = null;
    this.Email = null;
    this.Seq = null;
};

entity.FormAttachments = function () {
    this.FormAttachmentID = null;
    this.FormFK = null;
    this.FileName = null;
    this.FieldName = null;
};

entity.CirbFormCategory = function () {
    this.Id = null;
    this.IRBApplicationFormId = null;
    this.Category1 = null;
    this.Category2 = null;
    this.Category3 = null;
    this.Category4 = null;
    this.Category5 = null;
    this.Category6 = null;
    this.Category7 = null;
    this.Category8 = null;
    this.Category9 = null;
    this.Category10 = null;
    this.CreateDate = new Date();
    this.CreateByLoginFK = null;
    this.CreateByUserFK = null;
    this.UpdateDate = new Date();
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
};

entity.ChecklistSubmission = function () {
    this.NewSubmissionId = null;
    this.StudyFK = null;
    this.B1 = null;
    this.B2 = null;
    this.B3 = null;
    this.BF1 = null;
    this.E1 = null;
    this.E2 = null;
    this.F1 = null;
    this.PJ1 = null;
    this.All = null;
    this.B1Notes = null;
    this.B2Notes = null;
    this.B3Notes = null;
    this.BF1Notes = null;
    this.E1Notes = null;
    this.E2Notes = null;
    this.F1Notes = null;
    this.PJ1Notes = null;
    this.AllNotes = null;
    this.Notes = null;
    this.IsDeleted = null;
    this.CreateDate = new Date();
    this.CreateByLoginFK = null;
    this.CreateByUserFK = null;
    this.UpdateDate = new Date();
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.FormFK = null;
    this.FormStatusFK = null;
    this.CompleteDate = null;
    this.CompletedPreliminaryDate = null;
};

entity.ChecklistExemptionReview = function () {
    this.ChecklistExemptionReviewId = null;
    this.StudyFK = null;
    this.Category1 = null;
    this.Category2 = null;
    this.Category3 = null;
    this.Category4 = null;
    this.Category5 = null;
    this.Category6 = null;
    this.Category7 = null;
    this.Category8 = null;
    this.Category9 = null;
    this.Category10 = null;
    this.Consent1 = null;
    this.Consent2 = null;
    this.Consent3 = null;
    this.Consent4 = null;
    this.Consent5 = null;
    this.Comments = null;
    this.Outcome = null;
    this.IsRequestWaiver = null;
    this.IsDeleted = null;
    this.CreateDate = new Date();
    this.CreateByLoginFK = null;
    this.CreateByUserFK = null;
    this.UpdateDate = new Date();
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.FormFK = null;
    this.FormStatusFK = null;
    this.CompleteDate = null;
    this.Outcome1 = null;
    this.ApprovalDate = null;
    this.ValidTillDate = null;
};

entity.ChecklistExpeditedReview = function () {
    this.ChecklistExpeditedReviewId = null;
    this.StudyId = null;
    this.A = null;
    this.B = null;
    this.C = null;
    this.D = null;
    this.E = null;
    this.F = null;
    this.G = null;
    this.H = null;
    this.ACategory = null;
    this.BCategory = null;
    this.CCategory = null;
    this.DCategory = null;
    this.ECategory = null;
    this.FCategory = null;
    this.GCategory = null;
    this.HCategory = null;
    this.Consent1 = null;
    this.Consent2 = null;
    this.Consent3 = null;
    this.Consent4 = null;
    this.Consent5 = null;
    this.Outcome = null;
    this.IsRequestWaiver = null;
    this.IsDeleted = null;
    this.CreateDate = new Date();
    this.CreateByLoginFK = null;
    this.CreateByUserFK = null;
    this.UpdateDate = new Date();
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.FormFK = null;
    this.FormStatusFK = null;
    this.CompleteDate = null;
    this.Outcome1 = null;
    this.ApprovalDate = null;
    this.ValidTillDate = null;
    this.Comments = null;
};
entity.LaseReportEntity = function () {
    this.DataStatus = entity.enums.DataStatus.Create;
    this.FormAttachments = [];
    this.CreateByLoginFK = null;
    this.lsae24 = null;
    this.CreateByUserFK = null;
    this.CreateDate = null;
    this.FormFK = null;
    this.IsDeleted = null;
    this.lsae1 = null;
    this.lsae10 = null;
    this.lsae11 = null;
    this.lsae12 = null;
    this.lsae13 = null;
    this.lsae14 = null;
    this.lsae15 = null;
    this.lsae16 = null;
    this.lsae17 = null;
    this.lsae18 = null;
    this.lsae19 = null;
    this.lsae2 = null;
    this.lsae20 = null;
    this.lsae21_1 = null;
    this.lsae21_2 = null;
    this.lsae21_3 = null;
    this.lsae22_1 = null;
    this.lsae22_2 = null;
    this.lsae23_1 = null;
    this.lsae23_2 = null;
    this.lsae23_3 = null;
    this.lsae25 = null;
    this.lsae26 = null;
    this.lsae27 = null;
    this.lsae28 = null;
    this.lsae3 = null;
    this.lsae4_1 = null;
    this.lsae4_2 = null;
    this.lsae4_3 = null;
    this.lsae5 = null;
    this.lsae6 = null;
    this.lsae7 = null;
    this.lsae8 = null;
    this.lsae9 = null;
    this.LSAEReportID = null;
    this.RecordVersion = null;
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.UpdateDate = null;
    this.Version = null;
};
entity.FormStatusEntity = function (code, dv, des) {
    this.Code = code;
    this.DisplayValue = dv;
    this.Description = des;
};

entity.ProtDevForm = function () {
    this.ProtDevID = null;
    this.DataStatus = entity.enums.DataStatus.Create;
    this.FormFK = null;
    this.ProtDev1_A = "";
    this.ProtDev1_B = "";
    this.ProtDev1_C = "";
    this.ProtDev1_D = "";
    this.ProtDev1_E = "";
    this.ProtDev1_F = "";
    this.ProtDev1_G = null;
    this.ProtDev1_G_Yes_Txt = "";
    this.ProtDev1_G_No_Txt = "";
    this.ProtDevConfirm = null;
    this.CreateDate = null;
    this.CreateByLoginFK = null;
    this.CreateByUserFK = null;
    this.UpdateDate = null;
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.RecordVersion = null;
};

entity.OtherReportable = function () {
    this.OtherReportableID = null;
    this.DataStatus = entity.enums.DataStatus.Create;
    this.Attachments = new Array();
    this.FormFK = null;
    this.Title = null;
    this.Content = null;
    this.IsDeleted = null;
    this.CreateDate = null;
    this.CreateByLoginFK = null;
    this.CreateByUserFK = null;
    this.UpdateDate = null;
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.RecordVersion = null;
};

entity.UserInfo = function () {
    this.FamilyName = "";
    this.GivenName = "";
    this.FullName = "";
    this.Institution = "";
    this.Department = "";
};

entity.FormGenericCommentDetail = function () {
    this.CIRBFormGenericCommentID = null;
    this.Type = null;
    this.ParentGenericCommentID = null;
    this.Comment = "";
    this.IsReplied = false;
};

entity.CIRBQuery = function () {
    this.ReviewQueryID = null;
    this.CIRBQuerySessionFK = null;
    this.FormFK = null;
    this.Sequence = 0;
    this.QueryType = null;
    this.QueryTypeLink = '';
    this.Query = '';
    this.Reply = '';
    this.SectionType = '';
    this.QueryByUserFK = null;
    this.ReplyByUserFK = null;
    this.CreateDate = null;
    this.CreateByLoginFK = null;
    this.CreateByLoginFK = null;
    this.UpdateDate = null;
    this.UpdateByLoginFK = null;
    this.UpdateByUserFK = null;
    this.IsDelete = false;
};

entity.CIRBReviewerQuery = function () {
    this.ReviewerQueryID = null;
    this.ReviewSessionReviewerFK = null;
    this.CIRBQuerySessionFK = null;
    this.QueryType = null;
    this.QueryTypeLinkFK = null;
    this.Query = '';
    this.SectionType = '';
    this.IsDelete = false;
};

entity.CIRBQuerySession = function () {
    this.CIRBQuerySessionID = null;
    this.FormFK = null;
    this.QuerySource = '';
    this.QuerySourceFK = null;
    this.ReceiverUserFK = null;
    this.IsDispatched = false;
    this.DespatchDate = null;
    this.DispatchByUserFK = null;
    this.DispatchByLoginFK = null;
    this.IsReplied = false;
    this.ReplyDate = null;
    this.ReplyByUserFK = null;
    this.ReplyByLoginFK = null;
};

entity.DecisionLetter = function() {
    this.LetterID = null;
    this.LetterType = null;
    this.LetterDate = null,
    this.CIRBRefNo = null;
    this.Department = null,
    this.PIFullName = null,
    this.InstitutionName = null,
    this.PISurname = null,
    this.PIStudyMemberID = null;
    this.SourceFormID = null;
    this.StudyID = null,
    this.LetterTitle = null;
    this.StudyTitle = null;
    this.ChairmanFK = null;
    this.ChairmanName = null;
    this.LetterBody = null;
    this.FormFK = null;
    this.LetterCc = null;
};
