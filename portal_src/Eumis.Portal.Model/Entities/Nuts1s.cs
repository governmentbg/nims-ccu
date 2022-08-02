using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class Nuts1s
    {
        public Nuts1s()
        {
            this.Nuts2s = new List<Nuts2s>();
        }

        public int Nuts1Id { get; set; }
        public int CountryId { get; set; }
        public string NutsCode { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string FullPathName { get; set; }
        public string FullPathNameAlt { get; set; }
        public string FullPath { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Nuts2s> Nuts2s { get; set; }
    }
}
