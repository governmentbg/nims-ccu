using System.Collections.Generic;

namespace Eumis.Public.Common.Export
{
    public class ExportRow
    {
        public ExportRow()
        {
            this.Cells = new List<ExportCell>();
        }

        public List<ExportCell> Cells { get; set; }
    }
}
