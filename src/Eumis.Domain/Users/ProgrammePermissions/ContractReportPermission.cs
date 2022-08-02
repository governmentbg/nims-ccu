using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.ContractReportPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ContractReportPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ContractReportPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,

        [Description(Description = nameof(DomainEnumTexts.ContractReportPermissions_CanCheck), ResourceType = typeof(DomainEnumTexts))]
        CanCheck,
    }

    internal class ContractReportPermission : ProgrammePermission
    {
        private ContractReportPermission()
        {
        }

        public ContractReportPermission(int programmeId, ContractReportPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ContractReportPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ContractReportPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ContractReportPermissionMap : EntityTypeConfiguration<ContractReportPermission>
    {
        public ContractReportPermissionMap()
        {
        }
    }
}
