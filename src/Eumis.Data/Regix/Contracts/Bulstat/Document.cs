namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Document : Entry
    {
        public NomenclatureEntry DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentDate { get; set; }

        public string ValidFromDate { get; set; }

        public string DocumentName { get; set; }

        public Subject Author { get; set; }

        public string AuthorName { get; set; }
    }
}
