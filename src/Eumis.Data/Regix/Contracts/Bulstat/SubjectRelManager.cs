using SubjectCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.Subject>;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class SubjectRelManager : SubscriptionElement
    {
        public Subject RelatedSubject { get; set; }

        public NomenclatureEntry Position { get; set; }

        public SubjectCollection RepresentedSubjectsCollection { get; set; }
    }
}
