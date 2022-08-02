using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;

namespace Eumis.Data.Contracts.Repositories
{
    public delegate IContractProcedureNomsRepository ContractProcedureNomsRepositoryFactory(int[] programmeIds);

    public interface IContractProcedureNomsRepository : IEntityNomsRepository<Procedure, EntityNomVO>
    {
    }
}
