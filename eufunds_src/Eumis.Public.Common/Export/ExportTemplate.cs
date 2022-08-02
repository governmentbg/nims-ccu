using ClosedXML.Excel;
using System;
using System.Collections.Generic;

namespace Eumis.Public.Common.Export
{
    public class ExportTemplate
    {
        #region Configuration

        public const double DEFAULT_PX = 20;
        public const double DEFAULT_HEIGHT = 3 * DEFAULT_PX / 4d;

        public const double HEIGHT_BEFORE_START = DEFAULT_HEIGHT;
        public const double HEIGHT_AFTER_START_NOTES = DEFAULT_HEIGHT;
        public const double HEIGHT_AFTER_TABLE_HEADER = DEFAULT_HEIGHT;
        public const double HEIGHT_BETWEEN_TABLES = DEFAULT_HEIGHT * 3;
        public const double HEIGHT_BEFORE_END_NOTES = DEFAULT_HEIGHT;

        public const int MERGE_CELLS_COUNT = 10;

        public const string HEADER_BACKGROUND_COLOR = "#E9E9E9";
        public const string HEADER_FONT_COLOR = "#333";
        public const string HEADER_BORDER_COLOR = "#000000";

        public const string CELL_BACKGROUND_COLOR = "#FFF";
        public const string CELL_FONT_COLOR = "#333";
        public const string CELL_BORDER_COLOR = "#000000";

        #endregion

        public ExportTemplate()
        {
            this.Sheet = new ExportSheet();
        }

        public ExportTemplate(string name)
            : this()
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public ExportSheet Sheet { get; set; }

        public PageOrientation PageOrientation { get; set; }

        public static double PxToInches(double pixels)
        {
            return ((pixels - 12) / 7d) + 1;
        }

        public static IXLRange MergeRow(IXLWorksheet ws, IXLRow row)
        {
            return ws.Range(row.RowNumber(), 1, row.RowNumber(), MERGE_CELLS_COUNT).Merge();
        }

        public XLWorkbook GenererateExcel()
        {
            var workbook = new XLWorkbook();

            if (this.Sheet != null)
            {
                var worksheet = workbook.Worksheets.Add(this.Sheet.Name);

                var currentRow = worksheet.Row(1);
                MergeRow(worksheet, currentRow);
                currentRow.Height = HEIGHT_BEFORE_START;

                currentRow = currentRow.RowBelow();
                if (this.Sheet.StartNotes != null && this.Sheet.StartNotes.Count > 0)
                {
                    foreach (var note in this.Sheet.StartNotes)
                    {
                        // Merge cells for note
                        MergeRow(worksheet, currentRow).Value = note;

                        currentRow = currentRow.RowBelow();
                    }

                    MergeRow(worksheet, currentRow);
                    currentRow.Height = HEIGHT_AFTER_START_NOTES;
                    currentRow = currentRow.RowBelow();
                }

                if (this.Sheet.Tables != null && this.Sheet.Tables.Count > 0)
                {
                    int index = 0;
                    foreach (var table in this.Sheet.Tables)
                    {
                        // Set height if table is not last
                        if (index != 0)
                        {
                            MergeRow(worksheet, currentRow);
                            currentRow.Height = HEIGHT_BETWEEN_TABLES;
                            currentRow = currentRow.RowBelow();
                        }

                        // Table header
                        if (!string.IsNullOrWhiteSpace(table.Header))
                        {
                            // Merge cells for table header
                            var headerCells = MergeRow(worksheet, currentRow);
                            headerCells.Value = table.Header;

                            // Set styles for table header
                            headerCells.Style.Font.Bold = true;

                            currentRow = currentRow.RowBelow();
                            MergeRow(worksheet, currentRow);
                            currentRow.Height = HEIGHT_AFTER_TABLE_HEADER;
                            currentRow = currentRow.RowBelow();
                        }

                        // Table rows
                        if (table.Rows != null)
                        {
                            foreach (var row in table.Rows)
                            {
                                if (row.Cells != null)
                                {
                                    int currentIndex = 0;
                                    for (int i = 0; i < row.Cells.Count; i++)
                                    {
                                        currentIndex++;

                                        var definedCell = row.Cells[i];

                                        var excelCell = currentRow.Cell(currentIndex);

                                        // Continue if current cell is merged
                                        while (excelCell.IsMerged())
                                        {
                                            currentIndex++;
                                            excelCell = currentRow.Cell(currentIndex);
                                        }

                                        // Get Style
                                        IXLStyle style;
                                        if (definedCell.RowSpan > 1 || definedCell.ColSpan > 1)
                                        {
                                            var merge = worksheet.Range(currentRow.RowNumber(), currentIndex, currentRow.RowNumber() + definedCell.RowSpan - 1, currentIndex + definedCell.ColSpan - 1).Merge();

                                            merge.Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;
                                            style = merge.Style;

                                            if (!definedCell.IsNumber && !definedCell.IsDateTime)
                                            {
                                                merge.SetValue(definedCell.Value);
                                            }
                                            else
                                            {
                                                merge.Value = definedCell.Value;
                                            }

                                            merge.DataType = XLDataType.Text;
                                        }
                                        else
                                        {
                                            style = excelCell.Style;

                                            if (!definedCell.IsNumber && !definedCell.IsDateTime)
                                            {
                                                excelCell.SetValue(definedCell.Value);
                                            }
                                            else
                                            {
                                                excelCell.Value = definedCell.Value;
                                            }

                                            excelCell.DataType = XLDataType.Text;
                                        }

                                        // Set style
                                        if (definedCell.IsBold)
                                        {
                                            style.Font.Bold = true;
                                        }

                                        if (definedCell.IsItalic)
                                        {
                                            style.Font.Italic = true;
                                        }

                                        if (definedCell.IsNumber || definedCell.IsMoney)
                                        {
                                            if (definedCell.IsMoney)
                                            {
                                                style.NumberFormat.Format = "#,##0.00";
                                            }
                                            else
                                            {
                                                style.NumberFormat.SetNumberFormatId((int)XLPredefinedFormat.Number.Integer);
                                                style.NumberFormat.Format = "#,##0";
                                            }

                                            style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                            excelCell.DataType = XLDataType.Number;
                                        }
                                        else
                                        {
                                            style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                            style.Alignment.WrapText = true;
                                        }

                                        if (definedCell.IsDateTime)
                                        {
                                            excelCell.DataType = XLDataType.DateTime;
                                        }

                                        style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                        if (definedCell.IsHeader)
                                        {
                                            style.Font.Bold = true;
                                            style.Font.FontColor = XLColor.FromHtml(HEADER_FONT_COLOR);
                                            style.Fill.BackgroundColor = XLColor.FromHtml(HEADER_BACKGROUND_COLOR);
                                            style.Border.OutsideBorderColor = XLColor.FromHtml(HEADER_BORDER_COLOR);
                                        }
                                        else
                                        {
                                            style.Font.FontColor = XLColor.FromHtml(CELL_FONT_COLOR);
                                            style.Fill.BackgroundColor = XLColor.FromHtml(CELL_BACKGROUND_COLOR);
                                            style.Border.OutsideBorderColor = XLColor.FromHtml(CELL_BORDER_COLOR);
                                        }
                                    }
                                }

                                currentRow = currentRow.RowBelow();
                            }
                        }

                        index++;
                    }
                }

                if (this.Sheet.EndNotes != null && this.Sheet.EndNotes.Count > 0)
                {
                    MergeRow(worksheet, currentRow);
                    currentRow.Height = HEIGHT_BEFORE_END_NOTES;
                    currentRow = currentRow.RowBelow();
                    foreach (var note in this.Sheet.EndNotes)
                    {
                        // Merge cells for note
                        MergeRow(worksheet, currentRow).Value = note;

                        currentRow = currentRow.RowBelow();
                    }
                }

                if (this.Sheet.ExcelColumnWidths != null && this.Sheet.ExcelColumnWidths.Keys != null)
                {
                    foreach (int column in this.Sheet.ExcelColumnWidths.Keys)
                    {
                        var width = PxToInches(this.Sheet.ExcelColumnWidths[column]);
                        worksheet.Column(column).Width = width;
                    }
                }
            }

            return workbook;
        }

        public GenericXmlContainer GenerateXmlContainer()
        {
            var xmlsTables = new List<GenericXmlTable>();

            if (this.Sheet.Tables != null && this.Sheet.Tables.Count > 0)
            {
                foreach (var table in this.Sheet.Tables)
                {
                    var xmlTable = new GenericXmlTable() { Rows = new List<GenericXmlRow>() };

                    foreach (var row in table.Rows)
                    {
                        var xmlRow = new GenericXmlRow() { Cells = new List<GenericXmlCell>() };

                        foreach (var cell in row.Cells)
                        {
                            xmlRow.Cells.Add(new GenericXmlCell() { Value = cell.Value });
                        }

                        xmlTable.Rows.Add(xmlRow);
                    }

                    xmlsTables.Add(xmlTable);
                }
            }

            return new GenericXmlContainer()
            {
                Tables = xmlsTables,
            };
        }
    }
}
