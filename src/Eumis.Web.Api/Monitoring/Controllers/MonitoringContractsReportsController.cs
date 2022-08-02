using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Eumis.Authentication.Api;
using Eumis.Authentication.Authorization;
using Eumis.Common.Api;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Common.Excel;
using Eumis.Common.Json;
using Eumis.Data.Monitoring.Repositories;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Web.Api.Monitoring.DataObjects;

namespace Eumis.Web.Api.Monitoring.Controllers
{
    [RoutePrefix("api/monitoringReports/contracts")]
    public class MonitoringContractsReportsController : ApiController
    {
        private IAccessContext accessContext;
        private IUnitOfWork unitOfWork;
        private IMonitoringReportsRepository monitoringReportsRepository;
        private IAuthorizer authorizer;

        public MonitoringContractsReportsController(
            IAccessContext accessContext,
            IUnitOfWork unitOfWork,
            IMonitoringReportsRepository monitoringReportsRepository,
            IAuthorizer authorizer)
        {
            this.accessContext = accessContext;
            this.unitOfWork = unitOfWork;
            this.monitoringReportsRepository = monitoringReportsRepository;
            this.authorizer = authorizer;
        }

        [Route("")]
        public ContractsReportDO GetContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);
            var reportItems = this.monitoringReportsRepository.GetContractsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);
            var result = new ContractsReportDO();
            const int limit = 100;
            if (reportItems.Count > limit)
            {
                result.Items = reportItems.Take(limit).ToList();
                result.ResultIsClipped = true;
            }
            else
            {
                result.Items = reportItems;
            }

            return result;
        }

        [Route("export")]
        public HttpResponseMessage GetContractsExcelReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null)
        {
            this.authorizer.AssertCanDo(MonitoringActions.View);
            var report = this.monitoringReportsRepository.GetContractsReport(
                programmeId,
                programmePriorityId,
                procedureId,
                fromDate,
                toDate,
                currency,
                countryId,
                nuts1Id,
                nuts2Id,
                districtId,
                municipalityId,
                settlementId,
                protectedZoneId);
            var rows = new List<ContractReportRow>();
            foreach (var data in report)
            {
                var row = new ContractReportRow
                {
                    Programme = data.Programme,
                    Procedure = data.Procedure,
                    RegNumber = data.RegNumber,
                    Name = data.Name,
                    CompanyUin = data.CompanyUin,
                    CompanyName = data.CompanyName,
                    CompanyType = data.CompanyType,
                    CompanyLegalType = data.CompanyLegalType,
                    CompanyKidCode = data.CompanyKidCode,
                    CompanyAddress = data.CompanyAddress,
                    CompanyCorrespondenceAddress = data.CompanyCorrespondenceAddress,
                    CompanyEmail = data.CompanyEmail,
                    CompanySizeType = data.CompanySizeType,
                    ProjectDuration = data.ProjectDuration.GetValueOrDefault(),
                    ProjectKidCode = data.ProjectKidCode,
                    InitialContractDate = data.InitialContractDate?.ToString("dd.MM.yyyy"),
                    ActualContractDate = data.ActualContractDate?.ToString("dd.MM.yyyy"),
                    InitialStartDate = data.InitialStartDate?.ToString("dd.MM.yyyy"),
                    InitialCompletionDate = data.InitialCompletionDate?.ToString("dd.MM.yyyy"),
                    ActualStartDate = data.ActualStartDate?.ToString("dd.MM.yyyy"),
                    ActualCompletionDate = data.ActualCompletionDate?.ToString("dd.MM.yyyy"),
                    ContractTerminationDate = data.ContractTerminationDate?.ToString("dd.MM.yyyy"),
                    ContractExecutionStatus = data.ContractExecutionStatus?.GetEnumDescription(),
                    PaidAdvanceTotalAmount = data.PaidAdvanceTotalAmount.GetValueOrDefault(),
                    PaidAdvanceEuAmount = data.PaidAdvanceEuAmount.GetValueOrDefault(),
                    PaidAdvanceBgAmount = data.PaidAdvanceBgAmount.GetValueOrDefault(),
                    PaidIntermediateTotalAmount = data.PaidIntermediateTotalAmount.GetValueOrDefault(),
                    PaidIntermediateEuAmount = data.PaidIntermediateEuAmount.GetValueOrDefault(),
                    PaidIntermediateBgAmount = data.PaidIntermediateBgAmount.GetValueOrDefault(),
                    PaidFinalTotalAmount = data.PaidFinalTotalAmount.GetValueOrDefault(),
                    PaidFinalEuAmount = data.PaidFinalEuAmount.GetValueOrDefault(),
                    PaidFinalBgAmount = data.PaidFinalBgAmount.GetValueOrDefault(),
                    ReimbursedPrincipalTotalAmount = data.ReimbursedPrincipalTotalAmount.GetValueOrDefault(),
                    ReimbursedPrincipalEuAmount = data.ReimbursedPrincipalEuAmount.GetValueOrDefault(),
                    ReimbursedPrincipalBgAmount = data.ReimbursedPrincipalBgAmount.GetValueOrDefault(),
                    ReimbursedInterestTotalAmount = data.ReimbursedInterestTotalAmount.GetValueOrDefault(),
                    ReimbursedInterestEuAmount = data.ReimbursedInterestEuAmount.GetValueOrDefault(),
                    ReimbursedInterestBgAmount = data.ReimbursedInterestBgAmount.GetValueOrDefault(),
                };
                if (data.ContractAmounts.Count != 0)
                {
                    var contractAmount = data.ContractAmounts.ElementAt(0);
                    this.PopulateRowWithContractAmounts(row, contractAmount);
                }

                if (data.ContractAmounts.Count > 1)
                {
                    var remainingContractAmounts = data.ContractAmounts.Skip(1);
                    foreach (var remainingContractAmount in remainingContractAmounts)
                    {
                        rows.Add(row);
                        row = new ContractReportRow();
                        this.PopulateRowWithContractAmounts(row, remainingContractAmount);
                    }
                }

                rows.Add(row);
            }

            MemoryStream excelStream = new MemoryStream(); // The memory stream must not be disposed
            Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Eumis.Web.Api.Monitoring.Controllers.ExcelTemplates.contracts.xlsx")
                .CopyTo(excelStream);
            ExcelHelper.TransformTemplate(excelStream, 3, rows);
            return this.Request.CreateXmlResponse(excelStream, "contracts");
        }

        private void PopulateRowWithContractAmounts(ContractReportRow row, ContractsReportContractAmountsItem contractAmount)
        {
            row.ContractBudgetLevel3AmountNutsFullPathName = contractAmount.ContractBudgetLevel3AmountNutsFullPathName;
            row.InitialContractedTotalAmount = contractAmount.InitialContractedTotalAmount.GetValueOrDefault();
            row.InitialContractedBfpTotalAmount = contractAmount.InitialContractedBfpTotalAmount.GetValueOrDefault();
            row.InitialContractedEuAmount = contractAmount.InitialContractedEuAmount.GetValueOrDefault();
            row.InitialContractedBgAmount = contractAmount.InitialContractedBgAmount.GetValueOrDefault();
            row.InitialContractedSelfAmount = contractAmount.InitialContractedSelfAmount.GetValueOrDefault();
            row.ActualContractedTotalAmount = contractAmount.ActualContractedTotalAmount.GetValueOrDefault();
            row.ActualContractedBfpTotalAmount = contractAmount.ActualContractedBfpTotalAmount.GetValueOrDefault();
            row.ActualContractedEuAmount = contractAmount.ActualContractedEuAmount.GetValueOrDefault();
            row.ActualContractedBgAmount = contractAmount.ActualContractedBgAmount.GetValueOrDefault();
            row.ActualContractedSelfAmount = contractAmount.ActualContractedSelfAmount.GetValueOrDefault();
            row.ReportedTotalAmount = contractAmount.ReportedTotalAmount.GetValueOrDefault();
            row.ReportedBfpTotalAmount = contractAmount.ReportedBfpTotalAmount.GetValueOrDefault();
            row.ReportedEuAmount = contractAmount.ReportedEuAmount.GetValueOrDefault();
            row.ReportedBgAmount = contractAmount.ReportedBgAmount.GetValueOrDefault();
            row.ReportedSelfAmount = contractAmount.ReportedSelfAmount.GetValueOrDefault();
            row.ApprovedTotalAmount = contractAmount.ApprovedTotalAmount.GetValueOrDefault();
            row.ApprovedBfpTotalAmount = contractAmount.ApprovedBfpTotalAmount.GetValueOrDefault();
            row.ApprovedEuAmount = contractAmount.ApprovedEuAmount.GetValueOrDefault();
            row.ApprovedBgAmount = contractAmount.ApprovedBgAmount.GetValueOrDefault();
            row.ApprovedSelfAmount = contractAmount.ApprovedSelfAmount.GetValueOrDefault();
            row.UnapprovedTotalAmount = contractAmount.UnapprovedTotalAmount.GetValueOrDefault();
            row.UnapprovedBfpTotalAmount = contractAmount.UnapprovedBfpTotalAmount.GetValueOrDefault();
            row.UnapprovedEuAmount = contractAmount.UnapprovedEuAmount.GetValueOrDefault();
            row.UnapprovedBgAmount = contractAmount.UnapprovedBgAmount.GetValueOrDefault();
            row.UnapprovedSelfAmount = contractAmount.UnapprovedSelfAmount.GetValueOrDefault();
            row.CorrectedTotalAmount = contractAmount.CorrectedTotalAmount.GetValueOrDefault();
            row.CorrectedBfpTotalAmount = contractAmount.CorrectedBfpTotalAmount.GetValueOrDefault();
            row.CorrectedEuAmount = contractAmount.CorrectedEuAmount.GetValueOrDefault();
            row.CorrectedBgAmount = contractAmount.CorrectedBgAmount.GetValueOrDefault();
            row.CorrectedSelfAmount = contractAmount.CorrectedSelfAmount.GetValueOrDefault();
            row.CertifiedTotalAmount = contractAmount.CertifiedTotalAmount.GetValueOrDefault();
            row.CertifiedBfpTotalAmount = contractAmount.CertifiedBfpTotalAmount.GetValueOrDefault();
            row.CertifiedEuAmount = contractAmount.CertifiedEuAmount.GetValueOrDefault();
            row.CertifiedBgAmount = contractAmount.CertifiedBgAmount.GetValueOrDefault();
            row.CertifiedSelfAmount = contractAmount.CertifiedSelfAmount.GetValueOrDefault();
        }
    }
}