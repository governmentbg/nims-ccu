using System.Data.Entity;

namespace Eumis.Common.Db
{
    public interface IDbContextInitializer
    {
        void InitializeContext(DbContext context);
    }
}
