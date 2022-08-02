using Eumis.Data.ContractReportFinancialCSDs.PortalViewObjects;
using Eumis.Data.ContractReportIndicators.PortalViewObjects;
using Eumis.Data.ContractReports.PortalViewObjects;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Communicators
{
    public interface IDocumentRestApiCommunicator
    {
        string CreateEvalSessionSheetXml(string evalTableXml);

        string CreateEvalSessionStandpointXml();

        string CreateProcedureEvalTableXml(ProcedureEvalType evalType);

        string CopyProcedureEvalTableXml(ProcedureEvalType evalType, string evalTableXml);

        string CreateProjectCommunicationQuestionXml(string projectVersionXml);

        string CreateProjectCommunicationAnswerXml(string xml);

        string CreateProjectManagingAuthorityCommunicationQuestionXml();

        string CreateProjectManagingAuthorityCommunicationQuestionXml(ProjectMassCommunicationPVO communication);

        string CreateProjectManagingAuthorityCommunicationAnswerXml(string questionXml);

        string CreateFirstProjectVersionXml(Guid procedureGid);

        string CreateProjectVersionXmlFromCommunication(string answerXml);

        string CreateInitialContractVersionXml(
            string projectXml,
            string programmeCode,
            string projectRegNumber,
            string contractRegNumber,
            string authorityUin,
            UinType? authorityUinType,
            Guid contractGid);

        string LoadContractVersion(string versionXml);

        string CreateContractProcurementXml(string procurementXml, Guid contractVersionGid, string contractVersionXml, int orderNum);

        string CreateContractSpendingPlanXml(string spendingPlanXml, Guid contractVersionGid, string contractVersionXml);

        string CreateContractCommunicationXml(ContractCommunicationType communicationType);

        string CreateContractCommunicationXml(CommunicationPVO communication);

        string CreateContractReportTechnical(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        Task<string> CreateContractReportTechnicalAsync(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        string CopyContractReportTechnical(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string originTechnicalReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        Task<string> CopyContractReportTechnicalAsync(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string originTechnicalReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        string CreateContractReportPayment(
            Guid contractGid,
            Guid contractReportGid,
            ContractReportPaymentType type,
            string contractVersionXml,
            string contractProcurementXml,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        string CopyContractReportPayment(
            Guid contractGid,
            Guid contractReportGid,
            ContractReportPaymentType type,
            string contractVersionXml,
            string contractProcurementXml,
            string originPaymentReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        Task<string> CopyContractReportPaymentAsync(
           Guid contractGid,
           Guid contractReportGid,
           ContractReportPaymentType type,
           string contractVersionXml,
           string contractProcurementXml,
           string originPaymentReportXml,
           string contractNumber,
           string docNumber,
           string docSubNumber);

        string CreateContractReportFinancial(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            string contractProcurementXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        Task<string> CreateContractReportFinancialAsync(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            string contractProcurementXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts,
            string contractNumber,
            string docNumber,
            string docSubNumber);

        string CopyContractReportFinancial(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            string contractProcurementXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts,
            string originFinancialReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber,
            string procedureTypeAlias);

        Task<string> CopyContractReportFinancialAsync(
           Guid contractGid,
           Guid contractReportGid,
           string contractVersionXml,
           string lastFinancialReportXml,
           string advancePaymentReportXml,
           string contractProcurementXml,
           IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts,
           string originFinancialReportXml,
           string contractNumber,
           string docNumber,
           string docSubNumber);

        string CreateRegOfferXml(
            string contractVersionXml,
            string contractProcurementXml,
            Guid contractGid,
            Guid procurementsGid,
            Guid planGid,
            Guid positionGid);

        string LoadContractReportTechnical(
            string currentTechnicalReportXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators);

        Task<string> LoadContractReportTechnicalAsync(
            string currentTechnicalReportXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators);

        string LoadContractReportFinancial(
            string currentFinancialReportXml,
            string contractProcurementXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts);

        Task<string> LoadContractReportFinancialAsync(
            string currentFinancialReportXml,
            string contractProcurementXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts);

        string CreateInitialCheckListVersionXml();

        string CreateInitialCheckSheetVersionXml(string checkListXml);

        string CreateInitialContractReportCheckSheetVersionXml(
            string checkListXml,
            int contractId,
            int? contractProcurementXmlId,
            IList<CheckSheetProcurementPlanPVO> procurementPlans);

        string ReturnCheckSheetVersionXml(string lastVersionXml, int? roleOrderNum);
    }
}
