using Eumis.Domain.Projects;
using System.Collections.Generic;

namespace Eumis.ApplicationServices.Services.ProjectVersionXml
{
    public interface IProjectVersionXmlService
    {
        IList<string> CanCreateProjectVersion(int projectId, int evalSessionId);

        bool CanCreateProjectVersionFromRegData(int projectId, int evalSessionId);

        Domain.Projects.ProjectVersionXml CreateProjectVersion(int projectId, int userId, string createNote, string createNoteAlt);

        Domain.Projects.ProjectVersionXml CreateProjectVersionFromProjectData(int projectId, int userId);

        Domain.Projects.ProjectVersionXml CreateProjectServiceVersion(Project project, int companyId, int userId);

        void CreateProjectVersionFromCommunication(Eumis.Domain.Projects.ProjectCommunication communication, string answerXml);

        bool CanUpdateProjectVersionData(int versionId, int evalSessionId);

        bool CanDeleteProjectVersion(int versionId, int evalSessionId);

        byte[] GetProjectAttachedDocumentsZip(int projectVersionId);
    }
}
