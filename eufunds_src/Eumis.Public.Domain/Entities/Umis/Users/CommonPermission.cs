using System.Data.Entity.ModelConfiguration;
using System;

namespace Eumis.Public.Domain.Entities.Umis.Users
{
    public abstract class CommonPermission : UserPermission
    {
        internal static CommonPermission CreateCommonPermissionEntity(Type permissionType, object permission)
        {
            Type entityType = enumTypeToEntityType[permissionType];

            return (CommonPermission)Activator.CreateInstance(entityType, permission);
        }

        protected CommonPermission()
        {
        }

        protected CommonPermission(string permission)
            : base(permission)
        {
        }

        internal abstract Type PermissionType { get; }

        internal abstract object Permission { get; }
    }

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
