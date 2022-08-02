namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Case : Entry
    {
        public NomenclatureEntry Court { get; set; }

        public string Number { get; set; }

        public string Batch { get; set; }

        public int Register { get; set; }

        public string Chapter { get; set; }

        public int PageNumber { get; set; }
    }
}
