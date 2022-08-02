using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class Table3SheetRow : IndicatorSheetRow
    {
        public Table3SheetRow(Dictionary<int, string> row)
        {
            this.SheetType = Type.Table3;

            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.MapNodeId = this.GetIntCellValue(13, row);

            // Indicator fields
            this.Code = this.GetStringCellValue(15, row);
            this.Name = this.GetStringCellValue(16, row);
            this.Measure = this.GetStringCellValue(17, row);
            this.IsAggregated = this.GetStringCellValue(18, row);
            this.ReportingType = this.GetStringCellValue(19, row);

            // MapNodeIndicator fields
            this.RegionCategory = this.GetStringCellValue(20, row);
            this.BaseTotalValue = this.GetDecimalCellValue(21, row);
            this.BaseYear = this.GetIntCellValue(22, row);
            this.TargetTotalValue = this.GetDecimalCellValue(23, row);
            this.DataSource = this.GetStringCellValue(24, row);
            this.ReportingFrequancy = this.GetStringCellValue(25, row);
            this.NameAlt = this.GetStringCellValue(26, row);
        }
    }
}
