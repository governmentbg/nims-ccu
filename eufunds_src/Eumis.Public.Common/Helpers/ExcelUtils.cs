using Eumis.Public.Common.Export;
using Eumis.Public.Common.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Common.Helpers
{
    public static class ExcelUtils
    {
        public static ExportCell ToExportCell(this decimal money)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberDecimalSeparator = ".";
            nfi.NumberGroupSeparator = string.Empty;
            return new ExportCell { Value = Math.Round(money, 2).ToString("N", nfi), IsMoney = true };
        }

        public static ExportCell ToExportCell(this decimal? money, bool showResult = true)
        {
            if (!showResult)
            {
                return new ExportCell { Value = string.Empty };
            }

            if (!money.HasValue)
            {
                return 0m.ToExportCell();
            }

            return money.Value.ToExportCell();
        }

        public static ExportCell ToExportCell(this string data, bool showResult = true)
        {
            if (data == null || !showResult)
            {
                return new ExportCell { Value = string.Empty };
            }

            return new ExportCell { Value = data };
        }

        public static ExportCell ToExportCell(this IList<string> data)
        {
            if (data == null)
            {
                return new ExportCell { Value = string.Empty };
            }

            return new ExportCell { Value = string.Join(",", data) };
        }

        public static ExportCell ToExportCell(this IList<DateTime> data) => data.Select(e => e.ToString("MM.yyyy")).ToList().ToExportCell();

        public static ExportCell ToExportCell(this int number)
        {
            NumberFormatInfo nfi = new NumberFormatInfo { NumberGroupSeparator = string.Empty, NumberDecimalDigits = 0 };

            return new ExportCell { Value = number.ToString("n", nfi), IsNumber = true };
        }

        public static ExportCell ToExportCell(this int? number)
        {
            if (number.HasValue)
            {
                return number.Value.ToExportCell();
            }

            return 0.ToExportCell();
        }

        public static ExportCell ToExportCell(this DateTime? date)
        {
            if (!date.HasValue)
            {
                return string.Empty.ToExportCell();
            }

            return date.Value.ToExportCell();
        }

        public static ExportCell ToExportCell(this DateTime date)
        {
            return new ExportCell { Value = Helper.DateToBgFormat(date), IsDateTime = true };
        }

        public static ExportCell ToExportCell(this Enum enumeration) => new ExportCell { Value = enumeration.GetEnumDescription() };

        public static ExportCell ToExportHeaderCell(this string data)
        {
            var cell = data.ToExportCell();

            cell.IsBold = true;
            cell.IsItalic = true;

            return cell;
        }

        public static ExportCell ToExportHeaderCell(this decimal money)
        {
            var cell = money.ToExportCell();
            cell.IsBold = true;
            cell.IsItalic = true;

            return cell;
        }

        public static ExportCell ToExportHeaderCell(this decimal? money)
        {
            if (!money.HasValue)
            {
                return 0m.ToExportHeaderCell();
            }

            return money.Value.ToExportHeaderCell();
        }
    }
}
