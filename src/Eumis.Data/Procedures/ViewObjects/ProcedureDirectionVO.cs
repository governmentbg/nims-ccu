using Eumis.Common.Json;
using Eumis.Common.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureDirectionVO
    {
        public int? ProcedureDirectionId { get; set; }

        public int? ProgrammePriorityId { get; set; }

        [JsonIgnore]
        public string DirectionName { get; set; }

        [JsonIgnore]
        public string DirectionNameAlt { get; set; }

        public string Direction
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.DirectionName, this.DirectionNameAlt);
            }
        }

        [JsonIgnore]
        public string SubDirectionName { get; set; }

        [JsonIgnore]
        public string SubDirectionNameAlt { get; set; }

        public string SubDirection
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.SubDirectionName, this.SubDirectionNameAlt);
            }
        }

        public decimal? Amount { get; set; }
    }
}
