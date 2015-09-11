vkecrm.RegisterNameSpace('entity.keyvalues');

entity.keyvalues = {
    sectionKeys: null,
    sectionValues: null,
    sections: {
        "SA": "Section A: Protocol Title & Protocol Administrators",
        "SB": "Section B : Study Sites, Study Team & Submission Board",
        "SC": "Section C: Conflict of Interest",
        "SD": "Section D: Nature of Research",
        "SE": "Section E: Study Funding Information",
        "SF": "Section F: Research Methodology",
        "SG": "Section G: Research Details-Clinical Trials",
        "SH": "Section H: Recruitment Details",
        "SI": "Section I: Study Sites and Recruitment Targets",
        "SJ": "Section J: Exempt Review Criteria",
        "SK": "Section K: Research Participant Characteristics",
        "SL": "Section L: Research Participants – Pregnant Women, Fetuses & Neonates",
        "SM": "Section M: Research Participants – Children",
        "SN": "Section N: Research Participants - Prisoners",
        "SO": "Section O: Research Participant – Cognitively Impaired Persons",
        "SP": "Section P: Consent Process – Consent obtained",
        "P_": "Section P: Consent Process – Consent obtained",
        "SQ": "Section Q: Consent Process- Waiver of Consent",
        "SR": "Section R: Research Data Confidentiality",
        "SS": "Section S: Biological Materials Usage & Storage",
        "ST": "Section T: Data & Safety Monitoring",
        "SU": "Section U: Declaration of Principal Investigator",
        "RS": "Study Status Report",
        "RR": "Study Reactivation Report",
        "RC": "Study Closure Report",
        "Ot": "Other Attachment"
    },
    sectionTitles: [
        {
            key: "A1",
            value: "Please enter the Full Protocol Title and Protocol Number (if available) for this Study"
        },
        {
            key: "A2",
            value: "You may assign Protocol Administrators for this study below."
        },
        {
            key: "B1",
            value: "Please select the study sites."
        },
        {
            key: "B2",
            value: "Study Team members"
        },
        {
            key: "B3",
            value: "Submission Board and other IRB"
        },
        {
            key: "D1",
            value: "Please select one category that best describes your research activities."
        },
        {
            key: "D2",
            value: "Is this a US FDA IND/ IDE study or data is intended to be reported to FDA in support of an IND/IDE Application?"
        },
        {
            key: "E1",
            value: "Please give information regarding the study's Funding source or Sponsor information."
        },
        {
            key: "E2",
            value: "Payment of CIRB review fees (This section will only appear if E1 check Pharmaceutical/Industry Sponsored. Not mandatory fields)"
        },
        {
            key: "E3",
            value: "Who will be responsible for the payment and compensation of injury or illness arising from participation of participants in the study?"
        },
        {
            key: "E4",
            value: "Who will be responsible for research-related costs? For sponsored studies, please list the costs that will be borne by the sponsor."
        },
        {
            key: "F1",
            value: "Please provide an abstract of your proposed research (Up to 300 words)."
        },
        {
            key: "F2",
            value: "What are the Specific Aims and hypothesis of this study?"
        },
        {
            key: "F3",
            value: "Please briefly describe the background to the current study proposal. Critically evaluate the existing knowledge and specifically identify the gap that the proposed study is intended to fill."
        },
        {
            key: "F4",
            value: "Please provide a list of relevant references."
        },
        {
            key: "F5",
            value: "Please attach at least two relevant publications that support the conduct of the study."
        },
        {
            key: "F6",
            value: "Preliminary Studies/ Progress Reports. Please provide an account of the Principal Investigator＊s preliminary studies (if any) pertinent to this application."
        },
        {
            key: "F7",
            value: "Please state concisely the importance of the research described in this application by relating the specific aims to the long term objectives."
        },
        {
            key: "F8",
            value: "Discuss in detail the experimental design and procedures to be used to accomplish the specific aims of the study. If this study involves a retrospective medical record review, please specify the period of data collection. "
        },
        {
            key: "F9",
            value: "Please provide details on sample size and power calculation and the means by which data will be analyzed and interpreted (If applicable)."
        },
        {
            key: "F10",
            value: "List all activities that are carried out as part of research in this study. Please state/list all procedures involved in this research study and attach the data collection form (if any) which will be used for CIRB review. Please attach the data collection form, if applicable."
        },
        {
            key: "F11",
            value: "Please describe the participant＊s visits (frequency and procedures involved). For studies with multiple visits, please attach study schedule."
        },
        {
            key: "F12",
            value: "Discuss the potential difficulties and limitations of the proposed procedures and alternative approaches to achieve the aims."
        },
        {
            key: "F13",
            value: "What are the Potential Risks to Participants?"
        },
        {
            key: "F14",
            value: "What are the Potential Benefits (direct as well as indirect) to participants? Indirect benefit may refer to the medical knowledge gained in the future, from the research."
        },
        {
            key: "F15",
            value: "What is the estimated timeline for this study?"
        },
        {
            key: "F16",
            value: "Does this study have a Study Protocol?"
        },
        {
            key: "F17",
            value: "The PI is responsible for ensuring that all Study Participants give informed consent before enrolling into the study. "
        },
        {
            key: "G1",
            value: "Describe the study protocol(s) to be used. Include information of the study drug / device / surgical procedures that will be used in the trial. If the study involves the use of study drug / device, describe how you plan to manage the receipt, handling, storage, utilization, and disposal of the study drug/device."
        },
        {
            key: "G2",
            value: "Please attach the Investigator's Brochure or local product information sheet/leaflet, as applicable."
        },
        {
            key: "G3",
            value: "Describe standard/alternative treatments used at your institution for this condition."
        },
        {
            key: "G4",
            value: "Is this a placebo controlled trial?"
        },
        {
            key: "H1",
            value: "How will potential participants be identified? Please tick all the applicable boxes."
        },
        {
            key: "H2",
            value: "Who will make the first contact with participant?"
        },
        {
            key: "H3",
            value: "How will the participant be contacted?"
        },
        {
            key: "H4",
            value: "Will any advertising/ recruitment materials be used to recruit research participants?"
        },
        {
            key: "H5",
            value: "Will any other recruitment strategies be used (e.g. talks in public places, societies etc.)?"
        },
        {
            key: "H6",
            value: "What is the Recruitment   Period   (if applicable)?   Please   provide   us with   the approximate recruitment period."
        },
        {
            key: "H7",
            value: "How long will the participants be directly involved in the study (if applicable)? This includes the time from the screening procedures till completion of follow-up tests or examinations."
        },
        {
            key: "I1",
            value: "Please state the target number of research participants to be recruited for each study site in Singapore.  If exact numbers are not available, please give an approximate number range in the Recruitment Target."
        },
        {
            key: "I2",
            value: "Is this study part of an international study?"
        },
        {
            key: "J1",
            value: "Please describe and state the source of your samples/data."
        },
        {
            key: "J2",
            value: "Criteria to qualify for Exemption from CIRB review:"
        },
        {
            key: "K1",
            value: "Please list the inclusion criteria for research participants in this study."
        },
        {
            key: "K2",
            value: "Please list the exclusion criteria for research participants in this study."
        },
        {
            key: "K3",
            value: "Please state the age group of the research participants. "
        },
        {
            key: "K4",
            value: "Are there any recruitment restrictions based on the gender of the research participants (e.g. only males will be included in this study)? If ＆Yes＊, please provide a rationale for this gender restriction"
        },
        {
            key: "K5",
            value: "Are there any recruitment restrictions based on the race of the research participants (e.g. only Chinese participants will be included in this study)? If ＆Yes＊, please provide a rationale for this race restriction."
        },
        {
            key: "K6",
            value: "Do the potential research participants have a dependent relationship with the study team (e.g. doctor-patient, employee-employer, head-subordinate, student-teacher, departmental staff relationship)?"
        },
        {
            key: "K7",
            value: "Does the study involve any vulnerable research participants?"
        },
        {
            key: "K7",
            value: "Does the study involve any of the following?"
        },
        {
            key: "L1",
            value: "Please indicate if your research involves:"
        },
        {
            key: "L2",
            value: "For research studies that involve pregnant women, foetuses and/or neonates, the research must meet specific criteria. Please provide protocol specific information explaining how your proposed research project meets ALL of the following criteria."
        },
        {
            key: "L3",
            value: "Describe if the risk to the foetus is the least possible in order to achieve the research objectives."
        },
        {
            key: "L4",
            value: "Describe the additional safeguards that will be provided to protect the rights, safety and welfare of these vulnerable participants."
        },
        {
            key: "L5",
            value: "Special Informed Consent Requirements (Check all that apply)*"
        },
        {
            key: "L6",
            value: "Assurances by Principal Investigator"
        },
        {
            key: "M1",
            value: "Describe if appropriate studies have been conducted on animals and adults first, and data is available to assess risks to children participating in the research."
        },
        {
            key: "M2",
            value: "Please justify the need to involve children. Can the research question be answered through alternative means (e.g. involving adults only)?"
        },
        {
            key: "M3",
            value: "Describe how the relation of potential benefits to risks is at least as favourable as that presented by alternative approaches."
        },
        {
            key: "M4",
            value: "Describe any additional safeguards that will be provided to protect the rights, safety and welfare of these vulnerable participants."
        },
        {
            key: "M5",
            value: "What are the provisions for obtaining the child's assent and parental permission? "
        },
        {
            key: "N1",
            value: "Research involving Prisoners - Please provide protocol specific information explaining how your proposed research project meets the following criteria."
        },
        {
            key: "N2",
            value: "Is there any evidence of duress, coercion, or undue influence in the particular prison(s) from which participants will be recruited?"
        },
        {
            key: "N3",
            value: "Are potential research related risks to prisoners comparable to risks that would be accepted by non-prisoner volunteers?"
        },
        {
            key: "N4",
            value: "Describe the systems in place to ensure participant and data confidentiality."
        },
        {
            key: "N5",
            value: "Describe any additional safeguards that will be provided to protect the rights, safety and welfare of these vulnerable participants?"
        },
        {
            key: "O1",
            value: "Is this research relevant to this group of participants who are cognitively impaired?"
        },
        {
            key: "O2",
            value: "Are adequate procedures for evaluating the mental status of prospective participants employed to determine if they are capable of providing consent?"
        },
        {
            key: "O3",
            value: "Will legal representatives (LRs) be approached to give consent on behalf of the individuals judged incapable of providing consent?"
        },
        {
            key: "O4",
            value: "Will a separate Assent Form be used for cognitively impaired persons?"
        },
        {
            key: "O5",
            value: "If a participant is incapable of giving valid consent, will his/her objection to participation be overridden?"
        },
        {
            key: "O6",
            value: "Will an advocate or consent monitor be appointed to ensure that the preferences of potential participants are elicited and respected?"
        },
        {
            key: "O7",
            value: "Will an advocate or consent monitor be appointed to ensure the continuing agreement of participants to participate as the research progresses?"
        },
        {
            key: "O8",
            value: "Will the patient's physician or other health care provider be consulted before any individual is invited to participate in the research?"
        },
        {
            key: "O9",
            value: "Is there a possibility that the request to participate itself, may provoke anxiety, stress or any other serious negative response?"
        },
        {
            key: "O10",
            value: "Are there any other additional safeguards in place to protect the rights, safety and well-being of these vulnerable participants?"
        },
        {
            key: "P1",
            value: "Describe when the consent process will take place with the potential participant."
        },
        {
            key: "P2",
            value: "Where will the consent process take place with the potential participant (e.g. in room ward, outpatient clinic etc.)? Please justify why the place chosen for the consent process is suitable."
        },
        {
            key: "P3",
            value: "Please describe the consent process as follows:"
        },
        {
            key: "P5",
            value: "Does your study involve potential vulnerable participants whereby obtaining informed consent form from the participant is not possible and informed consent is required from a Legal Representative (LR)?"
        },
        {
            key: "P6",
            value: "Please describe the provisions to protect the \"privacyinterest\" of the participants (e.g. consent will be obtained in a separate room, free from intrusion and participants are comfortable with the proposed settings)."
        },
        {
            key: "P7",
            value: "Besides the Informed Consent Form, will any other materials or documents be used to explain the study to potential Research Participants (e.g. scripts, hand outs, brochures, videos, logs etc.)?"
        },
        {
            key: "P8",
            value: "Will research participants receive any monetary payments (including transportation allowances) or gifts for their participation in the study?"
        },
        {
            key: "P9",
            value: "Will consent be documented in the form of a written and signed Research Participant Information Sheet and Consent Form? "
        },
        {
            key: "P10",
            value: "Consent Language"
        },
        {
            key: "P11",
            value: "Will the study be recruiting participants under emergency situations, when prior consent of the participant is not possible, and the consent of the participant's legal representative, if present, should be requested?"
        },
        {
            key: "P12",
            value: "Do you have any additional comments regarding the Informed Consent process? "
        },
        {
            key: "Q1",
            value: "The study poses no more than minimal risk to research participants."
        },
        {
            key: "Q2",
            value: "Waiver of informed consent will not adversely affect the rights and welfare of research participants. Please justify how your study meets this criterion."
        },
        {
            key: "Q3",
            value: "The study cannot be practically conducted without the waiver of informed consent. Please justify how your study meets this criterion (e.g. the participants are no longer on follow-up, lost to follow-up or deceased)."
        },
        {
            key: "Q4",
            value: "Whenever appropriate, will the research participants be provided with additional pertinent information after participation?"
        },
        {
            key: "Q5",
            value: "Do you have any additional comments supporting the waiver of informed consent?  If yes, Please describe."
        },
        {
            key: "R1",
            value: "Will coded / anonymous research data be sent to the study sponsor (e.g. pharmaceutical- sponsored studies)?"
        },
        {
            key: "R2",
            value: "Will any part of the study procedures be recorded on audiotape, film/video, or other electronic medium?"
        },
        {
            key: "S1",
            value: "Will any biological materials (such as blood or tissue) be used in the study? This includes both prospectively collected and existing biological material."
        },
        {
            key: "T1",
            value: "The purpose of the Data and Safety Monitoring Plan is to ensure the safety and well-being of participants, and the integrity of the data collected for the study. Depending on the type and risk level of the study, this may include the Principal Investigator, experts within the department or institution, independent consultants or a combination of the said persons."
        },
        {
            key: "T2",
            value: "Please describe the frequency of review (e.g. daily, weekly, quarterly) and what data (e.g. adverse events/serious adverse events) will be monitored for safety."
        },
        {
            key: "T3",
            value: "How is data integrity monitored to ensure that study data is authentic, accurate and complete, and if the data correlates with the case report forms?"
        },
        {
            key: "T4",
            value: "Please describe the stopping criteria for the research study based on efficacy, futility and safety criteria."
        },
        {
            key: "T5",
            value: "Please state the route of dissemination of any data and safety information to the study sites, as well as the person/team responsible for doing so. "
        },
        {
            key: 'Generic',
            value: 'Generic'
        }
    ],

    PageNames: {
        cirbform: 'Application Form',
        cirbstudyclosurereport: 'Study Closure Report',
        cirbstudyreactivationreport: 'Study Reactivation Report',
        cirbstudystatusreport: 'Study Status Report',
        otherreportable: 'Other Reportable Event',
        lasereporting: 'LSAE Report',
        protdev: 'Protocol Deviation/Non-Compliance Report',
        decisionletter: 'Decision Letter'
    },

    DecisionLetterTitles: {
        approval: 'SINGHEALTH CENTRALISED INSTITUTIONAL REVIEW BOARD (CIRB) APPROVAL',
        exemption: 'SINGHEALTH CENTRALISED INSTITUTIONAL REVIEW BOARD (CIRB) EXEMPTION',
        'non-research': '  ',
        'non-approval': 'SINGHEALTH CENTRALISED INSTITUTIONAL REVIEW BOARD (CIRB) NON-APPROVAL',
        amendment: 'SINGHEALTH CENTRALISED INSTITUTIONAL REVIEW BOARD (CIRB) APPROVAL OF AMENDMENT'
    },

    //Search
    GetSectionKeys: function () {
        var $this = entity.keyvalues;

        if (!$this.sectionKeys) {
            $this.sectionKeys = $($this.sectionTitles).map(function (d) {
                return d.key;
            });
        }

        return $this.sectionKeys;
    },

    GetSectionValues: function () {
        var $this = entity.keyvalues;

        if (!$this.sectionValues) {
            $this.sectionValues = $($this.sectionTitles).map(function (d) {
                return d.value;
            });
        }

        return $this.sectionValues;
    },

    GetSectionValueByKey: function (key) {
        var $this = entity.keyvalues;

        var l = $this.sectionTitles.length;
        for (var i = 0; i < l; i++) {
            if ($this.sectionTitles[i].key == key.toUpperCase()) {
                return $this.sectionTitles[i].value;
            }
        }
        return "Generic";
    },

    ChairmanOutcomes: {
        1: "Approved",
        2: "Modifications/ Clarification required",
        3: "Recommend for Full Board Review",
        4: "Review by Another Reviewer required",
        5: "Non-Research"
    }
}