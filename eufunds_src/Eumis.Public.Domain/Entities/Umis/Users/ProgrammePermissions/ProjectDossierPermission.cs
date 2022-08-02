using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Проектно досие")]
    public enum ProjectDossierPermissions
    {
        [Description("Четене")]
        CanRead
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
                return Enum.Parse(typeof(ProjectDossierPermissions), base.PermissionString);
            }
        }
    }

    internal class ProjectDossierPermissionMap : EntityTypeConfiguration<ProjectDossierPermission>
    {
        public ProjectDossierPermissionMap()
        {
        }
    }
}
