using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ApplicationSectionVO
    {
        public ApplicationSectionType ApplicationSection { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ApplicationSectionType ApplicationSectionDesc => this.ApplicationSection;

        public bool? IsSelected { get; set; }

        public int OrderNum { get; set; }

        public int ProcedureId { get; set; }

        public IList<ApplicationSectionAdditionalSettingVO> AdditionalSettings { get; set; }
    }
}
