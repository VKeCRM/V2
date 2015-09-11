using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKeCRM.Framework.Web.State
{
    public struct ApplicationStateKey
    {
        public const string LoggerManager = "LoggerManager";
        public const string SiteUnmapUrlDictionary = "SiteUnmapUrlDictionary";

		public const string UserNameForSecurityUpgrade = "UserNameForSecurityUpgrade";
		public const string UserNameForUpdateContactDetails = "UserNameForUpdateContactDetails";
		public const string UserNameForUpdateMailingAddress = "UserNameForUpdateMailingAddress";
		public const string UserNameForAnswerSecurityQuestion = "UserNameForAnswerSecurityQuestion";
		public const string LeadTrackingSessionId = "LeadTrackingSessionId";
    }
}
