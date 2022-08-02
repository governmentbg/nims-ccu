using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureSpecFieldVO
    {
        public int ProcedureSpecFieldId { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }

        public bool IsActivated { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus ActiveStatus { get; set; }
    }
}
