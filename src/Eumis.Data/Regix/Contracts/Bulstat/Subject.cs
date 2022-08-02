using AddressCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.Address>;
using CommunicationCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.Communication>;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class Subject : Entry
    {
        public UIC UIC { get; set; }

        public NomenclatureEntry SubjectType { get; set; }

        public LegalEntity LegalEntitySubject { get; set; }

        public NaturalPerson NaturalPersonSubject { get; set; }

        public NomenclatureEntry CountrySubject { get; set; }

        public CommunicationCollection CommunicationsCollection { get; set; }

        public AddressCollection AddressesCollection { get; set; }

        public string Remark { get; set; }
    }
}
