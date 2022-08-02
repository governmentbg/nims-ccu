namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class SubjectPropFundingSource : Entry
    {
        public NomenclatureEntry Source { get; set; }

        public decimal Percent { get; set; }
    }
}
