namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class IdentificationDoc : Entry
    {
        public NomenclatureEntry IDType { get; set; }

        public string IDNumber { get; set; }

        public NomenclatureEntry Country { get; set; }

        public string IssueDate { get; set; }

        public string ExpiryDate { get; set; }
    }
}
