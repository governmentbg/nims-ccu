using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Procedures;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Procedures.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/appDocs")]
    public class ProcedureAppDocsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProceduresRepository proceduresRepository;
        private IProgrammesRepository programmesRepository;
        private IAuthorizer authorizer;

        public ProcedureAppDocsController(
            IUnitOfWork unitOfWork,
            IProceduresRepository proceduresRepository,
            IProgrammesRepository programmesRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.proceduresRepository = proceduresRepository;
            this.programmesRepository = programmesRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProcedureAppDocsVO> GetProcedureAppDocs(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.proceduresRepository.GetProcedureAppDocs(procedureId);
        }

        [Route("{appDocId:int}")]
        public ProcedureAppDocDO GetProcedureAppDoc(int procedureId, int appDocId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            var procedureAppDoc = procedure.FindProcedureApplicationDoc(appDocId);

            string documentExtension = null;
            if (procedureAppDoc.ProgrammeApplicationDocumentId != null)
            {
                documentExtension = this.programmesRepository.GetProgrammeApplicationDocumentExtension(procedureAppDoc.ProgrammeApplicationDocumentId.Value);
            }

            return new ProcedureAppDocDO(procedureAppDoc, procedure.Version, documentExtension);
        }

        [HttpGet]
        [Route("new")]
        public ProcedureAppDocDO NewProcedureAppDoc(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            var procedure = this.proceduresRepository.Find(procedureId);

            return new ProcedureAppDocDO(procedureId, procedure.Version);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppDocs.Create), IdParam = "procedureId")]
        public void AddProcedureAppDoc(int procedureId, ProcedureAppDocDO appDoc)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, appDoc.Version);

            procedure.AddProcedureApplicationDoc(
                appDoc.ProgrammeApplicationDocumentId,
                appDoc.Name,
                appDoc.IsRequired.Value,
                appDoc.IsSignatureRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{appDocId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppDocs.Edit), IdParam = "procedureId")]
        public void UpdateProcedureAppDoc(int procedureId, int appDocId, ProcedureAppDocDO appDoc)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, appDoc.Version);

            var procedureAppDoc = procedure.FindProcedureApplicationDoc(appDocId);

            string documentExtension = null;
            if (procedureAppDoc.ProgrammeApplicationDocumentId != null)
            {
                documentExtension = this.programmesRepository.GetProgrammeApplicationDocumentExtension(procedureAppDoc.ProgrammeApplicationDocumentId.Value);
            }

            procedure.UpdateProcedureApplicationDoc(
                appDocId,
                appDoc.ProgrammeApplicationDocumentId,
                appDoc.Name,
                documentExtension ?? appDoc.Extension,
                appDoc.IsRequired.Value,
                appDoc.IsSignatureRequired.Value);

            this.unitOfWork.Save();
        }

        [HttpDelete]
        [Route("{appDocId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppDocs.Delete), IdParam = "procedureId")]
        public void DeleteProcedureAppDoc(int procedureId, int appDocId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.RemoveProcedureApplicationDoc(appDocId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{appDocId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppDocs.Deactivate), IdParam = "procedureId", ChildIdParam = "appDocId")]
        public void DeactivateProcedureAppDoc(int procedureId, int appDocId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.DeactivateProcedureApplicationDoc(appDocId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{appDocId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.AppDocs.Activate), IdParam = "procedureId", ChildIdParam = "appDocId")]
        public void ActivateProcedureAppDoc(int procedureId, int appDocId, string version)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = System.Convert.FromBase64String(version);
            Procedure procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            procedure.ActivateProcedureApplicationDoc(appDocId);

            this.unitOfWork.Save();
        }
    }
}
