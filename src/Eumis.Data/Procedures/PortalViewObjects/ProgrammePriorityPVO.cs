using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProgrammePriorityPVO
    {
        public ProgrammePriorityPVO(ProgrammePriorityJson programPriority)
        {
            this.ProgrammePriorityGid = programPriority.Gid;
            this.ProgrammePriorityCode = programPriority.Code;
            this.ProgrammePriorityName = programPriority.Name;
            this.ProgrammePriorityNameAlt = programPriority.NameAlt;
        }

        public Guid ProgrammePriorityGid { get; set; }

        public string ProgrammePriorityCode { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProgrammePriorityNameAlt { get; set; }
    }
}
