using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Мониторинг и финансов контрол")]
    public enum MonitoringFinancialControlPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане по финансова част")]
        CanWriteFinancial,

        [Description("Писане по техническа част")]
        CanWriteTechnical
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
                return Enum.Parse(typeof(MonitoringFinancialControlPermissions), base.PermissionString);
            }
        }
    }

    internal class MonitoringFinancialControlPermissionMap : EntityTypeConfiguration<MonitoringFinancialControlPermission>
    {
        public MonitoringFinancialControlPermissionMap()
        {
        }
    }
}
