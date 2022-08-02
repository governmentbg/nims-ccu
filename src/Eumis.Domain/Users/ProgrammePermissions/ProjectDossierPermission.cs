using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.ProjectDossierPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ProjectDossierPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectDossierPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,
    }

    internal class ProjectDossierPermission : ProgrammePermission
    {
        private ProjectDossierPermission()
        {
        }

        public ProjectDossierPermission(int programmeId, ProjectDossierPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ProjectDossierPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ProjectDossierPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ProjectDossierPermissionMap : EntityTypeConfiguration<ProjectDossierPermission>
    {
        public ProjectDossierPermissionMap()
        {
        }
    }
}
