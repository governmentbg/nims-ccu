using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class Table12SheetRow : IndicatorSheetRow
    {
        public Table12SheetRow(Dictionary<int, string> row)
        {
            this.SheetType = Type.Table12;

            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.MapNodeId = this.GetIntCellValue(13, row);

            // Indicator fields
            this.Code = this.GetStringCellValue(16, row);
            this.Name = this.GetStringCellValue(17, row);
            this.Measure = this.GetStringCellValue(18, row);
            this.IsAggregated = this.GetStringCellValue(19, row);
            this.ReportingType = this.GetStringCellValue(20, row);

            // MapNodeIndicator fields
            this.BaseTotalValue = this.GetDecimalCellValue(21, row);
            this.BaseMenValue = this.GetDecimalCellValue(22, row);
            this.BaseWomenValue = this.GetDecimalCellValue(23, row);
            this.BaseYear = this.GetIntCellValue(24, row);

            this.TargetTotalValue = this.GetDecimalCellValue(25, row);
            this.TargetMenValue = this.GetDecimalCellValue(26, row);
            this.TargetWomenValue = this.GetDecimalCellValue(27, row);

            this.DataSource = this.GetStringCellValue(28, row);
            this.ReportingFrequancy = this.GetStringCellValue(29, row);
            this.NameAlt = this.GetStringCellValue(30, row);
        }
    }
}
