using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.CommonPermissions
{
    [Description(Description = nameof(DomainEnumTexts.MonitoringPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum MonitoringPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.MonitoringPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,
    }

    internal class MonitoringPermission : CommonPermission
    {
        private MonitoringPermission()
        {
        }

        public MonitoringPermission(MonitoringPermissions permission)
            : base(permission.ToString())
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(MonitoringPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(MonitoringPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class MonitoringPermissionMap : EntityTypeConfiguration<MonitoringPermission>
    {
        public MonitoringPermissionMap()
        {
        }
    }
}
