using System.Data.Entity;
using Eumis.Common.Db;

namespace Eumis.Domain.ExpenseTypes
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
