using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.Registrations
{
    public class RegistrationsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RegistrationMap());
            modelBuilder.Configurations.Add(new RegProjectXmlMap());
            modelBuilder.Configurations.Add(new RegProjectXmlFileMap());
            modelBuilder.Configurations.Add(new RegOfferXmlMap());
            modelBuilder.Configurations.Add(new RegOfferXmlFileMap());
        }
    }
}
