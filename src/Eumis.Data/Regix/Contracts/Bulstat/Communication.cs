namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Communication : Entry
    {
        public NomenclatureEntry Type { get; set; }

        public string Value { get; set; }
    }
}
