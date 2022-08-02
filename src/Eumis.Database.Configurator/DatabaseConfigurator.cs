using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Eumis.Database.Configurator.DbRows;
using Eumis.Database.Configurator.ScriptGenerators;

namespace Eumis.Database.Configurator
{
    public class DatabaseConfigurator
    {
        private List<Type> scriptsGeneratorTypes = new List<Type>
        {
            typeof(InvestmentPrioritiesGenerator),
            typeof(MapNodesGenerator),
            typeof(MapNodeInstitutionsGenerator),
            typeof(MapNodeBudgetsGenerator),
            typeof(IndicatorsGenerator),
        };

        public DatabaseConfigurator(string excelFilePath)
        {
            this.ExcelFilePath = excelFilePath;
        }

        public string ExcelFilePath { get; private set; }

        public List<ScriptData> GenerateScriptData()
        {
            IndicatorDbRow.GidGenerator.ResetDeterministicGuidCounter();
            MapNodeDbRow.GidGenerator.ResetDeterministicGuidCounter();

            List<ScriptData> scriptDataList = new List<ScriptData>();

            var sheetsRowsDictionary = this.GetSheetsRowsDictionary(this.ExcelFilePath);

            foreach (var scriptGeneratorType in this.scriptsGeneratorTypes)
            {
                ScriptsGenerator scriptGenerator = (ScriptsGenerator)Activator.CreateInstance(scriptGeneratorType, sheetsRowsDictionary);

                scriptDataList.AddRange(scriptGenerator.CreateScriptsData());
            }

            return scriptDataList;
        }

        private Dictionary<string, List<Dictionary<int, string>>> GetSheetsRowsDictionary(string excelFileName)
        {
            Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary = new Dictionary<string, List<Dictionary<int, string>>>();

            XDocument xDoc = XDocument.Load(excelFileName);
            var namespaceName = "urn:schemas-microsoft-com:office:spreadsheet";
            var sheets = xDoc
                .Element(XName.Get("Workbook", namespaceName))
                .Elements(XName.Get("Worksheet", namespaceName));

            foreach (var sheet in sheets)
            {
                IEnumerable<XElement> rawRows = sheet
                    .Element(XName.Get("Table", namespaceName))
                    .Elements(XName.Get("Row", namespaceName))
                    .Skip(1);

                var excelRows = new List<Dictionary<int, string>>();

                foreach (var rawRow in rawRows)
                {
                    int cellIndex = -1;

                    var cells = rawRow.Elements(XName.Get("Cell", namespaceName));

                    Dictionary<int, string> excelRow = new Dictionary<int, string>();
                    foreach (var cell in cells)
                    {
                        XAttribute indexAttribute = cell.Attribute(XName.Get("Index", namespaceName));

                        cellIndex = indexAttribute != null ? (int.Parse(indexAttribute.Value) - 1) : (cellIndex + 1);

                        var cellData = cell.Element(XName.Get("Data", namespaceName));
                        var textValue = cellData != null ? cellData.Value : string.Empty;

                        excelRow.Add(cellIndex, textValue);
                    }

                    excelRows.Add(excelRow);
                }

                string sheetName = sheet.FirstAttribute.Value;

                sheetsRowsDictionary.Add(sheetName, excelRows);
            }

            return sheetsRowsDictionary;
        }
    }
}
