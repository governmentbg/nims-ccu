using System.Data.Entity;

namespace Eumis.Public.Common.Db
{
    public interface IDbConfiguration
    {
        void AddConfiguration(DbModelBuilder modelBuilder);
    }
}
