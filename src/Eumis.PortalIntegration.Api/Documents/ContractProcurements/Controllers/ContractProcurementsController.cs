using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procurements.PortalViewObjects;
using Eumis.Data.Procurements.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Documents.ContractProcurements.DataObjects;

namespace Eumis.PortalIntegration.Api.Documents.ContractProcurements.Controllers
{
    [RoutePrefix("api/contractProcurements")]
    public class ContractProcurementsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IActionLogger actionLogger;
        private IAuthorizer authorizer;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IContractsRepository contractsRepository;
        private IProceduresRepository proceduresRepository;
        private IProcurementsRepository procurementsRepository;

        public ContractProcurementsController(
            IUnitOfWork unitOfWork,
            IActionLogger actionLogger,
            IAuthorizer authorizer,
            IContractProcurementsRepository contractProcurementsRepository,
            IContractsRepository contractsRepository,
            IProceduresRepository proceduresRepository,
            IProcurementsRepository procurementsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.actionLogger = actionLogger;
            this.authorizer = authorizer;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.contractsRepository = contractsRepository;
            this.proceduresRepository = proceduresRepository;
            this.procurementsRepository = procurementsRepository;
        }

        [Route("{procurementXmlGid:guid}")]
        public XmlDO GetContractProcurement(Guid procurementXmlGid)
        {
            var contractProcurement = this.contractProcurementsRepository.Find(procurementXmlGid, Source.AdministrativeAuthority);
            var contractProcurementProjectId = this.contractProcurementsRepository.GetProjectId(contractProcurement.ContractProcurementXmlId);

            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(ContractProcurementActions.View, contractProcurement.ContractProcurementXmlId),
                Tuple.Create<Enum, int?>(ProjectDossierActions.View, contractProcurementProjectId));

            var contract = this.contractsRepository.FindWithoutIncludes(contractProcurement.ContractId);
            var procedureProcurementDocuments = this.proceduresRepository.FindProcedureReportDocuments(contract.ProcedureId, ProcedureContractReportDocumentType.ProcurementDocument);

            return new ContractProcurementXmlDO(contractProcurement.Xml, contractProcurement.Version, procedureProcurementDocuments);
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{procurementXmlGid:guid}")]
        public XmlDO UpdateContractProcurementXml(Guid procurementXmlGid, XmlDO procurementXmlDO)
        {
            var contractProcurementId = this.contractProcurementsRepository.GetProcurementId(procurementXmlGid);
            this.authorizer.AssertCanDo(ContractProcurementActions.Edit, contractProcurementId);

            var contractProcurement = this.contractProcurementsRepository.FindForUpdate(procurementXmlGid, Source.AdministrativeAuthority, procurementXmlDO.Version);

            contractProcurement.SetXml(procurementXmlDO.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = contractProcurement.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.Procurements.UpdateXml),
                contractProcurement.ContractId,
                contractProcurement.ContractProcurementXmlId,
                procurementXmlDO,
                response);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{procurementXmlGid:guid}/enter")]
        public XmlDO EnterContractProcurement(Guid procurementXmlGid, XmlDO procurementXmlDO)
        {
            var contractProcurementId = this.contractProcurementsRepository.GetProcurementId(procurementXmlGid);
            this.authorizer.AssertCanDo(ContractProcurementActions.Edit, contractProcurementId);

            var contractProcurement = this.contractProcurementsRepository.FindForUpdate(procurementXmlGid, Source.AdministrativeAuthority, procurementXmlDO.Version);

            contractProcurement.ChangeStatusToEntered();

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                Version = contractProcurement.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogGroups.Contracts.Edit.Procurements.ChangeStatusToEntered),
                contractProcurement.ContractId,
                contractProcurement.ContractProcurementXmlId,
                procurementXmlDO,
                response);

            return response;
        }

        [HttpGet]
        [Route("centralProcurements")]
        public IList<CentralProcurementPVO> GetCentralProcurements()
        {
            return this.procurementsRepository.GetProcurementPVOs();
        }
    }
}
