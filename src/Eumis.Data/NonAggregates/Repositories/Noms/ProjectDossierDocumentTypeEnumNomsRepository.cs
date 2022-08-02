using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProjectDossierDocumentTypeEnumNomsRepository : EnumNomsRepository<ProjectDossierDocumentType>, IProjectDossierDocumentTypeEnumNomsRepository
    {
        private IUnitOfWork unitOfWork;

        public ProjectDossierDocumentTypeEnumNomsRepository(IUnitOfWork unitOfWork)
            : base()
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<EnumNomVO<ProjectDossierDocumentType>> GetNoms(ProjectDossierDocumentType[] ids, string term)
        {
            var results = Enum.GetValues(typeof(ProjectDossierDocumentType))
                .Cast<ProjectDossierDocumentType>()
                .Select(e => new EnumNomVO<ProjectDossierDocumentType>(e));

            if (ids.Length != 0)
            {
                return results.Where(p => ids.Contains(p.NomValueId)).ToList();
            }
            else
            {
                return results.ToList();
            }
        }
    }
}
