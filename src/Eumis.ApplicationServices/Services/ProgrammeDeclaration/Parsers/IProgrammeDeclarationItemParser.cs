using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.ProgrammeDeclaration.Parsers
{
    public interface IProgrammeDeclarationItemParser
    {
        IList<(int? OrderNum, string Content)> ParseExcel(Stream excelStream, out List<string> errors);
    }
}
