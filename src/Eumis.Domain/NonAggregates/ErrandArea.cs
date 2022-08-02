﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class ErrandArea
    {
        public ErrandArea()
        {
        }

        public int ErrandAreaId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ErrandAreaMap : EntityTypeConfiguration<ErrandArea>
    {
        public ErrandAreaMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrandAreaId);

            // Properties
            this.Property(t => t.ErrandAreaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ErrandAreas");
            this.Property(t => t.ErrandAreaId).HasColumnName("ErrandAreaId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
