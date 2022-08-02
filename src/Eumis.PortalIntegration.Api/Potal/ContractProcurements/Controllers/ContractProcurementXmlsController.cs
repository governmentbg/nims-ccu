using Eumis.ApplicationServices.Services.ContractProcurement;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures.ProcedureContractReportDocuments;
using Eumis.Log.ActionLogger;
using Eumis.Log.ActionLogger.Enums;
using Eumis.PortalIntegration.Api.Core;
using Eumis.PortalIntegration.Api.Potal.ContractProcurements.DataObjects;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Eumis.PortalIntegration.Api.Portal.ContractProcurements.Controllers
{
    [RoutePrefix("api/contractreg/contracts/{contractGid:guid}/procurements")]
    public class ContractProcurementXmlsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAccessContext accessContext;
        private IActionLogger actionLogger;
        private IContractProcurementsRepository contractProcurementsRepository;
        private IProceduresRepository proceduresRepository;
        private IContractsRepository contractsRepository;
        private IContractProcurementService contractProcurementService;

        public ContractProcurementXmlsController(
            IUnitOfWork unitOfWork,
            IAccessContext accessContext,
            IActionLogger actionLogger,
            IContractProcurementsRepository contractProcurementsRepository,
            IProceduresRepository proceduresRepository,
            IContractsRepository contractsRepository,
            IContractProcurementService contractProcurementService)
        {
            this.unitOfWork = unitOfWork;
            this.accessContext = accessContext;
            this.actionLogger = actionLogger;
            this.contractProcurementsRepository = contractProcurementsRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractsRepository = contractsRepository;
            this.contractProcurementService = contractProcurementService;
        }

        [Route("")]
        public ProcurementPagePVO GetContractProcurements(Guid contractGid, int offset = 0, int? limit = null)
        {
            var result = this.contractProcurementsRepository.GetPortalContractProcurements(contractGid, offset, limit);
            var canCreate = !this.contractProcurementService.CanCreateProcurement(contractGid).Any();

            return new ProcurementPagePVO(result, canCreate);
        }

        [Route("{procurementGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO GetContractProcurement(Guid contractGid, Guid procurementGid)
        {
            var procurement = this.contractProcurementsRepository.Find(procurementGid, Source.Beneficiary);

            return new XmlDO()
            {
                Xml = procurement.Xml,
                Version = procurement.Version,
                ModifyDate = procurement.ModifyDate,
            };
        }

        [Route("{procurementGid:guid}/edit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public ContractProcurementDO GetContractProcurementForEdit(Guid contractGid, Guid procurementGid)
        {
            var procurement = this.contractProcurementsRepository.Find(procurementGid, Source.Beneficiary);

            var contract = this.contractsRepository.FindWithoutIncludes(procurement.ContractId);
            var procedureProcurementDocuments = this.proceduresRepository.FindProcedureReportDocuments(contract.ProcedureId, ProcedureContractReportDocumentType.ProcurementDocument);

            return new ContractProcurementDO(procurement, procedureProcurementDocuments);
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("")]
        public XmlDO CreateContractProcurement(Guid contractGid)
        {
            if (this.contractProcurementService.CanCreateProcurement(contractGid).Any())
            {
                throw new HttpResponseException(
                    this.Request.CreateResponse(
                        HttpStatusCode.BadRequest,
                        new { error = PortalIntegrationErrors.ExistsProcurementInProgress }));
            }

            var contractId = this.contractsRepository.GetContractId(contractGid);

            ContractProcurementXml newContractProcurement = this.contractProcurementService.CreateProcurementFromBeneficiary(
                contractId,
                "Създадена от бенефициента процедурa за избор на изпълнител и сключени договори.");

            this.unitOfWork.Save();

            var result = new XmlDO
            {
                Gid = newContractProcurement.Gid,
                Xml = newContractProcurement.Xml,
                Version = newContractProcurement.Version,
                ModifyDate = newContractProcurement.ModifyDate,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractProcurements.Create),
                contractId,
                newContractProcurement.ContractProcurementXmlId,
                null,
                null);

            return result;
        }

        [HttpPut]
        [Transaction]
        [PessimisticLock]
        [Route("{procurementGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public XmlDO UpdateContractProcurement(Guid contractGid, Guid procurementGid, XmlDO procurementXml)
        {
            var procurement = this.contractProcurementsRepository.FindForUpdate(procurementGid, Source.Beneficiary, procurementXml.Version);

            procurement.SetXml(procurementXml.Xml);

            this.unitOfWork.Save();

            var response = new XmlDO
            {
                ModifyDate = procurement.ModifyDate,
                Version = procurement.Version,
            };

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractProcurements.UpdateXml),
                procurement.ContractId,
                procurement.ContractProcurementXmlId,
                null,
                null);

            return response;
        }

        [HttpPost]
        [Transaction]
        [PessimisticLock]
        [Route("{procurementGid:guid}/submit")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void SubmitContractProcurement(Guid contractGid, Guid procurementGid)
        {
            var procurement = this.contractProcurementsRepository.Find(procurementGid, Source.Beneficiary);

            procurement.ChangeStatusToEntered();

            this.contractProcurementService.ActivateProcurement(
                procurement.ContractProcurementXmlId,
                procurement.Version);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractProcurements.Activate),
                procurement.ContractId,
                procurement.ContractProcurementXmlId,
                null,
                null);
        }

        [HttpDelete]
        [Transaction]
        [PessimisticLock]
        [Route("{procurementGid:guid}")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("", "CA1801:ReviewUnusedParameters", Justification = "Action parameters left before lingting was enabled")]
        public void DeleteContractProcurement(Guid contractGid, Guid procurementGid)
        {
            var procurement = this.contractProcurementsRepository.Find(procurementGid, Source.Beneficiary);

            this.contractProcurementsRepository.Remove(procurement);

            this.unitOfWork.Save();

            this.actionLogger.LogAction(
                typeof(ActionLogPortalGroups.ContractProcurements.Delete),
                procurement.ContractId,
                procurement.ContractProcurementXmlId,
                null,
                null);
        }
    }
}
