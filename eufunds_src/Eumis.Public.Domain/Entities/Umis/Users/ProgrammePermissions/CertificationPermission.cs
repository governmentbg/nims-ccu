using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Сертификация")]
    public enum CertificationPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class CertificationPermission : ProgrammePermission
    {
        private CertificationPermission()
        {
        }

        public CertificationPermission(int programmeId, CertificationPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(CertificationPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(CertificationPermissions), base.PermissionString);
            }
        }
    }

    internal class CertificationPermissionMap : EntityTypeConfiguration<CertificationPermission>
    {
        public CertificationPermissionMap()
        {
        }
    }
}
