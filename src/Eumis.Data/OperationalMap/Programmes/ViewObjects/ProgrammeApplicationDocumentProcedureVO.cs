using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeApplicationDocumentProcedureVO
    {
        public int ProcedureId { get; set; }

        public string ProcedureCode { get; set; }

        public string ProcedureName { get; set; }

        public DateTime? ActivationDate { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureStatus Status { get; set; }
    }
}
