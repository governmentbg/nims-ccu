using System;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation
{
    public interface IEvalSessionAutomaticProjectEvaluationService
    {
        IList<string> CanExecuteEvalSessionAutomaticProjectEvaluation(int evalSessionId);

        IList<string> ExecuteEvalSessionAutomaticProjectEvaluation(
            int evalSessionId,
            byte[] version,
            Guid blobKey);
    }
}
