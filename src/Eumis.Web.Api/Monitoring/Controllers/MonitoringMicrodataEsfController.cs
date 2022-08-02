using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Db;
using Eumis.Common.Excel;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/microdataEsf")]
    public class MonitoringMicrodataEsfController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringMicrodataEsfController(
            IUnitOfWork unitOfWork,
            IMonitoringReportsRepository monitoringReportsRepository,
            IAuthorizer authorizer)
        {
            this.unitOfWork = unitOfWork;
            this.monitoringReportsRepository = monitoringReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("export")]
        public HttpResponseMessage GetContractReportPaymentsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);

            MemoryStream excelStream = new MemoryStream(); // The memory stream must not be disposed

            Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("Eumis.Web.Api.Monitoring.Controllers.ExcelTemplates.microdataEsf.xlsx")
                .CopyTo(excelStream);

            var microdataEsfItems = this.monitoringReportsRepository.GetMicrodataEsfReport(
                programmeId,
                programmePriorityId,
                procedureId,
                toDate);

            var rows = microdataEsfItems.Select(item =>
                new string[]
                {
                    item.Number,
                    item.FirstName,
                    item.MiddleName,
                    item.LastName,
                    item.Uin,
                    item.Gender.GetEnumDescription(),
                    item.Age.ToString(),
                    item.Occupation.GetEnumDescription(),
                    item.Education.GetEnumDescription(),
                    item.AddressDistrictName,
                    item.AddressSettlementName,
                    item.Phone,
                    item.Email,
                    this.GetBooleanDescription(item.IsEmigrant),
                    this.GetBooleanDescription(item.IsForeigner),
                    this.GetBooleanDescription(item.IsMinority),
                    this.GetBooleanDescription(item.IsGypsy),
                    this.GetBooleanDescription(item.IsDisabledPerson),
                    this.GetBooleanDescription(item.IsHomeless),
                    item.DisadvantagedPerson,
                    this.GetBooleanDescription(item.IsLivingInUnemployedHousehold),
                    this.GetBooleanDescription(item.IsLivingInUnemployedHouseholdWithChildren),
                    this.GetBooleanDescription(item.IsLivingInFamilyOfOneWithChildren),
                    item.JoiningDate.HasValue ? item.JoiningDate.Value.Year.ToString() : string.Empty,
                    item.JoiningDate.HasValue ? item.JoiningDate.Value.Month.ToString() : string.Empty,
                    item.JoiningDate.HasValue ? item.JoiningDate.Value.Day.ToString() : string.Empty,
                    item.Activity,
                    item.ActivityPlaceDistrictName,
                    item.ActivityPlaceSettlementName,
                    item.ParticipationState.GetEnumDescription(),
                    item.LeavingDate.HasValue ? item.LeavingDate.Value.Year.ToString() : string.Empty,
                    item.LeavingDate.HasValue ? item.LeavingDate.Value.Month.ToString() : string.Empty,
                    item.LeavingDate.HasValue ? item.LeavingDate.Value.Day.ToString() : string.Empty,
                    item.CancelationReason.GetEnumDescription(),
                    item.LeavingState.GetEnumDescription(),
                    item.ContractRegNumber,
                });

            ExcelHelper.TransformTemplate(excelStream, 3, rows);

            return this.Request.CreateXmlResponse(excelStream, "microdataEsf");
        }

        private string GetBooleanDescription(bool? b)
        {
            return b.HasValue ?
                (b.Value ? "да" : "не") :
                string.Empty;
        }
    }
}
