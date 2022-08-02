using System;
using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.NonAggregates.ViewObjects
{
    public class CompanyLegalTypeGidNomVO : EntityGidNomVO
    {
        public string Alias { get; set; }

        public Guid CompanyTypeGid { get; set; }
    }
}
