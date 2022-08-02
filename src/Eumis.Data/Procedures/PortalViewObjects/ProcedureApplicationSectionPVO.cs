using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using Eumis.Domain.Procedures.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureApplicationSectionPVO
    {
        public ProcedureApplicationSectionPVO(ProcedureApplicationSectionJson applicationSection)
        {
            this.IsSelected = applicationSection.IsSelected;
            this.OrderNum = applicationSection.OrderNum;

            this.Section = new EnumPVO<ApplicationSectionType>()
            {
                Description = applicationSection.Section,
                Value = applicationSection.Section,
            };

            if (applicationSection.AdditionalSettings != null)
            {
                this.AdditionalSettings = applicationSection
                    .AdditionalSettings
                    .Select(i => new ProcedureApplicationSectionAdditionalSettingPVO(i))
                    .ToList();
            }
            else
            {
                this.AdditionalSettings = new List<ProcedureApplicationSectionAdditionalSettingPVO>();
            }
        }

        public EnumPVO<ApplicationSectionType> Section { get; set; }

        public bool IsSelected { get; set; }

        public int OrderNum { get; set; }

        public IList<ProcedureApplicationSectionAdditionalSettingPVO> AdditionalSettings { get; set; }
    }
}
