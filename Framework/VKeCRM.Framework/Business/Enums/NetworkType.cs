using System;
using System.Runtime.Serialization;

namespace VKeCRM.Framework.Business.Enums
{
    [Serializable]
    [DataContract(Name = "NetworkType")]
    public enum NetworkType
    {
        [EnumMember]
        Internet = 1,

        [EnumMember]
        Intranet = 2
    }
}