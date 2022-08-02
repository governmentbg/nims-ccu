using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.Companies
{
    public class CompaniesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyPersonMap());
            modelBuilder.Configurations.Add(new LocalActionGroupMunicipalityMap());
        }
    }
}
