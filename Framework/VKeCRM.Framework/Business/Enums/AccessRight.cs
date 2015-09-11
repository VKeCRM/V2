using System.ComponentModel;
using System.Runtime.Serialization;

namespace VKeCRM.Framework.Business.Enums
{
    [DataContract(Name = "AccessRight")]
    public enum AccessRight
    {
        [EnumMember]
        [Description("ACTIONABLE")]
        Actionable = 1,

        [EnumMember]
        [Description("VIEWABLE")]
        Viewable = 2,

        [EnumMember]
        [Description("HIDDEN")]
        Hidden = 3
    }
}