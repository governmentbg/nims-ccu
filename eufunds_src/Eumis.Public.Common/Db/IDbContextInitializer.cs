using System.Data.Entity;

namespace Eumis.Public.Common.Db
{
    public interface IDbContextInitializer
    {
        void InitializeContext(DbContext context);
    }
}
