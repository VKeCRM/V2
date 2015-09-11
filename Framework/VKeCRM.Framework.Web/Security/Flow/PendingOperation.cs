using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;

namespace VKeCRM.Framework.Web.Flow
{
    [Serializable()]
    public class PendingOperation
    {
        #region Private Fields

        private string _suspendUrl = string.Empty;
        private DateTime _suspendedOn = DateTime.UtcNow; 
        private string _resumeUrl = string.Empty;
        private Hashtable _stateBag = new Hashtable();
        private bool _result = false;

        #endregion Private Fields

        #region Properties

        public string SuspendUrl
        {
            get
            {
                return _suspendUrl;
            }
            set
            {
                _suspendUrl = value ?? string.Empty;
            }
        }

        public string ResumeUrl
        {
            get
            {
                return _resumeUrl;
            }
            set
            {
                _resumeUrl = value ?? string.Empty;
            }
        }

        public bool Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }

        public DateTime SuspendedOn
        {
            get
            {
                return _suspendedOn;
            }
            set
            {
                _suspendedOn = value;
            }
        }

        public Hashtable StateBag
        {
            get
            {
                return _stateBag;
            }

            set
            {
                if (value!=null)
                    _stateBag = value;
            }
        }

        #endregion Properties
    }
}
