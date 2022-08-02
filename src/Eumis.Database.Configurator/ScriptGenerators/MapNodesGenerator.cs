using Eumis.Database.Configurator.DbRows;
using Eumis.Database.Configurator.SheetRows;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.ScriptGenerators
{
    internal class MapNodesGenerator : ScriptsGenerator
    {
        public MapNodesGenerator(Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary)
            : base(sheetsRowsDictionary)
        {
            this.ExcelSheetRows = this.GetExcelSheetRows("MapNodes");
        }

        protected List<Dictionary<int, string>> ExcelSheetRows { get; set; }

        protected override List<DbTableData> GetDbTableData()
        {
            List<IDbRow> mapNodeDbRows = new List<IDbRow>();
            List<IDbRow> mapNodeRelationDbRows = new List<IDbRow>();

            MapNodeDbRow programme = null;
            MapNodeDbRow programmePriority = null;
            MapNodeDbRow investmentPriority = null;

            foreach (var row in this.ExcelSheetRows)
            {
                MapNodeSheetRow sheetRow = new MapNodeSheetRow(row);

                if (sheetRow.ProgrammeId.HasValue)
                {
                    // Add MapNode row
                    programme = new MapNodeDbRow
                    {
                        MapNodeId = sheetRow.ProgrammeId.Value,
                        Type = "Programme",
                        Code = sheetRow.ProgrammeCode,
                        ShortName = sheetRow.ProgrammeShortName,
                        Name = sheetRow.ProgrammeName,
                        NameAlt = sheetRow.ProgrammeNameAlt,
                        PortalOrderNum = sheetRow.PortalOrderNum,
                        PortalName = sheetRow.PortalName,
                        PortalNameAlt = sheetRow.PortalNameAlt,
                        PortalShortNameAlt = sheetRow.PortalShortNameAlt,
                        IrregularityCode = sheetRow.IrregularityCode,
                        InvestmentPriorityId = null,
                        IsHidden = null,
                    };
                    mapNodeDbRows.Add(programme);

                    // Add MapNodeRelation row
                    MapNodeRelationDbRow programmeRelation = this.CreateMapNodeRelationDbRow(programme.MapNodeId, null, programme.MapNodeId, null);
                    mapNodeRelationDbRows.Add(programmeRelation);
                }

                if (sheetRow.ProgrammePriorityId.HasValue)
                {
                    // Add MapNode row
                    programmePriority = new MapNodeDbRow
                    {
                        MapNodeId = sheetRow.ProgrammePriorityId.Value,
                        Type = "ProgrammePriority",
                        Code = sheetRow.ProgrammePriorityCode,
                        ShortName = sheetRow.ProgrammePriorityShortName,
                        Name = sheetRow.ProgrammePriorityName,
                        NameAlt = sheetRow.ProgrammePriorityNameAlt,
                        InvestmentPriorityId = null,
                        IsHidden = null,
                    };
                    mapNodeDbRows.Add(programmePriority);

                    // Add MapNodeRelation row
                    MapNodeRelationDbRow programmePriorityRelation = this.CreateMapNodeRelationDbRow(programmePriority.MapNodeId, programme.MapNodeId, programme.MapNodeId, programmePriority.MapNodeId);
                    mapNodeRelationDbRows.Add(programmePriorityRelation);
                }

                if (sheetRow.ProgrammePriorityInvestmentPriorityId.HasValue)
                {
                    // Add MapNode row
                    investmentPriority = new MapNodeDbRow
                    {
                        MapNodeId = sheetRow.ProgrammePriorityInvestmentPriorityId.Value,
                        Type = "InvestmentPriority",
                        Code = sheetRow.InvestmentPriorityCode,
                        ShortName = sheetRow.InvestmentPriorityShortName,
                        Name = sheetRow.InvestmentPriorityName,
                        NameAlt = sheetRow.InvestmentPriorityNameAlt,
                        InvestmentPriorityId = sheetRow.InvestmentPriorityId,
                        IsHidden = sheetRow.InvestmentPriorityCode == "00",
                    };
                    mapNodeDbRows.Add(investmentPriority);

                    // Add MapNodeRelation row
                    MapNodeRelationDbRow investmentPriorityRelation = this.CreateMapNodeRelationDbRow(investmentPriority.MapNodeId, programmePriority.MapNodeId, programme.MapNodeId, programmePriority.MapNodeId);
                    mapNodeRelationDbRows.Add(investmentPriorityRelation);
                }

                if (sheetRow.SpecificTargetId.HasValue)
                {
                    // Add MapNode row
                    MapNodeDbRow specificTarget = new MapNodeDbRow
                    {
                        MapNodeId = sheetRow.SpecificTargetId.Value,
                        Type = "SpecificTarget",
                        Code = sheetRow.SpecificTargetCode,
                        ShortName = sheetRow.SpecificTargetShortName,
                        Name = sheetRow.SpecificTargetName,
                        NameAlt = sheetRow.SpecificTargetNameAlt,
                        InvestmentPriorityId = null,
                        IsHidden = null,
                    };
                    mapNodeDbRows.Add(specificTarget);

                    // Add MapNodeRelation row
                    MapNodeRelationDbRow specificTargetRelation = this.CreateMapNodeRelationDbRow(specificTarget.MapNodeId, investmentPriority.MapNodeId, programme.MapNodeId, programmePriority.MapNodeId);
                    mapNodeRelationDbRows.Add(specificTargetRelation);
                }
            }

            return new List<DbTableData>
            {
                new DbTableData(MapNodeDbRow.DbTableName, MapNodeDbRow.UseIdentityInsert, mapNodeDbRows),
                new DbTableData(MapNodeRelationDbRow.DbTableName, MapNodeRelationDbRow.UseIdentityInsert, mapNodeRelationDbRows),
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
