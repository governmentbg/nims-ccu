using System;
using System.Collections.Generic;

namespace Eumis.Portal.Model.Entities
{
    public partial class CompanyLegalType
    {
        public int CompanyLegalTypeId { get; set; }
        public System.Guid Gid { get; set; }
        public int CompanyTypeId { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public decimal Order { get; set; }
        public string Alias { get; set; }
        public virtual CompanyType CompanyType { get; set; }
    }
}
