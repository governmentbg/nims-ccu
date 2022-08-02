using Eumis.Public.Domain.Entities.Umis.EvalSessions;

namespace Eumis.Public.Data.UmisVOs
{
    public class EvalSessionPreliminaryProjectVO : EvalSessionResultProjectVO
    {
        public decimal? GrantAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public EvalSessionEvaluationResult PreliminaryResult { get; set; }

        public decimal? Points { get; set; }

        public int? OrderNum { get; set; }

        public EvalSessionProjectStandingStatus Status { get; set; }

        public string Note { get; set; }
    }
}
