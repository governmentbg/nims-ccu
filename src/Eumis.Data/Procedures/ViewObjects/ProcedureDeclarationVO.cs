using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureDeclarationVO
    {
        public int ProcedureDeclarationId { get; set; }

        public int OrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public ActiveStatus Status { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ActiveStatus StatusDescr
        {
            get { return this.Status; }
        }
    }
}
