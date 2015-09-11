using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VKeCRM.Common.Dictionary.Model;

namespace VKeCRM.Common.Dictionary
{
    public class ApplicationForm
    {
        #region Property

        public Dictionary<string, Section> SectionsDictionary { get; private set; }

        #endregion

        #region Constructor & Destructor

        public ApplicationForm()
        {
            SectionsDictionary = new Dictionary<string, Section>();

            var partRelyes = new List<PartRelys>();

            var sectionA = new Section("SectionA");
            var partRely = new PartRelys { PartsName = "All", Type = "AF" };
            var partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            var parts = CreatePart(2, sectionA, partRelyes);
            sectionA.Parts = parts;
            sectionA.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };

            var sectionB = new Section("SectionB");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(3, sectionB, partRelyes);
            sectionB.Parts = parts;
            sectionB.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };

            var sectionC = new Section("SectionC");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(1, sectionC, partRelyes);
            sectionC.Parts = parts;
            sectionC.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };

            var sectionD = new Section("SectionD");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(2, sectionD, partRelyes);
            sectionD.Parts = parts;
            sectionD.RelyTypes = new List<string>
            {
                "AF"
            };

            var sectionE = new Section("SectionE");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(4, sectionE, partRelyes);
            sectionE.Parts = parts;
            sectionE.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };

            var sectionF = new Section("SectionF");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "F1,F2,F8,F9,F10,F11,F12,F13,F14,F15,F16", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(17, sectionF, partRelyes);
            sectionF.Parts = parts;
            sectionF.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };


            var sectionG = new Section("SectionG");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(4, sectionG, partRelyes);
            sectionG.Parts = parts;

            var sectionH = new Section("SectionH");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(7, sectionH, partRelyes);
            sectionH.Parts = parts;
            sectionH.RelyTypes = new List<string>
            {
                "AF"
            };

            var sectionI = new Section("SectionI");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(2, sectionI, partRelyes);
            sectionI.Parts = parts;
            sectionI.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };

            var sectionJ = new Section("SectionJ");
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRelyE);
            parts = CreatePart(2, sectionJ, partRelyes);
            sectionJ.Parts = parts;
            sectionJ.RelyTypes = new List<string>
            {
                "EAF"
            };

            var sectionK = new Section("SectionK");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(8, sectionK, partRelyes);
            sectionK.Parts = parts;
            sectionK.RelyTypes = new List<string>
            {
                "AF"
            };

            var sectionL = new Section("SectionL");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(6, sectionL, partRelyes);
            sectionL.Parts = parts;
            sectionL.RelyParts.Add(sectionK.Parts.Find(p => p.PartName == "K7"));

            var sectionM = new Section("SectionM");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(5, sectionM, partRelyes);
            sectionM.Parts = parts;
            sectionM.RelyParts.Add(new Part("K7"));

            var sectionN = new Section("SectionN");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(5, sectionN, partRelyes);
            sectionN.Parts = parts;
            sectionN.RelyParts.Add(new Part("K7"));

            var sectionO = new Section("SectionO");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(10, sectionO, partRelyes);
            sectionO.Parts = parts;
            sectionO.RelyParts.Add(new Part("K7"));

            var sectionP = new Section("SectionP");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(12, sectionP, partRelyes);
            sectionP.Parts = parts;
            sectionP.RelyParts.Add(sectionK.Parts.Find(p => p.PartName == "F17"));
            sectionP.RelyParts.Add(sectionJ.Parts.Find(p=>p.PartName == "J2"));

            var sectionQ = new Section("SectionQ");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(5, sectionQ, partRelyes);
            sectionQ.Parts = parts;
            sectionQ.RelyParts.Add(sectionK.Parts.Find(p => p.PartName == "F17"));
            sectionQ.RelyParts.Add(sectionJ.Parts.Find(p => p.PartName == "J2"));

            var sectionR = new Section("SectionR");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(2, sectionR, partRelyes);
            sectionR.Parts = parts;
            sectionR.RelyTypes = new List<string>
            {
                "AF"
            };

            var sectionS = new Section("SectionS");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(1, sectionS, partRelyes);
            sectionS.Parts = parts;
            sectionS.RelyTypes = new List<string>
            {
                "AF"
            };

            var sectionT = new Section("SectionT");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            parts = CreatePart(5, sectionT, partRelyes);
            sectionT.Parts = parts;
            sectionT.RelyTypes = new List<string>
            {
                "AF"
            };

            var sectionU = new Section("SectionU");
            partRely = new PartRelys { PartsName = "All", Type = "AF" };
            partRelyE = new PartRelys { PartsName = "All", Type = "EAF" };
            partRelyes.Clear();
            partRelyes.Add(partRely);
            partRelyes.Add(partRelyE);
            parts = CreatePart(1, sectionU, partRelyes);
            sectionU.Parts = parts;
            sectionU.RelyTypes = new List<string>
            {
                "AF",
                "EAF"
            };


            SectionsDictionary.Add("A", sectionA);
            SectionsDictionary.Add("B", sectionB);
            SectionsDictionary.Add("C", sectionC);
            SectionsDictionary.Add("D", sectionD);
            SectionsDictionary.Add("E", sectionE);
            SectionsDictionary.Add("F", sectionF);
            SectionsDictionary.Add("G", sectionG);
            SectionsDictionary.Add("H", sectionH);
            SectionsDictionary.Add("I", sectionI);
            SectionsDictionary.Add("J", sectionJ);
            SectionsDictionary.Add("K", sectionK);
            SectionsDictionary.Add("L", sectionL);
            SectionsDictionary.Add("M", sectionM);
            SectionsDictionary.Add("N", sectionN);
            SectionsDictionary.Add("O", sectionO);
            SectionsDictionary.Add("P", sectionP);
            SectionsDictionary.Add("Q", sectionQ);
            SectionsDictionary.Add("R", sectionR);
            SectionsDictionary.Add("S", sectionS);
            SectionsDictionary.Add("T", sectionT);
            SectionsDictionary.Add("U", sectionU);
        }

        #endregion

        #region Private Method

        private List<Part> CreatePart(int count, Section section, List<PartRelys> partRelys = null)
        {
            var parts = new List<Part>();
            for (int i = 1; i <= count; i++)
            {
                var part = new Part(section.SectionName.Last().ToString(CultureInfo.InvariantCulture) + i)
                {
                    BelongSection = section
                };
                if (partRelys != null)
                {
                    foreach (var partRelyse in partRelys)
                    {
                        var partsName = partRelyse.PartsName.Split(',');
                        foreach (var s in partsName)
                        {
                            if (s == "All")
                            {
                                part.RelyTypes.Add(partRelyse.Type);
                            }
                            else if (s == part.PartName)
                            {
                                part.RelyTypes.Add(partRelyse.Type);
                            }
                        }
                    }
                }
                parts.Add(part);
            }
            return parts;
        }

        #endregion


        private class PartRelys
        {
            public string Type { get; set; }

            public string PartsName { get; set; }
        }
    }
}
