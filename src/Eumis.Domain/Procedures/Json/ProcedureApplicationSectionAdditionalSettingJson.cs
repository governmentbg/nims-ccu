using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures.Json
{
    public class ProcedureApplicationSectionAdditionalSettingJson
    {
        public ProcedureApplicationSectionAdditionalSettingJson()
        {
        }

        public ProcedureApplicationSectionAdditionalSettingJson(
            string name,
            bool isSelected)
        {
            this.Name = name;
            this.IsSelected = isSelected;
        }

        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
