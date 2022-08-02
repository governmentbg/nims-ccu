namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class SubjectRelBelonging : SubscriptionElement
    {
        public Subject RelatedSubject { get; set; }

        public NomenclatureEntry Type { get; set; }
    }
}
