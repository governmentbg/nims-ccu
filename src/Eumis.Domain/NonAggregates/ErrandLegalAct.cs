using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.NonAggregates
{
    public class ErrandLegalAct
    {
        public const int PmsErrandLegalActId = 2;
        public static readonly Guid PmsGid = Guid.Parse("7E9B44E8-742B-45E5-B967-7B7FEEC6E18A");

        public ErrandLegalAct()
        {
        }

        public int ErrandLegalActId { get; set; }

        public Guid Gid { get; set; }

        public string Name { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
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
