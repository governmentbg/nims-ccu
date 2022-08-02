using Eumis.Common.Json;
using Eumis.Domain.OperationalMap.Directions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.OperationalMap.Directions.ViewObjects
{
    public class DirectionInfoVO
    {
        public DirectionStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public DirectionStatus StatusDescr => this.Status;
    }
}
