using Autofac.Features.AttributeFilters;
using Eumis.Data.Registrations.Repositories;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractOffer
{
    internal class ContractProcurementsOfferClaimsContext : ClaimsContext, IContractProcurementsOfferClaimsContext
    {
        private int offerId;

        private IClaimsCache claimsCache;
        private IRegOfferXmlsRepository contractOfferRepository;

        public ContractProcurementsOfferClaimsContext(
        int offerId,
        [KeyFilter(ClaimsCaches.ContractProcurementOffer)]IClaimsCache claimsCache,
        IRegOfferXmlsRepository contractOfferRepository)
        : base(claimsCache)
        {
            this.offerId = offerId;
            this.claimsCache = claimsCache;
            this.contractOfferRepository = contractOfferRepository;
        }

        public int ContractOfferId
        {
            get
            {
                return this.offerId;
            }
        }

        public int ProgrammeId
        {
            get
            {
                return this.GetClaim(
                    this.ContractOfferId,
                    new ClaimKey("ProgrammeId"),
                    () => this.contractOfferRepository.GetProgrammeId(this.offerId));
            }
        }

        public int ContractId
        {
            get
            {
                return this.GetClaim(
                    this.ContractOfferId,
                    new ClaimKey("ContractId"),
                    () => this.contractOfferRepository.GetContractId(this.offerId));
            }
        }
    }
}
