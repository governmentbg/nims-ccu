using System.Data.Entity.ModelConfiguration;
using System;
using System.ComponentModel;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Процедури")]
    public enum ProcedurePermissions
    {
        [Description("Четене")]
        CanRead,

        [Description("Писане")]
        CanWrite,

        [Description("Проверяване")]
        CanCheck,

        [Description("Изтриване")]
        CanDelete
    }

    internal class ProcedurePermission : ProgrammePermission
    {
        private ProcedurePermission()
        {
        }

        public ProcedurePermission(int programmeId, ProcedurePermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(ProcedurePermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(ProcedurePermissions), base.PermissionString);
            }
        }
    }

    internal class ProcedurePermissionMap : EntityTypeConfiguration<ProcedurePermission>
    {
        public ProcedurePermissionMap()
        {
        }
    }
}
