using Eumis.ApplicationServices.Services.CertReport;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.CertReports.ViewObjects;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.CertReports;
using Eumis.Domain.CertReports.DataObjects;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Domain.Users.ProgrammePermissions;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using Eumis.Web.Api.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Eumis.Web.Api.CertReports.Controllers
{
    [RoutePrefix("api/certReports")]
    public class CertReportsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IAuthorizer authorizer;
        private IAccessContext accessContext;
        private IPermissionsRepository permissionsRepository;
        private ICertReportService certReportService;
        private ICertReportsRepository certReportsRepository;
        private IUsersRepository usersRepository;

        public CertReportsController(
            IUnitOfWork unitOfWork,
            IAuthorizer authorizer,
            IAccessContext accessContext,
            IPermissionsRepository permissionsRepository,
            ICertReportService certReportService,
            ICertReportsRepository certReportsRepository,
            IUsersRepository usersRepository)
        {
            this.unitOfWork = unitOfWork;
            this.authorizer = authorizer;
            this.accessContext = accessContext;
            this.permissionsRepository = permissionsRepository;
            this.certReportService = certReportService;
            this.certReportsRepository = certReportsRepository;
            this.usersRepository = usersRepository;
        }

        [Route("")]
        public IList<CertReportVO> GetCertReports()
        {
            this.authorizer.AssertCanDo(CertReportListActions.Search);

            var programmeIds = this.permissionsRepository.GetProgrammeIdsByPermission(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanRead);

            return this.certReportsRepository.GetCertReports(programmeIds);
        }

        [Route("checks")]
        public IList<CertReportVO> GetCertReportChecksCertReports()
        {
            this.authorizer.AssertCanDo(CertReportCheckListActions.Search);

            var programmeIds = System.Array.Empty<int>();

            return this.certReportsRepository.GetCertReportChecksCertReports(programmeIds);
        }

        [Route("~/api/financialCorrections/{financialCorrectionId:int}/certReports")]
        public IList<CertReportVO> GetFinancialCorrectionCertReports(int financialCorrectionId)
        {
            this.authorizer.AssertCanDo(FinancialCorrectionActions.View, financialCorrectionId);

            return this.certReportsRepository.GetFinancialCorrectionCertReports(financialCorrectionId);
        }

        [Route("{certReportId:int}")]
        public CertReportDO GetCertReport(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            var certReport = this.certReportsRepository.Find(certReportId);

            return new CertReportDO(certReport);
        }

        [Route("{certReportId:int}/info")]
        public CertReportInfoVO GetCertReportInfo(int certReportId)
        {
            this.authorizer.AssertCanDoAny(
                Tuple.Create<Enum, int?>(CertReportActions.View, certReportId),
                Tuple.Create<Enum, int?>(CertReportCheckActions.View, certReportId));

            return this.certReportsRepository.GetInfo(certReportId);
        }

        [HttpGet]
        [Route("new")]
        public CertReportDO NewCertReport()
        {
            this.authorizer.AssertCanDo(CertReportListActions.Create);

            return new CertReportDO()
            {
                Status = CertReportStatus.Draft,
                RegDate = DateTime.Now.Date,
            };
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Create))]
        public object CreateCertReport(CertReportDO certReport)
        {
            this.authorizer.AssertCanDo(CertReportListActions.Create);

            var newCertReport = this.certReportService.CreateCertReport(
                certReport.ProgrammeId.Value,
                certReport.RegDate.Value,
                certReport.DateFrom.Value,
                certReport.DateTo.Value,
                certReport.Type.Value,
                certReport.CertReportNumber);

            return new { CertReportId = newCertReport.CertReportId };
        }

        [HttpDelete]
        [Route("{certReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Delete), IdParam = "certReportId")]
        public void DeleteCertReport(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportListActions.Create);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.DeleteCertReport(certReportId, vers);
        }

        [HttpPost]
        [Route("{certReportId:int}/canDelete")]
        public ErrorsDO CreateCertReport(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportListActions.Create);

            var errorList = this.certReportService.CanDeleteCertReport(certReportId);

            return new ErrorsDO(errorList);
        }

        [HttpPut]
        [Route("{certReportId:int}")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.Edit.BasicData), IdParam = "certReportId")]
        public void UpdateCertReport(int certReportId, CertReportDO certReport)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            this.certReportService.UpdateCertReport(
                certReportId,
                certReport.Version,
                certReport.RegDate.Value,
                certReport.DateFrom.Value,
                certReport.DateTo.Value,
                certReport.CertReportNumber);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToEnded")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToEnded), IdParam = "certReportId")]
        public void ChangeCertReportStatusToEnded(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.Ended);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToUnchecked")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToUnchecked), IdParam = "certReportId")]
        public void ChangeCertReportStatusToUnchecked(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.Unchecked);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToDraft")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToDraft), IdParam = "certReportId")]
        public void ChangeCertReportStatusToDraft(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.Draft);
        }

        [HttpPost]
        [Route("{certReportId:int}/canChangeStatusToFinalStatuses")]
        public ErrorsDO CanChangeStatusToFinalStatuses(int certReportId)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            var errorList = this.certReportService.CanChangeCertReportStatusToFinalStatus(certReportId);

            return new ErrorsDO(errorList);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToApproved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToApproved), IdParam = "certReportId")]
        public void ChangeCertReportStatusToApproved(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.Approved);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToPartialyApproved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToPartialyApproved), IdParam = "certReportId")]
        public void ChangeCertReportStatusToPartialyApproved(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.PartialyApproved);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToUnapproved")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToUnapproved), IdParam = "certReportId")]
        public void ChangeCertReportStatusToUnapproved(int certReportId, string version)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.Unapproved);
        }

        [HttpPost]
        [Route("{certReportId:int}/changeStatusToReturned")]
        [Transaction]
        [ActionLog(Action = typeof(ActionLogGroups.CertReports.ChangeStatusToReturned), IdParam = "certReportId")]
        public object ChangeCertReportStatusToReturned(int certReportId, string version, ConfirmDO confirm)
        {
            this.authorizer.AssertCanDo(CertReportCheckActions.Edit, certReportId);

            byte[] vers = System.Convert.FromBase64String(version);

            var newCertReportId = this.certReportService.ChangeCertReportStatus(certReportId, vers, CertReportStatus.Returned, confirm.Note);

            return new { NewCertReportId = newCertReportId.Value };
        }
    }
}
