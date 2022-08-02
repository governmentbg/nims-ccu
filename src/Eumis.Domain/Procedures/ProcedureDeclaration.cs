using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public abstract partial class ProcedureDeclaration : IAggregateRoot
    {
        public ProcedureDeclaration()
        {
        }

        public ProcedureDeclaration(int procedureId, int programmeDeclarationId, bool isRequired)
        {
            this.Gid = Guid.NewGuid();
            this.ProcedureId = procedureId;
            this.ProgrammeDeclarationId = programmeDeclarationId;
            this.IsActivated = false;
            this.IsActive = true;
            this.IsRequired = isRequired;

            var currentDate = DateTime.Now;
            this.CreateDate = currentDate;
            this.ModifyDate = currentDate;
        }

        public int ProcedureDeclarationId { get; set; }

        public int ProgrammeDeclarationId { get; set; }

        public Guid Gid { get; set; }

        public int ProcedureId { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public bool IsRequired { get; set; }

        public abstract ProcedureDeclarationType Type { get; }

        public byte[] Version { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureDeclarationMap : EntityTypeConfiguration<ProcedureDeclaration>
    {
        public ProcedureDeclarationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureDeclarationId);

            this.Property(t => t.ProcedureDeclarationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProgrammeDeclarationId)
                .IsRequired();

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.IsActivated)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.IsRequired)
                .IsRequired();

            this.Ignore(t => t.Type);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.ModifyDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureDeclarations");
            this.Property(t => t.ProcedureDeclarationId).HasColumnName("ProcedureDeclarationId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");
            this.Property(t => t.ProgrammeDeclarationId).HasColumnName("ProgrammeDeclarationId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsRequired).HasColumnName("IsRequired");

            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            this.Map<ProcedureAppFormDeclaration>(t => t.Requires("Type").HasValue<int>((int)ProcedureDeclarationType.AppForm));
        }
    }
}
