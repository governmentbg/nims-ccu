using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProjectRegistrationStatusEnumNomsRepository : IEnumNomsRepository<ProjectRegistrationStatus>
    {
        public ProjectRegistrationStatusEnumNomsRepository()
        {
        }

        public EnumNomVO<ProjectRegistrationStatus> GetNom(ProjectRegistrationStatus e)
        {
            return new EnumNomVO<ProjectRegistrationStatus>(e);
        }

        public IList<EnumNomVO<ProjectRegistrationStatus>> GetNoms(string term)
        {
            return Enum.GetValues(typeof(ProjectRegistrationStatus))
                .Cast<ProjectRegistrationStatus>()
                .Select(e => new EnumNomVO<ProjectRegistrationStatus>(e))
                .Where(p => p.NomValueId != ProjectRegistrationStatus.Withdrawn)
                .ToList();
        }
    }
}
