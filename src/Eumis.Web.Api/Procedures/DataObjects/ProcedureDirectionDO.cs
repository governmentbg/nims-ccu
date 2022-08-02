using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureDirectionDO
    {
        public ProcedureDirectionDO()
        {
        }

        public ProcedureDirectionDO(ProcedureDirection procedureDirection, byte[] version)
        {
            this.ProcedureDirectionId = procedureDirection.ProcedureDirectionId;
            this.MapNodeId = procedureDirection.ProgrammePriorityId;
            this.DirectionId = procedureDirection.DirectionId;
            this.SubDirectionId = procedureDirection.SubDirectionId;
            this.Amount = procedureDirection.Amount;
            this.Version = version;
        }

        public int? ProcedureDirectionId { get; set; }

        public int MapNodeId { get; set; }

        public int DirectionId { get; set; }

        public int? SubDirectionId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? Amount { get; set; }

        public byte[] Version { get; set; }
    }
}
