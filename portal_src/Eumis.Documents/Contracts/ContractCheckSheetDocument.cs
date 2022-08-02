using Eumis.Common.Localization;
using System;

namespace Eumis.Documents.Contracts
{
    public class ContractCheckSheetDocument : ContractDocumentXml
    {
        public string checkSheetName { get; set; }

        public string programmeName { get; set; }

        public string procedureName { get; set; }

        public string contractName { get; set; }

        public string companyName { get; set; }

        public string contractProcurementName { get; set; }

        public string contractReportName { get; set; }

        public string certReportNumber { get; set; }

        public string spotCheckRegNumber { get; set; }

        public decimal? contractEuAmount { get; set; }

        public decimal? contractBgAmount { get; set; }

        public decimal? contractSelfAmount { get; set; }

        public DateTime? contractStartDate { get; set; }

        public DateTime? contractCompletionDate { get; set; }

        public string paymentRequestData { get; set; }

        public decimal? paymentRequestAmount { get; set; }

        public string notes { get; set; }

        public ContractPrivateNomenclature currentRespondent { get; set; }

        public bool? isContractReportCheckSheet { get; set; }

        public string contractExecutionStatusText { get; set; }

        public string contractExecutionStatusTextAlt { get; set; }

        public string displayContractExecutionStatus { get { return SystemLocalization.GetDisplayName(contractExecutionStatusText, contractExecutionStatusTextAlt); } }
    }
}
