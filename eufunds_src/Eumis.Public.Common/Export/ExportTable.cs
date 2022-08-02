using System.Collections.Generic;

namespace Eumis.Public.Common.Export
{
    public class ExportTable
    {
        public ExportTable()
        {
            this.Rows = new List<ExportRow>();
        }

        public ExportTable(string header)
            : this()
        {
            this.Header = header;
        }

        public string Header { get; set; }

        public List<ExportRow> Rows { get; set; }
    }
}
