using Eumis.Domain.OperationalMap.Programmes;
using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments.Parsers
{
    public interface IProgrammeApplicationDocumentParser
    {
        IList<ProgrammeApplicationDocument> ParseExcel(int programmeId, Stream excelStream, IList<string> programmeDocuments, out IList<string> errors);
    }
}
