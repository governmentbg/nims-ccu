using System.Collections.Generic;

namespace Eumis.Public.Common.Export
{
    public class ExportSheet
    {
        public ExportSheet()
        {
            this.StartNotes = new List<string>();
            this.Tables = new List<ExportTable>();
            this.EndNotes = new List<string>();
            this.ExcelColumnWidths = new Dictionary<int, int>();
        }

        public ExportSheet(string name)
            : this()
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public List<string> StartNotes { get; set; }

        public List<ExportTable> Tables { get; set; }

        public List<string> EndNotes { get; set; }

        public Dictionary<int, int> ExcelColumnWidths { get; set; }
    }
}
