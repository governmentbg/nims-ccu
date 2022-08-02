using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Комуникация сертифициращ орган")]
    public enum CertAuthorityCommunicationPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class CertAuthorityCommunicationPermission : ProgrammePermission
    {
        private CertAuthorityCommunicationPermission()
        {
        }

        public CertAuthorityCommunicationPermission(int programmeId, CertAuthorityCommunicationPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(CertAuthorityCommunicationPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(CertAuthorityCommunicationPermissions), base.PermissionString);
            }
        }
    }

    internal class CertAuthorityCommunicationPermissionMap : EntityTypeConfiguration<CertAuthorityCommunicationPermission>
    {
        public CertAuthorityCommunicationPermissionMap()
        {
        }
    }
}
