using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class LocationPVO
    {
        public LocationPVO()
        {
        }

        public LocationPVO(NutsLevel nutsLevel, string locationPath)
        {
            this.NutsLevel = new EnumPVO<NutsLevel>
            {
                Description = nutsLevel,
                Value = nutsLevel,
            };

            this.LocationFullPath = locationPath;
        }

        public EnumPVO<NutsLevel> NutsLevel { get; set; }

        public string LocationFullPath { get; set; }
    }
}
