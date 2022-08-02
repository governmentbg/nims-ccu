using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Одити")]
    public enum AuditPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class AuditPermission : ProgrammePermission
    {
        private AuditPermission()
        {
        }

        public AuditPermission(int programmeId, AuditPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(AuditPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(AuditPermissions), base.PermissionString);
            }
        }
    }

    internal class AuditPermissionMap : EntityTypeConfiguration<AuditPermission>
    {
        public AuditPermissionMap()
        {
        }
    }
}
