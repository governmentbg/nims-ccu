using Eumis.Data.EvalSessions.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.EvalSession
{
    public interface IEvalSessionService
    {
        EvalSessionLoadedProjectsFromFileVO ParseProjectsExcelFile(int evalSessionId, Guid blobKey);
    }
}
