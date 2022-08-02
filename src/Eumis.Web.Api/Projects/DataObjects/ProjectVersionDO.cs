using System;
using Eumis.Domain.Projects;
using System.Collections.Generic;
using System.Linq;
using Eumis.Web.Api.Core;

namespace Eumis.Web.Api.Projects.DataObjects
{
    public class ProjectVersionDO
    {
        public ProjectVersionDO()
        {
            this.Status = ProjectVersionXmlStatus.Draft;
        }

        public ProjectVersionDO(ProjectVersionXml projectVersion, ProjectFile projectVersionFile = null)
        {
            this.VersionId = projectVersion.ProjectVersionXmlId;
            this.XmlGid = projectVersion.Gid;
            this.Status = projectVersion.Status;
            this.CreateNote = projectVersion.CreateNote;
            this.CreateNoteAlt = projectVersion.CreateNoteAlt;
            this.Version = projectVersion.Version;
            this.ProjectFile = projectVersionFile != null ? new InternalFileDO(projectVersionFile.ProjectFileId, projectVersionFile.FileName) : null;
            this.ProjectFileSignatures = projectVersionFile != null ?
                projectVersionFile.ProjectFileSignatures.Select(p => new InternalFileDO(p.ProjectFileSignatureId, p.FileName)).ToList() :
                new List<InternalFileDO>();
        }

        public int? VersionId { get; set; }

        public Guid? XmlGid { get; set; }

        public ProjectVersionXmlStatus Status { get; set; }

        public string CreateNote { get; set; }

        public string CreateNoteAlt { get; set; }

        public InternalFileDO ProjectFile { get; set; }

        public IList<InternalFileDO> ProjectFileSignatures { get; set; }

        public byte[] Version { get; set; }
    }
}
