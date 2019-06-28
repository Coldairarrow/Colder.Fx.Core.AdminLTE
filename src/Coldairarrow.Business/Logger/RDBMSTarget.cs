using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Business
{
    public class RDBMSTarget : BaseTarget
    {
        protected override void Write(LogEventInfo logEvent)
        {
            base.Write(logEvent);
        }
    }
}
