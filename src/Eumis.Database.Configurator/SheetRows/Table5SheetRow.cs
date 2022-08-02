using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class Table5SheetRow : IndicatorSheetRow
    {
        public Table5SheetRow(Dictionary<int, string> row)
        {
            this.SheetType = Type.Table5;

            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.MapNodeId = this.GetIntCellValue(8, row);

            // Indicator fields
            this.Code = this.GetStringCellValue(11, row);
            this.Name = this.GetStringCellValue(12, row);
            this.Measure = this.GetStringCellValue(13, row);
            this.IsAggregated = this.GetStringCellValue(14, row);
            this.ReportingType = this.GetStringCellValue(15, row);

            // MapNodeIndicator fields
            this.FinanceSource = this.GetStringCellValue(16, row);
            this.RegionCategory = this.GetStringCellValue(17, row);

            this.TargetTotalValue = this.GetDecimalCellValue(18, row);
            this.TargetMenValue = this.GetDecimalCellValue(19, row);
            this.TargetWomenValue = this.GetDecimalCellValue(20, row);

            this.DataSource = this.GetStringCellValue(21, row);
            this.ReportingFrequancy = this.GetStringCellValue(22, row);
            this.NameAlt = this.GetStringCellValue(23, row);
        }
    }
}
