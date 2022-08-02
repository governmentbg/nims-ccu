using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureLocationJson
    {
        public ProcedureLocationJson()
        {
        }

        public ProcedureLocationJson(NutsLevel nutsLevel, string locationFullPath)
        {
            this.NutsLevel = nutsLevel;
            this.LocationFullPath = locationFullPath;
        }

        public NutsLevel NutsLevel { get; set; }

        public string LocationFullPath { get; set; }
    }
}
