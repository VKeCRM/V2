using System.Collections.Generic;
using System.Linq;

namespace VKeCRM.Common.Dictionary.Model
{
    public class Part
    {
        public string PartName { get; private set; }

        public string Description { get; set; }

        public List<string> RelyTypes { get; private set; }

        public List<Part> RelyParts { get; private set; }

        public Section BelongSection { get; set; }

        public bool IsDisplay { get; set; }

        public Part(string name)
        {
            PartName = name;
            RelyTypes = new List<string>();
            RelyParts = new List<Part>();
        }

        public void ChangeDisplay(Part relyPart, string relyType = "")
        {
            if (!string.IsNullOrEmpty(relyType))
            {
                IsDisplay = RelyTypes.Any(irbaApplicationType => relyType == irbaApplicationType);
                return;
            }

            if (RelyParts == null)
            {
                IsDisplay = true;
                return;
            }
                
            IsDisplay = RelyParts.Any(part => part.BelongSection == relyPart.BelongSection && part.PartName == relyPart.PartName);
        }
    }
}
