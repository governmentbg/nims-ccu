using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class Table6SheetRow : IndicatorSheetRow
    {
        public Table6SheetRow(Dictionary<int, string> row)
        {
            this.SheetType = Type.Table6;

            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.MapNodeId = this.GetIntCellValue(4, row);

            // Indicator fields
            this.Code = this.GetStringCellValue(8, row);
            this.Name = this.GetStringCellValue(10, row);
            this.Measure = this.GetStringCellValue(11, row);
            this.Kind = this.GetStringCellValue(9, row);
            this.IsAggregated = this.GetStringCellValue(12, row);
            this.ReportingType = this.GetStringCellValue(13, row);

            // MapNodeIndicator fields
            this.FinanceSource = this.GetStringCellValue(14, row);
            this.RegionCategory = this.GetStringCellValue(15, row);

            this.MilestoneTargetTotalValue = this.GetDecimalCellValue(16, row);
            this.MilestoneTargetMenValue = this.GetDecimalCellValue(17, row);
            this.MilestoneTargetWomenValue = this.GetDecimalCellValue(18, row);

            this.FinalTargetTotalValue = this.GetDecimalCellValue(21, row);
            this.FinalTargetMenValue = this.GetDecimalCellValue(19, row);
            this.FinalTargetWomenValue = this.GetDecimalCellValue(20, row);

            this.DataSource = this.GetStringCellValue(22, row);
            this.Description = this.GetStringCellValue(23, row);
            this.NameAlt = this.GetStringCellValue(24, row);
        }
    }
}
