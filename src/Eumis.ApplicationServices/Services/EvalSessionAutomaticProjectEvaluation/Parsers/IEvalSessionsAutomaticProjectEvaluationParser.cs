using Eumis.Domain.Projects.DataObjects;
using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation.Parsers
{
    public interface IEvalSessionsAutomaticProjectEvaluationParser
    {
        IList<AutomaticProjectVersionXmlDO> ParseExcel(
            Stream excelStream,
            List<string> errors);
    }
}
