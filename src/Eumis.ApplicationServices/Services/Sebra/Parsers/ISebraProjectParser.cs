using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.Sebra.Parsers
{
    public interface ISebraProjectParser
    {
        IList<string> ParseExcel(Stream excelStream);
    }
}
