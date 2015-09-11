using System.Collections;

namespace VKeCRM.Framework.Web.Flow
{
    public class SuspendContext
    {
        #region Private Fields

        private Hashtable _stateBag = new Hashtable();
        private string _suspendUrl = string.Empty;

        #endregion // Private Fields

        #region Properties

        public object this[string name]
        {
            get { return _stateBag[name]; }
            set { _stateBag[name] = value; }
        }

        #region ReadOnly Properties

        public string SuspendUrl
        {
            get { return _suspendUrl; }
        }

        public Hashtable StateBag
        {
            get { return _stateBag; }
        }

        #endregion // ReadOnly Properties

        #endregion // Properties
        
        #region Constructors

        private SuspendContext()
        {
        }

        public SuspendContext(string suspendUrl)
        {
            _suspendUrl = suspendUrl;
        }

        #endregion // Constructors
    }
}
