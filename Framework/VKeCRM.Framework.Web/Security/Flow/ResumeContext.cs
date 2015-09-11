using System.Collections;

namespace VKeCRM.Framework.Web.Flow
{
    public class ResumeContext
    {
        #region Private Fields

        private Hashtable _stateBag = new Hashtable();
        private bool _result = false;
        private string _suspendUrl = string.Empty;
        private string _resumeUrl = string.Empty;

        #endregion // Private Fields

        #region Properties

        public object this[string name]
        {
            get { return _stateBag[name]; }
            set { _stateBag[name] = value; }
        }

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        #region ReadOnly Properties

        public string SuspendUrl
        {
            get { return _suspendUrl; }
        }

        public string ResumeUrl
        {
            get { return _resumeUrl; }
        }

        public Hashtable StateBag
        {
            get { return _stateBag; }
        }

        #endregion // ReadOnly Properties

        #endregion // Properties

        #region Constructors

        private ResumeContext()
        {
        }

        public ResumeContext(string suspendUrl, string resumeUrl, bool result, Hashtable stateBag)
        {
            _suspendUrl = suspendUrl;
            _resumeUrl = resumeUrl;
            _result = result;
            _stateBag = stateBag;
        }

        #endregion // Constructors
    }
}
