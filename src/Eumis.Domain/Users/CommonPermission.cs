using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users
{
    public abstract class CommonPermission : UserPermission
    {
        protected CommonPermission()
        {
        }

        protected CommonPermission(string permission)
            : base(permission)
        {
        }

        internal abstract Type PermissionType { get; }

        internal abstract object Permission { get; }

        internal static CommonPermission CreateCommonPermissionEntity(Type permissionType, object permission)
        {
            Type entityType = EnumTypeToEntityType[permissionType];

            return (CommonPermission)Activator.CreateInstance(entityType, permission);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class CommonPermissionMap : EntityTypeConfiguration<CommonPermission>
    {
        public CommonPermissionMap()
        {
            // Ignore
            this.Ignore(t => t.PermissionType);
            this.Ignore(t => t.Permission);
        }
    }
}
