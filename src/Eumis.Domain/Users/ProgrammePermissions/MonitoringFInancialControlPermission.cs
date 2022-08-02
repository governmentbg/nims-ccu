using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.MonitoringFinancialControlPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum MonitoringFinancialControlPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.MonitoringFinancialControlPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.MonitoringFinancialControlPermissions_CanWriteFinancial), ResourceType = typeof(DomainEnumTexts))]
        CanWriteFinancial,

        [Description(Description = nameof(DomainEnumTexts.MonitoringFinancialControlPermissions_CanWriteTechnical), ResourceType = typeof(DomainEnumTexts))]
        CanWriteTechnical,
    }

    internal class MonitoringFinancialControlPermission : ProgrammePermission
    {
        private MonitoringFinancialControlPermission()
        {
        }

        public MonitoringFinancialControlPermission(int programmeId, MonitoringFinancialControlPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(MonitoringFinancialControlPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(MonitoringFinancialControlPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class MonitoringFinancialControlPermissionMap : EntityTypeConfiguration<MonitoringFinancialControlPermission>
    {
        public MonitoringFinancialControlPermissionMap()
        {
        }
    }
}
