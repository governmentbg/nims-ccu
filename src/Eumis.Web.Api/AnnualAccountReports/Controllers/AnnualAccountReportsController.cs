using Eumis.ApplicationServices.Communicators;
using Eumis.ApplicationServices.Services.AnnualAccountReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.AnnualAccountReports.Repositories;
using Eumis.Data.AnnualAccountReports.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.AnnualAccountReports;
using Eumis.Domain.AnnualAccountReports.DataObjects;
using Eumis.Domain.AnnualAccountReports.ViewObjects;
using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.AnnualAccountReports.Controllers
{
    [RoutePrefix("api/annualAccountReports")]
    public class AnnualAccountReportsController : ApiController
    {
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private IAnnualAccountReportsRepository annualAccountReportsRepository;
        private IAnnualAccountReportService annualAccountReportService;

        public AnnualAccountReportsController(
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            IAnnualAccountReportsRepository annualAccountReportsRepository,
            IAnnualAccountReportService annualAccountReportService)
        {
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.annualAccountReportsRepository = annualAccountReportsRepository;
            this.annualAccountReportService = annualAccountReportService;
        }

        [Route("")]
        public IList<AnnualAccountReportVO> GetAnnualAccountReports()
        {
            this.authorizer.AssertCanDo(AnnualAccountReportListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.annualAccountReportsRepository.GetAnnualAccountReports(programmeIds);
        }

        [Route("{annualAccountReportId:int}")]
        public AnnualAccountReportDO GetAnnualAccountReport(int annualAccountReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(AnnualAccountReportActions.View, annualAccountReportId),
                Tuple.Create<Enum, int?>(AnnualAccountReportActions.ViewAuditDcument, annualAccountReportId));

            var annualAccountReport = this.annualAccountReportsRepository.Find(annualAccountReportId);

            return new AnnualAccountReportDO(annualAccountReport);
        }

        [Route("{annualAccountReportId:int}/info")]
        public AnnualAccountReportInfoVO GetAnnualAccountReportInfo(int annualAccountReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(AnnualAccountReportActions.View, annualAccountReportId),
                Tuple.Create<Enum, int?>(AnnualAccountReportActions.ViewAuditDcument, annualAccountReportId));

            return this.annualAccountReportsRepository.GetInfo(annualAccountReportId);
        }

        [HttpGet]
        [Route("new")]
        public AnnualAccountReportDO NewAnnualAccountReport()
        {
            this.authorizer.AssertCanDo(AnnualAccountReportListActions.Create);

            return new AnnualAccountReportDO()
            {
                Status = AnnualAccountReportStatus.Draft,
                RegDate = DateTime.Now,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Create))]
        public object CreateAnnualAccountReport(AnnualAccountReportDO accountReport)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportListActions.Create);

            var newAnnualAccountReport = this.annualAccountReportService.CreateAnnualAccountReport(
                accountReport.ProgrammeId.Value,
                accountReport.RegDate.Value,
                accountReport.AccountPeriod);

            return new { newAnnualAccountReport.AnnualAccountReportId };
        }

        [HttpDelete]
        [Route("{annualAccountReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Delete), IdParam = "annualAccountReportId")]
        public void DeleteAnnualAccountReport(int annualAccountReportId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.DeleteAnnualAccountReport(annualAccountReportId, vers);
        }

        [HttpPost]
        [Route("{annualAccountReportId:int}/canDelete")]
        public ErrorsDO CanDeleteAnnualAccountReport(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportListActions.Create);

            var errorList = this.annualAccountReportService.CanDeleteAnnualAccountReport(annualAccountReportId);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{annualAccountReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.Edit.BasicData), IdParam = "annualAccountReportId")]
        public void UpdateAnnualAccountReport(int annualAccountReportId, AnnualAccountReportDO annualAccountReport)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            this.annualAccountReportService.UpdateAnnualAccountReport(
                annualAccountReportId,
                annualAccountReport.Version,
                annualAccountReport.RegDate,
                annualAccountReport.ApprovalDate);
        }

        [HttpPost]
        [Route("{annualAccountReportId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.ChangeStatusToEnded), IdParam = "annualAccountReportId")]
        public void ChangeAnnualAccountReportStatusToEnded(int annualAccountReportId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.ChangeAnnualAccountReportStatus(annualAccountReportId, vers, AnnualAccountReportStatus.Ended);
        }

        [HttpPost]
        [Route("{annualAccountReportId:int}/canChangeStatusToOpened")]
        public ErrorsDO CanChangeStatusToOpened(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            var errorList = this.annualAccountReportService.CanChangeStatus(annualAccountReportId, AnnualAccountReportStatus.Opened);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{annualAccountReportId:int}/changeStatusToOpened")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.ChangeStatusToOpened), IdParam = "annualAccountReportId")]
        public void ChangeAnnualAccountReportStatusToOpened(int annualAccountReportId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.ChangeAnnualAccountReportStatus(annualAccountReportId, vers, AnnualAccountReportStatus.Opened);
        }

        [HttpPost]
        [Route("{annualAccountReportId:int}/canChangeStatusToDraft")]
        public ErrorsDO CanChangeStatusToDraft(int annualAccountReportId)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            var errorList = this.annualAccountReportService.CanChangeStatus(annualAccountReportId, AnnualAccountReportStatus.Draft);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{annualAccountReportId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.AnnualAccountReports.ChangeStatusToDraft), IdParam = "annualAccountReportId")]
        public void ChangeStatusToDraft(int annualAccountReportId, string version)
        {
            this.authorizer.AssertCanDo(AnnualAccountReportActions.Edit, annualAccountReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.annualAccountReportService.ChangeAnnualAccountReportStatus(annualAccountReportId, vers, AnnualAccountReportStatus.Draft);
        }
    }
}
