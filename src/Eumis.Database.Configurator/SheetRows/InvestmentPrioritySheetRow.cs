using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class InvestmentPrioritySheetRow : SheetRow
    {
        public InvestmentPrioritySheetRow(Dictionary<int, string> row)
        {
            this.InterventionCategoryId = this.GetIntCellValue(0, row);
            this.InterventionCategoryCode = this.GetStringCellValue(1, row);
            this.InterventionCategoryName = this.GetStringCellValue(2, row);

            this.InvestmentPriorityId = this.GetIntCellValue(3, row);
            this.InvestmentPriorityCode = this.GetStringCellValue(4, row);
            this.InvestmentPriorityName = this.GetStringCellValue(5, row);
            this.InvestmentPriorityNameAlt = this.GetStringCellValue(6, row);
        }

        public int? InterventionCategoryId { get; private set; }

        public string InterventionCategoryCode { get; private set; }

        public string InterventionCategoryName { get; private set; }

        public int? InvestmentPriorityId { get; private set; }

        public string InvestmentPriorityCode { get; private set; }

        public string InvestmentPriorityName { get; private set; }

        public string InvestmentPriorityNameAlt { get; private set; }
    }
}
