using System.Collections.Generic;
using System.Linq;

namespace VKeCRM.Common.Dictionary.Model
{
    public class Section
    {
        public string SectionName { get; private set; }

        public List<Part> Parts { get; set; }

        public List<string> RelyTypes { get; set; }

        public List<Part> RelyParts { get; set; }

        public string Description { get; set; }

        public bool IsDisplay { get; private set; }

        public Section(string sectionName)
        {
            SectionName = sectionName;
            IsDisplay = false;
            Parts = new List<Part>();
            RelyParts = new List<Part>();
            RelyTypes = new List<string>();
        }

        public void ChangeDisplay(string relyPart, string relyType = "")
        {
            if (!string.IsNullOrEmpty(relyType))
            {
                IsDisplay = RelyTypes.Any(irbaApplicationType => relyType == irbaApplicationType);
                if(IsDisplay)
                    return;
            }

            if (string.IsNullOrEmpty(relyPart))
            {
                IsDisplay = false;
                return;
            }
                
            IsDisplay = RelyParts.Any(part => part !=null && part.PartName == relyPart);
        }
    }
}
