using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureBudgetComponentsVO
    {
        public int ProcedureBudgetComponentId { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public string Notes { get; set; }

        public ActiveStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus StatusDescription
        {
            get => this.Status;
        }

        public byte[] Version { get; set; }
    }
}
