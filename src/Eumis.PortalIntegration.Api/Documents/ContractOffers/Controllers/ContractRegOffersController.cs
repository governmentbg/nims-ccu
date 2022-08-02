using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Data.Registrations.Repositories;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Documents.ContractOffers.Controllers
{
    [RoutePrefix("api/contractOffers")]
    public class ContractRegOffersController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRegOfferXmlsRepository regOfferXmlsRepository;
        private IAuthorizer authorizer;

        public ContractRegOffersController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IRegOfferXmlsRepository regOfferXmlsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.regOfferXmlsRepository = regOfferXmlsRepository;
        }

        [Route("{offerGid:guid}")]
        public XmlDO GetContractRegistrationOffer(Guid offerGid)
        {
            var regOfferXml = this.regOfferXmlsRepository.FindActive(offerGid);
            var contractOfferProjectId = this.regOfferXmlsRepository.GetProjectId(regOfferXml.RegOfferXmlId);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractProcurementsOfferActions.View, regOfferXml.RegOfferXmlId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, contractOfferProjectId));

            return new XmlDO
            {
                Xml = regOfferXml.Xml,
                Version = regOfferXml.Version,
            };
        }
    }
}
