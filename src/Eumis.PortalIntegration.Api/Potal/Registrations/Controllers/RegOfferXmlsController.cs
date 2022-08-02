using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.Registrations;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Data.Registrations.Repositories;
using Eumis.Domain.Registrations;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.Registrations.Controllers
{
    [RoutePrefix("api/registration/offers")]
    public class RegOfferXmlsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IRegOfferXmlsRepository regOfferXmlsRepository;
        private IContractsRepository contractsRepository;
        private IDocumentRestApiCommunicator documentRestApiCommunicator;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IRegOfferService regOfferService;

        public RegOfferXmlsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IRegOfferXmlsRepository regOfferXmlsRepository,
            IContractsRepository contractsRepository,
            IDocumentRestApiCommunicator documentRestApiCommunicator,
            IContractProcurementsRepository contractProcurementsRepository,
            IRegOfferService regOfferService)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.regOfferXmlsRepository = regOfferXmlsRepository;
            this.contractsRepository = contractsRepository;
            this.documentRestApiCommunicator = documentRestApiCommunicator;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.regOfferService = regOfferService;
        }

        [Route("submitted")]
        public PagePVO<RegOfferXmlPVO> GetRegistrationSubmittedOffers(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? submitDate = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            return this.regOfferXmlsRepository.GetSubmittedForRegistration(this.accessContext.RegistrationId, offset, limit, dpName, name, companyName, submitDate, sortBy, sortOrder);
        }

        [Route("drafts")]
        public PagePVO<RegOfferXmlPVO> GetRegistrationDraftOffers(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null)
        {
            return this.regOfferXmlsRepository.GetDraftsForRegistration(this.accessContext.RegistrationId, offset, limit, dpName, name, companyName);
        }

        [Route("{offerGid:guid}/info")]
        public RegOfferXmlPVO GetRegistrationOfferInfo(Guid offerGid)
        {
            return this.regOfferXmlsRepository.GetInfoForRegistration(this.accessContext.RegistrationId, offerGid);
        }

        [Route("{offerGid:guid}")]
        public XmlDO GetRegistrationOffer(Guid offerGid)
        {
            var regOfferXml = this.regOfferXmlsRepository.Find(this.accessContext.RegistrationId, offerGid);

            return new XmlDO
            {
                Gid = regOfferXml.Gid,
                Xml = regOfferXml.Xml,
                Version = regOfferXml.Version,
            };
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        public XmlDO CreateRegistrationOffer(Guid dpGid)
        {
            var dpInfo = this.contractsRepository.GetInfoForContractDifferentiatedPosition(dpGid);

            var regOfferXml = this.documentRestApiCommunicator.CreateRegOfferXml(
                dpInfo.Item1,
                dpInfo.Item2,
                dpInfo.Item3,
                dpInfo.Item4,
                dpInfo.Item5,
                dpGid);

            var newRegOfferXml = new RegOfferXml(
                this.accessContext.RegistrationId,
                dpInfo.ContractDifferentiatedPositionId,
                regOfferXml);

            this.regOfferXmlsRepository.Add(newRegOfferXml);

            this.unitOfWork.Save();

            return new XmlDO
            {
                Gid = newRegOfferXml.Gid,
                Version = newRegOfferXml.Version,
                ModifyDate = newRegOfferXml.ModifyDate,
                Xml = newRegOfferXml.Xml,
            };
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegOfferXmls.Update), DisablePostData = true, ChildIdParam = "offerGid")]
        [Route("{offerGid:guid}")]
        public XmlDO UpdateRegistrationOffer(Guid offerGid, XmlDO xmlDO)
        {
            var regOfferXml = this.regOfferXmlsRepository.FindForUpdate(this.accessContext.RegistrationId, offerGid, xmlDO.Version);
            regOfferXml.Update(xmlDO.Xml);

            this.unitOfWork.Save();

            return new XmlDO
            {
                Gid = regOfferXml.Gid,
                Version = regOfferXml.Version,
                ModifyDate = regOfferXml.ModifyDate,
            };
        }

        [HttpPost]
        [Route("{offerGid:guid}/submit")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegOfferXmls.Submit), ChildIdParam = "offerGid")]
        public void SubmitRegistrationOffer(Guid offerGid, VersionDO versionDO)
        {
            this.regOfferService.SubmitRegistrationOffer(offerGid, versionDO.Version);
        }

        [HttpPost]
        [Route("{offerGid:guid}/withdraw")]
        [Transaction]
        [PessimisticLock]
        [ActionLog(Action = typeof(ActionLogPortalGroups.RegOfferXmls.Withdraw), ChildIdParam = "offerGid")]
        public void WithdrawRegistrationOffer(Guid offerGid, VersionDO versionDO)
        {
            this.regOfferService.WithdrawRegistrationOffer(offerGid, versionDO.Version);
        }
    }
}
