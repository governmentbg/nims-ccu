using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Resources;

namespace Eumis.Public.Data.UmisVOs
{
    public class EvalSessionStandingProjectVO : EvalSessionResultProjectVO
    {
        public decimal? GrantAmount { get; set; }

        public decimal? SelfAmount { get; set; }

        public bool? IsPassedPreliminary { get; set; }

        public decimal? PointsPreliminary { get; set; }

        public bool? IsPassedASD { get; set; }

        public decimal? PointsASD { get; set; }

        public bool? IsPassedTFO { get; set; }

        public decimal? PointsTFO { get; set; }

        public bool? IsPassedComplex { get; set; }

        public decimal? PointsComplex { get; set; }

        public int? OrderNum { get; set; }

        public EvalSessionProjectStandingStatus Status { get; set; }

        public decimal? CorrectedGrantAmount { get; set; }

        public decimal? CorrectedSelfAmount { get; set; }

        public string Note { get; set; }

        public string IsPassedPreliminaryText
        {
            get
            {
                return this.IsPassedPreliminary.HasValue ?
                    this.IsPassedPreliminary.Value ? Texts.EvalSessionResult_Standing_YesText : Texts.EvalSessionResult_Standing_NoText :
                    null;
            }
        }

        public string IsPassedASDText
        {
            get
            {
                return this.IsPassedASD.HasValue ?
                    this.IsPassedASD.Value ? Texts.EvalSessionResult_Standing_YesText : Texts.EvalSessionResult_Standing_NoText :
                    null;
            }
        }

        public string IsPassedTFOText
        {
            get
            {
                return this.IsPassedTFO.HasValue ?
                    this.IsPassedTFO.Value ? Texts.EvalSessionResult_Standing_YesText : Texts.EvalSessionResult_Standing_NoText :
                    null;
            }
        }

        public string IsPassedComplexText
        {
            get
            {
                return this.IsPassedComplex.HasValue ?
                    this.IsPassedComplex.Value ? Texts.EvalSessionResult_Standing_YesText : Texts.EvalSessionResult_Standing_NoText :
                    null;
            }
        }
    }
}
