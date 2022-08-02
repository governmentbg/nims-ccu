using Eumis.Common.Db;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectFilesRepository : AggregateRepository<ProjectFile>, IProjectFilesRepository
    {
        public ProjectFilesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProjectFile, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProjectFile, object>>[]
                {
                    p => p.ProjectFileSignatures,
                };
            }
        }

        public ProjectFile FindByProjectVersionXmlId(int projectVersionXmlId)
        {
            return this.Set()
                .Where(s => s.ProjectVersionXmlId == projectVersionXmlId)
                .SingleOrDefault();
        }

        public List<byte[]> GetFirstProjectFileSignatures(int projectId)
        {
            var group =
                (from pfs in this.unitOfWork.DbContext.Set<ProjectFileSignature>()
                 join pf in this.Set() on pfs.ProjectFileId equals pf.ProjectFileId
                 join pv in this.unitOfWork.DbContext.Set<ProjectVersionXml>() on pf.ProjectVersionXmlId equals pv.ProjectVersionXmlId
                 where pv.ProjectId == projectId
                 group pfs
                 by new
                 {
                     pv.ProjectVersionXmlId,
                     pv.OrderNum,
                 }
                 into g
                 orderby g.Key.OrderNum
                 select g).First();

            return group.Select(g => g.Signature).ToList();
        }
    }
}
