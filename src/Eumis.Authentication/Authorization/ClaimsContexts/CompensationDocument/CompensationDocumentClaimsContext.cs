using Autofac.Features.AttributeFilters;
using Eumis.Data.CompensationDocuments.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.CompensationDocument
{
    internal class CompensationDocumentClaimsContext : ClaimsContext, ICompensationDocumentClaimsContext
    {
        private int compensationDocumentId;

        private IClaimsCache claimsCache;
        private ICompensationDocumentsRepository compensationDocumentsRepository;

        public CompensationDocumentClaimsContext(
            int compensationDocumentId,
            [KeyFilter(ClaimsCaches.CompensationDocument)]IClaimsCache claimsCache,
            ICompensationDocumentsRepository compensationDocumentsRepository)
            : base(claimsCache)
        {
            this.compensationDocumentId = compensationDocumentId;
            this.claimsCache = claimsCache;
            this.compensationDocumentsRepository = compensationDocumentsRepository;
        }

        public int CompensationDocumentId
        {
            get
            {
                return this.compensationDocumentId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.compensationDocumentId,
                    new ClaimKey("ProgrammeId"),
                    () => this.compensationDocumentsRepository.GetProgrammeId(this.compensationDocumentId));
            }
        }
    }
}
