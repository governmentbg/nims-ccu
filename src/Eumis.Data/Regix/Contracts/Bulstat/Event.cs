namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Event : Entry
    {
        public Document Document { get; set; }

        public NomenclatureEntry EventType { get; set; }

        public string EventDate { get; set; }

        public NomenclatureEntry LegalBase { get; set; }

        public NomenclatureEntry EntryType { get; set; }

        public Case Case { get; set; }
    }
}
