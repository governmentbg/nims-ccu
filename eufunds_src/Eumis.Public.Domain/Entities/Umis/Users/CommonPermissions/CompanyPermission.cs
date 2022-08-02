using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.CommonPermissions
{
    [Description("Кандидати")]
    public enum CompanyPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class CompanyPermission : CommonPermission
    {
        private CompanyPermission()
        {
        }

        public CompanyPermission(CompanyPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(CompanyPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(CompanyPermissions), base.PermissionString);
            }
        }
    }

    internal class CompanyPermissionMap : EntityTypeConfiguration<CompanyPermission>
    {
        public CompanyPermissionMap()
        {
        }
    }
}
