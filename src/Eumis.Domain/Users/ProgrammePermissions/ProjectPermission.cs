using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.ProjectPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ProjectPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ProjectPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.ProjectPermissions_CanRegister), ResourceType = typeof(DomainEnumTexts))]
        CanRegister,

        [Description(Description = nameof(DomainEnumTexts.ProjectPermissions_CanWithdraw), ResourceType = typeof(DomainEnumTexts))]
        CanWithdraw,
    }

    internal class ProjectPermission : ProgrammePermission
    {
        private ProjectPermission()
        {
        }

        public ProjectPermission(int programmeId, ProjectPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ProjectPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ProjectPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ProjectPermissionMap : EntityTypeConfiguration<ProjectPermission>
    {
        public ProjectPermissionMap()
        {
        }
    }
}
