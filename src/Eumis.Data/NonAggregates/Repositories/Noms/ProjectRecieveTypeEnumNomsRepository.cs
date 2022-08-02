using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProjectRecieveTypeEnumNomsRepository : IEnumNomsRepository<ProjectRecieveType>
    {
        public ProjectRecieveTypeEnumNomsRepository()
        {
        }

        public EnumNomVO<ProjectRecieveType> GetNom(ProjectRecieveType e)
        {
            return new EnumNomVO<ProjectRecieveType>(e);
        }

        public IList<EnumNomVO<ProjectRecieveType>> GetNoms(string term)
        {
            return Enum.GetValues(typeof(ProjectRecieveType))
                .Cast<ProjectRecieveType>()
                .Select(e => new EnumNomVO<ProjectRecieveType>(e))
                .Where(p => p.NomValueId != ProjectRecieveType.Electronic)
                .ToList();
        }
    }
}
