using System.Data.Entity;
using Eumis.Common.Data;
using Eumis.Portal.Model.Entities;
using Eumis.Portal.Model.Entities.Mapping;

namespace Eumis.Portal.Model
{
    public class EumisDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlobContentMap());
            modelBuilder.Configurations.Add(new BlobMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new GParamMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new LoginCertificateMap());
            modelBuilder.Configurations.Add(new MunicipalityMap());
            modelBuilder.Configurations.Add(new ProtectedZoneMap());
            modelBuilder.Configurations.Add(new Nuts1sMap());
            modelBuilder.Configurations.Add(new Nuts2sMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SettlementMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new KidCodeMap());
            modelBuilder.Configurations.Add(new CompanyTypeMap());
            modelBuilder.Configurations.Add(new CompanyLegalTypeMap());
            modelBuilder.Configurations.Add(new CompanySizeTypeMap());
        } 
    }
}