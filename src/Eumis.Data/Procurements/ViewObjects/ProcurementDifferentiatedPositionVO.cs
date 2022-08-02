using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procurements.ViewObjects
{
    public class ProcurementDifferentiatedPositionVO
    {
        public int ProcurementDifferentiatedPositionId { get; set; }

        public int ProcurementId { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public string CompanyName { get; set; }

        public string CompanyUin { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public UinType CompanyUinType { get; set; }
    }
}
