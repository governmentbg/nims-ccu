using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.ProgrammeDeclarations
{
    public class ProgrammeDeclarationItem
    {
        public ProgrammeDeclarationItem()
        {
        }

        public ProgrammeDeclarationItem(int orderNum, string content)
        {
            this.Gid = Guid.NewGuid();
            this.OrderNum = orderNum;
            this.Content = content;
            this.IsActive = true;
        }

        public ProgrammeDeclarationItem(int programmeDeclarationId, int orderNum, string content)
            : this(orderNum, content)
        {
            this.ProgrammeDeclarationId = programmeDeclarationId;
        }

        public int ProgrammeDeclarationItemId { get; set; }

        public Guid Gid { get; set; }

        public int ProgrammeDeclarationId { get; set; }

        public int OrderNum { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public virtual ProgrammeDeclaration ProgrammeDeclaration { get; set; }

        public void SetAttributes(int orderNum, string content)
        {
            this.OrderNum = orderNum;
            this.Content = content;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammeDeclarationItemMap : EntityTypeConfiguration<ProgrammeDeclarationItem>
    {
        public ProgrammeDeclarationItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammeDeclarationItemId);

            this.Property(t => t.ProgrammeDeclarationItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.ProgrammeDeclarationId)
                .IsRequired();

            this.Property(t => t.OrderNum)
                .IsRequired();

            this.Property(t => t.Content)
                .HasMaxLength(100)
                .IsRequired();

            this.Property(t => t.IsActive)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProgrammeDeclarationItems");
            this.Property(t => t.ProgrammeDeclarationItemId).HasColumnName("ProgrammeDeclarationItemId");
            this.Property(t => t.ProgrammeDeclarationId).HasColumnName("ProgrammeDeclarationId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.OrderNum).HasColumnName("OrderNum");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            this.HasRequired(t => t.ProgrammeDeclaration)
                .WithMany(t => t.ProgrammeDeclarationItems)
                .HasForeignKey(t => t.ProgrammeDeclarationId)
                .WillCascadeOnDelete();
        }
    }
}
