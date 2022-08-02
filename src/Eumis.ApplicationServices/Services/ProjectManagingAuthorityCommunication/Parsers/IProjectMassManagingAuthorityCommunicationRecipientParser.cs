using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication.Parsers
{
    public interface IProjectMassManagingAuthorityCommunicationRecipientParser
    {
        IList<string> ParseExcel(Stream excelStream, out IList<string> errors);
    }
}
