using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureApplicationSectionJson
    {
        public ProcedureApplicationSectionJson()
        {
        }

        public ProcedureApplicationSectionJson(ProcedureApplicationSection applicationSection)
        {
            this.IsSelected = applicationSection.IsSelected;
            this.OrderNum = applicationSection.OrderNum;
            this.Section = applicationSection.Section;
            this.AdditionalSettings = new List<ProcedureApplicationSectionAdditionalSettingJson>();
        }

        public ApplicationSectionType Section { get; set; }

        public bool IsSelected { get; set; }

        public int OrderNum { get; set; }

        public IList<ProcedureApplicationSectionAdditionalSettingJson> AdditionalSettings { get; set; }
    }
}
