using System;
using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class CompanyType
    {
        public CompanyType()
        {
            this.CompanyLegalTypes = new List<CompanyLegalType>();
        }

        public int CompanyTypeId { get; set; }
        public System.Guid Gid { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public decimal Order { get; set; }
        public string Alias { get; set; }
        public virtual ICollection<CompanyLegalType> CompanyLegalTypes { get; set; }
    }
}
