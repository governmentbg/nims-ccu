using System.Data.Entity;
namespace Eumis.Common.Data
{
    public interface IDbConfiguration
    {
        void AddConfiguration(DbModelBuilder modelBuilder);
    }
}
