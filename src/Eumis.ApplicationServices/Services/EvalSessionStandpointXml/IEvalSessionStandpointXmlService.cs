using System;
using Eumis.Domain.EvalSessions;

namespace Eumis.ApplicationServices.Services.EvalSessionSheetXml
{
    public interface IEvalSessionStandpointXmlService
    {
        bool CanUpdateStandpoint(Guid standpointGid);

        Domain.EvalSessions.EvalSessionStandpointXml CreateStandpoint(EvalSessionStandpoint sessionStandpoint);
    }
}
