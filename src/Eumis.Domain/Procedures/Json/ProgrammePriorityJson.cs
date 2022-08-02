using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;

namespace Eumis.Domain.Procedures.Json
{
    public class ProgrammePriorityJson
    {
        public int ProgrammePriorityId { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }
}
