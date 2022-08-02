using System.Collections.Generic;
using Eumis.Database.Configurator.DbRows;
using Eumis.Database.Configurator.SheetRows;

namespace Eumis.Database.Configurator.ScriptGenerators
{
    internal class MapNodeBudgetsGenerator : ScriptsGenerator
    {
        public MapNodeBudgetsGenerator(Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary)
            : base(sheetsRowsDictionary)
        {
            this.ExcelSheetRows = this.GetExcelSheetRows("MapNodeBudgets");
        }

        protected List<Dictionary<int, string>> ExcelSheetRows { get; set; }

        protected override List<DbTableData> GetDbTableData()
        {
            List<IDbRow> mapNodeBudgetDbRows = new List<IDbRow>();

            MapNodeBudgetDbRow mapNodeBudget = null;

            int? currentProgrammeId = null;
            int? currentProgrammeProrityId = null;

            foreach (var row in this.ExcelSheetRows)
            {
                MapNodeBudgetSheetRow sheetRow = new MapNodeBudgetSheetRow(row);

                if (sheetRow.ProgrammeId.HasValue)
                {
                    currentProgrammeId = sheetRow.ProgrammeId.Value;
                }

                if (sheetRow.ProgrammePriorityId.HasValue)
                {
                    currentProgrammeProrityId = sheetRow.ProgrammePriorityId.Value;
                }

                if (string.IsNullOrWhiteSpace(sheetRow.FinanceSource) ||
                    !sheetRow.EuAmount.HasValue ||
                    !sheetRow.BgAmount.HasValue ||
                    !sheetRow.EuReservedAmount.HasValue ||
                    !sheetRow.BgReservedAmount.HasValue)
                {
                    continue;
                }

                mapNodeBudget = new MapNodeBudgetDbRow
                {
                    MapNodeId = currentProgrammeProrityId.Value,
                    ProgrammeId = currentProgrammeId.Value,
                    BudgetPeriodId = Nomenclatures.BudgetPeriodsDictionary.GetNomId(sheetRow.BudgetPeriodYear).Value,
                    FinanceSource = Nomenclatures.FinanceSourcesDictionary.GetNomId(sheetRow.FinanceSource).Value,
                    EuAmount = sheetRow.EuAmount.Value,
                    BgAmount = sheetRow.BgAmount.Value,
                    EuReservedAmount = sheetRow.EuReservedAmount.Value,
                    BgReservedAmount = sheetRow.BgReservedAmount.Value,
                };

                mapNodeBudgetDbRows.Add(mapNodeBudget);
            }

            return new List<DbTableData>
            {
                new DbTableData(MapNodeBudgetDbRow.DbTableName, MapNodeBudgetDbRow.UseIdentityInsert, mapNodeBudgetDbRows),
            };
        }

        private MapNodeRelationDbRow CreateMapNodeRelationDbRow(int mapNodeId, int? parentMapNodeId, int? programmeId, int? programmePriorityId)
        {
            MapNodeRelationDbRow mapNodeRelation = new MapNodeRelationDbRow
            {
                MapNodeId = mapNodeId,
                ParentMapNodeId = parentMapNodeId,
                ProgrammeId = programmeId,
                ProgrammePriorityId = programmePriorityId,
            };

            return mapNodeRelation;
        }
    }
}
