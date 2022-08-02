using System;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Users
{
    public abstract class ProgrammePermission : UserPermission
    {
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

        internal static ProgrammePermission CreateProgrammePermissionEntity(int programmeId, Type permissionType, object permission)
        {
            Type entityType = EnumTypeToEntityType[permissionType];

            return (ProgrammePermission)Activator.CreateInstance(entityType, programmeId, permission);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
