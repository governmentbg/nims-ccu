using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Debts.DataObjects
{
    public class ContractDebtVersionDO
    {
        public ContractDebtVersionDO()
        {
        }

        public ContractDebtVersionDO(ContractDebtVersion contractDebtVersion, string createdByUser)
        {
            this.ContractDebtVersionId = contractDebtVersion.ContractDebtVersionId;
            this.ContractDebtId = contractDebtVersion.ContractDebtId;
            this.OrderNum = contractDebtVersion.OrderNum;
            this.Status = contractDebtVersion.Status;
            this.ExecutionStatus = contractDebtVersion.ExecutionStatus;
            this.EuAmount = contractDebtVersion.EuAmount;
            this.BgAmount = contractDebtVersion.BgAmount;
            this.TotalAmount = contractDebtVersion.TotalAmount;

            this.CertStatus = contractDebtVersion.CertStatus;
            this.CertEuAmount = contractDebtVersion.CertEuAmount;
            this.CertBgAmount = contractDebtVersion.CertBgAmount;
            this.CertTotalAmount = contractDebtVersion.CertTotalAmount;
            this.CreateNotes = contractDebtVersion.CreateNotes;
            this.CreatedByUser = createdByUser;

            this.CreateDate = contractDebtVersion.CreateDate;
            this.ModifyDate = contractDebtVersion.ModifyDate;
            this.Version = contractDebtVersion.Version;
        }

        public int? ContractDebtVersionId { get; set; }

        public int? ContractDebtId { get; set; }

        public int? OrderNum { get; set; }

        public ContractDebtVersionStatus Status { get; set; }

        public ContractDebtExecutionStatus? ExecutionStatus { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? EuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TotalAmount { get; set; }

        public ContractDebtCertStatus? CertStatus { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertTotalAmount { get; set; }

        public string CreateNotes { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
