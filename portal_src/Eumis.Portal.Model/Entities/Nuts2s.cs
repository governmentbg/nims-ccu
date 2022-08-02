using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class Nuts2s
    {
        public Nuts2s()
        {
            this.Districts = new List<District>();
        }

        public int Nuts2Id { get; set; }
        public int Nuts1Id { get; set; }
        public string NutsCode { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string FullPathName { get; set; }
        public string FullPathNameAlt { get; set; }
        public string FullPath { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual Nuts1s Nuts1s { get; set; }
    }
}
