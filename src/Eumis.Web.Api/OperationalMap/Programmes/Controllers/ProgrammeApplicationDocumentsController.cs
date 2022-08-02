using Eumis.ApplicationServices.Services.ProgrammeApplicationDocuments;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Core.Relations;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Data.OperationalMap.Programmes.ViewObjects;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain;
using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.Programmes;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.OperationalMap.Programmes.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.OperationalMap.Programmes.Controllers
{
    [RoutePrefix("api/programmes/{programmeId}/applicationDocuments")]
    public class ProgrammeApplicationDocumentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IProgrammesRepository programmesRepository;
        private IProgrammeApplicationDocumentService programmeApplicationDocumentService;
        private IAuthorizer authorizer;

        public ProgrammeApplicationDocumentsController(
            IUnitOfWork unitOfWork,
            IProgrammesRepository programmesRepository,
            IProgrammeApplicationDocumentService programmeApplicationDocumentService,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.programmesRepository = programmesRepository;
            this.programmeApplicationDocumentService = programmeApplicationDocumentService;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<ProgrammeApplicationDocumentsVO> GetProgrammeApplicationDocuments(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            return this.programmesRepository.GetProgrammeApplicationDocuments(programmeId);
        }

        [Route("{programmeApplicationDocumentId:int}")]
        public ProgrammeApplicationDocumentDO GetProgrammeApplicationDocument(int programmeId, int programmeApplicationDocumentId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.View, programmeId);

            var programme = this.programmesRepository.Find(programmeId);

            var programmeApplicationDocument = programme.FindProgrammeApplicationDocument(programmeApplicationDocumentId);

            var isDocumentAttached = this.programmesRepository.IsProgrammeApplicationDocumentAttachedToProcedure(programmeApplicationDocumentId);

            return new ProgrammeApplicationDocumentDO(programmeApplicationDocument, isDocumentAttached, programme.Version);
        }

        [HttpGet]
        [Route("new")]
        public ProgrammeApplicationDocumentDO NewProgrammeApplicationDocument(int programmeId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var programme = this.programmesRepository.Find(programmeId);

            return new ProgrammeApplicationDocumentDO(programmeId, programme.Version);
        }

        [HttpPost]
        [Route("load")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ApplicationDocuments.Load), IdParam = "programmeId")]
        public void LoadProgrammeApplicationDocuments(int programmeId, FileDO file)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmeApplicationDocumentService.CanLoadProgrammeApplicationDocuments(programmeId, file.Key);

            if (errorList.Count > 0)
            {
                throw new DomainValidationException("Cannot create ProgrammeApplicationDocument.");
            }

            this.programmeApplicationDocumentService.LoadProgrammeApplicationDocuments(programmeId, file.Key);
        }

        [HttpPost]
        [Route("canLoad")]
        public ErrorsDO CanLoadProgrammeApplicationDocuments(int programmeId, FileDO file)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmeApplicationDocumentService.CanLoadProgrammeApplicationDocuments(programmeId, file.Key);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ApplicationDocuments.Create), IdParam = "programmeId")]
        public void AddProgrammeApplicationDocument(int programmeId, ProgrammeApplicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            Programme programme = this.programmesRepository.FindForUpdate(programmeId, document.Version);

            programme.AddProgrammeApplicationDocument(
                document.Name,
                document.Extension,
                document.IsSignatureRequired);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canAdd")]
        public ErrorsDO CanAddProgrammeApplicationDocument(int programmeId, [FromBody] ProgrammeApplicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            IList<string> errorList = this.programmeApplicationDocumentService.CanAddProgrammeApplicationDocuments(programmeId, document.Name);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{programmeApplicationDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ApplicationDocuments.Edit), IdParam = "programmeId", ChildIdParam = "programmeApplicationDocumentId")]
        public void UpdateProgrammeApplicationDocument(int programmeId, int programmeApplicationDocumentId, ProgrammeApplicationDocumentDO document)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            Programme programme = this.programmesRepository.FindForUpdate(programmeId, document.Version);

            programme.UpdateProgrammeApplicationDocument(
                programmeApplicationDocumentId,
                document.Extension,
                document.IsSignatureRequired);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{programmeApplicationDocumentId:int}/activate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ApplicationDocuments.Activate), IdParam = "programmeId", ChildIdParam = "programmeApplicationDocumentId")]
        public void ActivateProgrammeApplicationDocument(int programmeId, int programmeApplicationDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            Programme programme = this.programmesRepository.FindForUpdate(programmeId, vers);

            programme.ActivateProgrammeApplicationDocument(programmeApplicationDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPut]
        [Route("{programmeApplicationDocumentId:int}/deactivate")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ApplicationDocuments.Deactivate), IdParam = "programmeId", ChildIdParam = "programmeApplicationDocumentId")]
        public void DeactivateProgrammeApplicationDocument(int programmeId, int programmeApplicationDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            Programme programme = this.programmesRepository.FindForUpdate(programmeId, vers);

            programme.DeactivateProgrammeApplicationDocument(programmeApplicationDocumentId);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("{programmeApplicationDocumentId:int}/canDelete")]
        public ErrorsDO CanDeleteProgrammeApplicationDocument(int programmeId, int programmeApplicationDocumentId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            var errorList = this.programmeApplicationDocumentService.CanDeleteProgrammeApplicationDocument(programmeApplicationDocumentId);

            return new ErrorsDO(errorList);
        }

        [HttpDelete]
        [Route("{programmeApplicationDocumentId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Programme.Edit.ApplicationDocuments.Delete), IdParam = "programmeId", ChildIdParam = "programmeApplicationDocumentId")]
        public void DeleteProgrammeApplicationDocument(int programmeId, int programmeApplicationDocumentId, string version)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            byte[] vers = System.Convert.FromBase64String(version);
            Programme programme = this.programmesRepository.FindForUpdate(programmeId, vers);

            programme.RemoveProgrammeApplicationDocument(programmeApplicationDocumentId);

            this.unitOfWork.Save();
        }

        [Route("{programmeApplicationDocumentId:int}/relatedProcedures")]
        public IList<ProgrammeApplicationDocumentProcedureVO> GetProgrammeApplicationDocumentRelatedProcedures(int programmeId, int programmeApplicationDocumentId)
        {
            this.authorizer.AssertCanDo(ProgrammeActions.Edit, programmeId);

            return this.programmesRepository.GetApplicationDocumentRelatedProceduresData(programmeApplicationDocumentId);
        }
    }
}
