using Eumis.Common.Db;
using System.Data.Entity;

namespace Eumis.Domain.ActionLogs
{
    public class ActionLogsModelDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ActionLogMap());
        }
    }
}
