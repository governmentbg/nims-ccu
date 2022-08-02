using System;
using System.Collections.Generic;
using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.Contracts;
using Eumis.Public.Domain.Entities.Umis.EvalSessions;
using Eumis.Public.Domain.Entities.Umis.IndicativeAnnualWorkingProgrammes;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Data.Repositories
{
    public interface IUmisRepository
    {
        int GetSavedTrees();

        IList<ProgrammeBudgetDetailedVO> GetProgrammeBudgetDetailed(DateTime? from = null, DateTime? to = null, bool includeSelfAmount = false);

        IList<PPFundsWithProcedureFundsVO> GetPPFundsWithProcedureFunds(int programmeId);

        ProgrammeBudgetBySourceVO GetProgrammeBudgetBySource(int programmeId);

        ProgrammeBudgetWithContractedAndPayedVO GetProgrammeBudgetWithContractedAndPayed(int programmeId);

        ContractedFundsByYearAndSourceWrapperVO GetContractedFundsByYearAndSource(int contractId, bool isHistoric);

        ContractedFundsByAidModeVO GetContractedFundsByAidMode(int contractId);

        ICollection<PaidAmountsByYearVO> GetPaidAmountsByYear(int contractId);

        ProjectPageVO<ContractVO> GetContracts(
            int? startDateYearFrom = null,
            int? startDateYearTo = null,
            int? completionDateYearFrom = null,
            int? completionDateYearTo = null,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            string companyUin = null,
            UinType? companyUinType = null,
            string contractorUin = null,
            UinType? contractorUinType = null,
            string subcontractorUin = null,
            UinType? subcontractorUinType = null,
            string memberUin = null,
            UinType? memberUinType = null,
            string partnerUin = null,
            UinType? partnerUinType = null,
            string searchUin = null,
            string searchName = null,
            NutsLevel? regionNutsLevel = null,
            int? regionId = null,
            int offset = 0,
            int? limit = null);

        PageVO<StatisticContractVO> GetStatisticContracts(
            DateTime? startDateFrom = null,
            DateTime? completionDateTo = null,
            int? programmeId = null,
            int offset = 0,
            int? limit = null);

        PageVO<StatisticIndicatorVO> GetStatisticIndicators(
            int? programmeId = null,
            int offset = 0,
            int? limit = null,
            bool isEn = false);

        ProjectProposalWrapperVO GetStatisticProjects(
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            int offset = 0,
            int? limit = null);

        ContractDetailsVO GetContract(int contractId, bool isHistoric);

        PageVO<ContractBeneficiaryVO> GetContractBeneficiaries(
            string name = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            string uin = null,
            int offset = 0,
            int? limit = null);

        PageVO<ContractBeneficiaryWithoutFinancialCorrectionsVO> GetContractBeneficiariesWithoutFinancialCorrections(
            string name = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            string uin = null,
            string seat = null,
            int offset = 0,
            int? limit = null);

        IEnumerable<ContractBeneficiaryVO> GetSimpleContractBeneficiaries(
            string name = null);

        ContractBeneficiaryVO GetContractBeneficiary(
            string uin,
            UinType uinType,
            bool isHistoric);

        ContractBeneficiaryVO GetContractBeneficiary(int contractId, bool isHistoric);

        PageVO<ContractPartnerVO> GetContractPartners(
            string name = null,
            int? companyTypeId = null,
            int? companyLegalTypeId = null,
            string companyUin = null,
            int offset = 0,
            int? limit = null);

        IEnumerable<ContractPartnerVO> GetSimpleContractPartners(
            string name = null);

        ContractPartnerVO GetContractPartner(
            string uin,
            UinType uinType,
            bool isHistoric);

        ContractPartnerVO GetContractPartner(int partnerId, bool isHistoric);

        PageVO<ContractContractorVO> GetContractContractors(
            string name = null,
            string companyUin = null,
            int offset = 0,
            int? limit = null);

        IEnumerable<ContractContractorVO> GetSimpleContractContractors(
            string name = null);

        ContractContractorVO GetContractContractor(
            string uin,
            UinType uinType,
            bool isHistoric);

        ContractContractorVO GetContractContractor(int contractorId, bool isHistoric);

        PageVO<ContractSubcontractorVO> GetContractSubcontractors(
            ContractSubcontractType type,
            string name = null,
            string companyUin = null,
            int offset = 0,
            int? limit = null);

        ContractSubcontractorVO GetContractSubcontractor(
            ContractSubcontractType type,
            string uin,
            UinType uinType,
            bool isHistoric);

        UsersCountVO GetUsersCount();

        PageVO<UserStatisticsVO> GetUsersStatistics(int offset = 0, int? limit = null, bool isEn = false);

        OPStatisticsVO GetOPStatistics(int? programmeId = null);

        ProgrammesProceduresStatisticsVO GetProgrammesProceduresStatistics(
            int? programmeId = null,
            int offset = 0,
            int? limit = null);

        ProjectStatisticsWrapperVO GetProcedureProjectsStatistics(
            int procedureId,
            int offset = 0,
            int? limit = null);

        PageVO<EvalSessionAdminAdmissProjectVO> GetEvaluatedProjectsADS(
            int resultId,
            int offset = 0,
            int? limit = null);

        PageVO<EvalSessionPreliminaryProjectVO> GetPreliminaryProjects(
           int resultId,
           int offset = 0,
           int? limit = null);

        PageVO<EvalSessionStandingProjectVO> GetStandingProjects(
           int resultId,
           int offset = 0,
           int? limit = null);

        List<EvalSessionResultVO> GetEvalSessionResults(int procedureId, EvalSessionResultType resultType);

        EvalSessionResultVO GetEvalSessionResult(int evalSessionResultId);

        int GetProgrammePriorityTotalContractsCount(List<int> procedureIds);

        int GetProgrammePriorityTotalCompaniesCount(List<int> procedureIds);

        OpenDataVO GetOpenDataResult(int programmeId);

        PageVO<ActuallyPaidAmountsVO> GetActuallyPaidAmounts(
            GroupingLevel groupingLevel,
            int? programmeId = null,
            int? programmePriorityId = null,
            int? procedureId = null,
            DateTime? dateTo = null,
            int offset = 0,
            int? limit = null);

        List<IndicativeAnnualWorkingProgrammeVO> GetIndicativeAnnualWorkingProgrammes(
            int? programmeId,
            IndicativeAnnualWorkingProgrammeYear? year,
            IndicativeAnnualWorkingProgrammeType? type);

        IndicativeAnnualWorkingProgrammeVO GetIndicativeAnnualWorkingProgramme(int indicativeAnnualWorkingProgrammeId);

        PageVO<IndicativeAnnualWorkingProgrammeTableVO> GetIndicativeAnnualWorkingProgrammeTable(
            int iawpId,
            int offset = 0,
            int? limit = null);

        PageVO<Operations508ReportVO> GetOperations508Report(
            DateTime? startDateFrom = null,
            DateTime? completionDateTo = null,
            int? programmeId = null,
            int offset = 0,
            int? limit = null);
    }
}
