using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Eumis.Domain.OperationalMap.Programmes;

namespace Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments.Parsers
{
    public class ProgrammeApplicationDocumentParser : IProgrammeApplicationDocumentParser
    {
        private const string EmptyNomStr = "-";
        private const string FileNotMatchingTemplateError = "Избраният файл не отговаря на шаблона.";

        private const NumberStyles OpenXmlNumberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowExponent;

        private static readonly CultureInfo OpenXmlParseCulture = CultureInfo.InvariantCulture;
        private static readonly Regex ExtensionsRegex = new Regex(@"^[,;\s]*(\.[a-zA-Z0-9]{1,8})(\s*[,;\s]\s*\.[a-zA-Z0-9]{1,8}[,;\s]*)*$");

        private static readonly Dictionary<int, string> Headers = new Dictionary<int, string>
        {
            { 1, "№" },
            { 2, "Тип на документа" },
            { 3, "Разширение" },
            { 4, "Електронен подпис" },
        };

        public IList<ProgrammeApplicationDocument> ParseExcel(int programmeId, Stream excelStream, IList<string> programmeDocuments, out IList<string> errors)
        {
            errors = new List<string>();
            IList<ProgrammeApplicationDocument> documents = new List<ProgrammeApplicationDocument>();
            IDictionary<string, int> existingTypes = new Dictionary<string, int>();

            var rows = this.ReadExcel(excelStream, 4);

            int rowNumber = 0;

            foreach (var row in rows)
            {
                rowNumber++;

                var document = new ProgrammeApplicationDocument
                {
                    ProgrammeId = programmeId,
                    IsActive = true,
                };

                if (rowNumber == 1)
                {
                    if (!this.AreHeadersValid(row, Headers))
                    {
                        errors.Add(FileNotMatchingTemplateError);
                        return null;
                    }

                    continue;
                }
                else if (string.IsNullOrWhiteSpace(row[2]) && string.IsNullOrWhiteSpace(row[3]) && row[4] == EmptyNomStr)
                {
                    break;
                }

                string documentType = row[2];
                if (string.IsNullOrWhiteSpace(documentType))
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Тип на документа",
                        "задължително поле"));
                }
                else if (documentType.Length > 500)
                {
                    errors.Add(this.GetError(
                        rowNumber,
                        "Тип на документа",
                        "полето може да съдържа максимум 500 символа"));
                }
                else if (programmeDocuments.Contains(documentType))
                {
                    errors.Add($"Стойността в полето \"Тип на документа\" на ред \"{rowNumber}\" се дублира със типа на вече добавен документ от кандидата към тази оперативна програма");
                }
                else if (existingTypes.ContainsKey(documentType))
                {
                    errors.Add($"Стойността в полето \"Тип на документа\" на ред \"{rowNumber}\" се дублира със стойността на ред \"{existingTypes[documentType]}\"");
                }
                else
                {
                    existingTypes.Add(documentType, rowNumber);

                    document.Name = documentType;
                }

                string extension = row[3];
                if (!string.IsNullOrWhiteSpace(extension))
                {
                    if (!ExtensionsRegex.IsMatch(extension))
                    {
                        errors.Add(this.GetError(rowNumber, "Разширения"));
                    }
                    else if (extension.Length > 100)
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Разширения",
                            "полето може да съдържа максимум 100 символа"));
                    }
                    else
                    {
                        document.Extension = extension;
                    }
                }

                bool? isSignatureRequired = null;
                if (this.GetBoolean(row, 4, out isSignatureRequired))
                {
                    if (isSignatureRequired.HasValue)
                    {
                        document.IsSignatureRequired = isSignatureRequired.Value;
                    }
                    else
                    {
                        errors.Add(this.GetError(
                            rowNumber,
                            "Електронен подпис",
                            "задължително поле"));
                    }
                }
                else
                {
                    errors.Add(this.GetError(rowNumber, "Електронен подпис"));
                }

                documents.Add(document);
            }

            return documents;
        }

        private string GetError(int rowIndex, string cellName, string details = null)
        {
            var error = $"Невалидна стойност в полето \"{cellName}\" на ред \"{rowIndex}\"";

            if (!string.IsNullOrEmpty(details))
            {
                error = $"{error} ({details})";
            }

            return error;
        }

        private bool AreHeadersValid(string[] row, Dictionary<int, string> headers)
        {
            foreach (var key in headers.Keys)
            {
                if (headers[key].ToUpperInvariant() != row[key].ToUpperInvariant().Trim())
                {
                    return false;
                }
            }

            return true;
        }

        protected bool GetBoolean(string[] row, int column, out bool? value)
        {
            var str = row[column];
            if (!string.IsNullOrWhiteSpace(str) && str != EmptyNomStr)
            {
                if (str == "да")
                {
                    value = true;
                    return true;
                }
                else if (str == "не")
                {
                    value = false;
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

        private string GetColumnName(string cellReference)
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

        private int GetColumnNumber(string cellReference)
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

        private string GetCellValue(Cell cell, SharedStringItem[] sharedStrings)
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

        private IEnumerable<string[]> ReadExcel(Stream excelStream, int numberOfColumns)
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
    }
}
