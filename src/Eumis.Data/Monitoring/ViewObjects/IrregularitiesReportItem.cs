using Eumis.Common.Json;
using Eumis.Domain.Irregularities;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class IrregularitiesReportItem
    {
        public string BeneficiaryName { get; set; }

        public string BeneficiaryUin { get; set; }

        public string BeneficiaryType { get; set; }

        public string BeneficiaryLegalType { get; set; }

        public string BeneficiarySeatAddress { get; set; }

        public string BeneficiaryCorrespondenceAddress { get; set; }

        public string ContractRegNum { get; set; }

        public string Project { get; set; }

        public string IrregularitySignal { get; set; }

        public DateTime? IrregularitySignalRegDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public IrregularityCaseState? Status { get; set; }

        public string IrregularityRegNum { get; set; }

        public DateTime? IrregularityRegDate { get; set; }

        public decimal? IrregularityValue { get; set; }

        public string FinancialCorrections { get; set; }
    }
}
