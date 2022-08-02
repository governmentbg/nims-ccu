using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Eumis.Web.Api.NonAggregates.Controllers.Noms
{
    [RoutePrefix("api/nomenclatures/correctionTypesExtended")]
    public class CorrectionTypeExtendedNomsController : ApiController
    {
        public CorrectionTypeExtendedNomsController()
        {
        }

        [Route("{id}")]
        public EnumNomVO<CorrectionTypeExtended> GetNom(CorrectionTypeExtended id)
        {
            return new EnumNomVO<CorrectionTypeExtended>(id);
        }

        [Route("")]
        public IList<EnumNomVO<CorrectionTypeExtended>> GetNoms(int? contractId = null)
        {
            if (contractId.HasValue)
            {
                return Enum.GetValues(typeof(CorrectionTypeExtended))
                    .Cast<CorrectionTypeExtended>()
                    .Select(e => new EnumNomVO<CorrectionTypeExtended>(e))
                    .ToList();
            }
            else
            {
                return Enum.GetValues(typeof(CorrectionTypeExtended))
                    .Cast<CorrectionTypeExtended>()
                    .Select(e => new EnumNomVO<CorrectionTypeExtended>(e))
                    .Where(p => p.NomValueId == CorrectionTypeExtended.FlatFinancialCorrection)
                    .ToList();
            }
        }
    }
}
