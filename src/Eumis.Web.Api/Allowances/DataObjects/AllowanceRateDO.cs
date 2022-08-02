using System;
using Eumis.Domain.Allowances;

namespace Eumis.Web.Api.Allowances.DataObjects
{
    public class AllowanceRateDO
    {
        public AllowanceRateDO()
        {
        }

        public AllowanceRateDO(int allowanceId, byte[] version)
        {
            this.AllowanceId = allowanceId;
            this.Version = version;
        }

        public AllowanceRateDO(AllowanceRate allowanceRate, byte[] version)
        {
            this.AllowanceRateId = allowanceRate.AllowanceRateId;
            this.AllowanceId = allowanceRate.AllowanceId;
            this.Date = allowanceRate.Date;
            this.Rate = allowanceRate.Rate;

            this.Version = version;
        }

        public int? AllowanceRateId { get; set; }

        public int? AllowanceId { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Rate { get; set; }

        public byte[] Version { get; set; }
    }
}
