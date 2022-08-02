using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.Directions
{
    public partial class SubDirection
    {
        public SubDirection()
        {
        }

        public SubDirection(string name, string nameAlt)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.Name = name;
            this.NameAlt = nameAlt;
        }

        public int SubDirectionId { get; set; }

        public int DirectionId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public virtual Direction Direction { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class SubDirectionMap : EntityTypeConfiguration<SubDirection>
    {
        public SubDirectionMap()
        {
            // Primary Key
            this.HasKey(t => t.SubDirectionId);

            // Properties
            this.Property(t => t.SubDirectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.NameAlt)
                .IsRequired();
            this.Property(t => t.DirectionId)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SubDirections");
            this.Property(t => t.SubDirectionId).HasColumnName("SubDirectionId");
            this.Property(t => t.DirectionId).HasColumnName("DirectionId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");

            // Relationships
            this.HasRequired(t => t.Direction)
                .WithMany(t => t.SubDirections)
                .HasForeignKey(d => d.DirectionId)
                .WillCascadeOnDelete();
        }
    }
}
