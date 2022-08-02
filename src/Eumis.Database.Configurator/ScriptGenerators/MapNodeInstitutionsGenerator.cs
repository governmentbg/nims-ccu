using Eumis.Database.Configurator.DbRows;
using Eumis.Database.Configurator.SheetRows;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Database.Configurator.ScriptGenerators
{
    internal class MapNodeInstitutionsGenerator : ScriptsGenerator
    {
        public MapNodeInstitutionsGenerator(Dictionary<string, List<Dictionary<int, string>>> sheetsRowsDictionary)
            : base(sheetsRowsDictionary)
        {
            this.ExcelSheetRows = this.GetExcelSheetRows("MapNodeInstitutions");

            this.InstitutionDbRows = new List<InstitutionDbRow>();
            this.MapNodeInstitutionDbRows = new List<MapNodeInstitutionDbRow>();
        }

        protected List<Dictionary<int, string>> ExcelSheetRows { get; set; }

        private List<InstitutionDbRow> InstitutionDbRows { get; set; }

        private List<MapNodeInstitutionDbRow> MapNodeInstitutionDbRows { get; set; }

        private int GetNextInstitutionIdentity()
        {
            int identity = 1;

            if (this.InstitutionDbRows.Count > 0)
            {
                identity = this.InstitutionDbRows.Max(e => e.InstitutionId) + 1;
            }

            return identity;
        }

        protected override List<DbTableData> GetDbTableData()
        {
            int? currentProgrammeId = null;

            foreach (var row in this.ExcelSheetRows)
            {
                MapNodeInstitutionSheetRow sheetRow = new MapNodeInstitutionSheetRow(row);

                if (sheetRow.ProgrammeId.HasValue)
                {
                    currentProgrammeId = sheetRow.ProgrammeId.Value;
                }

                int? institutionId = null;

                InstitutionDbRow addedInstitutionDbRow =
                    this.InstitutionDbRows.SingleOrDefault(e => e.Name.ToLower() == sheetRow.Institution.ToLower());

                if (addedInstitutionDbRow != null)
                {
                    institutionId = addedInstitutionDbRow.InstitutionId;
                }
                else
                {
                    InstitutionDbRow institutionDbRow = new InstitutionDbRow
                    {
                        InstitutionId = this.GetNextInstitutionIdentity(),
                        Name = sheetRow.Institution,
                        Description = @"Описание",
                    };

                    this.InstitutionDbRows.Add(institutionDbRow);

                    institutionId = institutionDbRow.InstitutionId;
                }

                MapNodeInstitutionDbRow mapNodeInstitutionDbRow = new MapNodeInstitutionDbRow
                {
                    MapNodeId = currentProgrammeId.Value,
                    InstitutionId = institutionId.Value,
                    InstitutionTypeId = Nomenclatures.InstitutionsDictionary.GetNomId(sheetRow.InstitutionType).Value,
                    ContactName = @"Име на контакт",
                    ContactPosition = @"Длъжност",
                    ContactPhone = @"Телефонен номер",
                    ContactFax = @"Факс",
                    ContactEmail = @"e-mail",
                };

                this.MapNodeInstitutionDbRows.Add(mapNodeInstitutionDbRow);
            }

            var institutionList = new List<IDbRow>();
            institutionList.AddRange(this.InstitutionDbRows);

            var mapNodeInstitutionList = new List<IDbRow>();
            mapNodeInstitutionList.AddRange(this.MapNodeInstitutionDbRows);

            return new List<DbTableData>
            {
                new DbTableData(InstitutionDbRow.DbTableName, InstitutionDbRow.UseIdentityInsert, institutionList),
                new DbTableData(MapNodeInstitutionDbRow.DbTableName, MapNodeInstitutionDbRow.UseIdentityInsert, mapNodeInstitutionList),
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
