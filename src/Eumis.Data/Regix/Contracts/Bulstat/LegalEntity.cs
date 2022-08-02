namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class LegalEntity : Entry
    {
        public NomenclatureEntry Country { get; set; }

        public NomenclatureEntry LegalForm { get; set; }

        public NomenclatureEntry LegalStatute { get; set; }

        public NomenclatureEntry SubjectGroup { get; set; }

        public string CyrillicFullName { get; set; }

        public string CyrillicShortName { get; set; }

        public string LatinFullName { get; set; }

        public NomenclatureEntry SubordinateLevel { get; set; }

        public string TRStatus { get; set; }
    }
}
