using Eumis.Common.Db;
using Eumis.Data.Core;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProcedureSpecFieldMaxLengthNomsRepository : Repository, IProcedureSpecFieldMaxLengthNomsRepository
    {
        public ProcedureSpecFieldMaxLengthNomsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public EnumNomVO<ProcedureSpecFieldMaxLength> GetNom(ProcedureSpecFieldMaxLength e)
        {
            return new EnumNomVO<ProcedureSpecFieldMaxLength>(e);
        }

        public IList<EnumNomVO<ProcedureSpecFieldMaxLength>> GetNoms(string term)
        {
            return Enum.GetValues(typeof(ProcedureSpecFieldMaxLength))
                       .Cast<ProcedureSpecFieldMaxLength>()
                       .Where(sf => sf != ProcedureSpecFieldMaxLength.IBAN)
                       .Concat(new[] { ProcedureSpecFieldMaxLength.IBAN })
                       .Select(e => new EnumNomVO<ProcedureSpecFieldMaxLength>(e))
                       .ToList();
        }
    }
}
