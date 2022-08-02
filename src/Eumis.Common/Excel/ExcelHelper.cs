using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;

namespace Eumis.Common.Excel
{
    public static class ExcelHelper
    {
        private const string ExcelNamespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> Cache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        public const int CellLengthLimit = 32767;

        public static void TransformTemplate<T>(Stream excelTemplate, int templateRowNumber, IEnumerable<T> rows)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(excelTemplate, true))
            {
                WorksheetPart worksheetPart = doc.WorkbookPart.GetPartsOfType<WorksheetPart>().First();

                using (Stream partStream = worksheetPart.GetStream(FileMode.Open, FileAccess.ReadWrite))
                using (MemoryStream original = new MemoryStream((int)partStream.Length))
                {
                    partStream.CopyTo(original);
                    partStream.Seek(0, SeekOrigin.Begin);
                    original.Seek(0, SeekOrigin.Begin);

                    using (XmlReader reader = XmlReader.Create(original))
                    using (XmlWriter writer = XmlWriter.Create(partStream))
                    {
                        reader.MoveToContent();

                        writer.WriteStartDocument();
                        writer.WriteStartElement("worksheet", ExcelNamespace);
                        writer.WriteAttributes(reader, false);
                        reader.ReadStartElement("worksheet", ExcelNamespace);

                        while (!reader.EOF)
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "sheetData" && reader.NamespaceURI == ExcelNamespace)
                            {
                                writer.WriteStartElement("sheetData", ExcelNamespace);
                                writer.WriteAttributes(reader, false);
                                reader.ReadStartElement("sheetData", ExcelNamespace);

                                int rowNum = 1;

                                while (reader.NodeType != XmlNodeType.EndElement)
                                {
                                    if (rowNum == templateRowNumber)
                                    {
                                        if (rows.Any())
                                        {
                                            WriteTemplateRow(writer, reader, rowNum, rows);

                                            break;
                                        }
                                    }

                                    writer.WriteNode(reader, false);

                                    rowNum++;
                                }

                                writer.WriteEndElement();
                                reader.ReadEndElement();
                            }
                            else
                            {
                                writer.WriteNode(reader, false);
                            }
                        }
                    }
                }
            }
        }

        private static void WriteTemplateRow<T>(XmlWriter writer, XmlReader reader, int rowNum, IEnumerable<T> rows)
        {
            var columnStyles = new List<int>();

            reader.ReadStartElement("row", ExcelNamespace);

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "c" && reader.NamespaceURI == ExcelNamespace)
                {
                    columnStyles.Add(int.Parse(reader.GetAttribute("s")));
                }

                reader.Skip();
            }

            reader.ReadEndElement();

            if (typeof(T) == typeof(string[]))
            {
                foreach (var row in rows)
                {
                    WriteRow(writer, row as string[], rowNum, columnStyles);
                    rowNum++;
                }
            }
            else
            {
                foreach (var row in rows)
                {
                    WriteRow(writer, row, rowNum, columnStyles);
                    rowNum++;
                }
            }
        }

        private static void WriteRow(XmlWriter writer, string[] cells, int rowNum, List<int> columnStyles)
        {
            writer.WriteStartElement("row", ExcelNamespace);
            writer.WriteStartAttribute("r");
            writer.WriteValue(rowNum);
            writer.WriteEndAttribute();
            writer.WriteStartAttribute("spans");
            writer.WriteValue("1:" + cells.Count().ToString());
            writer.WriteEndAttribute();

            int cellNum = 0;
            foreach (var cell in cells)
            {
                WriteTextCell(writer, rowNum, cellNum, columnStyles[cellNum], cell);
                cellNum++;
            }

            writer.WriteEndElement();
        }

        private static void WriteRow<T>(XmlWriter writer, T row, int rowNum, List<int> columnStyles)
        {
            PropertyInfo[] properties = GetOrderedTypeProperties(typeof(T));

            writer.WriteStartElement("row", ExcelNamespace);
            writer.WriteStartAttribute("r");
            writer.WriteValue(rowNum);
            writer.WriteEndAttribute();
            writer.WriteStartAttribute("spans");
            writer.WriteValue("1:" + properties.Count().ToString());
            writer.WriteEndAttribute();

            int cellNum = 0;
            foreach (var prop in properties)
            {
                var value = prop.GetValue(row);

                if (prop.PropertyType == typeof(int))
                {
                    WriteNumberCell(writer, rowNum, cellNum, columnStyles[cellNum], (int)value);
                }
                else if (prop.PropertyType == typeof(decimal))
                {
                    WriteNumberCell(writer, rowNum, cellNum, columnStyles[cellNum], (decimal)value);
                }
                else if (prop.PropertyType == typeof(float))
                {
                    WriteNumberCell(writer, rowNum, cellNum, columnStyles[cellNum], (float)value);
                }
                else if (prop.PropertyType == typeof(double))
                {
                    WriteNumberCell(writer, rowNum, cellNum, columnStyles[cellNum], (double)value);
                }
                else
                {
                    WriteTextCell(writer, rowNum, cellNum, columnStyles[cellNum], value?.ToString());
                }

                cellNum++;
            }

            writer.WriteEndElement();
        }

        private static void WriteTextCell(XmlWriter xw, int rowNum, int cellNum, int style, string value)
        {
            xw.WriteStartElement("c", ExcelNamespace);

            xw.WriteStartAttribute("r");
            xw.WriteValue(IntToColumnId(cellNum) + rowNum.ToString());
            xw.WriteEndAttribute();

            xw.WriteStartAttribute("s");
            xw.WriteValue(style);
            xw.WriteEndAttribute();

            xw.WriteStartAttribute("t");
            xw.WriteValue("inlineStr");
            xw.WriteEndAttribute();

            if (value != null)
            {
                xw.WriteStartElement("is", ExcelNamespace);
                xw.WriteStartElement("t", ExcelNamespace);
                xw.WriteValue(value);
                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            xw.WriteEndElement();
        }

        private static void WriteNumberCell<T>(XmlWriter xw, int rowNum, int cellNum, int style, T value)
        {
            xw.WriteStartElement("c", ExcelNamespace);

            xw.WriteStartAttribute("r");
            xw.WriteValue(IntToColumnId(cellNum) + rowNum.ToString());
            xw.WriteEndAttribute();

            xw.WriteStartAttribute("s");
            xw.WriteValue(style);
            xw.WriteEndAttribute();

            xw.WriteStartElement("v", ExcelNamespace);
            xw.WriteValue(value);
            xw.WriteEndElement();

            xw.WriteEndElement();
        }

        private static string IntToColumnId(int i)
        {
            if (i >= 0 && i <= 25)
            {
                return ((char)(((int)'A') + i)).ToString();
            }

            if (i >= 26 && i <= 701)
            {
                int v = i - 26;
                int h = v / 26;
                int l = v % 26;
                return ((char)(((int)'A') + h)).ToString() + ((char)(((int)'A') + l)).ToString();
            }

            // 17576
            if (i >= 702 && i <= 18277)
            {
                int v = i - 702;
                int h = v / 676;
                int r = v % 676;
                int m = r / 26;
                int l = r % 26;
                return ((char)(((int)'A') + h)).ToString() +
                    ((char)(((int)'A') + m)).ToString() +
                    ((char)(((int)'A') + l)).ToString();
            }

            throw new Exception($"Column out of range ${i.ToString()}");
        }

        private static PropertyInfo[] GetOrderedTypeProperties(Type type)
        {
            Func<Type, PropertyInfo[]> valueFactory = (t) => t.GetProperties()
                            .Where(p => p.IsDefined(typeof(ColumnAttribute), false))
                            .OrderBy(p => ((ColumnAttribute)p.GetCustomAttributes(typeof(ColumnAttribute), false)[0]).Order)
                            .ToArray();

            return Cache.GetOrAdd(type, valueFactory);
        }
    }
}
