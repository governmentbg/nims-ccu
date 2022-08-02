using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureSharesVO
    {
        public int ProcedureShareId { get; set; }

        public int ProgrammeId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammeNameAlt { get; set; }

        public int ProgrammePriorityId { get; set; }

        public Guid ProgrammePriorityGid { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityNameAlt { get; set; }

        public decimal EuAmount { get; set; }

        public decimal BgAmount { get; set; }

        public bool IsPrimary { get; set; }
    }
}
