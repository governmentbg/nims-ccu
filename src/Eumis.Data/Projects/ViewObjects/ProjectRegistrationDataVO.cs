using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Eumis.Data.OperationalMap.Directions.ViewObjects;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;

namespace Eumis.Data.Projects.ViewObjects
{
    public class ProjectRegistrationDataVO
    {
        public int ProjectId { get; set; }

        public string ProgrammeName { get; set; }

        public string ProgrammePriorityName { get; set; }

        public string ProcedureName { get; set; }

        public string ProjectName { get; set; }

        public string RegNumber { get; set; }

        public DateTime RegDate { get; set; }

        public string ProjectXmlHash { get; set; }

        public string CompanyName { get; set; }

        public string Uin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType UinType { get; set; }

        public IList<DirectionItemVO> Directions { get; set; }
    }
}
