using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.Monitorstat.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Procedures.ViewObjects;
using Eumis.Domain.Monitorstat;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Eumis.Web.Api.Procedures.Controllers
{
    [RoutePrefix("api/procedures/{procedureId}/monitorstatDocuments")]
    public class ProcedureMonitorstatDocumentsController : ApiController
    {
        private IAuthorizer authorizer;
        private IMonitorstatSurveysRepository monitorstatRepository;
        private IProceduresRepository proceduresRepository;
        private IProcedureMonitorstatDocumentsRepository procedureMonitorstatDocumentsRepository;
        private IUnitOfWork unitOfWork;
        private IMonitorstatService monitorstatService;

        public ProcedureMonitorstatDocumentsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IMonitorstatSurveysRepository monitorstatRepository,
            IProceduresRepository proceduresRepository,
            IProcedureMonitorstatDocumentsRepository procedureMonitorstatDocumentsRepository,
            IMonitorstatService monitorstatService)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.monitorstatRepository = monitorstatRepository;
            this.proceduresRepository = proceduresRepository;
            this.procedureMonitorstatDocumentsRepository = procedureMonitorstatDocumentsRepository;
            this.monitorstatService = monitorstatService;
        }

        [Route("")]
        public IList<ProcedureMonitorstatDocumentVO> GetProcedureMonitorstatDocuments(int procedureId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.procedureMonitorstatDocumentsRepository.GetProcedureDocuments(procedureId);
        }

        [Route("getReports")]
        public IList<ProcedureMonitorstatDocumentVO> GetReports(int procedureId, MonitorstatYear? year = null, int? surveyId = null)
        {
            this.authorizer.AssertCanDo(ProcedureActions.View, procedureId);

            return this.procedureMonitorstatDocumentsRepository.GetUnattachedReports(procedureId, year, surveyId);
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.Attach), IdParam = "procedureId")]
        public object AttachProcedureMonitorstatDocuments(int procedureId, string version, int[] reportIds)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = Convert.FromBase64String(version);

            // assert procedure version is actual
            var procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            this.procedureMonitorstatDocumentsRepository.CreateProcedureDcuments(procedure.ProcedureId, reportIds);

            try
            {
                this.unitOfWork.Save();
            }
            catch
            {
                return new
                {
                    error = "reportAlreadyIncludedInProcedure",
                };
            }

            return null;
        }

        [HttpDelete]
        [Route("{documentId}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.Procedures.Edit.Monitorstat.Delete), IdParam = "procedureId", ChildIdParam = "documentId")]
        public void DeleteProcedureMonitorstatDocuments(int procedureId, string version, int documentId)
        {
            this.authorizer.AssertCanDo(ProcedureActions.Edit, procedureId);

            byte[] vers = Convert.FromBase64String(version);

            // assert procedure version is actual
            var procedure = this.proceduresRepository.FindForUpdate(procedureId, vers);

            var document = this.procedureMonitorstatDocumentsRepository.Find(documentId);

            this.procedureMonitorstatDocumentsRepository.Remove(document);

            this.unitOfWork.Save();
        }
    }
}
