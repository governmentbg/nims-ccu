using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Linq;

namespace Eumis.Data.ContractReportCertAuthorityFinancialCorrections.Repositories
{
    internal class ContractReportCertAuthorityFinancialCorrectionProcedureNomsRepository : EntityNomsRepository<Procedure, EntityNomVO>, IContractReportCertAuthorityFinancialCorrectionProcedureNomsRepository
    {
        private IAccessContext accessContext;

        public ContractReportCertAuthorityFinancialCorrectionProcedureNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(
                unitOfWork,
                t => t.ProcedureId,
                t => t.Code + " " + t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ProcedureId,
                    Name = t.Code + " " + t.Name,
                })
        {
            this.accessContext = accessContext;
        }

        protected override System.Linq.IQueryable<Procedure> GetQuery()
        {
            var programmeIds = System.Array.Empty<int>();
            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where programmeIds.Contains(ps.ProgrammeId)
                    select p).Distinct();
        }
    }
}
