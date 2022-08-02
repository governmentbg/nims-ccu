using System.Collections.Generic;
using Eumis.Database.Configurator.SheetRows.Abstract;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class MapNodeBudgetSheetRow : SheetRow
    {
        public MapNodeBudgetSheetRow(Dictionary<int, string> row)
        {
            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.ProgrammePriorityId = this.GetIntCellValue(4, row);

            this.BudgetPeriodYear = this.GetStringCellValue(8, row);
            this.FinanceSource = this.GetStringCellValue(9, row);
            this.EuAmount = this.GetDecimalCellValue(10, row);
            this.BgAmount = this.GetDecimalCellValue(11, row);
            this.EuReservedAmount = this.GetDecimalCellValue(12, row);
            this.BgReservedAmount = this.GetDecimalCellValue(13, row);
        }

        public int? ProgrammeId { get; private set; }

        public int? ProgrammePriorityId { get; private set; }

        public string BudgetPeriodYear { get; private set; }

        public string FinanceSource { get; private set; }

        public decimal? EuAmount { get; private set; }

        public decimal? BgAmount { get; private set; }

        public decimal? EuReservedAmount { get; private set; }

        public decimal? BgReservedAmount { get; private set; }
    }
}
