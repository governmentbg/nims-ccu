using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.Automatizations
{
    public class AutomatizationsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProjectContractAutomatizationMap());
            modelBuilder.Configurations.Add(new ContractReportAcceptedAutomatizationMap());
        }
    }
}
