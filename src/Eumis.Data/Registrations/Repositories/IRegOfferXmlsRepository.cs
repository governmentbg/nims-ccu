using Eumis.Common.Json;
using Eumis.Data.Core;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Data.Registrations.ViewObjects;
using Eumis.Domain.Registrations;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Registrations.Repositories
{
    public interface IRegOfferXmlsRepository : IAggregateRepository<RegOfferXml>
    {
        // Registrations
        RegOfferXml Find(int registrationId, Guid offerGid);

        RegOfferXml FindForUpdate(int registrationId, Guid offerGid, byte[] version);

        RegOfferXmlPVO GetInfoForRegistration(int registrationId, Guid offerGid);

        PagePVO<RegOfferXmlPVO> GetSubmittedForRegistration(
            int registrationId,
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? submitDate = null,
            string sortBy = null,
            SortOrder? sortOrder = null);

        PagePVO<RegOfferXmlPVO> GetDraftsForRegistration(
            int registrationId,
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null);

        // ContractRegistrations
        RegOfferXml Find(Guid contractGid, Guid offerGid);

        ContractRegOfferXmlPVO GetInfoForContractRegistration(Guid contractGid, Guid offerGid);

        PagePVO<ContractRegOfferXmlPVO> GetAllForContractRegistration(Guid contractGid, int offset = 0, int? limit = null);

        // Backend
        RegOfferXml FindActive(Guid offerGid);

        IList<ContractOfferVO> GetAllForContract(int contractId);

        ContractOfferVO GetOfferDetails(int offerId);

        int GetSubmittedCount(int contractDifferentiatedPositionId);

        // Rights management
        int GetProgrammeId(int offerId);

        int GetProjectId(int offerId);

        int GetContractId(int offerId);
    }
}
