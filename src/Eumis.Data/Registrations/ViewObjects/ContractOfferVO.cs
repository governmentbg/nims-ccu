using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Registrations.ViewObjects
{
    public class ContractOfferVO
    {
        public int Id { get; set; }

        public Guid Gid { get; set; }

        public DateTime SubmitDate { get; set; }

        public bool CanBeDisplayed { get; set; }

        public string Description { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }

        public string ProcurementPlanSubject { get; set; }

        public string ProcurementPlanObject { get; set; }

        public decimal ProcurementPlanExpectedAmount { get; set; }
    }
}
