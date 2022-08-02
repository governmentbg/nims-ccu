namespace Eumis.Public.Common.Export
{
    public class ExportCell
    {
        public ExportCell()
        {
            this.RowSpan = 1;
            this.ColSpan = 1;
        }

        public ExportCell(string value)
            : this()
        {
            this.Value = value;
        }

        public ExportCell(string value, int width)
            : this(value)
        {
            this.Width = width;
        }

        public ExportCell(string value, bool isHeader)
            : this(value)
        {
            this.IsHeader = isHeader;
        }

        public ExportCell(string value, int width, bool isHeader)
            : this(value, width)
        {
            this.IsHeader = isHeader;
        }

        public string Value { get; set; }

        public int? Width { get; set; }

        public bool IsHeader { get; set; }

        public int RowSpan { get; set; }

        public int ColSpan { get; set; }

        public bool IsBold { get; set; }

        public bool IsItalic { get; set; }

        public bool IsNumber { get; set; }

        public bool IsDateTime { get; set; }

        public bool IsMoney { get; set; }
    }
}
