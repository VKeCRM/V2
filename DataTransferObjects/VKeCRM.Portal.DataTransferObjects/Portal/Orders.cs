using System;
using System.Runtime.Serialization;

namespace VKeCRM.Portal.DataTransferObjects
{
    /// <summary>
    /// Custom properties of the Data Trasfer Object
    /// </summary>
    [Serializable]
    [DataContract]
    public class Orders
    {
        #region Declarations
        #endregion

        #region Properties

        [DataMember]
        public Guid Id
        {
            get;
            set;
        }

        [DataMember]
        public string OrderNo
        {
            get;
            set;
        }

        [DataMember]
        public string OrderName
        {
            get;
            set;
        }

        #endregion
    }
}
