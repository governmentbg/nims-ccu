using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Authentication.Authorization.ClaimsContexts.User;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Relations;
using Eumis.Data.Registrations.Repositories;
using Eumis.Data.Registrations.ViewObjects;

namespace Eumis.Web.Api.Contracts.Controllers
{
    [RoutePrefix("api/contracts/{contractId:int}/offers")]
    public class ContractOffersController : ApiController
    {
        private IRelationsRepository relationsRepository;
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IUserClaimsContext currentUserClaimsContext;
        private IContractsRepository contractsRepository;
        private UserClaimsContextFactory userClaimsContextFactory;
        private IRegOfferXmlsRepository regOfferXmlsRepository;

        public ContractOffersController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            UserClaimsContextFactory userClaimsContextFactory,
            IContractsRepository contractsRepository,
            IRegOfferXmlsRepository regOfferXmlsRepository,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.userClaimsContextFactory = userClaimsContextFactory;
            this.regOfferXmlsRepository = regOfferXmlsRepository;
            this.contractsRepository = contractsRepository;
            this.relationsRepository = relationsRepository;

            if (accessContext.IsUser)
            {
                this.currentUserClaimsContext = userClaimsContextFactory(accessContext.UserId);
            }
        }

        [Route("")]
        public IList<ContractOfferVO> GetContractOffers(int contractId)
        {
            this.authorizer.AssertCanDo(ContractActions.View, contractId);

            return this.regOfferXmlsRepository.GetAllForContract(contractId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/offers")]
        public IList<ContractOfferVO> GetProjectDossierContractOffers(int projectId, int contractId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            if (this.contractsRepository.FindWithoutIncludes(contractId).ProjectId != projectId)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            return this.regOfferXmlsRepository.GetAllForContract(contractId);
        }

        [Route("{offerId:int}")]
        public ContractOfferVO GetOffer(int contractId, int offerId)
        {
            this.authorizer.AssertCanDo(ContractProcurementsOfferActions.View, offerId);

            if (this.regOfferXmlsRepository.GetContractId(offerId) != contractId)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            return this.regOfferXmlsRepository.GetOfferDetails(offerId);
        }

        [Route("~/api/projectDossier/{projectId:int}/contract/{contractId:int}/offers/{offerId:int}")]
        public ContractOfferVO GetProjectDossierContractOffer(int projectId, int contractId, int offerId)
        {
            this.authorizer.AssertCanDo(ProjectDossierActions.View, projectId);
            this.relationsRepository.AssertProjectHasContract(projectId, contractId);

            if (this.regOfferXmlsRepository.GetProjectId(offerId) != projectId || this.regOfferXmlsRepository.GetContractId(offerId) != contractId)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            return this.regOfferXmlsRepository.GetOfferDetails(offerId);
        }
    }
}
