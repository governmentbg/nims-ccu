using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.OperationalMap.ProcedureManuals;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.OperationalMap.Programmes.ViewObjects
{
    public class ProgrammeProcedureManualsVO
    {
        public int ProgrammeProcedureManualId { get; set; }

        public int ProgrammeId { get; set; }

        public string Name { get; set; }

        public DateTime? ActivationDate { get; set; }

        public int VersionNum { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProgrammeProcedureManualStatus Status { get; set; }

        public FileVO File { get; set; }
    }
}
