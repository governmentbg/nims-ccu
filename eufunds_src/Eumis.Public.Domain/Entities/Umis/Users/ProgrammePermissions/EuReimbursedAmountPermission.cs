using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Възстановени от ЕК суми")]
    public enum EuReimbursedAmountPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class EuReimbursedAmountPermission : ProgrammePermission
    {
        private EuReimbursedAmountPermission()
        {
        }

        public EuReimbursedAmountPermission(int programmeId, EuReimbursedAmountPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(EuReimbursedAmountPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(EuReimbursedAmountPermissions), base.PermissionString);
            }
        }
    }

    internal class EuReimbursedAmountPermissionMap : EntityTypeConfiguration<EuReimbursedAmountPermission>
    {
        public EuReimbursedAmountPermissionMap()
        {
        }
    }
}
