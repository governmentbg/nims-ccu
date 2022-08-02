using System;
using System.Collections.Generic;

namespace Eumis.Documents.Contracts
{
    public class ContractInitializerReport : ContractInitializer
    {
        public string packageGid { get; set; }
        public string contractGid { get; set; }

        public string contractNumber { get; set; }
        public string docNumber { get; set; }
        public string docSubNumber { get; set; }

        public string contractVersionXml { get; set; }
        public string contractProcurementXml { get; set; }

        public string lastTechnicalReportXml { get; set; }
        public string lastFinancialReportXml { get; set; }
        public string advancePaymentReportXml { get; set; }

        public string originTechnicalReportXml { get; set; }
        public string originFinancialReportXml { get; set; }
        public string originPaymentReportXml { get; set; }

        public string procedureTypeAlias { get; set; }

        public List<ContractReportFinancialCSDBudgetItemPVO> approvedCumulativeCSDBudgetAmounts { get; set; }
        public List<ContractApprovedIndicator> approvedIndicators { get; set; }
    }
}