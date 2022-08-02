using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Core.Nomenclatures;
using Eumis.Data.Linq;
using Eumis.Domain.Projects;

namespace Eumis.Data.NonAggregates.Repositories.Noms
{
    internal class ProjectVersionXmlFileNomsRepository : EntityNomsRepository<ProjectVersionXmlFile, EntityNomVO>, IProjectVersionXmlFileNomsRepository
    {
        public ProjectVersionXmlFileNomsRepository(IUnitOfWork unitOfWork)
            : base(
                unitOfWork,
                t => t.FileId,
                t => t.Name,
                t => new EntityNomVO
                {
                    NomValueId = t.FileId,
                    Name = t.Name,
                })
        {
        }

        public IList<EntityNomVO> GetNomsForProjectVersion(
            int projectVersionXmlId,
            string term,
            int offset = 0,
            int? limit = null)
        {
            return (from p in this.unitOfWork.DbContext.Set<ProjectVersionXmlFile>().Where(x => x.ProjectVersionXmlId == projectVersionXmlId)
                    select p)
                .OrderBy(this.nameSelector)
                .WithOffsetAndLimit(offset, limit)
                .Select(this.voSelector)
                .ToList();
        }
    }
}
