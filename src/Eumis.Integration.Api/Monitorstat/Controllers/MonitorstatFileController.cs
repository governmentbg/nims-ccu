using Eumis.ApplicationServices.Services.CertReport;
using Eumis.ApplicationServices.Services.CertReportCheck;
using Eumis.ApplicationServices.Services.Monitorstat;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.Projects.Repositories;
using Eumis.Data.Users.Repositories;
using Eumis.Domain.CertReports.ViewObjects;
using Eumis.Integration.Api.Monitorstat.Contracts;
using Eumis.Log.ActionLogger.Attributes;
using Eumis.Log.ActionLogger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Integration.Api.Monitorstat
{
    [RoutePrefix("api/monitorstatFiles")]
    public class MonitorstatFileController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IMonitorstatService monitorstatService;

        public MonitorstatFileController(
            IUnitOfWork unitOfWork,
            IMonitorstatService monitorstatService)
        {
            this.unitOfWork = unitOfWork;
            this.monitorstatService = monitorstatService;
        }

        [HttpPost]
        [Route("")]
        [Transaction]
        public object RegisterMonitorstatDocument([FromBody]MonitorstatFileResult result)
        {
            var errors = this.monitorstatService.ReceiveMonitorstatFile(
                result.ProcedureIdentifier,
                result.SubjectIdentifier,
                result.SubjectIdentifierType,
                result.FileName,
                result.FileKey,
                result.SubjectRequestGuid);

            if (!errors.Any())
            {
                this.unitOfWork.Save();
            }

            return new { errors };
        }
    }
}
