using Eumis.Domain.Procedures.Json;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureApplicationSectionAdditionalSettingPVO
    {
        public ProcedureApplicationSectionAdditionalSettingPVO(ProcedureApplicationSectionAdditionalSettingJson applicationSectionAdditionalSetting)
        {
            this.Name = applicationSectionAdditionalSetting.Name;
            this.IsSelected = applicationSectionAdditionalSetting.IsSelected;
        }

        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
