using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eumis.Data.Contracts.PortalViewObjects;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core;
using Eumis.Domain.Contracts;
using Eumis.PortalIntegration.Api.Core;

namespace Eumis.PortalIntegration.Api.Portal.ContractVersions.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/versions")]
    public class ContractVersionXmlsController : ApiController
    {
        private IContractVersionsRepository contractVersionsRepository;

        public ContractVersionXmlsController(IContractVersionsRepository contractVersionsRepository)
        {
            this.contractVersionsRepository = contractVersionsRepository;
        }

        [Route("")]
        public PagePVO<ContractVersionPVO> GetContractVersions(Guid contractGid, int offset = 0, int? limit = null)
        {
            return this.contractVersionsRepository.GetPortalContractVersions(contractGid, offset, limit);
        }

        [Route("{versionGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO GetContractVersion(Guid contractGid, Guid versionGid)
        {
            ContractVersionXml version = this.contractVersionsRepository.Find(versionGid);

            if (!ContractVersionXml.FinalStatuses.Contains(version.Status))
            {
                throw new HttpResponseException(
                this.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new { error = PortalIntegrationErrors.ContractVersionInProgress }));
            }

            return new XmlDO
            {
                Xml = version.Xml,
                ModifyDate = version.ModifyDate,
            };
        }
    }
}
