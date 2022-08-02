using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Пакет отчетни документи към Договори")]
    public enum ContractReportPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite,

        [Description("Проверяване")]
        CanCheck
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
                return Enum.Parse(typeof(ContractReportPermissions), base.PermissionString);
            }
        }
    }

    internal class ContractReportPermissionMap : EntityTypeConfiguration<ContractReportPermission>
    {
        public ContractReportPermissionMap()
        {
        }
    }
}
