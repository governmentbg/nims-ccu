using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Сигнали за нередности")]
    public enum IrregularitySignalPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class IrregularitySignalPermission : ProgrammePermission
    {
        private IrregularitySignalPermission()
        {
        }

        public IrregularitySignalPermission(int programmeId, IrregularitySignalPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(IrregularitySignalPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(IrregularitySignalPermissions), base.PermissionString);
            }
        }
    }

    internal class IrregularitySignalPermissionMap : EntityTypeConfiguration<IrregularitySignalPermission>
    {
        public IrregularitySignalPermissionMap()
        {
        }
    }
}
