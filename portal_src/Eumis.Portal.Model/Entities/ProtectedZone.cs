using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class ProtectedZone
    {
        public int ProtectedZoneId { get; set; }
        public int CountryId { get; set; }
        public string NutsCode { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string FullPathName { get; set; }
        public string FullPathNameAlt { get; set; }
        public string FullPath { get; set; }
        public virtual Country Country { get; set; }
    }
}
