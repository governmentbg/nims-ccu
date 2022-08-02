using Eumis.Common.Config;
using Eumis.Data.ContractReportFinancialCSDs.PortalViewObjects;
using Eumis.Data.ContractReportIndicators.PortalViewObjects;
using Eumis.Data.ContractReports.PortalViewObjects;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Procedures.PortalViewObjects;
using Eumis.Data.Projects.PortalViewObjects;
using Eumis.Domain.Contracts;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Eumis.ApplicationServices.Communicators
{
    public class DocumentRestApiCommunicator : RestApiCommunicator, IDocumentRestApiCommunicator
    {
        public DocumentRestApiCommunicator()
            : base(
                  new Uri(
                      ConfigurationManager.AppSettings.GetWithEnv("Eumis.ApplicationServices:PortalLocationForAPIs"),
                      UriKind.Absolute))
        {
        }

        public string CreateEvalSessionSheetXml(string evalTableXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/evalSheet", new { Xml = evalTableXml }).Xml;
        }

        public string CreateEvalSessionStandpointXml()
        {
            return this.PostJson<RioDocument>("/api/s/initializer/standpoint", null).Xml;
        }

        public string CreateProcedureEvalTableXml(ProcedureEvalType evalType)
        {
            switch (evalType)
            {
                case ProcedureEvalType.Rejection:
                    return this.PostJson<RioDocument>("/api/s/initializer/evalTableRejection", null).Xml;
                case ProcedureEvalType.Weight:
                    return this.PostJson<RioDocument>("/api/s/initializer/evalTableWeight", null).Xml;
                default:
                    throw new Exception("Unknown evalType.");
            }
        }

        public string CopyProcedureEvalTableXml(ProcedureEvalType evalType, string evalTableXml)
        {
            switch (evalType)
            {
                case ProcedureEvalType.Rejection:
                    return this.PostJson<RioDocument>("/api/s/initializer/copyEvalTableRejection", new { Xml = evalTableXml }).Xml;
                case ProcedureEvalType.Weight:
                    return this.PostJson<RioDocument>("/api/s/initializer/copyEvalTableWeight", new { Xml = evalTableXml }).Xml;
                default:
                    throw new Exception("Unknown evalType.");
            }
        }

        public string CreateProjectCommunicationQuestionXml(string projectVersionXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/messageQuestion", new { Xml = projectVersionXml }).Xml;
        }

        public string CreateProjectCommunicationAnswerXml(string xml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/messageAnswer", new { Xml = xml }).Xml;
        }

        public string CreateProjectManagingAuthorityCommunicationQuestionXml()
        {
            return this.PostJson<RioDocument>("/api/s/initializer/managingAuthorityCommunicationQuestion", null).Xml;
        }

        public string CreateProjectManagingAuthorityCommunicationQuestionXml(ProjectMassCommunicationPVO communication)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/managingAuthorityCommunicationQuestionTemplate", communication).Xml;
        }

        public string CreateProjectManagingAuthorityCommunicationAnswerXml(string questionXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/managingAuthorityCommunicationAnswer", new { Xml = questionXml }).Xml;
        }

        public string CreateFirstProjectVersionXml(Guid procedureGid)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/projectNew", new { Gid = procedureGid }).Xml;
        }

        public string CreateProjectVersionXmlFromCommunication(string answerXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/projectFromMessage", new { Xml = answerXml }).Xml;
        }

        public string CreateInitialContractVersionXml(
            string projectXml,
            string programmeCode,
            string projectRegNumber,
            string contractRegNumber,
            string authorityUin,
            UinType? authorityUinType,
            Guid contractGid)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/bfpContract",
                new
                {
                    Xml = projectXml,
                    ProgrammeCode = programmeCode,
                    ProjectRegNumber = projectRegNumber,
                    ContractRegNumber = contractRegNumber,
                    AuthorityUin = authorityUin,
                    AuthorityUinType = authorityUinType,
                    ContractGid = contractGid,
                }).Xml;
        }

        public string LoadContractVersion(string versionXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/loadBFPContract", new { Xml = versionXml }).Xml;
        }

        public string CreateContractProcurementXml(string procurementXml, Guid contractVersionGid, string contractVersionXml, int orderNum)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/procurements", new { Gid = contractVersionGid, ContractVersionXml = contractVersionXml, Xml = procurementXml, DocNumber = orderNum.ToString() }).Xml;
        }

        public string CreateContractSpendingPlanXml(string spendingPlanXml, Guid contractVersionGid, string contractVersionXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/spendingPlan", new { Gid = contractVersionGid, ContractVersionXml = contractVersionXml, Xml = spendingPlanXml }).Xml;
        }

        public string CreateContractCommunicationXml(ContractCommunicationType communicationType)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/communication?type=" + communicationType, null).Xml;
        }

        public string CreateContractCommunicationXml(CommunicationPVO communication)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/communicationTemplate", communication).Xml;
        }

        public async Task<string> CreateContractReportTechnicalAsync(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            return (await this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/technicalReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastTechnicalReportXml = lastTechnicalReportXml,
                    ApprovedIndicators = lastTechnicalReportIndicators,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                })).Xml;
        }

        public string CreateContractReportTechnical(
           Guid contractGid,
           Guid contractReportGid,
           string contractVersionXml,
           string contractProcurementXml,
           string lastTechnicalReportXml,
           IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
           string contractNumber,
           string docNumber,
           string docSubNumber)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/technicalReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastTechnicalReportXml = lastTechnicalReportXml,
                    ApprovedIndicators = lastTechnicalReportIndicators,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                }).Xml;
        }

        public string CopyContractReportTechnical(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string originTechnicalReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/copyTechnicalReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastTechnicalReportXml = lastTechnicalReportXml,
                    OriginTechnicalReportXml = originTechnicalReportXml,
                    ApprovedIndicators = lastTechnicalReportIndicators,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                }).Xml;
        }

        public async Task<string> CopyContractReportTechnicalAsync(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators,
            string originTechnicalReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            var result = await this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/copyTechnicalReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastTechnicalReportXml = lastTechnicalReportXml,
                    OriginTechnicalReportXml = originTechnicalReportXml,
                    ApprovedIndicators = lastTechnicalReportIndicators,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                });

            return result.Xml;
        }

        public string CreateContractReportPayment(
            Guid contractGid,
            Guid contractReportGid,
            ContractReportPaymentType type,
            string contractVersionXml,
            string contractProcurementXml,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            var paymentType = new EnumPVO<ContractReportPaymentType>()
            {
                Value = type,
                Description = type,
            };

            return this.PostJson<RioDocument>(
                "/api/s/initializer/paymentRequest",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    Type = paymentType,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                }).Xml;
        }

        public string CopyContractReportPayment(
            Guid contractGid,
            Guid contractReportGid,
            ContractReportPaymentType type,
            string contractVersionXml,
            string contractProcurementXml,
            string originPaymentReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            var paymentType = new EnumPVO<ContractReportPaymentType>()
            {
                Value = type,
                Description = type,
            };

            return this.PostJson<RioDocument>(
                "/api/s/initializer/copyPaymentRequest",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    Type = paymentType,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    OriginPaymentReportXml = originPaymentReportXml,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                }).Xml;
        }

        public async Task<string> CopyContractReportPaymentAsync(
            Guid contractGid,
            Guid contractReportGid,
            ContractReportPaymentType type,
            string contractVersionXml,
            string contractProcurementXml,
            string originPaymentReportXml,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            var paymentType = new EnumPVO<ContractReportPaymentType>()
            {
                Value = type,
                Description = type,
            };

            var result = await this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/copyPaymentRequest",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    Type = paymentType,
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    OriginPaymentReportXml = originPaymentReportXml,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                });

            return result.Xml;
        }

        public string CreateContractReportFinancial(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            string contractProcurementXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/financeReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    LastFinancialReportXml = lastFinancialReportXml,
                    AdvancePaymentReportXml = advancePaymentReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    ApprovedCumulativeCSDBudgetAmounts = approvedCumulativeCSDBudgetAmounts,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                }).Xml;
        }

        public async Task<string> CreateContractReportFinancialAsync(
            Guid contractGid,
            Guid contractReportGid,
            string contractVersionXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            string contractProcurementXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts,
            string contractNumber,
            string docNumber,
            string docSubNumber)
        {
            var result = await this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/financeReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    LastFinancialReportXml = lastFinancialReportXml,
                    AdvancePaymentReportXml = advancePaymentReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    ApprovedCumulativeCSDBudgetAmounts = approvedCumulativeCSDBudgetAmounts,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                });

            return result.Xml;
        }

        public string CopyContractReportFinancial(
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
            string procedureTypeAlias)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/copyFinanceReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    LastFinancialReportXml = lastFinancialReportXml,
                    AdvancePaymentReportXml = advancePaymentReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    ApprovedCumulativeCSDBudgetAmounts = approvedCumulativeCSDBudgetAmounts,
                    OriginFinancialReportXml = originFinancialReportXml,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                    ProcedureTypeAlias = procedureTypeAlias,
                }).Xml;
        }

        public async Task<string> CopyContractReportFinancialAsync(
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
            string docSubNumber)
        {
            var result = await this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/copyFinanceReport",
                new
                {
                    ContractGid = contractGid,
                    PackageGid = contractReportGid,
                    ContractVersionXml = contractVersionXml,
                    LastFinancialReportXml = lastFinancialReportXml,
                    AdvancePaymentReportXml = advancePaymentReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    ApprovedCumulativeCSDBudgetAmounts = approvedCumulativeCSDBudgetAmounts,
                    OriginFinancialReportXml = originFinancialReportXml,
                    ContractNumber = contractNumber,
                    DocNumber = docNumber,
                    DocSubNumber = docSubNumber,
                });

            return result.Xml;
        }

        public string CreateRegOfferXml(
            string contractVersionXml,
            string contractProcurementXml,
            Guid contractGid,
            Guid procurementsGid,
            Guid planGid,
            Guid positionGid)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/offer",
                new
                {
                    ContractVersionXml = contractVersionXml,
                    ContractProcurementXml = contractProcurementXml,
                    ContractGid = contractGid,
                    ProcurementsGid = procurementsGid,
                    PlanGid = planGid,
                    PositionGid = positionGid,
                }).Xml;
        }

        public string LoadContractReportTechnical(
            string currentTechnicalReportXml,
            string contractProcurementXml,
            string lastTechnicalReportXml,
            IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/loadTechnicalReport",
                new
                {
                    Xml = currentTechnicalReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastTechnicalReportXml = lastTechnicalReportXml,
                    ApprovedIndicators = lastTechnicalReportIndicators,
                }).Xml;
        }

        public async Task<string> LoadContractReportTechnicalAsync(
           string currentTechnicalReportXml,
           string contractProcurementXml,
           string lastTechnicalReportXml,
           IList<ContractReportIndicatorsPVO> lastTechnicalReportIndicators)
        {
            var result = this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/loadTechnicalReport",
                new
                {
                    Xml = currentTechnicalReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastTechnicalReportXml = lastTechnicalReportXml,
                    ApprovedIndicators = lastTechnicalReportIndicators,
                });

            string xml = (await result).Xml;

            return xml;
        }

        public string LoadContractReportFinancial(
            string currentFinancialReportXml,
            string contractProcurementXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/loadFinanceReport",
                new
                {
                    Xml = currentFinancialReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastFinancialReportXml = lastFinancialReportXml,
                    AdvancePaymentReportXml = advancePaymentReportXml,
                    ApprovedCumulativeCSDBudgetAmounts = approvedCumulativeCSDBudgetAmounts,
                }).Xml;
        }

        public async Task<string> LoadContractReportFinancialAsync(
            string currentFinancialReportXml,
            string contractProcurementXml,
            string lastFinancialReportXml,
            string advancePaymentReportXml,
            IList<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts)
        {
            var result = await this.PostJsonAsync<RioDocument>(
                "/api/s/initializer/loadFinanceReport",
                new
                {
                    Xml = currentFinancialReportXml,
                    ContractProcurementXml = contractProcurementXml,
                    LastFinancialReportXml = lastFinancialReportXml,
                    AdvancePaymentReportXml = advancePaymentReportXml,
                    ApprovedCumulativeCSDBudgetAmounts = approvedCumulativeCSDBudgetAmounts,
                });

            return result.Xml;
        }

        public string CreateInitialCheckListVersionXml()
        {
            return this.PostJson<RioDocument>("/api/s/initializer/checkList", null).Xml;
        }

        public string CreateInitialCheckSheetVersionXml(string checkListXml)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/checkSheet", new { Xml = checkListXml }).Xml;
        }

        public string CreateInitialContractReportCheckSheetVersionXml(
            string checkListXml,
            int contractId,
            int? contractProcurementXmlId,
            IList<CheckSheetProcurementPlanPVO> procurementPlans)
        {
            return this.PostJson<RioDocument>(
                "/api/s/initializer/contractReportCheckSheet",
                new
                {
                    Xml = checkListXml,
                    ContractId = contractId,
                    ContractProcurementXmlId = contractProcurementXmlId,
                    ProcurementPlans = procurementPlans,
                }).Xml;
        }

        public string ReturnCheckSheetVersionXml(string checkListXml, int? roleOrderNum)
        {
            return this.PostJson<RioDocument>("/api/s/initializer/returnCheckSheet", new { Xml = checkListXml, RoleOrderNum = roleOrderNum }).Xml;
        }
    }
}
