using Eumis.Common.Json;
using Newtonsoft.Json;
using System;

namespace Eumis.Domain.Debts.DataObjects
{
    public class CorrectionDebtVersionDO
    {
        public CorrectionDebtVersionDO()
        {
        }

        public CorrectionDebtVersionDO(CorrectionDebtVersion correctionDebtVersion, string createdByUser, int flatFinancialCorrectionId)
        {
            this.CorrectionDebtVersionId = correctionDebtVersion.CorrectionDebtVersionId;
            this.CorrectionDebtId = correctionDebtVersion.CorrectionDebtId;
            this.FlatFinancialCorrectionId = flatFinancialCorrectionId;
            this.OrderNum = correctionDebtVersion.OrderNum;
            this.Status = correctionDebtVersion.Status;

            this.DebtEuAmount = correctionDebtVersion.DebtEuAmount;
            this.DebtBgAmount = correctionDebtVersion.DebtBgAmount;
            this.DebtBfpAmount = correctionDebtVersion.DebtBfpAmount;

            this.CertEuAmount = correctionDebtVersion.CertEuAmount;
            this.CertBgAmount = correctionDebtVersion.CertBgAmount;
            this.CertBfpAmount = correctionDebtVersion.CertBfpAmount;

            this.ReimbursedEuAmount = correctionDebtVersion.ReimbursedEuAmount;
            this.ReimbursedBgAmount = correctionDebtVersion.ReimbursedBgAmount;
            this.ReimbursedBfpAmount = correctionDebtVersion.ReimbursedBfpAmount;

            this.CreateNotes = correctionDebtVersion.CreateNotes;
            this.CreatedByUser = createdByUser;

            this.CreateDate = correctionDebtVersion.CreateDate;
            this.ModifyDate = correctionDebtVersion.ModifyDate;
            this.Version = correctionDebtVersion.Version;
        }

        public int? CorrectionDebtVersionId { get; set; }

        public int? CorrectionDebtId { get; set; }

        public int? FlatFinancialCorrectionId { get; set; }

        public int? OrderNum { get; set; }

        public CorrectionDebtVersionStatus Status { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? DebtEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? DebtBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? DebtBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? CertBfpAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ReimbursedEuAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ReimbursedBgAmount { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ReimbursedBfpAmount { get; set; }

        public string CreateNotes { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}
