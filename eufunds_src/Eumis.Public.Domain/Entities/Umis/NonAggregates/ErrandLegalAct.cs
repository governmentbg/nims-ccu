using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities.Umis.NonAggregates
{
    public class ErrandLegalAct
    {
        public ErrandLegalAct()
        {
        }

        public int ErrandLegalActId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }
    }

    public class ErrandLegalActMap : EntityTypeConfiguration<ErrandLegalAct>
    {
        public ErrandLegalActMap()
        {
            // Primary Key
            this.HasKey(t => t.ErrandLegalActId);

            // Properties
            this.Property(t => t.ErrandLegalActId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Gid)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ErrandLegalActs");
            this.Property(t => t.ErrandLegalActId).HasColumnName("ErrandLegalActId");
            this.Property(t => t.Gid).HasColumnName("Gid");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
