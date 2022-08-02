using System;
using System.Collections.Generic;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.Procurements.Repositories;
using Eumis.Data.Procurements.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procurements.DataOjects;

namespace Eumis.Web.Api.Procurements.Controllers
{
    [RoutePrefix("api/procurements/{procurementId}/documents")]
    public class ProcurementDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProcurementsRepository procurementsRepository;
        private IAuthorizer authorizer;
        private IRelationsRepository relationsRepository;

        public ProcurementDocumentsController(
            IUnitOfWork unitOfWork,
            IProcurementsRepository procurementsRepository,
            IAuthorizer authorizer,
            IRelationsRepository relationsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.procurementsRepository = procurementsRepository;
            this.authorizer = authorizer;
            this.relationsRepository = relationsRepository;
        }

        [Route("")]
        public IList<ProcurementDocumentVO> GetProcurementDocuments(int procurementId)
        {
            return this.procurementsRepository.GetProcurementDocuments(procurementId);
        }

        [Route("{documentId:int}")]
        public ProcurementDocumentDO GetProcurementDocument(int procurementId, int documentId)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var procurement = this.procurementsRepository.Find(procurementId);

            var procurementDocument = procurement.FindProcurementDocument(documentId);

            return new ProcurementDocumentDO(procurementDocument, procurement.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProcurementDocumentDO NewProcurementDocument(int procurementId)
        {
            var procurement = this.procurementsRepository.Find(procurementId);

            return new ProcurementDocumentDO(procurementId, procurement.Version);
        }

        [HttpPut]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.Documents.Edit), IdParam = "procurementId", ChildIdParam = "documentId")]
        public void UpdateProcurementDocument(int procurementId, int documentId, ProcurementDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var procurement = this.procurementsRepository.FindForUpdate(procurementId, document.Version);

            procurement.UpdateProcurementDocument(
                documentId,
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.Documents.Create), IdParam = "procurementId")]
        public object AddProcurementDocument(int procurementId, ProcurementDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            var procurement = this.procurementsRepository.FindForUpdate(procurementId, document.Version);

            var newProcurementDocument = procurement.CreateProcurementDocument(
                document.Name,
                document.Description,
                document.File != null ? document.File.Key : (Guid?)null);

            this.unitOfWork.Save();

            return new { ProcurementDocumentId = newProcurementDocument.ProcurementDocumentId };
        }

        [HttpDelete]
        [Route("{documentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procurements.Edit.Documents.Delete), IdParam = "procurementId", ChildIdParam = "documentId")]
        public void DeleteProcurementDocument(int procurementId, int documentId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);

            var procurement = this.procurementsRepository.FindForUpdate(procurementId, vers);

            procurement.RemoveProcurementDocument(documentId);

            this.unitOfWork.Save();
        }
    }
}
