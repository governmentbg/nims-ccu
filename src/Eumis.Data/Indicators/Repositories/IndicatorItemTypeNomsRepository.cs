using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Indicators.Repositories
{
    internal class IndicatorItemTypeNomsRepository : EntityNomsRepository<IndicatorItemType, EntityNomVO>
    {
        public IndicatorItemTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                q => q.IndicatorItemTypeId,
                q => q.Name,
                q => q.NameAlt,
                q => new EntityNomVO
                {
                    NomValueId = q.IndicatorItemTypeId,
                    Name = q.Name,
                    NameAlt = q.NameAlt,
                })
        {
        }
    }
}
