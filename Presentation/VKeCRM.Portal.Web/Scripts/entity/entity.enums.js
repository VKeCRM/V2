vkecrm.RegisterNameSpace("entity.enums");

entity.enums = {
    DataStatus: {
        Create: 0,
        Edit: 1,
        View: 2,
        Amendment: 3,
        Unlock: 4,
        AmendmentEdit: 5,
        AmendmentView: 6,
        AmendmentUnlock: 7,
        Print: 8,
        ChecklistUnlock: 9
    },
    AmendmentStatus: {
        Create: 0,
        Edit: 1,
        View: 2,
        Unlock: 3
    },

    SectionButtons: {
        Back: 1,
        Next: 2,
        SectionA: 3,
        SectionB: 4,
        SectionC: 5,
        SectionD: 6,
        SectionE: 7,
        SectionF: 8,
        SectionG: 9,
        SectionH: 10,
        SectionI: 11,
        SectionJ: 12,
        SectionK: 13,
        SectionL: 14,
        SectionM: 15,
        SectionN: 16,
        SectionO: 17,
        SectionP: 18,
        SectionQ: 19,
        SectionR: 20,
        SectionS: 21,
        SectionT: 22,
        SectionU: 23
    },

    SectionStep: {
        SelectionApp: 0,
        SectionA: 1,
        SectionB: 2,
        SectionC: 3,
        SectionD: 4,
        SectionE: 5,
        SectionF: 6,
        SectionG: 7,
        SectionH: 8,
        SectionI: 9,
        SectionJ: 10,
        SectionK: 11,
        SectionL: 12,
        SectionM: 13,
        SectionN: 14,
        SectionO: 15,
        SectionP: 16,
        SectionQ: 17,
        SectionR: 18,
        SectionS: 19,
        SectionT: 20,
        SectionU: 21,
        SectionEndorser: 22,
        SectionFinalise: 23,
        SectionQueryList: 24,
        SectionEndorserForDR: 25,
        SectionEndorserForIR: 26
    },

    StudyStatusReport: {
        StudyStatusReport: 1,
        StudyReactivationReprot: 2,
        StudyClosureReport: 3
    },

    PageStep: {
        CIRBReport: 1,
        CIRBApplication: 2,
        LASEReporting: 3,
        Reactivation: 4,
        ClosureReport: 5,
        ReplyList: 6
    },

    FormType: {
        CIRBApplicationForm: 'af',
        CIRBExemptionApplicationForm: 'eaf'
    },
    QueryType: {
        Section: "FormSection",
        Attachment: "FormAttachment",
        Generic: "Generic"
    },
    SectionKeyWord: {
        // Section A
        SA1: "A1",

        // Section B
        SB1: "B1",
        SB2: "B2",
        SB3: "B3",

        // Section C
        SC1: "C1",
        SC2: "C2",
        SC3: "C3",

        // Section D
        SD1: "D1",
        SD2: "D2",

        // SectionE
        SE1: "E1",
        SE2: "E2",
        SE3: "E3",
        SE4: "E4",

        // Section F
        SF1: "F1",
        SF2: "F2",
        SF3: "F3",
        SF4: "F4",
        SF5: "F5",
        SF6: "F6",
        SF7: "F7",
        SF8: "F8",
        SF9: "F9",
        SF10: "F10",
        SF11: "F11",
        SF12: "F12",
        SF13: "F13",
        SF14: "F14",
        SF15: "F15",
        SF16: "F16",
        SF17: "F17",


        // Section G
        SG1: "G1",
        SG2: "G2",
        SG3: "G3",
        SG4: "G4",


        // Section H
        SH1: "H1",
        SH2: "H2",
        SH3: "H3",
        SH4: "H4",
        SH5: "H5",
        SH6: "H6",
        SH7: "H7",

        // Section I
        SI1: "I1",
        SI2: "I2",


        // Section J
        SJ1: "J1",
        SJ2: "J2",

        // Section K
        SK1: "K1",
        SK2: "K2",
        SK3: "K3",
        SK4: "K4",
        SK5: "K5",
        SK6: "K6",
        SK7: "K7",
        SK8: "K8",


        //Section L
        SL1: "L1",
        SL2: "L2",
        SL3: "L3",
        SL4: "L4",
        SL5: "L5",
        SL6: "L6",

        //Section M
        SM1: "M1",
        SM2: "M2",
        SM3: "M3",
        SM4: "M4",
        SM5: "M5",


        //Section N
        SN1: "N1",
        SN2: "N2",
        SN3: "N3",
        SN4: "N4",
        SN5: "N5",

        //Section O
        SO1: "O1",
        SO2: "O2",
        SO3: "O3",
        SO4: "O4",
        SO5: "O5",
        SO6: "O6",
        SO7: "O7",
        SO8: "O8",
        SO9: "O9",
        SO10: "O10",

        // Section P
        SP1: "P1",
        SP2: "P2",
        SP3: "P3",
        SP4: "P4",
        SP5: "P5",
        SP6: "P6",
        SP7: "P7",
        SP8: "P8",
        SP9: "P9",
        SP10: "P10",
        SP11: "P11",
        SP12: "P12",

        // Section Q
        SQ1: "Q1",
        SQ2: "Q2",
        SQ3: "Q3",
        SQ4: "Q4",
        SQ5: "Q5",
        // Section R
        SR1: "R1",
        SR2: "R2",

        // Section S
        SS1: "S1",

        // Section T
        ST1: "T1",
        ST2: "T2",
        ST3: "T3",
        ST4: "T4",
        ST5: "T5"
    },

    StudyStatus: {
        SSR1: "SSR1",
        SSR2: "SSR2",
        SSR3: "SSR3"

    },

    Checklist: {
        NEWSUB: "NEWSUB",
        EXPREV: "EXPREV",
        EXPREVCONSENT: "EXPREVCONSENT",
        EXEREV: "EXEREV",
        EXEREVCONSENT: "EXEREVCONSENT"
    },

    GenericCommentType: {
        RDO: 0,
        DR: 1,
        IR: 2,
        PIRepley: 3
    },

    LetterStatus:
    {
        Create: 0,
        Edit: 1,
        View: 2
    }

};