using System;
using System.Data.Entity.ModelConfiguration;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;

namespace Eumis.Public.Domain.Entities.Umis.Procedures
{
    public class ProcedureLocation
    {
        public int ProcedureLocationId { get; set; }

        public int ProcedureId { get; set; }

        public NutsLevel NutsLevel { get; set; }

        public int? CountryId { get; set; }

        public int? Nuts1Id { get; set; }

        public int? Nuts2Id { get; set; }

        public int? DistrictId { get; set; }

        public int? MunicipalityId { get; set; }

        public int? SettlementId { get; set; }

        public int? ProtectedZoneId { get; set; }

        public virtual Procedure Procedure { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureLocationMap : EntityTypeConfiguration<ProcedureLocation>
    {
        public ProcedureLocationMap()
        {
            // Primary Key
            this.HasKey(t => t.ProcedureLocationId);

            this.Property(t => t.ProcedureId)
                .IsRequired();

            this.Property(t => t.NutsLevel)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ProcedureLocations");
            this.Property(t => t.ProcedureLocationId).HasColumnName("ProcedureLocationId");
            this.Property(t => t.ProcedureId).HasColumnName("ProcedureId");

            this.Property(t => t.NutsLevel).HasColumnName("NutsLevel");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.Nuts1Id).HasColumnName("Nuts1Id");
            this.Property(t => t.Nuts2Id).HasColumnName("Nuts2Id");
            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");
            this.Property(t => t.SettlementId).HasColumnName("SettlementId");
            this.Property(t => t.ProtectedZoneId).HasColumnName("ProtectedZoneId");

            this.HasRequired(t => t.Procedure)
                .WithMany(t => t.ProcedureLocations)
                .HasForeignKey(t => t.ProcedureId)
                .WillCascadeOnDelete();
        }
    }
}
