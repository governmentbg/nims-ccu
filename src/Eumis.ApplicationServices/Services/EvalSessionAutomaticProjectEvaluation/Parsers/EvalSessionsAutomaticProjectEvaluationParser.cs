using Eumis.ApplicationServices.Services.Core.Parsers;
using Eumis.Domain.Projects.DataObjects;
using System.Collections.Generic;
using System.IO;

namespace Eumis.ApplicationServices.Services.EvalSessionAutomaticProjectEvaluation.Parsers
{
    internal class EvalSessionsAutomaticProjectEvaluationParser : BaseExcelParser, IEvalSessionsAutomaticProjectEvaluationParser
    {
        private const int NumberOfColumns = 2;

        public IList<AutomaticProjectVersionXmlDO> ParseExcel(
            Stream excelStream,
            List<string> errors)
        {
            var projectRows = new List<AutomaticProjectVersionXmlDO>();
            var rows = this.ReadExcel(excelStream, NumberOfColumns);

            int rowNumber = 0;

            foreach (var row in rows)
            {
                rowNumber++;

                if (rowNumber == 1)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(row[1])
                    && string.IsNullOrWhiteSpace(row[2]))
                {
                    break;
                }

                string projectRegNumber = row[1];
                if (string.IsNullOrWhiteSpace(projectRegNumber))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Номер на ПП",
                        "задължително поле"));
                }

                decimal? amount = null;

                if (string.IsNullOrWhiteSpace(row[2]))
                {
                    errors.Add($"Невалидни данни на ред \"{rowNumber}\". При създаване на \"Нова версия на ПП\" е нужно да бъде попълнено поле Стойност/Сума");
                }

                if (!string.IsNullOrWhiteSpace(row[2]))
                {
                    if (!this.GetDecimal(row, 2, out amount))
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Стойност/Сума",
                            "очаквана стойност: число"));
                    }
                    else if (amount < 0)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Стойност/Сума",
                            "очаквана стойност: положително число"));
                    }
                }

                projectRows.Add(new AutomaticProjectVersionXmlDO(
                    projectRegNumber,
                    amount));
            }

            return projectRows;
        }
    }
}
