using System;

namespace Eumis.Data.Monitoring.ViewObjects
{
    public class EvaluationsReportItem
    {
        public string ProjectRegNum { get; set; }

        public string Company { get; set; }

        public string CompanyUin { get; set; }

        public decimal? InitialProjectTotalAmount { get; set; }

        public decimal? InitialProjectBfpAmount { get; set; }

        public decimal? InitialProjectSelfAmount { get; set; }

        public decimal? ActualProjectTotalAmount { get; set; }

        public decimal? ActualProjectBfpAmount { get; set; }

        public decimal? ActualProjectSelfAmount { get; set; }

        public DateTime? CommitteeStartDate { get; set; }

        public DateTime? CommitteeEndDate { get; set; }

        public int? CommunicationsDuration { get; set; }

        public int? CommunicationsCount { get; set; }
    }
}