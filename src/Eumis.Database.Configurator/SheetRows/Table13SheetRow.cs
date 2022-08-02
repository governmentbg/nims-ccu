using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class Table13SheetRow : IndicatorSheetRow
    {
        public Table13SheetRow(Dictionary<int, string> row)
        {
            this.SheetType = Type.Table13;

            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.MapNodeId = this.GetIntCellValue(4, row);

            // Indicator fields
            this.Code = this.GetStringCellValue(8, row);
            this.Name = this.GetStringCellValue(9, row);
            this.Measure = this.GetStringCellValue(10, row);
            this.IsAggregated = this.GetStringCellValue(11, row);
            this.ReportingType = this.GetStringCellValue(12, row);

            // MapNodeIndicator fields
            this.TargetTotalValue = this.GetDecimalCellValue(13, row);
            this.TargetMenValue = this.GetDecimalCellValue(14, row);
            this.TargetWomenValue = this.GetDecimalCellValue(15, row);

            this.DataSource = this.GetStringCellValue(16, row);
            this.NameAlt = this.GetStringCellValue(17, row);
        }
    }
}
