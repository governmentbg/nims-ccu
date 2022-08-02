using Eumis.Common.Db;
using Eumis.Domain.Projects;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Eumis.Data.Projects.Repositories
{
    internal class ProjectCommunicationFilesRepository : AggregateRepository<ProjectCommunicationFile>, IProjectCommunicationFilesRepository
    {
        public ProjectCommunicationFilesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        protected override Expression<Func<ProjectCommunicationFile, object>>[] Includes
        {
            get
            {
                return new Expression<Func<ProjectCommunicationFile, object>>[]
                {
                    p => p.ProjectCommunicationFileSignatures,
                };
            }
        }

        public ProjectCommunicationFile FindByProjectCommunicationAnswerId(int answerId)
        {
            return this.Set()
                .Where(s => s.ProjectCommunicationAnswerId == answerId)
                .SingleOrDefault();
        }
    }
}
