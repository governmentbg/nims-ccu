using System;

namespace Eumis.ApplicationServices.Services.Registrations
{
    public interface IRegOfferService
    {
        void SubmitRegistrationOffer(Guid offerGid, byte[] version);

        void WithdrawRegistrationOffer(Guid offerGid, byte[] version);
    }
}
