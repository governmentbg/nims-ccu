using Eumis.Common.Json;
using Eumis.Domain.Procurements;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procurements.ViewObjects
{
    public class ProcurementInfoVO
    {
        public string Name { get; set; }

        public ProcurementStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcurementStatus StatusName => this.Status;
    }
}
