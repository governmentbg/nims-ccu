using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.EvalSessionPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum EvalSessionPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.EvalSessionPermissions_CanAdministrate), ResourceType = typeof(DomainEnumTexts))]
        CanAdministrate,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionPermissions_CanEvaluate), ResourceType = typeof(DomainEnumTexts))]
        CanEvaluate,

        [Description(Description = nameof(DomainEnumTexts.EvalSessionPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,
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
                return Enum.Parse(typeof(EvalSessionPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class EvalSessionPermissionMap : EntityTypeConfiguration<EvalSessionPermission>
    {
        public EvalSessionPermissionMap()
        {
        }
    }
}
