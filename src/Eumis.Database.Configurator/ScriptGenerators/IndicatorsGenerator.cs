using Eumis.Common.Json;
using Eumis.Database.Configurator.DbRows;
using Eumis.Database.Configurator.SheetRows;
using Eumis.Database.Configurator.SheetRows.Abstract;
using Eumis.Domain.Indicators;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Database.Configurator.ScriptGenerators
{
    internal class IndicatorsGenerator : ScriptsGenerator
    {
        public IndicatorsGenerator(Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary)
            : base(sheetsRowsDictionary)
        {
            this.Table3SheetRows = this.GetExcelSheetRows("Table3");
            this.Table4SheetRows = this.GetExcelSheetRows("Table4");
            this.Table4aSheetRows = this.GetExcelSheetRows("Table4a");
            this.Table5SheetRows = this.GetExcelSheetRows("Table5");
            this.Table6SheetRows = this.GetExcelSheetRows("Table6");
            this.Table12SheetRows = this.GetExcelSheetRows("Table12");
            this.Table13SheetRows = this.GetExcelSheetRows("Table13");

            this.IndicatorDbRows = new List<IndicatorDbRow>();
            this.MapNodeIndicatorDbRows = new List<MapNodeIndicatorDbRow>();
        }

        protected List<Dictionary<int, string>> Table3SheetRows { get; set; }

        protected List<Dictionary<int, string>> Table4SheetRows { get; set; }

        protected List<Dictionary<int, string>> Table4aSheetRows { get; set; }

        protected List<Dictionary<int, string>> Table5SheetRows { get; set; }

        protected List<Dictionary<int, string>> Table6SheetRows { get; set; }

        protected List<Dictionary<int, string>> Table12SheetRows { get; set; }

        protected List<Dictionary<int, string>> Table13SheetRows { get; set; }

        private List<IndicatorDbRow> IndicatorDbRows { get; set; }

        private List<MapNodeIndicatorDbRow> MapNodeIndicatorDbRows { get; set; }

        protected override List<DbTableData> GetDbTableData()
        {
            var table3SheetRows = this.Table3SheetRows.Select(row => (IndicatorSheetRow)new Table3SheetRow(row)).ToList();
            this.AddDbRows(table3SheetRows);

            var table4SheetRows = this.Table4SheetRows.Select(row => (IndicatorSheetRow)new Table4SheetRow(row)).ToList();
            this.AddDbRows(table4SheetRows);

            var table4aSheetRows = this.Table4aSheetRows.Select(row => (IndicatorSheetRow)new Table4aSheetRow(row)).ToList();
            this.AddDbRows(table4aSheetRows);

            var table5SheetRows = this.Table5SheetRows.Select(row => (IndicatorSheetRow)new Table5SheetRow(row)).ToList();
            this.AddDbRows(table5SheetRows);

            var table6SheetRows = this.Table6SheetRows.Select(row => (IndicatorSheetRow)new Table6SheetRow(row)).ToList();
            this.AddDbRows(table6SheetRows);

            var table12SheetRows = this.Table12SheetRows.Select(row => (IndicatorSheetRow)new Table12SheetRow(row)).ToList();
            this.AddDbRows(table12SheetRows);

            var table13SheetRows = this.Table13SheetRows.Select(row => (IndicatorSheetRow)new Table13SheetRow(row)).ToList();
            this.AddDbRows(table13SheetRows);

            var indicatorRows = new List<IDbRow>();
            indicatorRows.AddRange(this.IndicatorDbRows);

            var mapNodeIndicatorRows = new List<IDbRow>();
            mapNodeIndicatorRows.AddRange(this.MapNodeIndicatorDbRows);

            return new List<DbTableData>
            {
                new DbTableData(IndicatorDbRow.DbTableName, IndicatorDbRow.UseIdentityInsert,  indicatorRows),
                new DbTableData(MapNodeIndicatorDbRow.DbTableName, MapNodeIndicatorDbRow.UseIdentityInsert, mapNodeIndicatorRows),
            };
        }

        private IndicatorDbRow GetAddedIndicatorDbRowByCode(int programmeId)
        {
            return this.IndicatorDbRows.SingleOrDefault(e => e.ProgrammeId == programmeId);
        }

        private int GetNextIndicatorIdentity()
        {
            int identity = 1;

            if (this.IndicatorDbRows.Count > 0)
            {
                identity = this.IndicatorDbRows.Max(e => e.IndicatorId) + 1;
            }

            return identity;
        }

        private string GetMapNodeIndicatorType(IndicatorSheetRow.Type type)
        {
            string returnValue = null;

            switch (type)
            {
                case IndicatorSheetRow.Type.Table3:
                    returnValue = "Table3";
                    break;
                case IndicatorSheetRow.Type.Table4:
                    returnValue = "Table4";
                    break;
                case IndicatorSheetRow.Type.Table4a:
                    returnValue = "Table4a";
                    break;
                case IndicatorSheetRow.Type.Table5:
                    returnValue = "Table5";
                    break;
                case IndicatorSheetRow.Type.Table6:
                    returnValue = "Table6";
                    break;
                case IndicatorSheetRow.Type.Table12:
                    returnValue = "Table12";
                    break;
                case IndicatorSheetRow.Type.Table13:
                    returnValue = "Table13";
                    break;
                default:
                    throw new ArgumentException();
            }

            return returnValue;
        }

        private void AddDbRows(List<IndicatorSheetRow> indicatorSheetRows)
        {
            int? currentProgrammeId = null;
            int? currentMapNodeId = null;

            foreach (var sheetRow in indicatorSheetRows)
            {
                if (sheetRow.ProgrammeId.HasValue)
                {
                    currentProgrammeId = sheetRow.ProgrammeId;
                }

                if (sheetRow.MapNodeId.HasValue)
                {
                    currentMapNodeId = sheetRow.MapNodeId;
                }

                int? indicatorId = null;

                IndicatorDbRow addedIndicatorDbRow = this.GetAddedIndicatorDbRowByCode(currentProgrammeId.Value);

                if (addedIndicatorDbRow != null)
                {
                    indicatorId = addedIndicatorDbRow.IndicatorId;
                }
                else
                {
                    IndicatorDbRow indicatorDbRow = new IndicatorDbRow
                    {
                        IndicatorId = this.GetNextIndicatorIdentity(),
                        ProgrammeId = currentProgrammeId.Value,
                        MeasureId = Nomenclatures.MeasuresDictionary.GetNomId(sheetRow.Measure).Value,
                        Name = sheetRow.Name,
                        NameAlt = sheetRow.NameAlt,
                        HasGenderDivision =
                        sheetRow.BaseMenValue.HasValue || sheetRow.BaseWomenValue.HasValue ||
                        sheetRow.TargetMenValue.HasValue || sheetRow.TargetWomenValue.HasValue ||
                        sheetRow.MilestoneTargetMenValue.HasValue || sheetRow.MilestoneTargetWomenValue.HasValue ||
                        sheetRow.FinalTargetMenValue.HasValue || sheetRow.FinalTargetWomenValue.HasValue,
                        ReportingType = Nomenclatures.ReportingTypesDictionary.GetNomId(sheetRow.ReportingType).Value,
                    };

                    this.IndicatorDbRows.Add(indicatorDbRow);

                    indicatorId = indicatorDbRow.IndicatorId;
                }

                bool hasQualitativeValue = !(sheetRow.BaseTotalValue.HasValue && sheetRow.TargetTotalValue.HasValue);

                MapNodeIndicatorDbRow mapNodeIndicatorDbRow = new MapNodeIndicatorDbRow
                {
                    MapNodeId = currentMapNodeId.Value,
                    IndicatorId = indicatorId.Value,
                    Type = this.GetMapNodeIndicatorType(sheetRow.SheetType),

                    RegionCategory = (int)RegionCategory.LessDevelopedRegions,
                    FinanceSource = Nomenclatures.FinanceSourcesDictionary.GetNomId(sheetRow.FinanceSource),

                    BaseTotalValue = sheetRow.BaseTotalValue,
                    BaseMenValue = sheetRow.BaseMenValue,
                    BaseWomenValue = sheetRow.BaseWomenValue,
                    BaseQualitativeValue = hasQualitativeValue ? sheetRow.BaseTotalValueString : null,
                    BaseYear = sheetRow.BaseYear,

                    TargetTotalValue = sheetRow.TargetTotalValue,
                    TargetMenValue = sheetRow.TargetMenValue,
                    TargetWomenValue = sheetRow.TargetWomenValue,
                    TargetQualitativeValue = hasQualitativeValue ? sheetRow.TargetTotalValueString : null,

                    MilestoneTargetTotalValue = sheetRow.MilestoneTargetTotalValue,
                    MilestoneTargetMenValue = sheetRow.MilestoneTargetMenValue,
                    MilestoneTargetWomenValue = sheetRow.MilestoneTargetWomenValue,

                    FinalTargetTotalValue = sheetRow.FinalTargetTotalValue,
                    FinalTargetMenValue = sheetRow.FinalTargetMenValue,
                    FinalTargetWomenValue = sheetRow.FinalTargetWomenValue,

                    MeasureId = Nomenclatures.MeasuresDictionary.GetNomId(sheetRow.BaseTargetValueMeasure),
                    DataSource = sheetRow.DataSource,
                    ReportingFrequancy = sheetRow.ReportingFrequancy,
                    CommonBaseIndicator = sheetRow.CommonBaseIndicator,
                    Description = sheetRow.Description,
                };

                this.MapNodeIndicatorDbRows.Add(mapNodeIndicatorDbRow);
            }
        }
    }
}
