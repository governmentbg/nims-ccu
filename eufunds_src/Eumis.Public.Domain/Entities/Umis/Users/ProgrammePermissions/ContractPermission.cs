using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Договори")]
    public enum ContractPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite,

        [Description("Проверяване")]
        CanCheck
    }

    internal class ContractPermission : ProgrammePermission
    {
        private ContractPermission()
        {
        }

        public ContractPermission(int programmeId, ContractPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ContractPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ContractPermissions), base.PermissionString);
            }
        }
    }

    internal class ContractPermissionMap : EntityTypeConfiguration<ContractPermission>
    {
        public ContractPermissionMap()
        {
        }
    }
}
