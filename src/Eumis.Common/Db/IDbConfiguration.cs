using System.Data.Entity;

namespace Eumis.Common.Db
{
    public interface IDbConfiguration
    {
        void AddConfiguration(DbModelBuilder modelBuilder);
    }
}
