using System;
using Eumis.Common.Json;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users.ProgrammePermissions
{
    [Description(Description = nameof(DomainEnumTexts.IndicatorPermissions), ResourceType = typeof(DomainEnumTexts))]
    public enum IndicatorPermissions
    {
        [Description(Description = nameof(DomainEnumTexts.IndicatorPermissions_CanRead), ResourceType = typeof(DomainEnumTexts))]
        CanRead,

        [Description(Description = nameof(DomainEnumTexts.IndicatorPermissions_CanWrite), ResourceType = typeof(DomainEnumTexts))]
        CanWrite,
    }

    internal class IndicatorPermission : ProgrammePermission
    {
        private IndicatorPermission()
        {
        }

        public IndicatorPermission(int programmeId, IndicatorPermissions permission)
            : base(permission.ToString(), programmeId)
        {
        }

        internal override Type PermissionType
        {
            get
            {
                return typeof(IndicatorPermissions);
            }
        }

        internal override object Permission
        {
            get
            {
                return Enum.Parse(typeof(IndicatorPermissions), this.PermissionString);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    internal class IndicatorPermissionMap : EntityTypeConfiguration<IndicatorPermission>
    {
        public IndicatorPermissionMap()
        {
        }
    }
}
