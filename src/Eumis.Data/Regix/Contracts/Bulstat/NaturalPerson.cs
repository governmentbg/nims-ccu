namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class NaturalPerson : Entry
    {
        public NomenclatureEntry Country { get; set; }

        public string EGN { get; set; }

        public string LNC { get; set; }

        public string CyrillicName { get; set; }

        public string LatinName { get; set; }

        public string BirthDate { get; set; }

        public IdentificationDoc IdentificationDoc { get; set; }
    }
}
