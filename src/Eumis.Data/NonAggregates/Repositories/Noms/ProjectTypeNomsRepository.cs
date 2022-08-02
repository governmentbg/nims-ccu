using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Projects;
using System;
using System.Linq;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProjectTypeNomsRepository : EntityNomsRepository<ProjectType, EntityNomVO>, IProjectTypeNomsRepository
    {
        public ProjectTypeNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.ProjectTypeId,
                t => t.Name,
                t => t.NameAlt,
                t => new EntityNomVO
                {
                    NomValueId = t.ProjectTypeId,
                    Name = t.Name,
                    NameAlt = t.NameAlt,
                })
        {
        }

        public EntityNomVO GetNomByAlias(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new ArgumentNullException(nameof(alias));
            }

            var predicate =
                PredicateBuilder.True<ProjectType>()
                .AndPropertyEquals(t => t.Alias, alias);

            return this.unitOfWork.DbContext.Set<ProjectType>()
                .Where(predicate)
                .Select(t => new EntityNomVO
                {
                    NomValueId = t.ProjectTypeId,
                    Name = t.Name,
                    NameAlt = t.NameAlt,
                })
                .Single();
        }
    }
}
