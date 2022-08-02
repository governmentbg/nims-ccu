using Eumis.Database.Configurator.SheetRows.Abstract;
using System.Collections.Generic;

namespace Eumis.Database.Configurator.SheetRows
{
    internal class MapNodeInstitutionSheetRow : SheetRow
    {
        public MapNodeInstitutionSheetRow(Dictionary<int, string> row)
        {
            this.ProgrammeId = this.GetIntCellValue(0, row);
            this.InstitutionType = this.GetStringCellValue(4, row);
            this.Institution = this.GetStringCellValue(5, row);
        }

        public int? ProgrammeId { get; private set; }

        public string InstitutionType { get; private set; }

        public string Institution { get; private set; }
    }
}
