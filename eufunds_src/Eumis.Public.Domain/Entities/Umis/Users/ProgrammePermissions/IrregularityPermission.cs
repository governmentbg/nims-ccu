using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Нередности")]
    public enum IrregularityPermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite
    }

    internal class IrregularityPermission : ProgrammePermission
    {
        private IrregularityPermission()
        {
        }

        public IrregularityPermission(int programmeId, IrregularityPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(IrregularityPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(IrregularityPermissions), base.PermissionString);
            }
        }
    }

    internal class IrregularityPermissionMap : EntityTypeConfiguration<IrregularityPermission>
    {
        public IrregularityPermissionMap()
        {
        }
    }
}
