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
    public class ProcedureMassCommunicationInfoVO
    {
        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureMassCommunicationStatus StatusName
        {
            get
            {
                return this.Status;
            }
        }

        public ProcedureMassCommunicationStatus Status { get; set; }

        public string ProcedureCode { get; set; }

        public byte[] Version { get; set; }
    }
}
