using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Eumis.Common.Json;
using Eumis.Data.ContractReports.Repositories;
using Eumis.Data.ContractReports.ViewObjects;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.ApplicationServices.Services.ContractReportMicro
{
    public abstract class ContractReportMicroParserBase
    {
        protected const string FileNotMatchingTemplateError = "Избраният файл не отговаря на шаблона за съответния тип микроданни.";
        protected const string EmptyNomStr = "-";
        private const NumberStyles DecimalNumberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowExponent;
        private const NumberStyles OpenXmlNumberStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowExponent;

        private static readonly Regex DistrictRegex = new Regex(@"^Област");
        private static readonly Regex MunicipalityRegex = new Regex(@"^Община");
        private static readonly CultureInfo OpenXmlParseCulture = CultureInfo.InvariantCulture;

        private readonly Lazy<IList<EntityNomVO>> districts;
        private readonly Lazy<IList<ContractReportMicrosMunicipalityNomVO>> municipalities;
        private readonly Lazy<IList<ContractReportMicrosSettlementNomVO>> settlements;

        private IContractReportMicrosDistrictNomsRepository contractReportMicrosDistrictNomsRepository;
        private IContractReportMicrosMunicipalityNomsRepository contractReportMicrosMunicipalityNomsRepository;
        private IContractReportMicrosSettlementNomsRepository contractReportMicrosSettlementNomsRepository;

        public ContractReportMicroParserBase(
            IContractReportMicrosDistrictNomsRepository contractReportMicrosDistrictNomsRepository,
            IContractReportMicrosMunicipalityNomsRepository contractReportMicrosMunicipalityNomsRepository,
            IContractReportMicrosSettlementNomsRepository contractReportMicrosSettlementNomsRepository)
        {
            this.contractReportMicrosDistrictNomsRepository = contractReportMicrosDistrictNomsRepository;
            this.contractReportMicrosMunicipalityNomsRepository = contractReportMicrosMunicipalityNomsRepository;
            this.contractReportMicrosSettlementNomsRepository = contractReportMicrosSettlementNomsRepository;

            this.districts = new Lazy<IList<EntityNomVO>>(() => this.contractReportMicrosDistrictNomsRepository.GetAllDistrictNoms(), LazyThreadSafetyMode.None);
            this.municipalities = new Lazy<IList<ContractReportMicrosMunicipalityNomVO>>(() => this.contractReportMicrosMunicipalityNomsRepository.GetAllMunicipalityNoms(), LazyThreadSafetyMode.None);
            this.settlements = new Lazy<IList<ContractReportMicrosSettlementNomVO>>(() => this.contractReportMicrosSettlementNomsRepository.GetAllSettlementNoms(), LazyThreadSafetyMode.None);
        }

        protected bool AreHeadersValid(string[] row, Dictionary<int, string> headers)
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

        protected bool GetDate(string[] row, int yearColumn, int monthColumn, int dayColumn, out DateTime? value)
        {
            var yearStr = row[yearColumn];
            var monthStr = row[monthColumn];
            var dayStr = row[dayColumn];

            if ((string.IsNullOrWhiteSpace(yearStr) || yearStr == EmptyNomStr) &&
                (string.IsNullOrWhiteSpace(monthStr) || monthStr == EmptyNomStr) &&
                (string.IsNullOrWhiteSpace(dayStr) || dayStr == EmptyNomStr))
            {
                value = null;
                return true;
            }

            DateTime date;
            if (DateTime.TryParseExact(string.Format("{0}-{1}-{2}", yearStr, monthStr, dayStr), "yyyy-M-d", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                value = date;
                return true;
            }

            value = null;
            return false;
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

        protected bool GetEnum<TEnum>(string[] row, int column, out TEnum? value)
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            var str = row[column];
            if (!string.IsNullOrWhiteSpace(str) && str != EmptyNomStr)
            {
                str = str.Trim().ToUpperInvariant();
                foreach (var obj in Enum.GetValues(typeof(TEnum)))
                {
                    var enumVal = (Enum)obj;
                    if (enumVal.GetEnumDescription().ToUpperInvariant() == str)
                    {
                        value = (TEnum)(object)enumVal;
                        return true;
                    }
                }

                value = null;
                return false;
            }
            else
            {
                value = null;
                return true;
            }
        }

        protected bool GetDistrictId(string[] row, int column, out int? value)
        {
            var districtStr = row[column];
            if (!string.IsNullOrWhiteSpace(districtStr) && districtStr != EmptyNomStr)
            {
                districtStr = DistrictRegex.Replace(districtStr, string.Empty).Trim();

                var district = this.districts.Value.SingleOrDefault(d => d.Name == districtStr);
                if (district == null)
                {
                    value = null;
                    return false;
                }
                else
                {
                    value = district.NomValueId;
                    return true;
                }
            }

            value = null;
            return true;
        }

        protected bool GetMunicipality(string[] row, int column, out ContractReportMicrosMunicipalityNomVO value)
        {
            var municipalityStr = row[column];
            if (!string.IsNullOrWhiteSpace(municipalityStr) && municipalityStr != EmptyNomStr)
            {
                municipalityStr = MunicipalityRegex.Replace(municipalityStr, string.Empty).Trim();

                var municipality = this.municipalities.Value.SingleOrDefault(m => m.Name == municipalityStr);
                if (municipality == null)
                {
                    value = null;
                    return false;
                }
                else
                {
                    value = municipality;
                    return true;
                }
            }

            value = null;
            return true;
        }

        protected bool GetSettlement(string[] row, int column, int? districtId, out ContractReportMicrosSettlementNomVO value)
        {
            var settlementStr = row[column];
            if (!string.IsNullOrWhiteSpace(settlementStr) && settlementStr != EmptyNomStr)
            {
                var candidates = this.settlements.Value.Where(s => s.Name == settlementStr);
                if (candidates.Count() > 1)
                {
                    if (districtId.HasValue)
                    {
                        candidates = candidates.Where(s => s.DistrictId == districtId);
                    }
                    else
                    {
                        value = null;
                        return false;
                    }
                }

                var settlement = candidates.SingleOrDefault();
                if (settlement == null)
                {
                    value = null;
                    return false;
                }
                else
                {
                    value = settlement;
                    return true;
                }
            }

            value = null;
            return true;
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
    }
}
