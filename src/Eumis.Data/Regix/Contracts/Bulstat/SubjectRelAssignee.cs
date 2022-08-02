using SubjectCollection = System.Collections.Generic.List<Eumis.Data.Regix.Contracts.Bulstat.Subject>;

namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class SubjectRelAssignee : SubscriptionElement
    {
        public SubjectCollection RelatedSubjectsCollection { get; set; }

        public NomenclatureEntry Type { get; set; }
    }
}
