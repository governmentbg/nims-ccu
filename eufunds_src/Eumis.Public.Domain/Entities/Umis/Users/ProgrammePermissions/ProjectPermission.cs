using System.Data.Entity.ModelConfiguration;
using System;
using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Проектни предложения")]
    public enum ProjectPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Регистриране")]
        CanRegister,

        [Description("Оттегляне")]
        CanWithdraw
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
                return Enum.Parse(typeof(ProjectPermissions), base.PermissionString);
            }
        }
    }

    internal class ProjectPermissionMap : EntityTypeConfiguration<ProjectPermission>
    {
        public ProjectPermissionMap()
        {
        }
    }
}
