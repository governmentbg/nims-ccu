using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Contracts.ContractReportMicros
{
    public class ContractReportMicrosType3Item
    {
        public ContractReportMicrosType3Item()
        {
            this.Ketchup = new ContractReportMicrosType3ItemFoodData();
            this.TomatoPaste = new ContractReportMicrosType3ItemFoodData();
            this.GreenPeas = new ContractReportMicrosType3ItemFoodData();
            this.HotchPotch = new ContractReportMicrosType3ItemFoodData();
            this.Nectar = new ContractReportMicrosType3ItemFoodData();
            this.Compote = new ContractReportMicrosType3ItemFoodData();
            this.Jam = new ContractReportMicrosType3ItemFoodData();
            this.MeatCan = new ContractReportMicrosType3ItemFoodData();
            this.FishCan = new ContractReportMicrosType3ItemFoodData();
            this.WheatFlour = new ContractReportMicrosType3ItemFoodData();
            this.Rice = new ContractReportMicrosType3ItemFoodData();
            this.Macaroni = new ContractReportMicrosType3ItemFoodData();
            this.Bulgur = new ContractReportMicrosType3ItemFoodData();
            this.Beans = new ContractReportMicrosType3ItemFoodData();
            this.Lentils = new ContractReportMicrosType3ItemFoodData();
            this.Biscuit = new ContractReportMicrosType3ItemFoodData();
            this.Waffle = new ContractReportMicrosType3ItemFoodData();
            this.Sugar = new ContractReportMicrosType3ItemFoodData();
            this.Honey = new ContractReportMicrosType3ItemFoodData();
            this.Oil = new ContractReportMicrosType3ItemFoodData();
            this.Lokum = new ContractReportMicrosType3ItemFoodData();
        }

        public int ContractReportMicrosType3ItemId { get; set; }

        public int ContractReportMicroId { get; set; }

        public int? DistrictId { get; set; }

        public int? MunicipalityId { get; set; }

        public ContractReportMicrosType3ItemFoodData Ketchup { get; set; }

        public ContractReportMicrosType3ItemFoodData TomatoPaste { get; set; }

        public ContractReportMicrosType3ItemFoodData GreenPeas { get; set; }

        public ContractReportMicrosType3ItemFoodData HotchPotch { get; set; }

        public ContractReportMicrosType3ItemFoodData Nectar { get; set; }

        public ContractReportMicrosType3ItemFoodData Compote { get; set; }

        public ContractReportMicrosType3ItemFoodData Jam { get; set; }

        public ContractReportMicrosType3ItemFoodData MeatCan { get; set; }

        public ContractReportMicrosType3ItemFoodData FishCan { get; set; }

        public ContractReportMicrosType3ItemFoodData WheatFlour { get; set; }

        public ContractReportMicrosType3ItemFoodData Rice { get; set; }

        public ContractReportMicrosType3ItemFoodData Macaroni { get; set; }

        public ContractReportMicrosType3ItemFoodData Bulgur { get; set; }

        public ContractReportMicrosType3ItemFoodData Beans { get; set; }

        public ContractReportMicrosType3ItemFoodData Lentils { get; set; }

        public ContractReportMicrosType3ItemFoodData Biscuit { get; set; }

        public ContractReportMicrosType3ItemFoodData Waffle { get; set; }

        public ContractReportMicrosType3ItemFoodData Sugar { get; set; }

        public ContractReportMicrosType3ItemFoodData Honey { get; set; }

        public ContractReportMicrosType3ItemFoodData Oil { get; set; }

        public ContractReportMicrosType3ItemFoodData Lokum { get; set; }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ContractReportMicrosType3ItemMap : EntityTypeConfiguration<ContractReportMicrosType3Item>
    {
        public ContractReportMicrosType3ItemMap()
        {
            // Primary Key
            this.HasKey(t => t.ContractReportMicrosType3ItemId);

            // Properties
            this.Property(t => t.ContractReportMicrosType3ItemId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(t => t.ContractReportMicroId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ContractReportMicrosType3Items");
            this.Property(t => t.ContractReportMicrosType3ItemId).HasColumnName("ContractReportMicrosType3ItemId");
            this.Property(t => t.ContractReportMicroId).HasColumnName("ContractReportMicroId");

            this.Property(t => t.DistrictId).HasColumnName("DistrictId");
            this.Property(t => t.MunicipalityId).HasColumnName("MunicipalityId");

            this.Property(t => t.Ketchup.TargetValue).HasColumnName("KetchupTargetValue");
            this.Property(t => t.Ketchup.ActualValue).HasColumnName("KetchupActualValue");

            this.Property(t => t.TomatoPaste.TargetValue).HasColumnName("TomatoPasteTargetValue");
            this.Property(t => t.TomatoPaste.ActualValue).HasColumnName("TomatoPasteActualValue");

            this.Property(t => t.GreenPeas.TargetValue).HasColumnName("GreenPeasTargetValue");
            this.Property(t => t.GreenPeas.ActualValue).HasColumnName("GreenPeasActualValue");

            this.Property(t => t.HotchPotch.TargetValue).HasColumnName("HotchPotchTargetValue");
            this.Property(t => t.HotchPotch.ActualValue).HasColumnName("HotchPotchActualValue");

            this.Property(t => t.Nectar.TargetValue).HasColumnName("NectarTargetValue");
            this.Property(t => t.Nectar.ActualValue).HasColumnName("NectarActualValue");

            this.Property(t => t.Compote.TargetValue).HasColumnName("CompoteTargetValue");
            this.Property(t => t.Compote.ActualValue).HasColumnName("CompoteActualValue");

            this.Property(t => t.Jam.TargetValue).HasColumnName("JamTargetValue");
            this.Property(t => t.Jam.ActualValue).HasColumnName("JamActualValue");

            this.Property(t => t.MeatCan.TargetValue).HasColumnName("MeatCanTargetValue");
            this.Property(t => t.MeatCan.ActualValue).HasColumnName("MeatCanActualValue");

            this.Property(t => t.FishCan.TargetValue).HasColumnName("FishCanTargetValue");
            this.Property(t => t.FishCan.ActualValue).HasColumnName("FishCanActualValue");

            this.Property(t => t.WheatFlour.TargetValue).HasColumnName("WheatFlourTargetValue");
            this.Property(t => t.WheatFlour.ActualValue).HasColumnName("WheatFlourActualValue");

            this.Property(t => t.Rice.TargetValue).HasColumnName("RiceTargetValue");
            this.Property(t => t.Rice.ActualValue).HasColumnName("RiceActualValue");

            this.Property(t => t.Macaroni.TargetValue).HasColumnName("MacaroniTargetValue");
            this.Property(t => t.Macaroni.ActualValue).HasColumnName("MacaroniActualValue");

            this.Property(t => t.Bulgur.TargetValue).HasColumnName("BulgurTargetValue");
            this.Property(t => t.Bulgur.ActualValue).HasColumnName("BulgurActualValue");

            this.Property(t => t.Beans.TargetValue).HasColumnName("BeansTargetValue");
            this.Property(t => t.Beans.ActualValue).HasColumnName("BeansActualValue");

            this.Property(t => t.Lentils.TargetValue).HasColumnName("LentilsTargetValue");
            this.Property(t => t.Lentils.ActualValue).HasColumnName("LentilsActualValue");

            this.Property(t => t.Biscuit.TargetValue).HasColumnName("BiscuitTargetValue");
            this.Property(t => t.Biscuit.ActualValue).HasColumnName("BiscuitActualValue");

            this.Property(t => t.Waffle.TargetValue).HasColumnName("WaffleTargetValue");
            this.Property(t => t.Waffle.ActualValue).HasColumnName("WaffleActualValue");

            this.Property(t => t.Sugar.TargetValue).HasColumnName("SugarTargetValue");
            this.Property(t => t.Sugar.ActualValue).HasColumnName("SugarActualValue");

            this.Property(t => t.Honey.TargetValue).HasColumnName("HoneyTargetValue");
            this.Property(t => t.Honey.ActualValue).HasColumnName("HoneyActualValue");

            this.Property(t => t.Oil.TargetValue).HasColumnName("OilTargetValue");
            this.Property(t => t.Oil.ActualValue).HasColumnName("OilActualValue");

            this.Property(t => t.Lokum.TargetValue).HasColumnName("LokumTargetValue");
            this.Property(t => t.Lokum.ActualValue).HasColumnName("LokumActualValue");
        }
    }
}
