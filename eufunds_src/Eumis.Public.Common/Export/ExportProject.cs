using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Common.Export
{
    public class ExportProject
    {
        public string Id { get; set; }

        public string SourceofFunding { get; set; }

        public DateTime InitialDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateofDecisionforFunding { get; set; }

        public string ProjectBeneficiaryEntityId { get; set; }

        public string[] PlaceOfExecution { get; set; }

        public string Name { get; set; }

        public decimal? TotalValue { get; set; }

        public decimal? BeneficiaryFunding { get; set; }

        public decimal[] ActuallyPaid { get; set; }

        public int DurationInMonths { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string[] PartnerEntityIds { get; set; }
    }
}
