using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class Municipality
    {
        public Municipality()
        {
            this.Settlements = new List<Settlement>();
        }

        public int MunicipalityId { get; set; }
        public int DistrictId { get; set; }
        public string LauCode { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string NameAlt { get; set; }
        public string FullPathName { get; set; }
        public string FullPathNameAlt { get; set; }
        public string FullPath { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<Settlement> Settlements { get; set; }
    }
}
