using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Registrations
{
    public class RegistrationsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RegistrationMap());
            modelBuilder.Configurations.Add(new RegProjectXmlMap());
            modelBuilder.Configurations.Add(new RegOfferXmlMap());
        }
    }
}
