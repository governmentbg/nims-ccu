using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class Table4SheetRow : IndicatorSheetRow
    {
        public Table4SheetRow(Dictionary<int, string> row)
        {
            this.SheetType = Type.Table4;

            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.MapNodeId = this.GetIntCellValue(8, row);

            // Indicator fields
            this.Code = this.GetStringCellValue(11, row);
            this.Name = this.GetStringCellValue(12, row);
            this.Measure = this.GetStringCellValue(13, row);
            this.IsAggregated = this.GetStringCellValue(14, row);
            this.ReportingType = this.GetStringCellValue(15, row);

            // MapNodeIndicator fields
            this.RegionCategory = this.GetStringCellValue(16, row);

            this.BaseTotalValueString = this.GetStringCellValue(18, row);
            this.BaseTotalValue = this.GetDecimalCellValue(18, row);
            this.BaseMenValue = this.GetDecimalCellValue(19, row);
            this.BaseWomenValue = this.GetDecimalCellValue(20, row);
            this.BaseYear = this.GetIntCellValue(22, row);

            this.TargetTotalValueString = this.GetStringCellValue(23, row);
            this.TargetTotalValue = this.GetDecimalCellValue(23, row);
            this.TargetMenValue = this.GetDecimalCellValue(24, row);
            this.TargetWomenValue = this.GetDecimalCellValue(25, row);

            this.BaseTargetValueMeasure = this.GetStringCellValue(21, row);
            this.DataSource = this.GetStringCellValue(26, row);
            this.ReportingFrequancy = this.GetStringCellValue(27, row);
            this.CommonBaseIndicator = this.GetStringCellValue(17, row);
            this.NameAlt = this.GetStringCellValue(28, row);
        }
    }
}
