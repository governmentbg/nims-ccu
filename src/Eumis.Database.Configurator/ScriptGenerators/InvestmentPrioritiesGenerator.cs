using Eumis.Database.Configurator.DbRows;
using Eumis.Database.Configurator.SheetRows;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.ScriptGenerators
{
    internal class InvestmentPrioritiesGenerator : ScriptsGenerator
    {
        public InvestmentPrioritiesGenerator(Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary)
            : base(sheetsRowsDictionary)
        {
            this.ExcelSheetRows = this.GetExcelSheetRows("InvestmentPriorities");
        }

        protected List<Dictionary<int, string>> ExcelSheetRows { get; set; }

        protected override List<DbTableData> GetDbTableData()
        {
            List<IDbRow> dbRows = new List<IDbRow>();

            int? currentInterventionCategoryId = null;
            foreach (var row in this.ExcelSheetRows)
            {
                InvestmentPrioritySheetRow sheetRow = new InvestmentPrioritySheetRow(row);

                if (sheetRow.InterventionCategoryId.HasValue)
                {
                    currentInterventionCategoryId = sheetRow.InterventionCategoryId.Value;
                }

                if (sheetRow.InvestmentPriorityId.HasValue)
                {
                    InvestmentPriorityDbRow tableRow = new InvestmentPriorityDbRow
                    {
                        InvestmentPriorityId = sheetRow.InvestmentPriorityId.Value,
                        InterventionCategoryId = currentInterventionCategoryId.Value,
                        Code = sheetRow.InvestmentPriorityCode,
                        Name = sheetRow.InvestmentPriorityName,
                        NameAlt = sheetRow.InvestmentPriorityNameAlt,
                    };

                    dbRows.Add(tableRow);
                }
            }

            return new List<DbTableData>
            {
                new DbTableData(InvestmentPriorityDbRow.DbTableName, InvestmentPriorityDbRow.UseIdentityInsert, dbRows),
            };
        }
    }
}
