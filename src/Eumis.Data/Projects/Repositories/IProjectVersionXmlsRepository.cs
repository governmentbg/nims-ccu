using System;
using System.Collections.Generic;
using Eumis.Data.Projects.ViewObjects;
using Eumis.Domain.Projects;
using Eumis.Domain.Projects.ViewObjects;

namespace Eumis.Data.Projects.Repositories
{
    public interface IProjectVersionXmlsRepository : IAggregateRepository<ProjectVersionXml>
    {
        IList<ProjectVersionVO> GetProjectVersions(int projectId, bool finalizedOnly = false);

        IList<ProjectVersionXml> GetNonArchivedProjectVersions(int projectId);

        ProjectVersionXml GetActualProjectVersion(int projectId);

        bool HasProjectWithoutActualProjectVersion(int[] projectIds);

        ProjectVersionXml GetLastProjectVersion(int projectId);

        ProjectVersionXml GetLastArchivedProjectVersion(int projectId);

        ProjectVersionXml Find(Guid gid);

        ProjectVersionXml FindForUpdate(Guid gid, byte[] version);

        ProjectVersionXmlStatus? GetLastVersionStatus(int projectId);

        int GetProjectId(int versionId);

        int GetProjectVersionId(Guid gid);

        int GetNextOrderNum(int projectId);

        IList<ProjectGrandAmountsVO> GetProgrammeBudgetGrandAmountForActualProjectVersions(int[] projectsIds);

        IList<ProcedureGrandAmountsVO> GetProgrammeBudgetSpentAmount(int[] projectIds);

        int? GetActualProjectVersionId(int projectId);

        int? GetProjectVersionXmlFileId(int projectVersionXmlId, Guid typeGid);

        Guid? GetProjectVersionDeclarationGid(int projectVersionXmlId, Guid declarationGid);
    }
}
