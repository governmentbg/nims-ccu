using System.Linq;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportProcedureNomsRepository : EntityNomsRepository<Procedure, EntityNomVO>, IContractReportProcedureNomsRepository
    {
        private IAccessContext accessContext;

        public ContractReportProcedureNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
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
            var programmeIds = this.unitOfWork.CreateProgrammeIdsByPermissionQuery(this.accessContext.UserId, ContractReportPermissions.CanWrite);

            var externalUserContractProcedures = from cu in this.unitOfWork.DbContext.Set<ContractUser>().Where(x => x.UserId == this.accessContext.UserId)
                                                 join c in this.unitOfWork.DbContext.Set<Contract>() on cu.ContractId equals c.ContractId
                                                 join p in this.unitOfWork.DbContext.Set<Procedure>() on c.ProcedureId equals p.ProcedureId
                                                 select p;

            return (from p in this.unitOfWork.DbContext.Set<Procedure>()
                    join ps in this.unitOfWork.DbContext.Set<ProcedureShare>() on p.ProcedureId equals ps.ProcedureId
                    where programmeIds.Contains(ps.ProgrammeId)
                    select p)
                    .Union(externalUserContractProcedures)
                    .Distinct();
        }
    }
}
