using Eumis.Common.Json;
using Eumis.Domain.Core;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Contracts.DataObjects
{
    public class ContractReportCertAuthorityFinancialCorrectionCSDDO
    {
        public ContractReportCertAuthorityFinancialCorrectionCSDDO()
        {
        }

        public ContractReportCertAuthorityFinancialCorrectionCSDDO(
            ContractReportCertAuthorityFinancialCorrectionCSD contractReportCertAuthorityFinancialCorrectionCSD,
            string checkedByUser,
            ContractReportFinancialCSDBudgetItem contractReportFinancialCSDBudgetItem,
            ContractReportFinancialCSD contractReportFinancialCSD,
            string budgetItemCheckedByUser,
            string budgetItemTechCheckedByUser)
        {
            this.ContractReportCertAuthorityFinancialCorrectionCSDId = contractReportCertAuthorityFinancialCorrectionCSD.ContractReportCertAuthorityFinancialCorrectionCSDId;
            this.ContractReportCertAuthorityFinancialCorrectionId = contractReportCertAuthorityFinancialCorrectionCSD.ContractReportCertAuthorityFinancialCorrectionId;
            this.ContractReportFinancialCSDBudgetItemId = contractReportCertAuthorityFinancialCorrectionCSD.ContractReportFinancialCSDBudgetItemId;
            this.ContractReportFinancialId = contractReportCertAuthorityFinancialCorrectionCSD.ContractReportFinancialId;
            this.ContractReportId = contractReportCertAuthorityFinancialCorrectionCSD.ContractReportId;
            this.ContractId = contractReportCertAuthorityFinancialCorrectionCSD.ContractId;
            this.Gid = contractReportCertAuthorityFinancialCorrectionCSD.Gid;

            this.Sign = contractReportCertAuthorityFinancialCorrectionCSD.Sign;
            this.Status = contractReportCertAuthorityFinancialCorrectionCSD.Status;
            this.Notes = contractReportCertAuthorityFinancialCorrectionCSD.Notes;
            this.CheckedByUser = checkedByUser;
            this.CheckedDate = contractReportCertAuthorityFinancialCorrectionCSD.CheckedDate;

            this.CertifiedEuAmount = contractReportCertAuthorityFinancialCorrectionCSD.CertifiedEuAmount;
            this.CertifiedBgAmount = contractReportCertAuthorityFinancialCorrectionCSD.CertifiedBgAmount;
            this.CertifiedBfpTotalAmount = contractReportCertAuthorityFinancialCorrectionCSD.CertifiedBfpTotalAmount;
            this.CertifiedSelfAmount = contractReportCertAuthorityFinancialCorrectionCSD.CertifiedSelfAmount;
            this.CertifiedTotalAmount = contractReportCertAuthorityFinancialCorrectionCSD.CertifiedTotalAmount;

            this.CreateDate = contractReportCertAuthorityFinancialCorrectionCSD.CreateDate;
            this.ModifyDate = contractReportCertAuthorityFinancialCorrectionCSD.ModifyDate;
            this.Version = contractReportCertAuthorityFinancialCorrectionCSD.Version;

            this.ContractReportFinancialCSDBudgetItem = new ContractReportFinancialCSDBudgetItemDO(
                contractReportFinancialCSDBudgetItem,
                contractReportFinancialCSD,
                budgetItemCheckedByUser,
                budgetItemTechCheckedByUser);
        }

        public int ContractReportCertAuthorityFinancialCorrectionCSDId { get; set; }

        public int ContractReportCertAuthorityFinancialCorrectionId { get; set; }

        public int ContractReportFinancialCSDBudgetItemId { get; set; }

        public int ContractReportFinancialId { get; set; }

        public int ContractReportId { get; set; }

        public int ContractId { get; set; }

        public Guid Gid { get; set; }

        public Sign? Sign { get; set; }

        public ContractReportCertAuthorityFinancialCorrectionCSDStatus Status { get; set; }

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
