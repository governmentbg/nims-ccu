using System;
using System.Collections.Generic;
using Eumis.Data.Core;
using Eumis.Data.Monitoring.ViewObjects;
using Eumis.Domain.Arachne;
using Eumis.Domain.Contracts;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.PhysicalExecution;

namespace Eumis.Data.Monitoring.Repositories
{
    public interface IMonitoringReportsRepository : IRepository
    {
        Anex3ReportVO GetAnex3Report(int contractId);

        IList<AdvancePaymentsReportItem> GetAdvancePaymentsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null);

        IList<DoubleFundingReportItem> GetDoubleFundingReport(string uin);

        IList<FinancialExecutionTable1ReportItem> GetFinancialExecutionTable1Report(int programmeId, DateTime date, int? programmePriorityId = null);

        IList<FinancialExecutionTable2ReportItem> GetFinancialExecutionTable2Report(int programmeId, DateTime date, int? programmePriorityId = null);

        IList<FinancialExecutionTable3ReportItem> GetFinancialExecutionTable3Report(int programmeId, Year year);

        IList<ProjectsReportItem> GetProjectsReport(
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
            int? protectedZoneId = null);

        IList<ContractsReportReportItem> GetContractReportsReport(
            int? programmeId,
            int? programmePriorityId,
            int? procedureId,
            int? contractId,
            DateTime? toDate,
            ContractReportType? reportType,
            ContractReportStatus? reportStatus);

        IList<IndicatorReportItemVO> GetIndicatorsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null,
            ContractExecutionStatus? contractExecutionStatus = null,
            int? countryId = null,
            int? nuts1Id = null,
            int? nuts2Id = null,
            int? districtId = null,
            int? municipalityId = null,
            int? settlementId = null,
            int? protectedZoneId = null);

        IList<BudgetLevelsReportItem> GetBudgetLevelsReport(
            BudgetLevel budgetLevel,
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
            int? protectedZoneId = null);

        IList<FinancialCorrectionsReportItem> GetFinancialCorrectionsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null);

        IList<ConcludedContractsReportItem> GetConcludedContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            string uin = null);

        IList<BeneficiaryProjectsContractsReportItem> GetBeneficiaryProjectsContractsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null);

        IList<EvaluationsReportItem> GetEvaluationsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null);

        IList<ContractReportPaymentsReportItem> GetContractReportPaymentsReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            Currency? currency = null);

        IList<IrregularitiesReportItem> GetIrregularitiesReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null);

        IList<PinReportItem> GetPinReport(
            int? programmeId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string uin = null);

        IList<MicrodataEsfReportItem> GetMicrodataEsfReport(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? toDate = null);

        IList<ContractsReportItem> GetContractsReport(
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
            int? protectedZoneId = null);

        IList<ProgrammeSummaryReportItem> GetProgrammeSummaryReport(
            GroupingLevel groupingLevel,
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
            int? protectedZoneId = null);

        IList<ExpenseTypesReportItem> GetExpenseTypesReport(int? programmeId = null, DateTime? toDate = null);
    }
}
