using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class ContractDebtReportVO
    {
        public int ContractDebtId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        public string RegNumber { get; set; }

        public string Company { get; set; }

        public string ContractNumber { get; set; }

        public DateTime RegDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string IrregularityNum { get; set; }

        public int? FinancialCorrectionNum { get; set; }

        public decimal NewDebtPrincipalBfpEuAmount { get; set; }

        public decimal NewDebtPrincipalBfpBgAmount { get; set; }

        public decimal NewDebtPrincipalTotalAmount { get; set; }

        public decimal DebtPrincipalBfpEuAmount { get; set; }

        public decimal DebtPrincipalBfpBgAmount { get; set; }

        public decimal DebtPrincipalTotalAmount { get; set; }

        public decimal DebtInterestBfpEuAmount { get; set; }

        public decimal DebtInterestBfpBgAmount { get; set; }

        public decimal DebtInterestTotalAmount { get; set; }

        public decimal DebtTotalAmount { get; set; }

        public decimal RaPrincipalBfpEuAmount { get; set; }

        public decimal RaPrincipalBfpBgAmount { get; set; }

        public decimal RaPrincipalBfpTotalAmount { get; set; }

        public decimal RaInterestBfpEuAmount { get; set; }

        public decimal RaInterestBfpBgAmount { get; set; }

        public decimal RaInterestBfpTotalAmount { get; set; }

        public decimal RaBfpTotalAmount { get; set; }

        public DateTime? ReimbursementDate { get; set; }

        public decimal DaPrincipalBfpEuAmount { get; set; }

        public decimal DaPrincipalBfpBgAmount { get; set; }

        public decimal DaPrincipalBfpTotalAmount { get; set; }

        public decimal DaInterestBfpEuAmount { get; set; }

        public decimal DaInterestBfpBgAmount { get; set; }

        public decimal DaInterestBfpTotalAmount { get; set; }

        public decimal DaBfpTotalAmount { get; set; }

        public DateTime? DeductionDate { get; set; }

        public decimal InterestBfpEuAmount { get; set; }

        public decimal InterestBfpBgAmount { get; set; }

        public decimal InterestTotalAmount { get; set; }

        public decimal RemainingDebtPrincipalBfpEuAmount { get; set; }

        public decimal RemainingDebtPrincipalBfpBgAmount { get; set; }

        public decimal RemainingDebtPrincipalBfpTotalAmount { get; set; }

        public decimal RemainingDebtInterestBfpEuAmount { get; set; }

        public decimal RemainingDebtInterestBfpBgAmount { get; set; }

        public decimal RemainingDebtInterestBfpTotalAmount { get; set; }

        public decimal RemainingDebtBfpTotalAmount { get; set; }

        public List<ContractDebtPaymentCertReportVO> PaymentsCertReports { get; set; }

        public List<ContractDebtPaymentCertReportVO> CorrectionsCertReports { get; set; }
    }
}
