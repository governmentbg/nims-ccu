using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class District
    {
        public District()
        {
            this.Municipalities = new List<Municipality>();
        }

        public int DistrictId { get; set; }
        public int Nuts2Id { get; set; }
        public string NutsCode { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string FullPathName { get; set; }
        public string FullPathNameAlt { get; set; }
        public string FullPath { get; set; }
        public virtual Nuts2s Nuts2s { get; set; }
        public virtual ICollection<Municipality> Municipalities { get; set; }
    }
}
