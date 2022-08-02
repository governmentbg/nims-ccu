using System;
using Eumis.Domain.CertAuthorityChecks;

namespace Eumis.Web.Api.CertAuthorityChecks.DataObjects
{
    public class CertAuthorityCheckAscertainmentDO
    {
        public CertAuthorityCheckAscertainmentDO()
        {
        }

        public CertAuthorityCheckAscertainmentDO(int certAuthorityCheckId, byte[] version)
        {
            this.CertAuthorityCheckId = certAuthorityCheckId;
            this.Version = version;
        }

        public CertAuthorityCheckAscertainmentDO(CertAuthorityCheckAscertainment ascertainment, byte[] version)
        {
            this.AscertainmentId = ascertainment.CertAuthorityCheckAscertainmentId;
            this.CertAuthorityCheckId = ascertainment.CertAuthorityCheckId;
            this.Type = ascertainment.Type;
            this.Ascertainment = ascertainment.Ascertainment;
            this.Status = ascertainment.Status;
            this.Recommendation = ascertainment.Recommendation;
            this.RecommendationDeadline = ascertainment.RecommendationDeadline;
            this.RecommendationExecutionStatus = ascertainment.RecommendationExecutionStatus;
            this.CertAuthorityComment = ascertainment.CertAuthorityComment;
            this.ManagingAuthorityComment = ascertainment.ManagingAuthorityComment;

            this.Version = version;
        }

        public int CertAuthorityCheckId { get; set; }

        public int AscertainmentId { get; set; }

        public CertAuthorityCheckAscertainmentType? Type { get; set; }

        public string Ascertainment { get; set; }

        public CertAuthorityCheckAscertainmentStatus? Status { get; set; }

        public string Recommendation { get; set; }

        public DateTime? RecommendationDeadline { get; set; }

        public CertAuthorityCheckAscertainmentExecutionStatus? RecommendationExecutionStatus { get; set; }

        public string CertAuthorityComment { get; set; }

        public string ManagingAuthorityComment { get; set; }

        public byte[] Version { get; set; }
    }
}
