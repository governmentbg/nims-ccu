using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureQuestionsVO
    {
        public int ProcedureQuestionId { get; set; }

        public int ProcedureId { get; set; }

        public string CreatedByUser { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActivated { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus ActiveStatus { get; set; }

        public FileVO File { get; set; }
    }
}
