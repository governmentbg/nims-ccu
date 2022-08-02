using Eumis.ApplicationServices.Services.EvalSessionReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Data.EvalSessions.Repositories;
using Eumis.Data.EvalSessions.ViewObjects;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using Eumis.Web.Api.EvalSessions.DataObjects;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.EvalSessions.Controllers
{
    [RoutePrefix("api/evalSessions/{evalSessionId}/reports")]
    public class EvalSessionReportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEvalSessionReportsRepository evalSessionReportsRepository;
        private IEvalSessionReportService evalSessionReportService;
        private IAuthorizer authorizer;

        public EvalSessionReportsController(
            IUnitOfWork unitOfWork,
            IEvalSessionReportsRepository evalSessionReportsRepository,
            IEvalSessionReportService evalSessionReportService,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.evalSessionReportsRepository = evalSessionReportsRepository;
            this.evalSessionReportService = evalSessionReportService;
            this.authorizer = authorizer;
        }

        [Route("")]
        public IList<EvalSessionReportVO> GetEvalSessionReports(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            return this.evalSessionReportsRepository.GetEvalSessionReports(evalSessionId);
        }

        [Route("{evalSessionReportId:int}")]
        public EvalSessionReportDO GetEvalSessionReport(int evalSessionId, int evalSessionReportId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.ViewSessionData, evalSessionId);

            var report = this.evalSessionReportsRepository.Find(evalSessionReportId);

            return new EvalSessionReportDO(report);
        }

        [HttpGet]
        [Route("new")]
        public EvalSessionReportDO NewEvalSessionReport(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            return new EvalSessionReportDO();
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Reports.Create), IdParam = "evalSessionId")]
        public object AddEvalSessionReport(int evalSessionId, EvalSessionReportDO evalSessionReport)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            if (this.evalSessionReportService.CanCreate(evalSessionId).Count != 0)
            {
                throw new InvalidOperationException("Cannot create new report.");
            }

            var report = this.evalSessionReportService.CreateReport(evalSessionId, evalSessionReport.Type.Value, evalSessionReport.Description);
            this.evalSessionReportsRepository.Add(report);

            this.unitOfWork.Save();

            return new { ReportId = report.EvalSessionReportId };
        }

        [HttpPost]
        [Route("{evalSessionReportId:int}/cancel")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.EvalSessions.Edit.Reports.Delete), IdParam = "evalSessionId", ChildIdParam = "evalSessionReportId")]
        public void CancelEvalSessionReport(int evalSessionId, int evalSessionReportId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            if (this.evalSessionReportService.CanDelete(evalSessionReportId).Count > 0)
            {
                throw new InvalidOperationException("Cannot delete report");
            }

            byte[] vers = System.Convert.FromBase64String(version);
            var report = this.evalSessionReportsRepository.FindForUpdate(evalSessionReportId, vers);
            report.Remove(confirm.Note);

            this.unitOfWork.Save();
        }

        [HttpPost]
        [Route("canCreate")]
        public ErrorsDO CanCreateEvalSessionReport(int evalSessionId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionReportService.CanCreate(evalSessionId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{evalSessionReportId:int}/canCancel")]
        public ErrorsDO CanCancelEvalSessionReport(int evalSessionId, int evalSessionReportId)
        {
            this.authorizer.AssertCanDo(EvalSessionActions.EditSessionData, evalSessionId);

            var errorList = this.evalSessionReportService.CanDelete(evalSessionReportId);

            return new ErrorsDO(errorList);
        }
    }
}
