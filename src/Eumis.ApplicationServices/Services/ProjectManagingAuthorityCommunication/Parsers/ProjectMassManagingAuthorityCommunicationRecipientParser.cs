using Eumis.ApplicationServices.Services.Core.Parsers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ProjectManagingAuthorityCommunication.Parsers
{
    internal class ProjectMassManagingAuthorityCommunicationRecipientParser : BaseExcelParser, IProjectMassManagingAuthorityCommunicationRecipientParser
    {
        private const int NumberOfColumns = 1;

        private static readonly IDictionary<int, string> Headers = new Dictionary<int, string>()
        {
            { 1, "Номер на ПП" },
        };

        public IList<string> ParseExcel(Stream excelStream, out IList<string> errors)
        {
            errors = new List<string>();
            var rows = this.ReadExcel(excelStream, NumberOfColumns);

            if (!this.AreHeadersValid(rows.FirstOrDefault(), Headers))
            {
                errors.Add(ApplicationServicesTexts.EvalSessionProjectParser_FileNotMatchingTemplateError);
            }

            IList<string> recipientRegNumbers = rows
                .Where(row => !string.IsNullOrWhiteSpace(row[1]))
                .Select(row => row[1].Trim())
                .Skip(1)
                .Distinct()
                .ToList();

            return recipientRegNumbers;
        }
    }
}
