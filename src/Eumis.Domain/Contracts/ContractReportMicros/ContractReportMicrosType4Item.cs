using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public class ContractReportMicrosType4Item
    {
        public int ContractReportMicrosType4ItemId { get; set; }

        public int ContractReportMicroId { get; set; }

        public int? DistrictId { get; set; }

        public int? MunicipalityId { get; set; }

        public decimal? FruitAmounts { get; set; }

        public decimal? VegetableAmounts { get; set; }

        public decimal? Group1TotalAmounts { get; set; }

        public decimal? MeatAmounts { get; set; }

        public decimal? EggAmounts { get; set; }

        public decimal? FishAmounts { get; set; }

        public decimal? Group2TotalAmounts { get; set; }

        public decimal? FlourAmounts { get; set; }

        public decimal? BreadAmounts { get; set; }

        public decimal? PotatoAmounts { get; set; }

        public decimal? RiceAmounts { get; set; }

        public decimal? StarchProductAmounts { get; set; }

        public decimal? Group3TotalAmounts { get; set; }

        public decimal? SugarAmounts { get; set; }

        public decimal? MilkProductAmounts { get; set; }

        public decimal? FatsOrOilsAmounts { get; set; }

        public decimal? FastFoodAmounts { get; set; }

        public decimal? OtherFoodAmounts { get; set; }

        public decimal? Group4TotalAmounts { get; set; }

        public int? TotalDishesCount { get; set; }

        public int? TotalPackagesCount { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMicrosType4ItemMap : EntityTypeConfiguration<ContractReportMicrosType4Item>
    {
        public ContractReportMicrosType4ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosType4ItemId);

            // Properties
            this.Property(t => t.ContractReportMicrosType4ItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractReportMicroId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosType4Items");
            this.Property(t => t.ContractReportMicrosType4ItemId).HasColumnName("ContractReportMicrosType4ItemId");
            this.Property(t => t.ContractReportMicroId).HasColumnName("ContractReportMicroId");

            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");

            this.Property(t => t.FruitAmounts).HasColumnName("FruitAmounts");
            this.Property(t => t.VegetableAmounts).HasColumnName("VegetableAmounts");
            this.Property(t => t.Group1TotalAmounts).HasColumnName("Group1TotalAmounts");

            this.Property(t => t.MeatAmounts).HasColumnName("MeatAmounts");
            this.Property(t => t.EggAmounts).HasColumnName("EggAmounts");
            this.Property(t => t.FishAmounts).HasColumnName("FishAmounts");
            this.Property(t => t.Group2TotalAmounts).HasColumnName("Group2TotalAmounts");

            this.Property(t => t.FlourAmounts).HasColumnName("FlourAmounts");
            this.Property(t => t.BreadAmounts).HasColumnName("BreadAmounts");
            this.Property(t => t.PotatoAmounts).HasColumnName("PotatoAmounts");
            this.Property(t => t.RiceAmounts).HasColumnName("RiceAmounts");
            this.Property(t => t.StarchProductAmounts).HasColumnName("StarchProductAmounts");
            this.Property(t => t.Group3TotalAmounts).HasColumnName("Group3TotalAmounts");

            this.Property(t => t.SugarAmounts).HasColumnName("SugarAmounts");

            this.Property(t => t.MilkProductAmounts).HasColumnName("MilkProductAmounts");

            this.Property(t => t.FatsOrOilsAmounts).HasColumnName("FatsOrOilsAmounts");

            this.Property(t => t.FastFoodAmounts).HasColumnName("FastFoodAmounts");
            this.Property(t => t.OtherFoodAmounts).HasColumnName("OtherFoodAmounts");
            this.Property(t => t.Group4TotalAmounts).HasColumnName("Group4TotalAmounts");

            this.Property(t => t.TotalDishesCount).HasColumnName("TotalDishesCount");
            this.Property(t => t.TotalPackagesCount).HasColumnName("TotalPackagesCount");
        }
    }
}
