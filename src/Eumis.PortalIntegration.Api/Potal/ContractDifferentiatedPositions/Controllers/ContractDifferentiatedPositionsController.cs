using Eumis.Common.Db;
using Eumis.Common.Json;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using System;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.ContractDifferentiatedPositions.Controllers
{
    [RoutePrefix("api/differentiatedpositions")]
    public class ContractDifferentiatedPositionsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IContractsRepository contractsRepository;

        public ContractDifferentiatedPositionsController(
            IUnitOfWork unitOfWork,
            IContractsRepository contractsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.contractsRepository = contractsRepository;
        }

        [AllowAnonymous]
        [Route("announced")]
        public PagePVO<ContractDifferentiatedPositionPVO> GetAnnouncedContractDifferentiatedPositions(
            int offset = 0,
            int? limit = null,
            string dpName = null,
            string name = null,
            string companyName = null,
            DateTime? offersDeadlineDate = null,
            DateTime? noticeDate = null,
            string sortBy = null,
            SortOrder? sortOrder = null)
        {
            return this.contractsRepository.GetAnnouncedContractDifferentiatedPositions(
                offset,
                limit,
                dpName,
                name,
                companyName,
                offersDeadlineDate,
                noticeDate,
                sortBy,
                sortOrder);
        }

        [AllowAnonymous]
        [Route("archived")]
        public PagePVO<ContractDifferentiatedPositionPVO> GetArchivedContractDifferentiatedPositions(
           int offset = 0,
           int? limit = null,
           string dpName = null,
           string name = null,
           string companyName = null)
        {
            return this.contractsRepository.GetArchivedContractDifferentiatedPositions(offset, limit, dpName, name, companyName);
        }

        [AllowAnonymous]
        [Route("{dpGid:guid}")]
        public ContractDifferentiatedPositionPVO GetContractDifferentiatedPosition(Guid dpGid)
        {
            return this.contractsRepository.GetContractDifferentiatedPosition(dpGid);
        }
    }
}
