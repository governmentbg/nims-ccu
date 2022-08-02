using System.Data.Entity.ModelConfiguration;
using System;

namespace Eumis.Public.Domain.Entities.Umis.Users
{
    public abstract class ProgrammePermission : UserPermission
    {
        internal static ProgrammePermission CreateProgrammePermissionEntity(int programmeId, Type permissionType, object permission)
        {
            Type entityType = enumTypeToEntityType[permissionType];

            return (ProgrammePermission)Activator.CreateInstance(entityType, programmeId, permission);
        }

        protected ProgrammePermission()
        {
        }

        protected ProgrammePermission(string permission, int programmeId)
            : base(permission)
        {
            this.ProgrammeId = programmeId;
        }

        public int ProgrammeId { get; set; }

        internal abstract Type PermissionType { get; }

        internal abstract object Permission { get; }
    }

    public class ProgrammePermissionMap : EntityTypeConfiguration<ProgrammePermission>
    {
        public ProgrammePermissionMap()
        {
            // Properties
            this.Property(t => t.ProgrammeId)
                .IsRequired();

            // Table & Column Mappings
            this.Property(t => t.ProgrammeId).HasColumnName("ProgrammeId");

            // Ignore
            this.Ignore(t => t.PermissionType);
            this.Ignore(t => t.Permission);
        }
    }
}
