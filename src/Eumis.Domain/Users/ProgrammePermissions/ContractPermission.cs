using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.ContractPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ContractPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ContractPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.ContractPermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,

        [Description(Description = nameof(DomainEnumTexts.ContractPermissions_CanCheck), ResourceType = typeof(DomainEnumTexts))]
        CanCheck,
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
                return Enum.Parse(typeof(ContractPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ContractPermissionMap : EntityTypeConfiguration<ContractPermission>
    {
        public ContractPermissionMap()
        {
        }
    }
}
