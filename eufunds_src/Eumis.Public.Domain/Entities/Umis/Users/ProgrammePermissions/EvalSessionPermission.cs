using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.Users.ProgrammePermissions
{
    [Description("Оценителна сесия по процедура")]
    public enum EvalSessionPermissions
    {
        [Description("Администриране")]
        CanAdministrate,

        [Description("Оценяване")]
        CanEvaluate
    }

    internal class EvalSessionPermission : ProgrammePermission
    {
        private EvalSessionPermission()
        {
        }

        public EvalSessionPermission(int programmeId, EvalSessionPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(EvalSessionPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(EvalSessionPermissions), base.PermissionString);
            }
        }
    }

    internal class EvalSessionPermissionMap : EntityTypeConfiguration<EvalSessionPermission>
    {
        public EvalSessionPermissionMap()
        {
        }
    }
}
