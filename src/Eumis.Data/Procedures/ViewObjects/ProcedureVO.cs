using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureVO
    {
        public int ProcedureId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public IList<string> ProgrammeNames { get; set; }

        public DateTime? ActivationDate { get; set; }

        public DateTime? EndingDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureStatus Status { get; set; }

        public decimal? BgAmount { get; set; }
    }
}
