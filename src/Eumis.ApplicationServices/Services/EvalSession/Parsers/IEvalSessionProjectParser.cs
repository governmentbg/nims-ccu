using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.EvalSession.Parsers
{
    public interface IEvalSessionProjectParser
    {
        IList<string> ParseExcel(Stream excelStream);
    }
}
