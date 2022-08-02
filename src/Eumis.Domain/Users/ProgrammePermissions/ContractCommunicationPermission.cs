using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.ContractCommunicationPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ContractCommunicationPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.ContractCommunicationPermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,
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
                return Enum.Parse(typeof(ContractCommunicationPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ContractCommunicationPermissionMap : EntityTypeConfiguration<ContractCommunicationPermission>
    {
        public ContractCommunicationPermissionMap()
        {
        }
    }
}
