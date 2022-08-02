using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Data.Registrations.Repositories;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.ContractOffers.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/offers")]
    public class ContractRegOffers1Controller : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IRegOfferXmlsRepository regOfferXmlsRepository;

        public ContractRegOffers1Controller(
            IUnitOfWork unitOfWork,
            IRegOfferXmlsRepository regOfferXmlsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.regOfferXmlsRepository = regOfferXmlsRepository;
        }

        [Route("")]
        public PagePVO<ContractRegOfferXmlPVO> GetRegistrationOffers(Guid contractGid, int offset = 0, int? limit = null)
        {
            return this.regOfferXmlsRepository.GetAllForContractRegistration(contractGid, offset, limit);
        }

        [Route("{offerGid:guid}/info")]
        public ContractRegOfferXmlPVO GetRegistrationOfferInfo(Guid contractGid, Guid offerGid)
        {
            return this.regOfferXmlsRepository.GetInfoForContractRegistration(contractGid, offerGid);
        }

        [Route("{offerGid:guid}")]
        public XmlDO GetRegistrationOffer(Guid contractGid, Guid offerGid)
        {
            var regOfferXml = this.regOfferXmlsRepository.Find(contractGid, offerGid);

            return new XmlDO
            {
                Gid = regOfferXml.Gid,
                Xml = regOfferXml.Xml,
                Version = regOfferXml.Version,
            };
        }
    }
}
