using System.Data.Entity.ModelConfiguration;

namespace Eumis.Public.Domain.Entities
{
    public partial class OpStatOverride
    {
        public OpStatOverride()
        {
        }

        public string ProgrammeCode { get; set; }
        public int? ProjectsCount { get; set; }
        public int? ContractsCount { get; set; }
        public decimal? ContractedEuAmount { get; set; }
        public decimal? ContractedBgAmount { get; set; }
        public decimal? ContractedSelfAmount { get; set; }
        public decimal? PaidEuAmount { get; set; }
        public decimal? PaidBgAmount { get; set; }
    }

    public class OpStatOverrideMap : EntityTypeConfiguration<OpStatOverride>
    {
        public OpStatOverrideMap()
        {
            // Primary Key
            this.HasKey(t => t.ProgrammeCode);

            // Table & Column Mappings
            this.ToTable("OpStatOverrides");
            this.Property(t => t.ProgrammeCode).HasColumnName("ProgrammeCode");
            this.Property(t => t.ProjectsCount).HasColumnName("ProjectsCount");
            this.Property(t => t.ContractsCount).HasColumnName("ContractsCount");
            this.Property(t => t.ContractedEuAmount).HasColumnName("ContractedEuAmount");
            this.Property(t => t.ContractedBgAmount).HasColumnName("ContractedBgAmount");
            this.Property(t => t.ContractedSelfAmount).HasColumnName("ContractedSelfAmount");
            this.Property(t => t.PaidEuAmount).HasColumnName("PaidEuAmount");
            this.Property(t => t.PaidBgAmount).HasColumnName("PaidBgAmount");
        }
    }
}
