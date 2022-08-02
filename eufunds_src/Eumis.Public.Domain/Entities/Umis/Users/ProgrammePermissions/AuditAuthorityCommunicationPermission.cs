using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Комуникация одитен орган")]
    public enum AuditAuthorityCommunicationPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class AuditAuthorityCommunicationPermission : ProgrammePermission
    {
        private AuditAuthorityCommunicationPermission()
        {
        }

        public AuditAuthorityCommunicationPermission(int programmeId, AuditAuthorityCommunicationPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(AuditAuthorityCommunicationPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(AuditAuthorityCommunicationPermissions), base.PermissionString);
            }
        }
    }

    internal class AuditAuthorityCommunicationPermissionMap : EntityTypeConfiguration<AuditAuthorityCommunicationPermission>
    {
        public AuditAuthorityCommunicationPermissionMap()
        {
        }
    }
}
