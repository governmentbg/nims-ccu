using System;
using System.Collections.Generic;
using Eumis.Domain.EvalSessions;

namespace Eumis.ApplicationServices.Services.EvalSessionSheetXml
{
    public interface IEvalSessionSheetXmlService
    {
        bool CanUpdateSheet(Guid sheetGid);

        Domain.EvalSessions.EvalSessionSheetXml CreateSheet(EvalSessionSheet sessionSheet);
    }
}
