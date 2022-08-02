using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Eumis.Data;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Indicators.Repositories;
using Eumis.Data.NonAggregates.Repositories;
using Eumis.Data.OperationalMap.Programmes.Repositories;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.ProgrammePriorities;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/nutsLevels")]
    public class NutsLevelNomsController : EnumNomsController<NutsLevel>
    {
        private IEnumNomsRepository<NutsLevel> nutsLevelEnumNomsRepository;

        public NutsLevelNomsController(IEnumNomsRepository<NutsLevel> nutsLevelEnumNomsRepository)
            : base(nutsLevelEnumNomsRepository)
        {
            this.nutsLevelEnumNomsRepository = nutsLevelEnumNomsRepository;
        }
    }
}
