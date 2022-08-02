using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedureMassCommunications/{communicationId:int}/recipients")]
    public class ProcedureMassCommunicationRecipientsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IProcedureMassCommunicationsRepository procedureMassCommunicationsRepository;

        public ProcedureMassCommunicationRecipientsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IProcedureMassCommunicationsRepository procedureMassCommunicationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.procedureMassCommunicationsRepository = procedureMassCommunicationsRepository;
        }

        [Route("unattachedContracts")]
        public IList<ProcedureMassCommunicationRecipientVO> GetContractReportCorrectionsForAnnualAccountReportCertCorrections(int communicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);
            var communication = this.procedureMassCommunicationsRepository.FindWithoutIncludes(communicationId);
            return this.procedureMassCommunicationsRepository.GetUnattachedContracts(communicationId, communication.ProcedureId);
        }

        [Route("")]
        public IList<ProcedureMassCommunicationRecipientVO> GetAnnualAccountReportCertCorrections(int communicationId)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.View, communicationId);

            return this.procedureMassCommunicationsRepository.GetAttachedContracts(communicationId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProcedureMassCommunication.Edit.Recipients.Create), IdParam = "communicationId")]
        public void CreateAnnualAccountReportCertifiedCorrection(int communicationId, string version, int[] contractIds)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);

            byte[] vers = Convert.FromBase64String(version);

            var communication = this.procedureMassCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.AddRecipients(contractIds);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{contractId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.ProcedureMassCommunication.Edit.Recipients.Delete), IdParam = "communicationId", ChildIdParam = "contractId")]
        public void DeleteAnnualAccountReportCertifiedCorrection(int communicationId, int contractId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureMassCommunicationActions.Edit, communicationId);

            byte[] vers = Convert.FromBase64String(version);

            var communication = this.procedureMassCommunicationsRepository.FindForUpdate(communicationId, vers);

            communication.RemoveRecipient(contractId);

            this.unitOfWork.Save();
        }
    }
}
