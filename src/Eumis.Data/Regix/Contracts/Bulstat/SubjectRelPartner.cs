namespace Eumis.Data.Regix.Contracts.Bulstat
{
    public partial class SubjectRelPartner : SubscriptionElement
    {
        public Subject RelatedSubject { get; set; }

        public NomenclatureEntry Role { get; set; }

        public decimal Percent { get; set; }

        public decimal Amount { get; set; }
    }
}
