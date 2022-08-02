using System;
using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class SapCertReportVO
    {
        public SapCertReportVO()
        {
        }

        public SapCertReportVO(
            string contractRegNumber,
            int? paymentVersionNum,
            string certReportNumber,
            string programmePriorityCode,
            string period,
            DateTime? date,
            FinaceSourcesSAP financeSource,
            decimal amount = 0)
            : this()
        {
            this.ContractRegNum = contractRegNumber;
            this.PaymentVersionNum = paymentVersionNum;
            this.CertReportNumber = certReportNumber;
            this.ProgrammePriorityCode = programmePriorityCode;
            this.Period = period;
            this.FinanceSource = financeSource;
            this.Date = date;
            this.TotalAmount = amount;
        }

        public string ContractRegNum { get; set; }

        public int? PaymentVersionNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public FinaceSourcesSAP? FinanceSource { get; set; }

        public decimal? TotalAmount { get; set; }

        public DateTime? Date { get; set; }

        public string Period { get; set; }

        public string CertReportNumber { get; set; }

        public string ProgrammePriorityCode { get; set; }
    }
}
