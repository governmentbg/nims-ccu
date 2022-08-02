using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Domain.CertReports.ViewObjects
{
    public class ProgrammePaidContributionInfoForFinancialInstrumentsVO
    {
        public string ProgrammePriorityName { get; set; }

        public decimal CertifiedTotalAmount { get; set; }

        public decimal FinancialInstrumentsAmount { get; set; }

        public decimal CorrespondingPublicSpendingAmount { get; set; }
    }
}
