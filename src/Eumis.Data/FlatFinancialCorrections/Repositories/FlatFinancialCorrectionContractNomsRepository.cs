using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.Data.FlatFinancialCorrections.Repositories
{
    internal class FlatFinancialCorrectionContractNomsRepository : EntityNomsRepository<Contract, EntityNomVO>, IFlatFinancialCorrectionContractNomsRepository
    {
        private IAccessContext accessContext;

        public FlatFinancialCorrectionContractNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(
                unitOfWork,
                t => t.ContractId,
                t => t.RegNumber + " " + t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ContractId,
                    Name = t.RegNumber + " " + t.Name,
                })
        {
            this.accessContext = accessContext;
        }

        public override EntityNomVO GetNom(int nomValueId)
        {
            return (from c in this.unitOfWork.DbContext.Set<Contract>()
                    where c.ContractId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber + " " + c.Name,
                    }).SingleOrDefault();
        }

        public IList<EntityNomVO> GetContractNoms(int[] programmeIds, string term, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<Contract>()
                .AndStringContains(p => p.Name, term);

            return (from c in this.unitOfWork.DbContext.Set<Contract>().Where(predicate)
                    where programmeIds.Contains(c.ProgrammeId)
                    select new EntityNomVO
                    {
                        NomValueId = c.ContractId,
                        Name = c.RegNumber + " " + c.Name,
                    })
                    .Distinct()
                    .OrderBy(t => t.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public override IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var programmeIds = this.unitOfWork.CreateProgrammeIdsByPermissionQuery(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial).ToArray();

            return this.GetContractNoms(programmeIds, term, offset, limit);
        }
    }
}
