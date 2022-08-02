using Autofac.Features.AttributeFilters;
using Eumis.Data.Audits.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.Audit
{
    internal class AuditClaimsContext : ClaimsContext, IAuditClaimsContext
    {
        private int auditId;

        private IClaimsCache claimsCache;
        private IAuditsRepository auditsRepository;

        public AuditClaimsContext(
            int auditId,
            [KeyFilter(ClaimsCaches.Audit)]IClaimsCache claimsCache,
            IAuditsRepository auditsRepository)
            : base(claimsCache)
        {
            this.auditId = auditId;
            this.claimsCache = claimsCache;
            this.auditsRepository = auditsRepository;
        }

        public int AuditId
        {
            get
            {
                return this.auditId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.auditId,
                    new ClaimKey("ProgrammeId"),
                    () => this.auditsRepository.GetProgrammeId(this.auditId));
            }
        }

        public int? ContractId
        {
            get
            {
                return this.GetClaim(
                    this.auditId,
                    new ClaimKey("ContractId"),
                    () => this.auditsRepository.GetContractId(this.auditId));
            }
        }
    }
}
