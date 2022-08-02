using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Комуникация към Договори")]
    public enum ContractCommunicationPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class ContractCommunicationPermission : ProgrammePermission
    {
        private ContractCommunicationPermission()
        {
        }

        public ContractCommunicationPermission(int programmeId, ContractCommunicationPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ContractCommunicationPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ContractCommunicationPermissions), base.PermissionString);
            }
        }
    }

    internal class ContractCommunicationPermissionMap : EntityTypeConfiguration<ContractCommunicationPermission>
    {
        public ContractCommunicationPermissionMap()
        {
        }
    }
}
