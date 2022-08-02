using System.Linq;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Data.ContractReportTechnicalCorrections.Repositories
{
    internal class ContractReportTechnicalCorrectionProcedureNomsRepository : EntityNomsRepository<Procedure, EntityNomVO>, IContractReportTechnicalCorrectionProcedureNomsRepository
    {
        private IAccessContext accessContext;

        public ContractReportTechnicalCorrectionProcedureNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
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
            var programmeIds = this.unitOfWork.CreateProgrammeIdsByPermissionQuery(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteTechnical);

            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where programmeIds.Contains(ps.ProgrammeId)
                    select p).Distinct();
        }
    }
}
