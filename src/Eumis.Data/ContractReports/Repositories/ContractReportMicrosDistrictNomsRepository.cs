using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts.ContractReportMicros;

namespace Eumis.Data.ContractReports.Repositories
{
    internal class ContractReportMicrosDistrictNomsRepository : EntityNomsRepository<ContractReportMicrosDistrict, EntityNomVO>, IContractReportMicrosDistrictNomsRepository
    {
        public ContractReportMicrosDistrictNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ContractReportMicrosDistrictId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.ContractReportMicrosDistrictId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetAllDistrictNoms()
        {
            return (from d in this.unitOfWork.DbContext.Set<ContractReportMicrosDistrict>()
                    select new EntityNomVO
                    {
                        NomValueId = d.ContractReportMicrosDistrictId,
                        Name = d.Name,
                    }).ToList();
        }
    }
}
