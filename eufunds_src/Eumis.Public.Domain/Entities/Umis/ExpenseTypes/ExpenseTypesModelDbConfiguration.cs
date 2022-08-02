using Eumis.Public.Common.Db;
using System.Data.Entity;

namespace Eumis.Public.Domain.Entities.Umis.ExpenseTypes
{
    public class ExpenseTypesModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ExpenseTypeMap());
            modelBuilder.Configurations.Add(new ExpenseSubTypeMap());
        }
    }
}
