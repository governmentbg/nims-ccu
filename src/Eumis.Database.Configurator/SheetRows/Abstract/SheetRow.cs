using System.Collections.Generic;
using System.Globalization;

namespace Eumis.Database.Configurator.SheetRows.Abstract
{
    internal abstract class SheetRow
    {
        protected int? GetIntCellValue(int columnIndex, Dictionary<int, string> row)
        {
            int? value = null;

            if (row.ContainsKey(columnIndex) && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                value = int.Parse(row[columnIndex]);
            }

            return value;
        }

        protected bool? GetBoolCellValue(int columnIndex, Dictionary<int, string> row)
        {
            bool? value = null;

            if (row.ContainsKey(columnIndex) && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                value = bool.Parse(row[columnIndex]);
            }

            return value;
        }

        protected string GetStringCellValue(int columnIndex, Dictionary<int, string> row)
        {
            string value = null;

            if (row.ContainsKey(columnIndex) && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                value = row[columnIndex].Trim();
            }

            return value;
        }

        protected decimal? GetDecimalCellValue(int columnIndex, Dictionary<int, string> row)
        {
            decimal? value = null;

            if (row.ContainsKey(columnIndex) && !string.IsNullOrWhiteSpace(row[columnIndex]))
            {
                decimal dValue;

                if (decimal.TryParse(row[columnIndex], NumberStyles.Number, CultureInfo.GetCultureInfo("bg-BG"), out dValue))
                {
                    value = dValue;
                }
            }

            return value;
        }
    }
}
