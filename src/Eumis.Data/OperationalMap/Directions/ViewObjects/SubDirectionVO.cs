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
    public class SubDirectionVO
    {
        public int SubDirectionId { get; set; }

        public int DirectionId { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }
    }
}
