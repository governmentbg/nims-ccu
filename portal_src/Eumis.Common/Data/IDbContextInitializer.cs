using System.Data.Entity;
namespace Eumis.Common.Data
{
    public interface IDbContextInitializer
    {
        void InitializeContext(DbContext context);
    }
}
