using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Eumis.ApplicationServices.Services.Core.Parsers
{
    public abstract class BaseExcelParser
    {
        protected const string FileNotMatchingTemplateError = "Избраният файл не отговаря на шаблона.";
        protected const string EmptyNomStr = "-";
        private const NumberStyles OpenXmlNumberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowExponent;
        private const NumberStyles DecimalNumberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowExponent;
        private static readonly CultureInfo OpenXmlParseCulture = CultureInfo.InvariantCulture;

        protected IEnumerable<string[]> ReadExcel(Stream excelStream, int numberOfColumns)
        {
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(excelStream, false))
            {
                var workbookPart = spreadsheetDocument.WorkbookPart;
                var workbook = workbookPart.Workbook;

                SharedStringItem[] sharedStrings = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                // search only visible sheets
                var sheet = workbook.Descendants<Sheet>().First();

                var worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);

                using (OpenXmlReader reader = OpenXmlReader.Create(worksheetPart))
                {
                    while (reader.Read())
                    {
                        if (reader.ElementType != typeof(Row))
                        {
                            continue;
                        }

                        string[] row = new string[numberOfColumns + 1];

                        reader.ReadFirstChild();
                        do
                        {
                            if (reader.ElementType != typeof(Cell))
                            {
                                continue;
                            }

                            Cell c = (Cell)reader.LoadCurrentElement();

                            int cellColumnIndex = this.GetColumnNumber(c.CellReference);

                            if (cellColumnIndex > numberOfColumns)
                            {
                                continue;
                            }

                            row[cellColumnIndex] = this.GetCellValue(c, sharedStrings);
                        }
                        while (reader.ReadNextSibling());

                        yield return row;
                    }

                    yield break;
                }
            }
        }

        protected bool AreHeadersValid(string[] row, IDictionary<int, string> headers)
        {
            foreach (var key in headers.Keys)
            {
                if (string.IsNullOrWhiteSpace(row[key]))
                {
                    return false;
                }

                if (headers[key].ToUpperInvariant() != row[key].ToUpperInvariant().Trim())
                {
                    return false;
                }
            }

            return true;
        }

        protected string GetColumnName(string cellReference)
        {
            for (int i = 0; i <= cellReference.Length; i++)
            {
                if (char.IsNumber(cellReference[i]))
                {
                    // get the letter only part of the cell reference
                    return cellReference.Substring(0, i);
                }
            }

            throw new Exception("Invalid cellReference.");
        }

        protected int GetColumnNumber(string cellReference)
        {
            string columnName = this.GetColumnName(cellReference);

            int columnNumber = 0;
            int factor = 1;
            for (int i = columnName.Length - 1; i >= 0; i--)
            {
                columnNumber += factor * (cellReference[i] - 'A' + 1);
                factor *= 26;
            }

            return columnNumber;
        }

        protected string GetCellValue(Cell cell, SharedStringItem[] sharedStrings)
        {
            string cellValue = string.Empty;
            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                if (cell.CellValue != null && !string.IsNullOrWhiteSpace(cell.CellValue.Text))
                {
                    SharedStringItem ssi = sharedStrings[int.Parse(cell.CellValue.Text, OpenXmlNumberStyle, OpenXmlParseCulture)];
                    if (ssi.Text != null)
                    {
                        cellValue = ssi.Text.Text;
                    }
                }
            }
            else if (cell.DataType != null && cell.DataType == CellValues.InlineString)
            {
                if (cell.InlineString != null && cell.InlineString.Text != null)
                {
                    cellValue = cell.InlineString.Text.Text;
                }
            }
            else if (cell.DataType != null && (cell.DataType == CellValues.Date || cell.DataType == CellValues.Number))
            {
                if (cell.CellValue != null && !string.IsNullOrWhiteSpace(cell.CellValue.Text))
                {
                    cellValue = double.Parse(cell.CellValue.Text, OpenXmlNumberStyle, OpenXmlParseCulture).ToString(CultureInfo.InvariantCulture);
                }
            }
            else
            {
                if (cell.CellValue != null)
                {
                    cellValue = cell.CellValue.InnerText;
                }
            }

            return cellValue;
        }

        protected string GetError(int rowIndex, string cellName, string details = null)
        {
            var error = $"Невалидна стойност в полето \"{cellName}\" на ред \"{rowIndex}\"";

            if (!string.IsNullOrEmpty(details))
            {
                error = $"{error} ({details})";
            }

            return error;
        }

        protected bool GetInteger(string[] row, int column, out int? value)
        {
            var str = row[column];
            if (!string.IsNullOrWhiteSpace(str))
            {
                int val;
                if (int.TryParse(str, out val))
                {
                    value = val;
                    return true;
                }
                else
                {
                    value = null;
                    return false;
                }
            }
            else
            {
                value = null;
                return true;
            }
        }

        protected bool GetDecimal(string[] row, int column, out decimal? value)
        {
            var str = row[column];
            if (!string.IsNullOrWhiteSpace(str))
            {
                double val;
                if (double.TryParse(str, DecimalNumberStyle, CultureInfo.InvariantCulture, out val))
                {
                    try
                    {
                        value = Convert.ToDecimal(val);
                    }
                    catch (OverflowException)
                    {
                        value = null;
                        return false;
                    }

                    return true;
                }
                else
                {
                    value = null;
                    return false;
                }
            }
            else
            {
                value = null;
                return true;
            }
        }

        protected bool IsEmptyRow(string[] row)
        {
            foreach (var column in row)
            {
                if (!string.IsNullOrWhiteSpace(column))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
