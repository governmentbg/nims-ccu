using Eumis.ApplicationServices.Services.Core.Parsers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.ProgrammeDeclaration.Parsers
{
    internal class ProgrammeDeclarationItemParser : BaseExcelParser, IProgrammeDeclarationItemParser
    {
        private const int NumberOfColumns = 2;

        private static readonly IDictionary<int, string> Headers = new Dictionary<int, string>()
        {
            { 1, "№ на ред" },
            { 2, "Съдържание" },
        };

        public IList<(int? OrderNum, string Content)> ParseExcel(Stream excelStream, out List<string> errors)
        {
            errors = new List<string>();
            IList<(int? OrderNum, string Content)> itemRows = new List<(int? OrderNum, string Content)>();

            var rows = this.ReadExcel(excelStream, NumberOfColumns);

            if (!this.AreHeadersValid(rows.FirstOrDefault(), Headers))
            {
                errors.Add(ApplicationServicesTexts.Common_Parsers_FileNotMatchingTemplateError);
                return itemRows;
            }

            int rowNumber = 0;
            foreach (var row in rows)
            {
                rowNumber++;

                if (rowNumber == 1)
                {
                    continue;
                }
                else if (this.IsEmptyRow(row))
                {
                    break;
                }

                int? orderNum = null;
                if (this.GetInteger(row, 1, out orderNum))
                {
                    if (!orderNum.HasValue)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "№ на ред",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "№ на ред",
                        "очаквана стойност: цяло число"));
                }

                string content = row[2];
                if (string.IsNullOrWhiteSpace(content))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Съдържание",
                        "задължително поле"));
                }

                itemRows.Add((orderNum, content));
            }

            return itemRows;
        }
    }
}
