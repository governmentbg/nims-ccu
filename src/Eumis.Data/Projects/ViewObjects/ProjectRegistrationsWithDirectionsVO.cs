using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectRegistrationsWithDirectionsVO : ProjectRegistrationsVO
    {
       public List<DirectionItemVO> Directions { get; set; }
    }
}
