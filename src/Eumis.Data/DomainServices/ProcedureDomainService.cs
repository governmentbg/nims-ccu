using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Services;

namespace Eumis.Data.DomainServices
{
    internal class ProcedureDomainService : IProcedureDomainService
    {
        private IProceduresRepository proceduresRepository;

        public ProcedureDomainService(IProceduresRepository proceduresRepository)
        {
            this.proceduresRepository = proceduresRepository;
        }

        public int GetProcedureIdByCode(string procedureCode)
        {
            return this.proceduresRepository.GetProcedureIdByProcedureCode(procedureCode);
        }
    }
}
