using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.ApplicationServices.Services.CompensationDocument;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CompensationDocuments.Repositories;
using Eumis.Data.CompensationDocuments.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Counters;
using Eumis.Domain.MonitoringFinancialControl.CompensationDocuments;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.CompensationDocuments.DataObjects;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.CompensationDocuments.Controllers
{
    [RoutePrefix("api/compensationDocuments")]
    public class CompensationDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICountersRepository countersRepository;
        private ICompensationDocumentsRepository compensationDocumentsRepository;
        private ICompensationDocumentService compensationDocumentService;

        public CompensationDocumentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICountersRepository countersRepository,
            ICompensationDocumentsRepository compensationDocumentsRepository,
            ICompensationDocumentService compensationDocumentService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.countersRepository = countersRepository;
            this.compensationDocumentsRepository = compensationDocumentsRepository;
            this.compensationDocumentService = compensationDocumentService;
        }

        [Route("")]
        public IList<CompensationDocumentVO> GetCompensationDocuments(
            int? programmeId = null,
            CompensationDocumentType? type = null,
            CompensationDocumentStatus? status = null)
        {
            this.authorizer.AssertCanDo(CompensationDocumentListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            if (programmeId.HasValue)
            {
                programmeIds = programmeIds.Where(id => id == programmeId).ToArray();
            }

            return this.compensationDocumentsRepository.GetCompensationDocuments(programmeIds, type, status);
        }

        [HttpGet]
        [Route("new")]
        public NewCompensationDocumentDO NewCompensationDocument()
        {
            this.authorizer.AssertCanDo(CompensationDocumentListActions.Create);

            return new NewCompensationDocumentDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.Create))]
        public object CreateCompensationDocument(NewCompensationDocumentDO newDoc)
        {
            this.authorizer.AssertCanDo(CompensationDocumentListActions.Create);

            var newCompensationDoc = this.compensationDocumentService.CreateCompensationDocument(
                this.accessContext.UserId,
                newDoc.Type.Value,
                newDoc.CompensationSign.Value,
                newDoc.CompensationDocDate.Value,
                newDoc.ContractId.Value,
                newDoc.ProgrammePriorityId.Value,
                newDoc.ContractReportPaymentId);

            return new { CompensationDocumentId = newCompensationDoc.CompensationDocumentId };
        }

        [Route("{compensationDocumentId:int}/info")]
        public CompensationDocumentInfoVO GetCompensationDocumentInfo(int compensationDocumentId)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.View, compensationDocumentId);

            return this.compensationDocumentsRepository.GetInfo(compensationDocumentId);
        }

        [Route("{compensationDocumentId:int}/data")]
        public CompensationDocumentBasicDataVO GetCompensationDocumentBasicData(int compensationDocumentId)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.View, compensationDocumentId);

            return this.compensationDocumentsRepository.GetBasicData(compensationDocumentId);
        }

        [Route("{compensationDocumentId:int}")]
        public CompensationDocumentDO GetCompensationDocumentData(int compensationDocumentId)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.View, compensationDocumentId);

            var compensationDocument = this.compensationDocumentsRepository.Find(compensationDocumentId);

            return new CompensationDocumentDO(compensationDocument);
        }

        [HttpPut]
        [Route("{compensationDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.Edit.CompensationDocData), IdParam = "compensationDocumentId")]
        public void UpdateCompensationDocumentData(int compensationDocumentId, CompensationDocumentDO compensationDocDO)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, compensationDocDO.Version);

            compensationDocument.UpdateData(
                compensationDocDO.CompensationSign.Value,
                compensationDocDO.CompensationDocDate.Value,
                compensationDocDO.Description,
                compensationDocDO.CompensationReason,
                compensationDocDO.BfpEuAmount,
                compensationDocDO.BfpBgAmount,
                compensationDocDO.BfpCrossAmount,
                compensationDocDO.SelfAmount);
            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{compensationDocumentId:int}/enter")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.ChangeStatusToEntered), IdParam = "compensationDocumentId")]
        public void EnterCompensationDocument(int compensationDocumentId, string version)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, vers);

            if (compensationDocument.IsActivated)
            {
                compensationDocument.ChangeStatusToEntered();
            }
            else
            {
                this.countersRepository.CreateCompensationDocumentCounter(compensationDocument.ContractId);
                var regNum = this.countersRepository.GetNextCompensationDocumentNumber(compensationDocument.ContractId);

                compensationDocument.ChangeStatusToEntered(regNum);
            }

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{compensationDocumentId:int}/setToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.ChangeStatusToDraft), IdParam = "compensationDocumentId")]
        public void MakeDraft(int compensationDocumentId, string version)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, vers);

            compensationDocument.ChangeStatusToDraft();

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{compensationDocumentId:int}/setToRemoved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.ChangeStatusToRemoved), IdParam = "compensationDocumentId")]
        public void MakeRemoved(int compensationDocumentId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, vers);

            compensationDocument.ChangeStatusToDeleted(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{compensationDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CompensationDocuments.Delete), IdParam = "compensationDocumentId")]
        public void Delete(int compensationDocumentId, string version)
        {
            this.authorizer.AssertCanDo(CompensationDocumentActions.Edit, compensationDocumentId);

            byte[] vers = System.Convert.FromBase64String(version);
            var compensationDocument = this.compensationDocumentsRepository.FindForUpdate(compensationDocumentId, vers);

            this.compensationDocumentsRepository.Remove(compensationDocument);

            this.unitOfWork.Save();
        }
    }
}
