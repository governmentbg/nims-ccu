using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Authentication.Authorization.ClaimsContexts.ContractOffer
{
    internal delegate IContractProcurementsOfferClaimsContext ContractProcurementsOfferClaimsContextFactory(int offerId);

    internal interface IContractProcurementsOfferClaimsContext
    {
        int ContractOfferId { get; }

        int ProgrammeId { get; }

        int ContractId { get; }
    }
}
