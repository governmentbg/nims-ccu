using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportFinancialCertCorrectionCSDDO
    {
        public ContractReportFinancialCertCorrectionCSDDO()
        {
        }

        public ContractReportFinancialCertCorrectionCSDDO(
            ContractReportFinancialCertCorrectionCSD contractReportFinancialCertCorrectionCSD,
            string checkedByUser,
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string budgetItemCheckedByUser,
            string budgetItemTechCheckedByUser)
        {
            this.ContractReportFinancialCertCorrectionCSDId = contractReportFinancialCertCorrectionCSD.ContractReportFinancialCertCorrectionCSDId;
            this.ContractReportFinancialCertCorrectionId = contractReportFinancialCertCorrectionCSD.ContractReportFinancialCertCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportFinancialCertCorrectionCSD.ContractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportFinancialCertCorrectionCSD.ContractReportFinancialId;
            this.ContractReportId = contractReportFinancialCertCorrectionCSD.ContractReportId;
            this.ContractId = contractReportFinancialCertCorrectionCSD.ContractId;
            this.Gid = contractReportFinancialCertCorrectionCSD.Gid;

            this.Sign = contractReportFinancialCertCorrectionCSD.Sign;
            this.Status = contractReportFinancialCertCorrectionCSD.Status;
            this.Notes = contractReportFinancialCertCorrectionCSD.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportFinancialCertCorrectionCSD.CheckedDate;

            this.CertifiedEuAmount = contractReportFinancialCertCorrectionCSD.CertifiedEuAmount;
            this.CertifiedBgAmount = contractReportFinancialCertCorrectionCSD.CertifiedBgAmount;
            this.CertifiedBfpTotalAmount = contractReportFinancialCertCorrectionCSD.CertifiedBfpTotalAmount;
            this.CertifiedSelfAmount = contractReportFinancialCertCorrectionCSD.CertifiedSelfAmount;
            this.CertifiedTotalAmount = contractReportFinancialCertCorrectionCSD.CertifiedTotalAmount;

            this.CreateDate = contractReportFinancialCertCorrectionCSD.CreateDate;
            this.ModifyDate = contractReportFinancialCertCorrectionCSD.ModifyDate;
            this.Version = contractReportFinancialCertCorrectionCSD.Version;

            this.ContractReportFinancialCSDBudgetItem = new ContractReportFinancialCSDBudgetItemDO(
                contractReportFinancialCSDBudgetItem,
                contractReportFinancialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        public int ContractReportFinancialCertCorrectionCSDId { get; set; }

        public int ContractReportFinancialCertCorrectionId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportFinancialCertCorrectionCSDStatus Status { get; set; }

        public string Notes { get; set; }

        public string CheckedByUser { get; set; }

        public DateTime? CheckedDate { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedBfpTotalAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedSelfAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertifiedTotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public ContractReportFinancialCSDBudgetItemDO ContractReportFinancialCSDBudgetItem { get; set; }
    }
}
