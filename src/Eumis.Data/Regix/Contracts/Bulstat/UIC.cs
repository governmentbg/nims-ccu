namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class UIC : Entry
    {
        public string UICValue { get; set; }

        public NomenclatureEntry UICType { get; set; }
    }
}
