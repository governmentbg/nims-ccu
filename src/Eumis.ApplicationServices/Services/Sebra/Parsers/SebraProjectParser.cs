using Eumis.ApplicationServices.Services.Core.Parsers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.Sebra.Parsers
{
    internal class SebraProjectParser : BaseExcelParser, ISebraProjectParser
    {
        private const int NumberOfColumns = 1;

        private static readonly IDictionary<int, string> Headers = new Dictionary<int, string>()
        {
            { 1, "Номер на ПП" },
        };

        public IList<string> ParseExcel(Stream excelStream)
        {
            var rows = this.ReadExcel(excelStream, NumberOfColumns);

            if (!this.AreHeadersValid(rows.FirstOrDefault(), Headers))
            {
                return new List<string>();
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
