using System;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class CorrectionDebtVersionVO
    {
        public int CorrectionDebtVersionId { get; set; }

        public int CorrectionDebtId { get; set; }

        public int OrderNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public CorrectionDebtVersionStatus Status { get; set; }

        public decimal? DebtEuAmount { get; set; }

        public decimal? DebtBgAmount { get; set; }

        public decimal? CertEuAmount { get; set; }

        public decimal? CertBgAmount { get; set; }

        public decimal? ReimbursedEuAmount { get; set; }

        public decimal? ReimbursedBgAmount { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }
    }
}