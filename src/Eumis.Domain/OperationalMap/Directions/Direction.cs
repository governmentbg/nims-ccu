using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.Directions
{
    public partial class Direction : IAggregateRoot
    {
        public Direction()
        {
        }

        public Direction(string name, string nameAlt)
            : this()
        {
            this.Gid = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
            this.Status = DirectionStatus.Draft;
            this.UpdateAttributes(name, nameAlt);
        }

        public int DirectionId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public DirectionStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<SubDirection> SubDirections { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class DirectionMap : EntityTypeConfiguration<Direction>
    {
        public DirectionMap()
        {
            // Primary Key
            this.HasKey(t => t.DirectionId);

            // Properties
            this.Property(t => t.DirectionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.Name)
                .IsRequired();
            this.Property(t => t.NameAlt)
                .IsRequired();
            this.Property(t => t.Status)
                .IsRequired();
            this.Property(t => t.Gid)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Directions");
            this.Property(t => t.DirectionId).HasColumnName("DirectionId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
