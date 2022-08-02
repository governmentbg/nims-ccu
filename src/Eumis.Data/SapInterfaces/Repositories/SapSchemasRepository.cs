using System.Linq;
using Eumis.Common.Db;
using Eumis.Domain.SapInterfaces;

namespace Eumis.Data.SapInterfaces.Repositories
{
    internal class SapSchemasRepository : AggregateRepository<SapSchema>, ISapSchemasRepository
    {
        public SapSchemasRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public SapSchema GetActiveSchema(SapFileType type)
        {
            return this.Set().Single(ss => ss.IsActive && ss.Type == type);
        }
    }
}
