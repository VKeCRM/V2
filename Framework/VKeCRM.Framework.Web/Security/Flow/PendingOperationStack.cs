using System;
using System.Collections.Generic;

namespace VKeCRM.Framework.Web.Flow
{
    [Serializable()]
    public class PendingOperationStack : Stack<PendingOperation>
    {
    }
}
