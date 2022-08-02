namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Address : Entry
    {
        public NomenclatureEntry AddressType { get; set; }

        public NomenclatureEntry Country { get; set; }

        public string PostalCode { get; set; }

        public string PostalBox { get; set; }

        public string ForeignLocation { get; set; }

        public NomenclatureEntry Location { get; set; }

        public NomenclatureEntry Region { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Building { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }
    }
}
