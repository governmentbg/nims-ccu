using Eumis.Domain.SapInterfaces;

namespace Eumis.Data.SapInterfaces.Repositories
{
    public interface ISapSchemasRepository : IAggregateRepository<SapSchema>
    {
        SapSchema GetActiveSchema(SapFileType type);
    }
}
