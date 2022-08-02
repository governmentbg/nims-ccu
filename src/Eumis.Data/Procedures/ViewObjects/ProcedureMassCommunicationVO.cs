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
    public class ProcedureMassCommunicationVO
    {
        public int ProcedureMassCommunicationId { get; set; }

        public string ProcedureCode { get; set; }

        public string Subject { get; set; }

        public DateTime ModifyDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureMassCommunicationStatus Status { get; set; }
    }
}
