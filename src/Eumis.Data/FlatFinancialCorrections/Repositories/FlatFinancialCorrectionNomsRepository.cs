using Eumis.Common.Auth;
using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Core.Permissions;
using Eumis.Data.Linq;
using Eumis.Domain.MonitoringFinancialControl.FlatFinancialCorrections;
using Eumis.Domain.Users.ProgrammePermissions;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.FlatFinancialCorrections.Repositories
{
    internal class FlatFinancialCorrectionNomsRepository : Repository, IFlatFinancialCorrectionNomsRepository
    {
        private IAccessContext accessContext;

        public FlatFinancialCorrectionNomsRepository(IUnitOfWork unitOfWork, IAccessContext accessContext)
            : base(unitOfWork)
        {
            this.accessContext = accessContext;
        }

        public EntityNomVO GetNom(int nomValueId)
        {
            return (from fc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>()
                    where fc.FlatFinancialCorrectionId == nomValueId
                    select new EntityNomVO
                    {
                        NomValueId = fc.FlatFinancialCorrectionId,
                        Name = "ФКСП " + fc.OrderNum,
                    })
                    .SingleOrDefault();
        }

        public IEnumerable<EntityNomVO> GetNoms(string term, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<FlatFinancialCorrection>()
                .AndStringContains(fc => "ФКСП " + fc.OrderNum, term);

            var programmeIds = this.unitOfWork.CreateProgrammeIdsByPermissionQuery(this.accessContext.UserId, MonitoringFinancialControlPermissions.CanWriteFinancial);

            return (from fc in this.unitOfWork.DbContext.Set<FlatFinancialCorrection>().Where(predicate)
                    where fc.Status == FlatFinancialCorrectionStatus.Actual && programmeIds.Contains(fc.ProgrammeId)
                    select new EntityNomVO
                    {
                        NomValueId = fc.FlatFinancialCorrectionId,
                        Name = "ФКСП " + fc.OrderNum,
                    })
                    .OrderBy(p => p.Name)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }
    }
}
