using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.ProcedurePermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum ProcedurePermissions
    {
        [Description(Description = nameof(DomainEnumTexts.ProcedurePermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.ProcedurePermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,

        [Description(Description = nameof(DomainEnumTexts.ProcedurePermissions_CanCheck), ResourceType = typeof(DomainEnumTexts))]
        CanCheck,

        [Description(Description = nameof(DomainEnumTexts.ProcedurePermissions_CanDelete), ResourceType = typeof(DomainEnumTexts))]
        CanDelete,
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
                return Enum.Parse(typeof(ProcedurePermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class ProcedurePermissionMap : EntityTypeConfiguration<ProcedurePermission>
    {
        public ProcedurePermissionMap()
        {
        }
    }
}
