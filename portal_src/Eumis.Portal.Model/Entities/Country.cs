using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class Country
    {
        public Country()
        {
            this.Nuts1s = new List<Nuts1s>();
            this.ProtectedZones = new List<ProtectedZone>();
        }

        public int CountryId { get; set; }
        public string NutsCode { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public virtual ICollection<Nuts1s> Nuts1s { get; set; }
        public virtual ICollection<ProtectedZone> ProtectedZones { get; set; }
    }
}
