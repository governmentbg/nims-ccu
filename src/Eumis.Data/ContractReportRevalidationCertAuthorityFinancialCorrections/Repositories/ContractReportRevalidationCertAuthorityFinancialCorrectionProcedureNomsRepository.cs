using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Linq;

namespace Eumis.Data.ContractReportRevalidationCertAuthorityFinancialCorrections.Repositories
{
    internal class ContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsRepository : EntityNomsRepository<Procedure, EntityNomVO>, IContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsRepository
    {
        private IAccessContext accessContext;

        public ContractReportRevalidationCertAuthorityFinancialCorrectionProcedureNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
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
            var programmeIds = Array.Empty<int>();

            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where programmeIds.Contains(ps.ProgrammeId)
                    select p).Distinct();
        }
    }
}
